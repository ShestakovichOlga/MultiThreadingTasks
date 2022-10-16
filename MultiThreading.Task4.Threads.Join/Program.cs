/*
 * 4.	Write a program which recursively creates 10 threads.
 * Each thread should be with the same body and receive a state with integer number, decrement it,
 * print and pass as a state into the newly created thread.
 * Use Thread class for this task and Join for waiting threads.
 * 
 * Implement all of the following options:
 * - a) Use Thread class for this task and Join for waiting threads.
 * - b) ThreadPool class for this task and Semaphore for waiting threads.
 */

using System;
using System.Threading;

namespace MultiThreading.Task4.Threads.Join
{
    class Program
    {
        private static int _countThreads = 10;
        private static readonly Semaphore _semaphore = new Semaphore(0, 10);
        static void Main(string[] args)
        {
            Console.WriteLine("4.	Write a program which recursively creates 10 threads.");
            Console.WriteLine("Each thread should be with the same body and receive a state with integer number, decrement it, print and pass as a state into the newly created thread.");
            Console.WriteLine("Implement all of the following options:");
            Console.WriteLine();
            Console.WriteLine("- a) Use Thread class for this task and Join for waiting threads.");
            Console.WriteLine("- b) ThreadPool class for this task and Semaphore for waiting threads.");

            Console.WriteLine();

            CreateThreads(20);

            _countThreads = 10;

            CreateThreadPool(15);

            Console.ReadLine();
        }

        private static void CreateThreads(int integer)
        {
            if (_countThreads == 0)
                return;

            var thread = new Thread(() =>
            {
                Console.WriteLine(integer);
                CreateThreads(--integer);
            });

            _countThreads--;

            thread.Start();
            thread.Join();
        }

        private static void CreateThreadPool(int integer)
        {
            if (_countThreads == 0)
                return;

            ThreadPool.QueueUserWorkItem(_ =>
            {
                Console.WriteLine(integer);
                CreateThreadPool(--integer);

                _semaphore.Release();
            });

            _countThreads--;

            _semaphore.WaitOne();
        }
    }
}
