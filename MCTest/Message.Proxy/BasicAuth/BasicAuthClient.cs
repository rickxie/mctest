using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;

namespace Message.Proxy
{
    public class BasicAuthClient : AuthBase
    {
        private const string NullExceptionStr = @"Exception 401 username or password cann't be empty or null";
        private const string BaseAddressExceptionStr = @"Exception 402 baseAddress cann't be empty or null";
        private const string ClientIdException = @"Exception 403 clientId or secret is not match";
        private readonly string username = null;
        private readonly string password = null;
        public BasicAuthClient(string username, string password, string baseAddress)
        {

            this.username = username;
            this.password = password;
            if (string.IsNullOrWhiteSpace(username) && string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentNullException(NullExceptionStr);
            }
            _httpClient.BaseAddress = new Uri(baseAddress);
        }

        public string Post(string url, string param)
        {
            //注意这里的格式哦，为 "username:password"
            string usernamePassword = username + ":" + password;
            var base64Key= Convert.ToBase64String(new ASCIIEncoding().GetBytes(usernamePassword));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64Key);
            _httpClient.DefaultRequestHeaders.Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var result = HttpPost(url, new StringContent(param, Encoding.ASCII, "application/json"));

            //Regex reg = new Regex(@"""unAuthorizedRequest"":([^""|^,|^}]+)");
            //var g = reg.Match(result).Groups;

            //if (g[1].Value.ToUpper().Equals("TRUE"))
            //{
            //    result = Post(url, param);
            //}

            return result;
        }
    }
}
