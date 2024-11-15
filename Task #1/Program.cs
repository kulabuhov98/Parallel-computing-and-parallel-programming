// See https://aka.ms/new-console-template for more information
using System;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        int N = 0; // Начальное число
        int M = 1000; // Конечное число
        int numberOfThreads = 5;

        for (int i = 0; i < numberOfThreads; i++)
        {
            Thread thread = new Thread(() => PrintNumbers(N, M));
            thread.Start();
        }

        // Опционально: чтобы основная программа не завершилась сразу
        Console.WriteLine("Нажмите любую клавишу для завершения...");
        Console.ReadKey();
    }

    static void PrintNumbers(int start, int end)
    {
        Random rand = new Random();
        while (true)
        {
            for (int j = start; j <= end; j++)
            {
                Console.WriteLine($"Поток {Thread.CurrentThread.ManagedThreadId}: {j}");
                Thread.Sleep(50); // Приостановка на 50 мс
            }
            // Опционально: небольшая пауза между циклами
            Thread.Sleep(rand.Next(100, 500));
        }
    }
}