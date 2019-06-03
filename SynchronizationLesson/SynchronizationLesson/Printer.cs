using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Runtime.Remoting.Contexts;

namespace SynchronizationLesson
{
    [Synchronization]
    public class Printer : ContextBoundObject
    {
        private int _number = 0;
        private object lockObject = new object();

        public void Print()
        {
            Interlocked.Increment(ref _number);

            Monitor.Enter(lockObject);
            try
            {
                var curentThread = Thread.CurrentThread;
                Console.WriteLine($"{curentThread.Name} начинает работать");

                for (int i = 0; i < 10; i++)
                {
                    Thread.Sleep(5 * new Random().Next(100));
                    Console.Write(i + " " + "(" + _number + ")");
                    _number = curentThread.ManagedThreadId;
                }

                Console.WriteLine($"\n{curentThread.Name} закончил работу");
            }
            finally
            {
                Monitor.Exit(lockObject);
            }
        }
    }
}


            /*lock (lockObject)
            {
                var curentThread = Thread.CurrentThread;
Console.WriteLine($"{curentThread.Name} начинает работать");

                for (int i = 0; i< 10; i++)
                {
                    Thread.Sleep(5 * new Random().Next(100));
                    Console.Write(i + " " + "(" + _number + ")");
                    _number = curentThread.ManagedThreadId;
                }

                Console.WriteLine($"\n{curentThread.Name} закончил работу");

            }*/
