using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MCTest
{
    public class RegexExcract : IOutput
    {
        public void Main()
        {
            //            var str = @"{""access_token"":""5bG4TUx_OI2rZvda8JPDtoC7fC4oH1IhqSqj3O75uXkCMwGIo7QptDE9VG_IcgnAOmonqXvzxzuJ7ICeYmlwle_Lyp2JNe2ONMzzRKzd_foX2ZpWQ6pg7qT--zHH7XCxo8R4qnt6rzZOkWBqs7SZoF3JgOqMFmbhjOlj1D2 - wHPi37_4Nj1AtT - dFRzCBQszt05YC_d3aV9hDJd5g3AnDZIo02rldesnNECifOCuVCt25hwEG3CrdT9J5C7Kbw_zmewBI7X0jFFnGg99Dc5J0uiipseDd7Yt_e6Oc5C7gsYGR1l7Q2r8dO8QxptwQM9wkcKV8r2h8KalGaxY8PcU97yAm7dUt2cGS1e8 - KTt_Lv1lxmXuI09pswOXH4d3ZuNHapEzF - 1NnwDRNKAiKqBt1 - rLgsgwhgIDGHIPIU8bPzUVz4JhdVG4tsbZfJFjSpo"",""token_type"":""bearer"",""expires_in"":9}";
            //            Regex reg = new Regex(@"""access_token"":""([^""]+)"",");

            string inputJson = @"{{""platform"": ""all"", ""audience"" : {0}, ""notification"" : {{""alert"" : ""{1}"", ""android"" : {{}}, ""ios"" : {{ ""extras"" : {{ ""newsid"" : 321}} }} }} }}";
//            inputJson = "{\"platform\": \"all\", \"audience\" : {0}, \"notification\" : {\"alert\" : \"{1}\"";
            var request = string.Format(inputJson, "to","msg");
            Console.WriteLine(request);
            //            var str = @"""unAuthorizedRequest"":true"",""";

            var str = "{\"sendno\":\"0\",\"msg_id\":\"1488484392\"}";
            // (result[1]).Value == "\"0\""
            Regex reg = new Regex(@"""sendno"":([^,]+)");
            var result = reg.Match(str).Groups;
            foreach (var item in result)
            {
                Console.WriteLine(item);
            }
        }
    }
}
