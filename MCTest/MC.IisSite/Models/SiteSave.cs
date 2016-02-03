using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MC.IisSite.Models
{
    public class SiteSave
    {
        public string Id { get; set; }
        [AllowHtml]
        public string Detail { get; set; }
    }
}