// See https://aka.ms/new-console-template for more information
using System;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        int Q = 190; // Количество таймеров
        int N = 0; // Нижняя граница случайного числа
        int M = 1000; // Верхняя граница случайного числа
        int interval = N * 10; // Периодичность в миллисекундах

        Timer[] timers = new Timer[Q];
        for (int i = 0; i < Q; i++)
        {
            timers[i] = new Timer(state =>
            {
                PrintRandomNumber(N, M);
            }, null, 0, interval);
        }

        Console.WriteLine($"Запущено {Q} таймеров. Нажмите любую клавишу для завершения...");
        Console.ReadKey();

        // Освобождение ресурсов таймеров
        foreach (var timer in timers)
        {
            timer.Dispose();
        }
    }

    static void PrintRandomNumber(int min, int max)
    {
        // Используем локальный экземпляр Random для избежания проблем с потокобезопасностью
        int threadId = Thread.CurrentThread.ManagedThreadId;
        int number;
        lock (typeof(Program))
        {
            // Создаем один экземпляр Random для всей программы
            // Альтернативно, можно использовать ThreadLocal<Random>
            number = new Random().Next(min, max + 1);
        }
        Console.WriteLine($"Поток {threadId}: {number}");
    }
}
