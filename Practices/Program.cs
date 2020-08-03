using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace Practices
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Hello World!");
			string[] arr = { "dog", "racecar", "car" };
			string[] arrError = { "aa","aa"};
			LongestCommonPrefix(arrError);
			RomanToInt("IV");
			RemoveOuterParentheses("(()())(())");
			int[][] points = new int[3][];
			points[0] = new int[2] { 1, 1 };
			points[1] = new int[2] { 3, 4 };
			points[2] = new int[2] { -1, 0 };
			MinTimeToVisitAllPoints(points);
			Console.WriteLine(BalancedStringSplit("RLRRLLRLRL"));
			ListNode thrid = new ListNode(1);
			ListNode second = new ListNode(0, thrid);
			ListNode first = new ListNode(1, second);
			GetDecimalValue(first);
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
			return result; //凡是要用到 Contains 的，将其装入字典可以显著加快运行速度
		}

		public string RestoreString(string s, int[] indices)
		{
			char[] result = new char[s.Length];
			for (int i = 0; i < s.Length; i++)
			{
				Array.Copy(s.ToCharArray(), i, result, indices[i], 1);
			}
			return new string(result);//string[i] 的类型是 char
		}

		public int[] SmallerNumbersThanCurrent(int[] nums)
		{
			int[] result = new int[nums.Length];
			for (int i = 0; i < nums.Length; i++)
				for (int j = 0; j < nums.Length; j++)
					if (nums[i] > nums[j])
						result[i]++;
			return result;
		}

		public int XorOperation(int n, int start)
		{
			int[] nums = new int[n];
			int result = 0;
			for (int i = 0; i < n; i++)
			{
				nums[i] = start + 2 * i;
			}
			foreach (var item in nums)
			{
				result ^= item;
			}
			return result;
		}

		public int SubtractProductAndSum(int n)
		{
			int result = 0;
			int sum = 0;
			int pro = 0;
			while (n != 0)
			{
				result = n % 10;
				sum += result;
				pro *= result;
				n /= 10;
			}
			result = pro - sum;
			return result;// 其实可以把数字变成字符串，这样就可以不用%10了（
		}

		public int[] DecompressRLElist(int[] nums)
		{
			List<int> resultL = new List<int>();
			for (int i = 0; i < nums.Length; i += 2)
			{
				for (int j = 0; j < nums[i]; j++)
				{
					resultL.Add(nums[i + 1]);
				}
			}
			return resultL.ToArray();
		}

		public int[] CreateTargetArray(int[] nums, int[] index)
		{
			ArrayList list = new ArrayList();
			// List<int> 比这东西快很多
			// update: 去你妈的，运行时间根本是玄学
			int[] result = new int[index.Length];
			for (int i = 0; i < index.Length; i++)
			{
				list.Insert(index[i], nums[i]);
			}
			for (int i = 0; i < index.Length; i++)
			{
				result[i] = (int)list[i];
			}
			return result;
		}
		// 108ms it just stupid
		public static int BalancedStringSplit(string s)
		{
			int result = 0;
			char[] subString = new char[s.Length];
			for (int i = 0; i < s.Length; i += 2)
			{
				subString.SetValue(s[i], i);
				subString.SetValue(s[i + 1], i + 1);
				if (IsStringBalanced(subString))
				{
					subString = new char[s.Length];
					result++;
				}
			}
			return result;
		}

		public static bool IsStringBalanced(char[] s)
		{
			int numL = 0;
			int numR = 0;
			for (int i = 0; i < s.Length; i++)
			{
				if (s[i] == '\0')
					continue;
				if (s[i] == 'L')
					numL++;
				else
					numR++;
			}
			return numL == numR;
		}
		// 76ms why???????
		public int BalancedStringSplit2(string s)
		{
			char[] ca = s.ToArray();
			int result = 0;
			int count = 0;
			for (int i = 0; i < s.Length; i++)
			{
				if (s[i] == 'L')
					count++;
				else
					count--;
				if (count == 0)
					result++;
			}
			return result;
		}

		public int FindNumbers(int[] nums)
		{
			int result = 0;
			foreach (var item in nums)
			{
				if (item.ToString().Length % 2 == 0)
					result++;
			}
			return result;
		}


		public class ListNode
		{
			public int val;
			public ListNode next;
			public ListNode(int val = 0, ListNode next = null)
			{
				this.val = val;
				this.next = next;
			}
		}


		public static int GetDecimalValue(ListNode head)
		{
			int result = 0;
			List<int> trueList = new List<int>();
			do
			{
				trueList.Add(head.val);
				head = head.next;
			} while (head != null);
			for (int i = 0; i < trueList.Count(); i++)
			{
				if (trueList[i] == 1)
				{
					result += 1 << (trueList.Count() - i - 1);
				}
			}
			return result;
		}
		// grandma level algorithms
		public static int MinTimeToVisitAllPoints(int[][] points)
		{
			int result = 0;
			int[] start = points[0];
			int[] end = points[points.Length - 1];
			int i = 1;
			while (start != end && i < points.Length)
			{
				if (start[0] > points[i][0])
					start[0]--;
				else if (start[0] < points[i][0])
					start[0]++;
				if (start[1] > points[i][1])
					start[1]--;
				else if (start[1] < points[i][1])
					start[1]++;
				if (start[0] == points[i][0] && start[1] == points[i][1])
				{
					i++;
					continue;
				}
				result++;
			}
			return result;
		}

		public int OddCells(int n, int m, int[][] indices)
		{
			int result = 0;
			int[][] finalMatrix = new int[n][];
			for (int i = 0; i < n; i++)
			{
				finalMatrix[i] = new int[m];
			}
			for (int i = 0; i < indices.Length; i++)
			{
				for (int j = 0; j < m; j++)
				{
					finalMatrix[indices[i][0]][j]++;
				}
				for (int j = 0; j < n; j++)
				{
					finalMatrix[j][indices[i][1]]++;
				}
			}
			foreach (var item in finalMatrix)
			{
				foreach (var s in item)
				{
					if (Convert.ToBoolean(s & 1))
						result++;
				}
			}
			return result;
		}

		public static string RemoveOuterParentheses(string S)
		{
			string result = string.Empty;
			Stack<char> function = new Stack<char>();
			function.Push(S[0]);
			List<List<char>> subList = new List<List<char>>();
			subList.Add(new List<char>());
			subList[0].Add(S[0]);
			int j = 0;
			for (int i = 1; i < S.Length; i++)
			{
				subList[j].Add(S[i]);
				if (S[i] == ')')
				{
					function.Pop();
					if (function.Count == 0)
					{
						j++;
						subList.Add(new List<char>());
					}
					continue;
				}
				else
				{
					function.Push(S[i]);
				}
			}
			for (int i = 0; i < subList.Count - 1; i++)
			{
				subList[i].RemoveAt(0);
				subList[i].RemoveAt(subList[i].Count - 1);
				result += new string(subList[i].ToArray());
			}
			return result;
		}

		//tricket
		public string RemoveOuterParenthesesTricket(string S)
		{
			int len = 0;
			StringBuilder sb = new StringBuilder();

			foreach (char c in S)
			{
				if (c == '(')
				{
					len++;
					if (len > 1)
					{
						sb.Append(c);
					}
				}
				else if (c == ')')
				{
					if (len > 1)
					{
						sb.Append(c);
					}
					len--;
				}
			}
			return sb.ToString();
		}
		// why Dictionary.ContainsKey does not work?
		// may need rewrite GetHashCode() and Equal()?
		public static int RomanToInt(string s)
		{
			int result = 0;
			foreach (var item in s)
			{
				int v = item switch
				{
					'I' => 1,
					'V' => 5,
					'X' => 10,
					'L' => 50,
					'C' => 100,
					'D' => 500,
					'M' => 1000,
					_ => throw new NotImplementedException()
				};
				result += v;
			}
			Dictionary<char[], int> dict = new Dictionary<char[], int>();
			char[] pair = new char[2];
			for (int i = 0; i < s.Length; i += 2)
			{
				pair[0] = s[i];
				if (i + 1 == s.Length)
					pair[1] = ' ';
				else
					pair[1] = s[i + 1];
				if (dict.ContainsKey(pair))
					dict[pair]++;
				else
					dict.Add(pair, 1);
			}
			char[][] less = new char[6][];
			for (int i = 0; i < 6; i++)
				less[i] = new char[2];
			less[0][0] = 'I';
			less[0][1] = 'V';
			less[1][0] = 'I';
			less[1][1] = 'X';
			less[2][0] = 'X';
			less[2][1] = 'L';
			less[3][0] = 'X';
			less[3][1] = 'C';
			less[4][0] = 'C';
			less[4][1] = 'D';
			less[5][0] = 'C';
			less[5][1] = 'M';
			int[] reduce = new int[6] { 2, 2, 10, 10, 100, 100 };
			foreach (var item in less)
				if (dict.ContainsKey(item))
					result -= reduce[Array.IndexOf(less, item)];
			return result;
		}
		// it is common prefix no common substring...
		//
		public static string LongestCommonPrefix(string[] strs)
		{
			string result = string.Empty;
			int minLen = strs[0].Length;
			string minStr = strs[0];
			foreach (var item in strs)
				if (item.Length < minLen)
				{
					minLen = item.Length;
					minStr = item;
				}
			int mid = minLen, low = 0;
			string sub = string.Empty;
			while (mid > low)
			{
				mid = (low + mid) / 2;
				sub = minStr.Substring(low, (mid - low) == 0 ? 1 : (mid - low));
				bool isSub = true;

				for (int i = 0; i < strs.Length; i++)
				{
					for (int j = 0, h = low; j < sub.Length && (h < mid || h < low+1); j++, h++)
						if (sub[j] != strs[i][h])
						{
							isSub = false;
							break;
						}
					if (!isSub)
						break;
				}
				if (isSub && ((mid != low) || minLen == 1))
				{
					low = mid;
					mid += mid - low;
					result += sub;
				}
			}

			return result;
		}
	}
}
