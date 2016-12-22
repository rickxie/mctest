using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MCTest
{
    public class 工厂
    {
       public static IOutput GetLession(string name)
        {
            IOutput output = null;

            if (name.Equals("显式隐式转换"))
            {
                output = new 显式隐式转换();
            }
            return output;
        }

        public static IOutput 通过反射获取实例<T>()
        {
            var type = typeof (T).FullName.ToString();
            IOutput output = (IOutput) Assembly.GetExecutingAssembly().CreateInstance(type);
            return output;
        }
        public void a() { }
    }
}
