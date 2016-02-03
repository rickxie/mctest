using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Message.Proxy;

namespace MCTest
{
    public class BasicAuthTest:IOutput
    {
        public void Main()
        {
            //            var client = new OAuthClient("1234", "5678", "http://shaappt0001.ad.shalu.com:8091/", OAuthType.ClientIdAndSecrect);
            var client = new BasicAuthClient("db05e8b15df63426b6f81fb2", "acc52df6210d6b37c45295b0", "https://api.jpush.cn/");
            var returnStr = client.Post("/v3/push", "{\"platform\": \"all\", \"audience\" : \"all\", \"notification\" : {\"alert\" : \"测试通知，不要看\", \"android\" : {}, \"ios\" : {\"extras\" : { \"newsid\" : 321} } } }");
            Console.Write(returnStr);
        }
    }
}
