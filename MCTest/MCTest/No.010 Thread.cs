using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MCTest
{
    public class ThreadTest:IOutput
    {
        public void Main()
        {
//			new Thread(ThreadA).Start();
//			new Thread(ThreadB).Start();
//			new Thread(ThreadC).Start();
			new Thread(TaskTest).Start();
        }

		/// <summary>
        /// Thread
        /// </summary>
        private void ThreadA()
        {
            var now = DateTime.Now;

            var treadList = new List<Thread>();
            for (int i = 0; i < 10000; i++)
            {
                var ai = i;
                var td = new Thread(() => Console.WriteLine(ai + ":" + Thread.CurrentThread.ManagedThreadId));
					td.Start();
				treadList.Add(td);

            }
            int maxWorkerThreads, workerThreads;
            int portThreads;
            while (true)
            {
                ThreadPool.GetMaxThreads(out maxWorkerThreads, out portThreads);
                ThreadPool.GetAvailableThreads(out workerThreads, out portThreads);
                Console.WriteLine(workerThreads);
                if (maxWorkerThreads - workerThreads == 0)
                {
                    Console.WriteLine("Thread Finished!");
                    break;
                }
            }
            var span = DateTime.Now - now;
            Console.WriteLine("Total(ms):" + span.TotalMilliseconds);
        }
		/// <summary>
        /// ThradPool
        /// </summary>
        private void ThreadB()
        {
            var now = DateTime.Now;

            for (int i = 0; i < 10000; i++)
            {
                var ai = i;
                ThreadPool.QueueUserWorkItem(r =>
                {
                    Console.WriteLine(ai + ":" + Thread.CurrentThread.ManagedThreadId);

                });
            }



            int maxWorkerThreads, workerThreads;
            int portThreads;
            while (true)
            {
                ThreadPool.GetMaxThreads(out maxWorkerThreads, out portThreads);
                ThreadPool.GetAvailableThreads(out workerThreads, out portThreads);
                    Console.WriteLine(workerThreads);
                if (maxWorkerThreads - workerThreads == 0)
                {
                    Console.WriteLine("Thread Finished!");
                    break;
                }
            }
            var span = DateTime.Now - now;
            Console.WriteLine("Total(ms):" + span.TotalMilliseconds);
        }
		/// <summary>
        /// Task
        /// </summary>
        private void ThreadC()
        {
            var now = DateTime.Now;
		    var taskList = new List<Task>();
		    for (int i = 0; i < 10000; i++)
		    {
		        var ai = i;
		        var varTask = new Task(r =>{
		            Console.WriteLine(ai + ":" + Thread.CurrentThread.ManagedThreadId);
		        }, null);
		        taskList.Add(varTask);
		        varTask.Start();
		    }
		    Task.WaitAll(taskList.ToArray());


            var span = DateTime.Now - now;
            Console.WriteLine("Total(ms):" + span.TotalMilliseconds);
        }

        private void TaskTest()
        {
            var now = DateTime.Now;
            var task = Task.Factory.StartNew<TaskOutput>(() =>
            {
                var to = new TaskOutput() ;
				Thread.Sleep(2000);
                to.Msg = "success";
                return to;
            });

			Console.WriteLine(task.Result.Msg);
            var span = DateTime.Now - now;
            Console.WriteLine("Total(ms):" + span.TotalMilliseconds);
        }

		public class TaskOutput
        {
		     public string Msg { get; set; }
		}

    }
}
