using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Web;
using System.Web.Mvc;
using MC.IisSite.Models;
using System.DirectoryServices;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace MC.IisSite.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            System.Drawing.Image imgSource = Image.FromFile(@"C:\inetpub\愿景周.jpg"); ;
            System.Drawing.Imaging.ImageFormat thisFormat = imgSource.RawFormat;
            int destWidth = 872, destHeight = 384;
            int sW = destWidth, sH = destHeight;

            Bitmap outBmp = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage(outBmp);
            g.Clear(Color.Black);

            // 设置画布的描绘质量
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            g.DrawImage((Image)imgSource.Clone(), new Rectangle((destWidth - sW) / 2, (destHeight - sH) / 2, sW, sH), 0, 0, imgSource.Width, imgSource.Height, GraphicsUnit.Pixel);
            g.Dispose();

            // 以下代码为保存图片时，设置压缩质量
            EncoderParameters encoderParams = new EncoderParameters();
            long[] quality = new long[1];
            quality[0] = 100;

            EncoderParameter encoderParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
            encoderParams.Param[0] = encoderParam;

            try
            {
                //获得包含有关内置图像编码解码器的信息的ImageCodecInfo 对象。
                ImageCodecInfo[] arrayICI = ImageCodecInfo.GetImageEncoders();
                ImageCodecInfo jpegICI = null;
                for (int x = 0; x < arrayICI.Length; x++)
                {
                    if (arrayICI[x].FormatDescription.Equals("JPEG"))
                    {
                        jpegICI = arrayICI[x];//设置JPEG编码
                        break;
                    }
                }

                if (jpegICI != null)
                {
                    outBmp.Save(@"C:\\abc.jpg", jpegICI, encoderParams);
                }
                else
                {
                    outBmp.Save(@"C:\\abc.jpg", thisFormat);
                }

            }
            catch
            {
            }
            finally
            {
                imgSource.Dispose();
                outBmp.Dispose();
            }


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