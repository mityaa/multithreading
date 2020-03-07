﻿/*
 * 5. Write a program which creates two threads and a shared collection:
 * the first one should add 10 elements into the collection and the second should print all elements
 * in the collection after each adding.
 * Use Thread, ThreadPool or Task classes for thread creation and any kind of synchronization constructions.
 */
using System;
using System.Collections.Generic;
using System.Threading;

namespace MultiThreading.Task5.Threads.SharedCollection
{
	class Program
	{

		private static readonly IList<int> SharedList = new List<int>();

		static void Main(string[] args)
		{
			Console.WriteLine("5. Write a program which creates two threads and a shared collection:");
			Console.WriteLine("the first one should add 10 elements into the collection and the second should print all elements in the collection after each adding.");
			Console.WriteLine("Use Thread, ThreadPool or Task classes for thread creation and any kind of synchronization constructions.");
			Console.WriteLine();

			ThreadPool.QueueUserWorkItem(WritingJob);
			ThreadPool.QueueUserWorkItem(ReadingJob);

			Thread.Sleep(3000);

			Console.ReadLine();
		}

		private static void WritingJob(object state)
		{
			var elemsCount = 10;
			var random = new Random();
			for (var i = 0; i < elemsCount; i++)
			{
				SharedList.Add(random.Next(0, elemsCount));
			}
		}

		private static void ReadingJob(object statw)
		{
			foreach (var elem in SharedList)
			{
				Console.WriteLine(elem);
			}
		}
	}
}
