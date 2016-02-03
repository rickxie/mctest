using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MCTest
{
    public class 特性:IOutput
    {
        public void Main()
        {
            var airplan = new Airplane();
            airplan.Name = "abc";
            var type = typeof (Airplane);
            var desc = (DescriptionAttribute) Attribute.GetCustomAttribute(type, typeof (DescriptionAttribute));
            var property = (DescriptionAttribute)airplan.Name.GetType().GetCustomAttribute(typeof(DescriptionAttribute));
            Console.WriteLine("输出特性:" + desc.Name);
            Console.WriteLine("输出特性:" + property.Name);
        }
    }

    [Description("战斗机类","abcde")]
    public class Airplane
    {
        [Description("类型中文", Level = "haha")]
        public string Type { get; set; }
        
        [Obsolete("已经弃用")]
        public string Name { get; set; }
        public string Value { get; set; }
    
        [Conditional("DEBUG")]
        public void Method() { }
    }
    [AttributeUsage(AttributeTargets.All)]
    public class DescriptionAttribute : Attribute
    {
        public string[] Name { get; set; }
        public string Level { get; set; }

        public DescriptionAttribute(params string[] name)
        {
            this.Name = name;
        }
    }
}
