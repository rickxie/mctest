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
    public class RandomNumberGenerator : IOutput
    {
        private const int MAX_LENGTH = 8;
        private const string RN_STRING = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ-_";
        public List<Task> allTask = new List<Task>();
        public static int j = 0;
        public bool Stoped = false;

        public void Main()
        {
            System.Timers.Timer timer = new System.Timers.Timer(1000);
            timer.Elapsed += new ElapsedEventHandler((sender, args) =>
            {
                Console.WriteLine(DateTime.Now);
            });
            timer.Start();
        }

        public void CountForOneSecond(Action action)
        {
            Stopwatch s = new Stopwatch();
            var a = 0;
            s.Start();
            while (true)
            {
                a++;
                action();
//                Console.WriteLine("count:" + a);
                if (s.Elapsed.Seconds == 1)
                {
                    break;
                }
            }
            Console.WriteLine("Total:" + a);
        }

        public void SetupTask()
        {

            for (int i = 0; i < 12; i++)
            {
                Task a = new Task(() =>
                {
                    while (true)
                    {
                        j++;
                        Console.WriteLine(j);
                        //                        Console.WriteLine(j + " " + Thread.CurrentThread.ManagedThreadId + " " + GetId());
                        if (Stoped)
                        {
                            break;
                        }
                    }
                });
                a.Start();
                allTask.Add(a);
            }
        }

        public string GetId()
        {
            RNGCryptoServiceProvider provide = new RNGCryptoServiceProvider();
            var by = new byte[MAX_LENGTH];
            provide.GetNonZeroBytes(by);
            var chrs = new char[MAX_LENGTH];
            for (int i = 0; i < MAX_LENGTH; i++)
            {
                var index = by[i]%RN_STRING.Length;
                chrs[i] = RN_STRING[index];
            }
            return new string(chrs);
        }

        public void GetCpuUsageRate()
        {
            Process[] p = Process.GetProcessesByName("devenv");//获取指定进程信息
            // Process[] p = Process.GetProcesses();//获取所有进程信息
            string cpu = string.Empty;
            string info = string.Empty;

            PerformanceCounter pp = new PerformanceCounter();//性能计数器
            pp.CategoryName = "Process";//指定获取计算机进程信息  如果传Processor参数代表查询计算机CPU 
            pp.CounterName = "% Processor Time";//占有率
            //如果pp.CategoryName="Processor",那么你这里赋值这个参数 pp.InstanceName = "_Total"代表查询本计算机的总CPU。
            pp.InstanceName = "devenv";//指定进程 
            pp.MachineName = ".";
            if (p.Length > 0)
            {
                foreach (Process pr in p)
                {
                    while (true)//1秒钟读取一次CPU占有率。
                    {
                        info = pr.ProcessName + "内存：" +
                                                (Convert.ToInt64(pr.WorkingSet64.ToString()) / 1024).ToString();//得到进程内存
                        Console.WriteLine(info + "    CPU使用情况：" + Math.Round(pp.NextValue(), 2).ToString() + "%");
                        Thread.Sleep(1000);
                    }
                }
            }
        }
    }
     


}
