/*
 * 2.	Write a program, which creates a chain of four Tasks.
 * First Task – creates an array of 10 random integer.
 * Second Task – multiplies this array with another random integer.
 * Third Task – sorts this array by ascending.
 * Fourth Task – calculates the average value. All this tasks should print the values to console.
 */
using System;
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

            // feel free to add your code
            var random = new Random();

            var tasks = Task.Run(() =>
            {
                var integers = new int[10];
                for (var i = 0; i < integers.Length; i++)
                {
                    integers[i] = random.Next(0,20);
                    Console.WriteLine(integers[i]);
                }
                return integers;

            }).ContinueWith(result =>
            {
                var integerArray = result.Result;
                var multiplier = random.Next(1,100);
                Console.WriteLine($"\n\nmultiplier = {multiplier}\n");
                for (var i = 0; i < integerArray.Length; i++)
                {
                    integerArray[i] *= multiplier;
                    Console.WriteLine(integerArray[i]);
                }
                return integerArray;

            }).ContinueWith(result =>
            {
                var array = result.Result;
                var sortedArray = array.OrderBy(x => x).ToArray();
                Console.WriteLine("\n\nSorted integers:\n");
                foreach (var item in sortedArray)
                {
                    Console.WriteLine(item);
                }
                return sortedArray;

            }).ContinueWith(result =>
            {
                var array = result.Result;
                Console.WriteLine($"\n\nAverage value: {array.Average()}");
            });

            tasks.Wait();

            Console.ReadLine();
        }
    }
}
