using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MCTest
{
    public class 显式隐式转换 : IOutput
    {
        public void Main()
        {
            var type = typeof(Dog);
            var a = (McAttribute)type.GetCustomAttribute(typeof(McAttribute), true);
            Console.WriteLine(a.Name);
        }
    }

    [Mc("this is a dog")]
    class Dog
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public static implicit operator Dog(Cat cat)
        {
            return new Dog { Age = cat.Age, Name = cat.Name };
        }
        public static implicit operator Cat(Dog cat)
        {
            return new Cat
            {
                Name = cat.Name,
                Age = cat.Age
            };
        }
    }

    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class McAttribute : Attribute
    {
        public string Name { get; set; }
        public McAttribute(string name)
        {
            Name = name;
        }
    }
    class ConvertY
    {

    }

    class Cat
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
