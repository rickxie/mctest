using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCTest
{
    public class Clone:IOutput
    {
        public void Main()
        {
            var obj = new BeCloneObj()
            {
                Id = "1",
				Name = "Hello"
            };

            var obj2 = obj.Clone();
            obj2.Id = "2";

			Console.WriteLine("Obj1: " + obj.Id);
			Console.WriteLine("Obj2: " + obj2.Id);

        }
    }

    public class BeCloneObj
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public BeCloneObj Clone()
        {
            return (BeCloneObj)MemberwiseClone();
        }
    }
}
