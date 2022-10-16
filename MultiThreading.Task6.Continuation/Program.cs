/*
*  Create a Task and attach continuations to it according to the following criteria:
   a.    Continuation task should be executed regardless of the result of the parent task.
   b.    Continuation task should be executed when the parent task finished without success.
   c.    Continuation task should be executed when the parent task would be finished with fail and parent task thread should be reused for continuation
   d.    Continuation task should be executed outside of the thread pool when the parent task would be cancelled
   Demonstrate the work of the each case with console utility.
*/
using System;
using System.Threading.Tasks;

namespace MultiThreading.Task6.Continuation
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Create a Task and attach continuations to it according to the following criteria:");
            Console.WriteLine("a.    Continuation task should be executed regardless of the result of the parent task.");
            Console.WriteLine("b.    Continuation task should be executed when the parent task finished without success.");
            Console.WriteLine("c.    Continuation task should be executed when the parent task would be finished with fail and parent task thread should be reused for continuation.");
            Console.WriteLine("d.    Continuation task should be executed outside of the thread pool when the parent task would be cancelled.");
            Console.WriteLine("Demonstrate the work of the each case with console utility.");
            Console.WriteLine();

            var task = new Task(() =>
            {
                Console.WriteLine("Parent task started");
                throw new Exception();
            });

            //Continuation task should be executed regardless of the result of the parent task
            task.ContinueWith(
                task1 => Console.WriteLine($"Task was executed regardless of the result of the parent task"),
                TaskContinuationOptions.None);

            //Continuation task should be executed when the parent task finished without success.
            task.ContinueWith(
                task2 => Console.WriteLine($"Task was executed when the parent task finished without success"),
                TaskContinuationOptions.OnlyOnFaulted);

            //Continuation task should be executed when the parent task would be finished with fail and parent task thread should be reused for continuation
            task.ContinueWith(
                task3 => Console.WriteLine($"Task was executed when the parent task would be finished with fail and parent task thread should be reused for continuation"),
                TaskContinuationOptions.ExecuteSynchronously);

            //Continuation task should be executed outside of the thread pool when the parent task would be cancelled
            task.ContinueWith(
                task4 => Console.WriteLine($"Task was executed outside of the thread pool when the parent task would be cancelled"),
                TaskContinuationOptions.OnlyOnCanceled);

            task.Start();

            Console.ReadLine();
        }
    }
}
