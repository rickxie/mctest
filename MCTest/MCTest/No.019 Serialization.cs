using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Web.Script.Serialization;
using Microsoft.Win32;

namespace MCTest
{
    public class Serialization : IOutput
    {
        public void Main()
        {
            System.Runtime.Serialization.FormatterConverter fc = new FormatterConverter();
//            System.Runtime.Serialization.Json.DataContractJsonSerializer 
            JavaScriptSerializer sc = new JavaScriptSerializer();
            var dt = new DataTable();
            dt.Columns.Add(new DataColumn("A"));
            dt.Rows.Add(dt.NewRow()["A"]);
            sc.Serialize(dt);
//            Stopwatch sp = new Stopwatch();
//            sp.Start();
//            do
//            {
//                new Task(() =>
//                {
//                    Console.WriteLine("Thread Id: " + Thread.CurrentThread.ManagedThreadId);
//                }).Start();
//            } while (sp.Elapsed.Seconds != 2);
//            sp.Stop();
        }
    }

    public class Dtto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
    }
     


}
