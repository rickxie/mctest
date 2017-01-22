using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCTest
{
    public class FileLog: TestBase,IOutput
    {
		public object _lock  = new object();

        public void Log(string txt)
        {
            lock (_lock)
            {
                var logDir = GetRelativeDir("Logs/");
                var filePath = logDir + "logs.txt";
                //            using (var fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite))
                //            {
                using (var sr = new StreamWriter(filePath, true, Encoding.Default))
                {
                    sr.WriteLine(txt);
                    sr.Close();
                }
            }
        }

        public void Main()
        {
             Test(10000, () =>
             {
                 Log(DateTime.Now.ToString());
             });
             Test(10000, () =>
             {
                 Log(DateTime.Now.ToString());
             });


        }
    }
}
