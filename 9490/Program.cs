﻿using System;
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
            Console.WriteLine(CountPerfects(q1)); //2



            //Q2
            Queue<int> q2 = Arr2Q(new int[] { 1,2,3,4 });
            Console.WriteLine(q2);
            Queue<int> q3 = ProductOfOthers(q2);
            Console.WriteLine(q3);
            Console.WriteLine(q2);
            Console.WriteLine(RemoveTill10(q2));


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
            if (m <= 1) return false; // invalid m

            Queue<int> tmp = new Queue<int>();
            int sumBefore = 0;
            bool isPerfect = false;
            int index = 1; // Track the current position in the queue

            while (!q.IsEmpty())
            {
                int value = q.Remove();

                if (index < m)
                    sumBefore += value; // Add to the sum before the m-th element
                else if (index == m)
                    isPerfect = (value == sumBefore); // Check m-th element

                tmp.Insert(value); 
                index++;
            }

            // Restore the original queue
            while (!tmp.IsEmpty())
                q.Insert(tmp.Remove());

            // If m > q size, isPerfect = false
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




        //Q2
        //Return a new Q, in each position the product of all other elements
        //need to restore q

        static Queue<int> ProductOfOthers(Queue<int> q)
        {
            Queue<int> result = new Queue<int>();
            Queue<int> tmp = new Queue<int>();
            int totalProduct = 1;            

            // Compute the total product of all elements
            while (!q.IsEmpty())
            {
                int value = q.Remove();
                totalProduct *= value;
                tmp.Insert(value); //q->tmp
            }

            // Compute the product of all other elements for each position
            while (!tmp.IsEmpty())
            {
                int value = tmp.Remove();
                result.Insert(totalProduct / value);
                q.Insert(value); // tmp->q
            }

            return result;
        }
        static int RemoveTill10(Queue<int> q)
        {
            int count = 0;

            while (!q.IsEmpty())
            {
                if (ProductOfOthers(q).Head()<10)
                    return count;
                count++;
            }

            return -1;
        }



        //Q3
        // Function to calculate maximum sum of any subsequence of length k
        // no need to restore q
        static int MaxSumSubsequence(Queue<int> q, int k)
        {
            Queue<int> tmp = new Queue<int>(); //sliding window holds k elements
            int maxSum = int.MinValue;
            int currentSum = 0;
            int count = 0; //counts elements processed. once count=k, tmp is full

            while (!q.IsEmpty())
            {
                int value = q.Remove();
                tmp.Insert(value);
                currentSum += value;
                count++;

                if (count > k) //if tmp holds k+1 elements
                    currentSum -= tmp.Remove(); //remove the head from tmp and update sum

                if (count >= k) //update max as needed
                    maxSum = Math.Max(maxSum, currentSum); 
            }

            return maxSum;
        }


    }
}
