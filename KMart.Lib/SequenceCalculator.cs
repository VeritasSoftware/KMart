using System;
using System.Collections.Generic;
using System.Linq;

namespace KMart.Lib
{
    public class SequenceCalculator
    {
        public static string FindLongestIncreasingSubsequence(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return "";
            }

            // 1. Parse the input string into an integer array
            int[] nums;
            try
            {
                nums = input.Split(' ')
                            .Where(s => !string.IsNullOrWhiteSpace(s))
                            .Select(int.Parse)
                            .ToArray();
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input format. Ensure all inputs are valid integers separated by single spaces.");
                return "";
            }

            if (nums.Count() == 0)
            {
                return "";
            }

            if (nums == null || nums.Length == 0) return "";

            int n = nums.Length;
            int[] dp = new int[n];
            int[] prev = new int[n];
            int maxLength = 0;
            int lastIndex = 0;

            for (int i = 0; i < n; i++)
            {
                dp[i] = 1;
                prev[i] = -1; // To reconstruct the path
                for (int j = 0; j < i; j++)
                {
                    // Use strict inequality '<' for strictly increasing
                    if (nums[i] > nums[j] && dp[i] < dp[j] + 1)
                    {
                        dp[i] = dp[j] + 1;
                        prev[i] = j;
                    }
                }

                // If multiple exist, the '<' in dp[i] > maxLength ensures
                // we pick the first one (by not updating on equality)
                if (dp[i] > maxLength)
                {
                    maxLength = dp[i];
                    lastIndex = i;
                }
            }

            // Reconstruct LIS
            List<int> result = new List<int>();
            while (lastIndex != -1)
            {
                result.Add(nums[lastIndex]);
                lastIndex = prev[lastIndex];
            }
            result.Reverse();

            // Create result string
            var resultStr = string.Join(" ", result);

            return resultStr;
        }        
    }
}
