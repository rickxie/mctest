using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCTest
{
    public class TestBase 
    {
        public void Test(int times, Action func, string name = null)
        {
            name = name ?? string.Empty;
            Task.Factory.StartNew(() =>
            {
                var timeList = new List<double>();
                Stopwatch sp = new Stopwatch();
                sp.Start();
                for (int i = 0; i < times; i++)
                {
                    Stopwatch sp2 = new Stopwatch();
                    sp2.Start();
                    func();
                    sp2.Stop();
                    timeList.Add(sp2.Elapsed.TotalMilliseconds);
                    //Console.WriteLine(string.Format(name +" 本次耗时:{0}", sp2.Elapsed.TotalMilliseconds));
                }
                sp.Stop();
                Console.WriteLine(string.Format(name + " 共计测试 {0} 次 共耗时:{1} 平均:{2}", times, sp.Elapsed.TotalMilliseconds, timeList.Average()));
            });
        }

        public static string RootPath => AppDomain.CurrentDomain.BaseDirectory;
        public static string GetRelativeDir(string relevantDir)
        {
            var dir = RootPath + relevantDir;
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            return dir;
        }
    }
}
