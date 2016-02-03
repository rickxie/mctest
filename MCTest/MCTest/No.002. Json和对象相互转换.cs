using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace MCTest
{
    public class Json和对象相互转换: IOutput
    {
        public void Main()
        {
            var jsonBstring = "{Name:'太天真', Type:'没头脑'}";
            var jsonSstring = "{name:'太天真', type:'没头脑'}";
            //从String专成对象
            var obj = JsonConvert.DeserializeObject<JsonStringWithObj>(jsonBstring);
            var Sobj = JsonConvert.DeserializeObject<JsonStringWithObj>(jsonSstring,
                new JsonSerializerSettings() {ContractResolver = new CamelCasePropertyNamesContractResolver()});
            Console.WriteLine("输出：" + obj.Name);
            Console.WriteLine("输出：" + Sobj.Type);
            //将对象序列化为String
            var serializerStr = JsonConvert.SerializeObject(obj, new JsonSerializerSettings(){ContractResolver = new CamelCasePropertyNamesContractResolver()});
            Console.WriteLine("输出：" + serializerStr);


            //Type value practice
            var collection = new Dictionary<string, TypeValue>();
            collection.Add("LiuLin", new TypeValue { Type = "1", Value = "男" });
            collection.Add("Jonny", new TypeValue { Type = "1", Value = "理工科" });
            collection.Add("MC", new TypeValue { Type = "1", Value = "小" });
            var collctionStr = JsonConvert.SerializeObject(collection);
            Console.WriteLine("输出：" + collctionStr);
            var str =
                "{'XiaoFang':{Type:'13456',Value:'男'},'Jonny':{Type:'1',Value:'理工科'},'MC':{Type:'1',Value:'小'}}";
            var obj1 = JsonConvert.DeserializeObject<Dictionary<string, TypeValue>>(str);

            Console.WriteLine(obj1["XiaoFang"].Type);
        }
    }

    public class JsonStringWithObj
    {
        public string Name { get; set; }
        public string Type { get; set; }
    }


    
    public class TypeValue
    {
        public string Type { get; set; }
        public string Value { get; set; }
    }
}
