using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Practices
{
    public class TestModel
    {
        public int Count { get; set; }
        public decimal Money { get; set; }
        public string Name { get; set; }
        public bool IsSuccess { get; set; }
        public DateTime Date { get; set; }
    }

    class LCGUsage
	{
		public static class OldFieldPrinter
		{
			public static string GetFieldContent<T>(List<T> list, string[] listFiled)
			{
				StringBuilder stringBuilder = new();
				foreach (var item in list)
				{
					foreach (var itemName in listFiled)
					{
						Func<T, object> func = EmitGetter<T>(itemName);
						var value = func(item);
						stringBuilder.AppendFormat(value + "\t");
					}
					stringBuilder.AppendLine();
				}
				return stringBuilder.ToString();
			}

			private static Func<T, object> EmitGetter<T>(string propertyName)
			{
				var type = typeof(T);
				var dynamicMethod = new DynamicMethod("Get", typeof(object), new[] { type }, type);
				var iLGenerator = dynamicMethod.GetILGenerator();
				iLGenerator.Emit(OpCodes.Ldarg_0);
				var property = type.GetProperty(propertyName);
				iLGenerator.Emit(OpCodes.Callvirt, property.GetMethod);
				iLGenerator.Emit(OpCodes.Box, property.PropertyType);
				iLGenerator.Emit(OpCodes.Ret);
				return dynamicMethod.CreateDelegate(typeof(Func<T, object>)) as Func<T, object>;
			}
		}

        public static class OldEnhancedFieldPrinter
        {
            private readonly static Dictionary<Type, Dictionary<string, Delegate>> mCache = new();
            private static Delegate GeneratePropertyToStringStub(Type targetType, string propertyName)
            {
                if (targetType == null)
                    throw new ArgumentNullException(nameof(targetType));
                var property = targetType.GetProperty(propertyName);
                if (property == null)
                    throw new ArgumentException($"Property [{propertyName}] does not exist.");

                var propertyType = property.PropertyType;
                var method = new DynamicMethod("Getter", typeof(object), new Type[] { targetType });
                var il = method.GetILGenerator();

                il.Emit(OpCodes.Ldarg_0);
                il.Emit(OpCodes.Callvirt, property.GetMethod);
                il.Emit(OpCodes.Box, propertyType);
                il.Emit(OpCodes.Ret);

                return method.CreateDelegate(typeof(Func<,>).MakeGenericType(targetType, typeof(object)));
            }

            private static Delegate GetStub(Dictionary<string, Delegate> target, Type type, string property)
            {
                if (!target.TryGetValue(property, out var stub))
                {
                    stub = GeneratePropertyToStringStub(type, property);
                    target.Add(property, stub);
                }
                return stub;
            }

            private static IEnumerable<Delegate> GetStubs(Type targetType, IEnumerable<string> propertyNames)
            {
                Dictionary<string, Delegate> properties = null;
                lock (mCache)
                {
                    if (!mCache.TryGetValue(targetType, out properties))
                    {
                        properties = new Dictionary<string, Delegate>();
                        mCache.Add(targetType, properties);
                    }
                }
                lock (properties)
                    return propertyNames.Select(x => GetStub(properties, targetType, x)).ToArray();
            }

            public static string GetFieldContent<T>(IEnumerable<T> targets, IEnumerable<string> properties)
            {
                var builder = new StringBuilder(16);
                var stubs = GetStubs(typeof(T), properties).Cast<Func<T, object>>();
                foreach (var each in targets)
                {
                    builder.AppendJoin('\t', stubs.Select(x => x(each)));
                    builder.AppendLine();
                }
                return builder.ToString();
            }
        }

        public static class FieldPrinter
        {
            private readonly static Dictionary<Type, Dictionary<string, Delegate>> mCache = new();
            private static Delegate GeneratePropertyToStringStub(Type targetType, string propertyName)
            {
                if (targetType == null)
                    throw new ArgumentNullException(nameof(targetType));
                var property = targetType.GetProperty(propertyName);
                if (property == null)
                    throw new ArgumentException($"Property [{propertyName}] does not exist.");

                var propertyType = property.PropertyType;
                var toStringMethod = propertyType.GetMethod("ToString", Array.Empty<Type>());
                var method = new DynamicMethod("Getter", typeof(string), new Type[] { targetType });
                var il = method.GetILGenerator();

                if (targetType.IsValueType)
                {
                    il.Emit(OpCodes.Ldarga, 0);
                    il.Emit(OpCodes.Call, property.GetMethod);
                }
                else
                {
                    il.Emit(OpCodes.Ldarg_0);
                    il.Emit(OpCodes.Callvirt, property.GetMethod);
                }

                if (propertyType.IsValueType)
                {
                    var localBuilder = il.DeclareLocal(propertyType);
                    //Need to store valuetype to local first
                    il.Emit(OpCodes.Stloc, localBuilder);
                    //Then load address
                    il.Emit(OpCodes.Ldloca, localBuilder);
                    //no constrained call for overriden ToString method
                    if (propertyType.IsPrimitive || toStringMethod != toStringMethod.GetBaseDefinition())
                        il.Emit(OpCodes.Call, toStringMethod);
                    else
                    {
                        il.Emit(OpCodes.Constrained, propertyType);
                        il.Emit(OpCodes.Callvirt, toStringMethod);
                    }
                }
                else
                    il.Emit(OpCodes.Callvirt, toStringMethod);
                il.Emit(OpCodes.Ret);

                return method.CreateDelegate(typeof(Func<,>).MakeGenericType(targetType, typeof(string)));
            }

            private static Delegate GetStub(Dictionary<string, Delegate> target, Type type, string property)
            {
                if (!target.TryGetValue(property, out var stub))
                {
                    stub = GeneratePropertyToStringStub(type, property);
                    target.Add(property, stub);
                }
                return stub;
            }

            private static IEnumerable<Delegate> GetStubs(Type targetType, IEnumerable<string> propertyNames)
            {
                Dictionary<string, Delegate> properties = null;
                lock (mCache)
                {
                    if (!mCache.TryGetValue(targetType, out properties))
                    {
                        properties = new Dictionary<string, Delegate>();
                        mCache.Add(targetType, properties);
                    }
                }
                lock (properties)
                    return propertyNames.Select(x => GetStub(properties, targetType, x)).ToArray();
            }

            public static string GetFieldContent<T>(IEnumerable<T> targets, IEnumerable<string> properties)
            {
                var builder = new StringBuilder(16);
                var stubs = GetStubs(typeof(T), properties).Cast<Func<T, string>>();
                foreach (var each in targets)
                {
                    builder.AppendJoin('\t', stubs.Select(x => x(each)));
                    builder.AppendLine();
                }
                return builder.ToString();
            }
        }
    }
}
