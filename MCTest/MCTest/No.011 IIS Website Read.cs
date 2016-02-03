using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MCTest
{
    public class IisSiteReader : IOutput
    {
        public void Main()
        {
            DirectoryEntry rootEntry = new DirectoryEntry("IIS://localhost/w3svc");
            int siteID = 1;
            IPAddress hostIp = Dns.GetHostAddresses(Dns.GetHostName()).ToList().First(d => d.AddressFamily == AddressFamily.InterNetwork);
            foreach (DirectoryEntry entry in rootEntry.Children)
            {
                if (entry.SchemaClassName.Equals("IIsWebServer", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Name: {0}", entry.Name);
                    Console.WriteLine("Path: {0}", IISWorker.GetWebsitePhysicalPath(entry));
                    Console.WriteLine("ServerBindings: {0}", entry.Properties["ServerBindings"].Value);
                    Console.WriteLine();
                    DirectoryEntry virEntry = new DirectoryEntry(entry.Path + "/ROOT");
                    foreach (DirectoryEntry entryVirtual in virEntry.Children)
                    {
                        if (entryVirtual.SchemaClassName.Equals("IIsWebVirtualDir", StringComparison.OrdinalIgnoreCase))
                        {
                            Console.WriteLine("SchemaClassName: {0}", entryVirtual.SchemaClassName);
                            Console.WriteLine("Name: {0}", entryVirtual.Name);
                            Console.WriteLine("Path: {0}", entryVirtual.Properties["Path"].Value);
                            Console.WriteLine();
                        }
                    }
                    int ID = Convert.ToInt32(entry.Name);
                    if (ID >= siteID)
                    {
                        siteID = ID + 1;
                    }
                }
            }
        }
    }
    public class IISWorker
    {
        /// <summary>
        /// 得到网站的物理路径
        /// </summary>
        /// <param name="rootEntry">网站节点</param>
        /// <returns></returns>
        public static string GetWebsitePhysicalPath(DirectoryEntry rootEntry)
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
