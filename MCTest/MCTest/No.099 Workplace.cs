using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCTest
{
    public class Workplace:IOutput
    {
        public void Main()
        {
            var projectName = "Shalu";
            var two = projectName.Split('.');
            Console.WriteLine(two[0]);
            Console.WriteLine(two[1]);
        }
    }
}
