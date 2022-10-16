/*
 * 5. Write a program which creates two threads and a shared collection:
 * the first one should add 10 elements into the collection and the second should print all elements
 * in the collection after each adding.
 * Use Thread, ThreadPool or Task classes for thread creation and any kind of synchronization constructions.
 */
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThreading.Task5.Threads.SharedCollection
{
    class Program
    {
        private static readonly Semaphore _semaphore = new Semaphore(0, 1);
        private static List<int> _sharedCollection = new List<int>(10);
        static void Main(string[] args)
        {
            Console.WriteLine("5. Write a program which creates two threads and a shared collection:");
            Console.WriteLine("the first one should add 10 elements into the collection and the second should print all elements in the collection after each adding.");
            Console.WriteLine("Use Thread, ThreadPool or Task classes for thread creation and any kind of synchronization constructions.");
            Console.WriteLine();
      
            Task.Run(() => Add());
            Task.Run(() => Print());

            Console.ReadLine();
        }
        private static void Add()
        {
            var random = new Random();

            for (int i = 0; i < 10; i++)
            {
                var randomValue = random.Next(10);
                _sharedCollection.Add(randomValue);
                Console.WriteLine($"Elelment added : {randomValue}");
                _semaphore.Release();
                _semaphore.WaitOne();
            }
        }

        private static void Print()
        {
            for (int i = 0; i < _sharedCollection.Capacity; i++)
            {
                _semaphore.WaitOne();
                Console.WriteLine($"Elelment printed : {_sharedCollection[i]}");
                _semaphore.Release();
            }
        }
    }
}
