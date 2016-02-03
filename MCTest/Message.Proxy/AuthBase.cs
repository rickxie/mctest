using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace Message.Proxy
{
    public class AuthBase
    {
        private const string ConnectException = @"Exception 404 connect failed ";
        protected readonly HttpClient _httpClient = new HttpClient();
        protected string HttpPost(string url, HttpContent content)
        {
            string result;
            try
            {
                result = _httpClient.PostAsync(url, content)
                        .Result.Content.ReadAsStringAsync().Result;
            }
            catch (Exception ex)
            {
                throw new Exception(ConnectException + ex.Message);
            }
            return result;
        }
    }
}
