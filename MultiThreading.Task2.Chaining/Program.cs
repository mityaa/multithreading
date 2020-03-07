/*
 * 2.	Write a program, which creates a chain of four Tasks.
 * First Task – creates an array of 10 random integer.
 * Second Task – multiplies this array with another random integer.
 * Third Task – sorts this array by ascending.
 * Fourth Task – calculates the average value. All this tasks should print the values to console.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiThreading.Task2.Chaining
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine(".Net Mentoring Program. MultiThreading V1 ");
			Console.WriteLine("2.	Write a program, which creates a chain of four Tasks.");
			Console.WriteLine("First Task – creates an array of 10 random integer.");
			Console.WriteLine("Second Task – multiplies this array with another random integer.");
			Console.WriteLine("Third Task – sorts this array by ascending.");
			Console.WriteLine("Fourth Task – calculates the average value. All this tasks should print the values to console");
			Console.WriteLine();

			const int minLimit = 1;
			const int maxLimit = 100;

			Task.Factory.StartNew(() =>
			{
				var intArr = new int[10];
				SetRandomIntToArr(intArr, minLimit, maxLimit);
				return intArr;
			}).ContinueWith(firstTaskResult =>
			{
				var previousTaskArr = firstTaskResult.Result;
				SetRandomIntToArr(previousTaskArr, minLimit, maxLimit);
				return previousTaskArr;
			}).ContinueWith(secondTaskResult =>
			{
				var previousTaskArr = secondTaskResult.Result;
				Array.Sort(previousTaskArr);
				PrintArr(previousTaskArr);
				return previousTaskArr;
			}).ContinueWith(thirdTaskResult =>
			{
				var previousTaskArr = thirdTaskResult.Result;
				var average = previousTaskArr.Sum() / previousTaskArr.Length;
				Console.WriteLine($"The average value = {average}");
			});

			Console.ReadLine();
		}

		private static void PrintArr(IEnumerable<int> array)
		{
			Console.WriteLine("---------------------------");
			foreach (var arrayElem in array)
			{
				Console.WriteLine(arrayElem);
			}
		}
		private static void SetRandomIntToArr(int[] arr, int minStep, int maxStep)
		{
			if (arr == null)
				throw new Exception("You missed set array of ints");

			var random = new Random();
			for (var i = 0; i < arr.Length; i++)
			{
				arr[i] = random.Next(minStep, maxStep);
			}

			PrintArr(arr);
		}
	}
}
