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
        List<int> threadId = new List<int>();
        async Task<bool> TaskA_Async(int i)
        {
            Console.WriteLine(string.Format("Start "+i+" Thread:{0}", Thread.CurrentThread.ManagedThreadId));
            await Task.Delay(5000);
            if (!threadId.Exists(r=>r == Thread.CurrentThread.ManagedThreadId))
            {
                threadId.Add(Thread.CurrentThread.ManagedThreadId);
            }
            Console.WriteLine(string.Format("End  " + i + "  Thread:{0}", Thread.CurrentThread.ManagedThreadId));
            return true;
        }
      
        private void DoOtherThing()
        {
            Console.WriteLine("Doing other things");
        }
        async void AnsyMain()
        {
            var now = DateTime.Now;
            List<Task> tasks = new List<Task>();
            Console.WriteLine(string.Format("Main Method Thread:{0}", Thread.CurrentThread.ManagedThreadId));
            for (int i = 0; i < 10000; i++)
            {
                tasks.Add(TaskA_Async(i));
            }
            Console.WriteLine("After TaskA");
            DoOtherThing();
            Console.WriteLine(string.Format("Main Method Thread:{0}", Thread.CurrentThread.ManagedThreadId));
            Task.WaitAll(tasks.ToArray());
            Console.WriteLine("Total thread Count:{0}", threadId.Count);
            Console.WriteLine("Total time:{0}", (DateTime.Now - now).TotalMilliseconds);
        }
         public void Main()
        {
             AnsyMain();
             Console.WriteLine("END");
        }
        
    }
}
