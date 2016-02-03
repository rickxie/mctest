using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCTest
{
    public class 十六进制表示 : IOutput
    {
        public void Main()
        {
            var a = 0x1;
            var b = 0x9;
            var c = 0x11;
            var d = 0x02;

            Console.WriteLine("输出a的值:" + a);
            Console.WriteLine("输出a的值:" + b);
            Console.WriteLine("输出a的值:" + c);
            Console.WriteLine("输出a的值:" + d);
        }
    }
}
