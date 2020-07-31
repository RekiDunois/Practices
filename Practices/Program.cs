using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;

namespace Practices
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }

        public static int[] RunningSum(int[] nums)
        {
            int[] result = new int[nums.Length];
            Parallel.For(0, nums.Length, (i) =>
            {
                for (int j = 0; j <= i; j++)
                {
                    result[i] += nums[j];
                }
            });
            return result;
        }

        public double Reverse(int x)
        {
            double result = 0;
            double check = 0;
            int bit = 1;
            int i;
            while (check != x)
            {
                check = x % Math.Pow(10, bit);
                bit++;
            }
            for (i = 1; i <= bit; i++)
            {
                result += (x % Math.Pow(10, i)) * Math.Pow(10, bit - i);
            }
            return result;
        }

        public int[] Shuffle(int[] nums, int n)
        {
            int len = nums.Length;
            int[] result = new int[len];
            int[] x = new int[n];
            int[] y = new int[n];
            int i, j;
            x = nums.Skip(0).Take(n).ToArray();
            y = nums.Skip(n).Take(n).ToArray();
            j = 0;
            for (i = 0; i < len; i += 2)
            {
                result[i] = x[j];
                j++;
            }
            j = 0;
            for (i = 1; i < len; i += 2)
            {
                result[i] = y[j];
                j++;
            }
            return result;
        }

        public IList<bool> KidsWithCandies(int[] candies, int extraCandies)
        {
            IList<bool> result = new List<bool>();
            int max = candies.Max();
            foreach (var item in candies)
            {
                result.Add(item + extraCandies >= max);
            }
            return result;
        }

        public int NumIdenticalPairs(int[] nums)
        {
            int result = 0;
            int i, j;
            int len = nums.Length - 1;
            for (i = 0; i < len; i++)
            {
                for (j = i + 1; j < len + 1; j++) 
                {
                    if (nums[i] == nums[j])
                        result++;
                }
            }
            return result;
        }

        public string DefangIPaddr(string address)
        {
            return address.Replace(".", "[.]");
        }

        public int NumberOfSteps(int num)
        {
            int result = 0;
            while (num != 0) 
            {
                if (num % 2 != 0)
                {
                    num = num - 1;
                    result++;
                }
                num = num / 2;
                if (num != 0)
                    result++;
            }
            return result;
        }

        public int NumJewelsInStones(string J, string S)
        {
            int result = 0;
            foreach (var item in S)
            {
                if (J.Contains(item))
                {
                    result++;
                }
            }
            return result;
        }

        public string RestoreString(string s, int[] indices)
        {
            //SortedDictionary<int, char> dict = new SortedDictionary<int, char>();
            //int i = 0;
            //string result = "";
            //foreach (var item in indices)
            //{
            //    dict.Add(item, s[i]);
            //    i++;
            //}
            //foreach (var item in dict)
            //{
            //    result.Append<char>(item.Value);
            //}
            //Array.Sort(indices);
            string result = "";
            string a = "";
            for (int i = 0; i < s.Length; i++)
            {
                s.CopyTo(indices[i], );
                result.Insert(s.Length - 1, );
            }
            return result;
        }
    }
}
