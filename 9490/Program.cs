using System;
//using System.Collections.Generic;
using System.Text;
using Unit4.CollectionsLib;

namespace _9490
{
    class Program
    {
        static void Main(string[] args)
        {
            //Q1
            Queue<int> q1 = Arr2Q(new int[] { 1, 2, 3, 6, 10 });
            Console.WriteLine(IsPerfect(q1,3)); //T
            Console.WriteLine(q1);

            Console.WriteLine(IsPerfect(q1,5)); //F
            Console.WriteLine(CountPerfects(q1));



            //Q2
            Queue<int> q2 = Arr2Q(new int[] { 1,2,3,4 });
            Console.WriteLine(q2);
            Queue<int> q3 = ProductOfOthers(q2);
            Console.WriteLine(q3);
            Console.WriteLine(q2);

            //Q3
            Queue<int> q = Arr2Q(new int[] { 5, 11, 6, 9, 3, 6, 3 });

            Console.WriteLine(q);

            int k = 3;
            int maxSum = MaxSumSubsequence(q, k);
            Console.WriteLine($"Maximum sum of any subsequence of length {k}: {maxSum}");


        }

        // Convert array to Queue
        static Queue<int> Arr2Q(int[] arr)
        {
            Queue<int> q = new Queue<int>();
            foreach (int value in arr)
                q.Insert(value);
            return q;
        }

        //Q1
        static bool IsPerfect(Queue<int> q, int m)
        {
            if (m <= 1 || m > Size(q)) return false;

            Queue<int> tmp = new Queue<int>();
            int sumBefore = 0;
            bool isPerfect = false;
            int size = Size(q);

            // Store elements in temporary queue and calculate the sum before the m-th element
            for (int i = 1; i <= size; i++)
            {
                int value = q.Remove();
                if (i < m)
                    sumBefore += value;
                else if (i == m)
                    isPerfect = (value == sumBefore);
                tmp.Insert(value);
            }

            // Restore the original queue
            while (!tmp.IsEmpty())
                q.Insert(tmp.Remove());

            return isPerfect;
        }


        static int CountPerfects(Queue<int> q)
        {
            int count = 0;
            int size = Size(q);

            for (int i = 1; i <= size; i++)
                if (IsPerfect(q, i)) count++;

            return count;
        }



        //Q3
        // Function to calculate maximum sum of any subsequence of length k
        // no need to restore q
        static int MaxSumSubsequence(Queue<int> q, int k)
        {
            Queue<int> tmp = new Queue<int>();
            int maxSum = int.MinValue;
            int currentSum = 0;
            int size = Size(q);

            // Calculate the maximum sum of a subsequence of length k
            for (int i = 0; i < size; i++)
            {
                int value = q.Remove();
                tmp.Insert(value);

                if (i < k)
                    currentSum += value;
                else
                {
                    currentSum += value - tmp.Remove();
                }

                if (i >= k - 1)
                    maxSum = Math.Max(maxSum, currentSum);

                q.Insert(value);
            }

            return maxSum;
        }

        //Q2
        //Return a new Q, in each position the product of all other elements
        //need to restore q
        static Queue<int> ProductOfOthers(Queue<int> q)
        {
            Queue<int> result = new Queue<int>();
            int totalProduct = 1;
            int size = Size(q);

            // Compute the total product of all elements
            for (int i = 0; i < size; i++)
            {
                int value = q.Remove();
                totalProduct *= value;
                q.Insert(value); // Restore the queue
            }

            // Compute the product of all other elements for each position
            for (int i = 0; i < size; i++)
            {
                int value = q.Remove();
                result.Insert(totalProduct / value);
                q.Insert(value); // Restore the queue
            }

            return result;
        }


        // calculate size of a queue
        static int Size(Queue<int> q)
        {
            Queue<int> tmp = new Queue<int>();
            int count = 0;
            while (!q.IsEmpty())
            {
                count++;
                tmp.Insert(q.Remove());
            }
            while (!tmp.IsEmpty())
                q.Insert(tmp.Remove());
            return count;
        }
    }
}
