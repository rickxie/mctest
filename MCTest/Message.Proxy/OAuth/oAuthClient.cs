using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;

namespace Message.Proxy
{
    public class OAuthClient : AuthBase
    {
        private const string NullExceptionStr = @"Exception 401 ClientId or secret cann't be empty or null";
        private const string BaseAddressExceptionStr = @"Exception 402 baseAddress cann't be empty or null";
        private const string ClientIdException = @"Exception 403 clientId or secret is not match";
        private readonly string _clientId = null;
        private readonly string _secret = null;
        private string _accessToken = null;
        private OAuthType Type;
        public OAuthClient(string clientId, string secret, string baseAddress, OAuthType type)
        {
            this._clientId = clientId;
            this._secret = secret;
            Type = type;
            if (string.IsNullOrWhiteSpace(clientId) && string.IsNullOrWhiteSpace(secret))
            {
                throw new ArgumentNullException(NullExceptionStr);
            }
            else if (string.IsNullOrWhiteSpace(baseAddress))
            {
                throw new ArgumentNullException(NullExceptionStr);
            }

            _httpClient.BaseAddress = new Uri(baseAddress);
          
            GetAccessToken();
        }

        private void GetAccessToken()
        {
            var parameters = new Dictionary<string, string>();
            if (Type == OAuthType.ClientIdAndSecrect)
            {
                parameters.Add("client_id", _clientId);
                parameters.Add("client_secret", _secret);
                parameters.Add("grant_type", "client_credentials");
            }
            else
            {
                parameters.Add("username", _clientId);
                parameters.Add("password", _secret);
                parameters.Add("grant_type", "password");
            }
            var result = HttpPost("/token", new FormUrlEncodedContent(parameters));
            Regex reg = new Regex(@"""access_token"":""([^""]+)"",");
            var g = reg.Match(result).Groups;
            if (g == null || string.IsNullOrWhiteSpace(g[1].Value))
            {
                throw new Exception(ClientIdException);
            }
            this._accessToken = g[1].Value;
        }

        public string Post(string url, string param)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this._accessToken);
            _httpClient.DefaultRequestHeaders.Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var result = HttpPost(url, new StringContent(param, Encoding.UTF8, "application/json"));
            Regex reg = new Regex(@"""unAuthorizedRequest"":([^""|^,|^}]+)");
            var g = reg.Match(result).Groups;

            if (g[1].Value.ToUpper().Equals("TRUE"))
            {
                GetAccessToken();
                result = Post(url, param);
            }
             
            return result;
        }

        
    }

}
