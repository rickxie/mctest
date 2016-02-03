using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Message.Proxy;
using Newtonsoft.Json;

namespace MCTest
{
    public class CorssDomain:IOutput
    {
        public void Main()
        {
//            var client = new OAuthClient("1234", "5678", "http://shaappt0001.ad.shalu.com:8091/", OAuthType.ClientIdAndSecrect);
            var client = new OAuthClient("admin", "123qwe", "http://localhost:1221/", OAuthType.UserNameAndPassword);
            var returnStr = client.Post("/api/services/app/sys/ResetValidationCode", "{ 'phoneNumber': '18817712347'}");

//            var returnStr = client.Post("/api/services/app/sys/ResetPassword", "{ 'phoneNumber': '18621713857', 'Password':'123456', 'Code': '3531'}");
            Console.Write(returnStr);
        }
    }


    
}
