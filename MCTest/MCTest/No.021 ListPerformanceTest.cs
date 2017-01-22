using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCTest
{
    public class ListPerformanceTest: TestBase, IOutput
    {
       public void Main()
       {
           var list = new List<string>();
           string[] array = new string[100000];
           Hashtable ht = new Hashtable();
           Dictionary<string,string> dict = new Dictionary<string, string>();
           for (int i = 0; i < 100000; i++)
           {
               list.Add(i + "Test");
               array[i] = i + "Test";
                ht.Add(i, i + "Test");
                dict.Add(i.ToString(), i + "Test");
           }
//            Test(1000, () =>
//            {
//                a.Any(r => r == "1");
////                a.Contains("1");
//            }, "List");
            //Test(10000, () =>
            //{
            //    b.Contains("1");
            //}, "Array");
            Test(1000, () =>
            {
//                    array.Contains("A");
                                array.Any(r=> r == "A");
            }, "HashTable");
            //Test(10000, () =>
            //{
            //    d.ContainsKey("1");
            //}, "Dictionary");
       }
    }
}
