/*
 * 1.	Write a program, which creates an array of 100 Tasks, runs them and waits all of them are not finished.
 * Each Task should iterate from 1 to 1000 and print into the console the following string:
 * “Task #0 – {iteration number}”.
 */
using System;
using System.Threading.Tasks;

namespace MultiThreading.Task1._100Tasks
{
	class Program
	{
		const int TaskAmount = 100;
		const int MaxIterationsCount = 1000;

		static void Main(string[] args)
		{
			Console.WriteLine(".Net Mentoring Program. Multi threading V1.");
			Console.WriteLine("1.	Write a program, which creates an array of 100 Tasks, runs them and waits all of them are not finished.");
			Console.WriteLine("Each Task should iterate from 1 to 1000 and print into the console the following string:");
			Console.WriteLine("“Task #0 – {iteration number}”.");
			Console.WriteLine();

			HundredTasks();

			Console.ReadLine();
		}

		static void HundredTasks()
		{
			var tasksArr = new Task[TaskAmount];
			var random = new Random();

			for (var i = 0; i < tasksArr.Length; i++)
			{
				tasksArr[i] = new Task(param =>
				{
					var taskNum = (int)param;
					var iterationCount = random.Next(1, MaxIterationsCount);
					for (var j = 0; j < iterationCount; j++)
					{
						Output(taskNum, j);
					}
				}, i);
			}

			Parallel.ForEach<Task>(tasksArr, (t) => { t.Start(); });
			Task.WaitAll();
		}

		static void Output(int taskNumber, int iterationNumber)
		{
			Console.WriteLine($"Task #{taskNumber} – {iterationNumber}");
		}
	}
}
