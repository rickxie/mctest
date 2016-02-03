using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Web;
using System.Web.Mvc;
using MC.IisSite.Models;
using System.DirectoryServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace MC.IisSite.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            List<MC.IisSite.Models.IisSite> sites = GetList();
            var oldStr = System.IO.File.ReadAllText(Server.MapPath("~/") + "\\Data\\SiteData.json");
            var getOld = JsonConvert.DeserializeObject<List<SiteSave>>(oldStr);
            if (getOld != null)
            {
                foreach (var s in sites)
                {
                    foreach (var o in getOld)
                    {
                        if (s.Id == o.Id)
                        {
                            s.Replaced = o.Detail;
                        }
                    }
                }
            }
            return View(sites);
        }

        [HttpPost]
        public ActionResult Save(List<SiteSave> save)
        {
            var path = Server.MapPath("~/") + "\\Data\\SiteData.json";
            var oldStr = System.IO.File.ReadAllText(path);
            var getOld = JsonConvert.DeserializeObject<List<SiteSave>>(oldStr);
            foreach (var s in save)
            {
                if (getOld == null)
                {
                    getOld = new List<SiteSave>();
                }
                if (getOld.All(r => r.Id != s.Id))
                {
                    getOld.Add(s);
                }
                else
                {
                    getOld.Find(r => r.Id == s.Id).Detail = s.Detail;
                }
            }
            var strNeedToSave = JsonConvert.SerializeObject(getOld);
            System.IO.File.WriteAllText(path, strNeedToSave);
            return new JsonResult();
        }
        private List<MC.IisSite.Models.IisSite> GetList()
        {
            List<MC.IisSite.Models.IisSite> sites = new List<MC.IisSite.Models.IisSite>();
            DirectoryEntry rootEntry = new DirectoryEntry("IIS://localhost/w3svc");
            IPAddress hostIp = Dns.GetHostAddresses(Dns.GetHostName()).ToList().First(d => d.AddressFamily == AddressFamily.InterNetwork);
            foreach (DirectoryEntry entry in rootEntry.Children)
            {
                if (!entry.SchemaClassName.Equals("IIsWebServer", StringComparison.OrdinalIgnoreCase)) continue;

                var binding = entry.Properties["ServerBindings"].Value.ToString().Split(':');
                sites.Add(new Models.IisSite()
                {
                    Id =  entry.Name,
                    Ip =  binding[0],
                    Port = binding[1],
                    Memo = GetWebsitePhysicalPath(entry)
                });
            }

            return sites;
        }
        private string GetWebsitePhysicalPath(DirectoryEntry rootEntry)
        {
            string physicalPath = "";
            foreach (DirectoryEntry childEntry in rootEntry.Children)
            {
                if ((childEntry.SchemaClassName == "IIsWebVirtualDir") && (childEntry.Name.ToLower() == "root"))
                {
                    if (childEntry.Properties["Path"].Value != null)
                    {
                        physicalPath = childEntry.Properties["Path"].Value.ToString();
                    }
                    else
                    {
                        physicalPath = "";
                    }
                }
            }
            return physicalPath;
        }
    }
}