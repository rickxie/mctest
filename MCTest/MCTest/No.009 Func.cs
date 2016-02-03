using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Components.DictionaryAdapter.Xml;

namespace MCTest
{
    public class FuncTest:IOutput
    {
        private Func<int, int, int> plus = (x, y) => x+y;
        private Action<int, int> plus2 = (x, y) => { Console.WriteLine(x + y); };
        private Predicate<int> squrt = (x) => x < 5;
        public void Main()
        {
            Console.WriteLine("x:{0},y:{1}=> x+y :{2}", 5,6,plus(5,6));
            plus2(8, 9);
        }
    }
}
