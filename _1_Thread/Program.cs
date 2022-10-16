using System.Diagnostics;
using System.Threading;

namespace _1_Thread
{
    class Porgram
    {
        public static void Main(string[] args)
        {
            int idThread = Thread.CurrentThread.ManagedThreadId;
            Console.WriteLine("Головний потік {0}",idThread);
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Thread queue = new Thread(Queue);
            queue.Start();
            SendMessage();

            queue.Join();

            stopWatch.Stop();
            // Get the elapsed time as a TimeSpan value.
            TimeSpan ts = stopWatch.Elapsed;

            // Format and display the TimeSpan value.
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            Console.WriteLine("RunTime " + elapsedTime);


        }
        private static void Queue()
        {
            int idThread = Thread.CurrentThread.ManagedThreadId;
            var consoleColor = Console.ForegroundColor;
            Console.WriteLine("Другорядний потік {0}", idThread);
            for (int i = 0; i < 10; i++)
            {
                //Thread.Sleep(200);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Обробка пасажира: {0}", i + 1);
                Console.ForegroundColor = consoleColor;
            }
        }
        private static void SendMessage()
        {
            for (int i = 0; i < 10; i++)
            {
                //Thread.Sleep(300);
                Console.WriteLine("Відправка повідомлень: {0}", i + 1);
            }
        }
    }
}

