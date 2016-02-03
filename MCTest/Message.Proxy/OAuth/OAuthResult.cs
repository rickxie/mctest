using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Message.Proxy
{
    public class OAuthResult
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public long expires_in { get; set; }
    }


    public enum OAuthType
    {
        UserNameAndPassword,
        ClientIdAndSecrect
    }
}
