using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Microsoft.Win32;
namespace MCTest
{
    public class TaskSs : IOutput
    {
        public void Main()
        {
            Stopwatch sp = new Stopwatch();
            sp.Start();
            do
            {
                new Task(() =>
                {
                    Console.WriteLine("Thread Id: " + Thread.CurrentThread.ManagedThreadId);
                }).Start();
            } while (sp.Elapsed.Seconds != 2);
            sp.Stop();
        }
    }
     


}
