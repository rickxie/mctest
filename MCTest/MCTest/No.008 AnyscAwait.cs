using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MCTest
{
    public class AnyscAwait: IOutput
    {
        async Task<bool> ConsoleA()
        {
            Trace.WriteLine(string.Format("Start ConsoleA Thread:{0}", Thread.CurrentThread.ManagedThreadId));
            await Task.Delay(5000);
            Trace.WriteLine(string.Format("End ConsoleA Thread:{0}", Thread.CurrentThread.ManagedThreadId));
            return true;
        }
        async Task<bool> ConsoleB()
        {
            Trace.WriteLine(string.Format("ConsoleB Thread:{0}", Thread.CurrentThread.ManagedThreadId));
            await Task.Delay(5000);
            Trace.WriteLine(string.Format("End ConsoleB Thread:{0}", Thread.CurrentThread.ManagedThreadId));
            return true;
        }
        async Task<bool> ConsoleC()
        {
            Trace.WriteLine(string.Format("ConsoleC Thread:{0}", Thread.CurrentThread.ManagedThreadId));
            await Task.Delay(5000);
            Trace.WriteLine(string.Format("End ConsoleC Thread:{0}", Thread.CurrentThread.ManagedThreadId));
            return true;
        }
        async void AnsyMain()
        {
            var now = DateTime.Now;
            
            Trace.WriteLine(string.Format("AnsyMain Thread:{0}", Thread.CurrentThread.ManagedThreadId));
            //await ConsoleA();
            //Console.WriteLine("AfterA");
            //await ConsoleB();
            //Console.WriteLine("AfterB");
            //var result = await ConsoleC();
            //Console.WriteLine("AfterC");

            var taskA = ConsoleA();
            Console.WriteLine("AfterA");
            var taskB = ConsoleB();
            Console.WriteLine("AfterB");
            var taskC = ConsoleC();
            Console.WriteLine("AfterC");
            Task.WaitAll(taskA, taskB, taskC);
            
            Console.WriteLine("Total cost:{0}", (DateTime.Now - now).TotalMilliseconds);
        }
         public void Main()
        {
            Trace.WriteLine(string.Format("Main Thread:{0}", Thread.CurrentThread.ManagedThreadId));
             AnsyMain();
             Trace.WriteLine(string.Format("After Thread:{0}", Thread.CurrentThread.ManagedThreadId));
             Console.WriteLine("END");
        }
        
    }
}
