using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MC.IisSite.Models
{
    public class IisSite
    {
        public string Id { get; set; }
        public string Port { get; set; }
        public string Name { get; set; }
        public string Ip { get; set; }
        public string Memo { get; set; }
        public string Protocol { get; set; }
        public string Replaced { get; set; }
        public IisSite()
        {
            Protocol = "http";
        }
    }
}