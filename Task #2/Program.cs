// See https://aka.ms/new-console-template for more information
using System;
using System.Threading;

class Program
{
    // Параметры задачи
    static int K = 50;    // Количество итераций
    static int N = 600;    // Нижняя граница случайного числа
    static int M = 1200;   // Верхняя граница случайного числа

    static void Main(string[] args)
    {
        // Установка максимального количества рабочих и I/O потоков в пуле
        int maxWorkerThreads = 50;
        int maxIOThreads = 50;
        bool success = ThreadPool.SetMaxThreads(maxWorkerThreads, maxIOThreads);
        if (!success)
        {
            Console.WriteLine("Не удалось установить максимальное количество потоков.");
            return;
        }

        // CountdownEvent для ожидания завершения всех задач
        CountdownEvent countdown = new CountdownEvent(K);

        // Создание экземпляра Random
        Random rand = new Random();

        for (int i = 0; i < K; i++)
        {
            // Захват локальной копии переменных для использования в потоке
            int localN = N;
            int localM = M;

            ThreadPool.QueueUserWorkItem(state =>
            {
                try
                {
                    // Генерация случайного числа
                    int randomNumber;
                    // Обеспечиваем потокобезопасность доступа к Random
                    lock (rand)
                    {
                        randomNumber = rand.Next(localN, localM + 1);
                    }

                    // Получение идентификатора текущего потока
                    int threadId = Thread.CurrentThread.ManagedThreadId;

                    // Вывод результата
                    Console.WriteLine($"Поток ID: {threadId}, Случайное число: {randomNumber}");
                }
                finally
                {
                    // Сигнализируем завершение задачи
                    countdown.Signal();
                }
            });
        }

        // Ожидаем завершения всех задач
        countdown.Wait();

        Console.WriteLine("Все задачи завершены.");
    }
}
