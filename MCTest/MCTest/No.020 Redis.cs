using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Web.Script.Serialization;
using Castle.Components.DictionaryAdapter;
using Castle.Core.Internal;
using Microsoft.Win32;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace MCTest
{
    public class RedisTest : IOutput
    {
        public void Main()
        {

            var serverConf = "127.0.0.1:6380,127.0.0.1:6381";
            var sentinelConf = "127.0.0.1:5000,127.0.0.1:5001";
            RedisClient client = new RedisClient(serverConf, sentinelConf);
            client.SystemKey = "Redis";
            client.Start();
            List<RoleId> roleids = new List<RoleId>();

            for (int i = 0; i < 100000; i++)
            {
                roleids.Add(new RoleId() {Id =  i.ToString(), JobId =  "JobId+"+i, Name = "Xiaoming", Sex = "男"});
            }
            
            client.ListSet("Role", roleids);
//            client.ListSet("Role1", roleids);
//            client.ListSet("Role2", roleids);
//            client.ListSet("Role3", roleids);
//            client.ListSet("Role4", roleids);


            var times = new List<double>();
            for (int i = 0; i < 15; i++)
            {
                Stopwatch sp = new Stopwatch();
                sp.Start();
                var count = client.ListGet<RoleId>("Role").Count;
//                    var count1 = client.ListGet<RoleId>("Role1").Count;
//                    var count2 = client.ListGet<RoleId>("Role2").Count;
//                    var count3 = client.ListGet<RoleId>("Role3").Count;
//                    var count4 = client.ListGet<RoleId>("Role4").Count;
                sp.Stop();
                times.Add(sp.Elapsed.TotalMilliseconds);
                Console.WriteLine(string.Format("本次耗时:{0}", sp.Elapsed.TotalMilliseconds));
            }
            Console.WriteLine(string.Format("共计获取{0}条数据， 测试 {1} 次 平均:{2}", 100000, times.Count, times.Average()));
        }

        private void WriteError()
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine("Failed");
        }

        private void WriteLine(string text)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine(text);
        }

        private static void WriteWarn(string text)
        {
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.WriteLine(text);
        }

        public static void SubSentinel()
        {
//            sentinelsub.Subscribe("+switch-master", (channel, message) =>
//            {
//                WriteWarn((string)message);
//            });
        }


        public class RoleId
        {
            public string Id { get; set; }
            public string JobId { get; set; }
            public string Name { get; set; }
            public string Sex { get; set; }
        }
    }


    public interface ICacheClient
    {
        void Start();
        void Stop();

    }

    public class CacheSubscribe
    {

    }

    public class RedisClient : ICacheClient
    {
        public string SystemKey = string.Empty;
        private static ConnectionMultiplexer _conn;
        private static ConnectionMultiplexer _sentinel;
        private static ISubscriber subScriber;
        private static ISubscriber _sentinelsub;

        /// <summary>
        /// Stoped = 0 , Runing = 1
        /// </summary>
        public RedisServerStatus Status = RedisServerStatus.Stoped;

        private readonly List<AddressPort> _serverConf;
        private readonly List<AddressPort> _sentinelConf;

        public RedisClient(string serverConf, string sentinelConf)
        {
            _serverConf = new EditableList<AddressPort>();
            _sentinelConf = new EditableList<AddressPort>();
            if (serverConf.IsNullOrEmpty())
            {
                throw new NoNullAllowedException("serverConf can't be empty or null");
            }
            if (sentinelConf.IsNullOrEmpty())
            {
                throw new NoNullAllowedException("sentinelConf can't be empty or null");
            }
            if (!IsValidConfig(serverConf, _serverConf))
            {
                throw new ArgumentException(
                    "serverConf string format is wrong. should be like '127.0.0.1:8080,127.0.0.1:8085'");
            }
            if (!IsValidConfig(sentinelConf, _sentinelConf))
            {
                throw new ArgumentException(
                    "sentinelConf string format is wrong. should be like '127.0.0.1:8080,127.0.0.1:8085'");
            }
        }

        private bool IsValidConfig(string str, List<AddressPort> result)
        {
            var rg =
                new Regex(
                    @"(25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[0-9]{1,2})(\.(25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[0-9]{1,2})){3}\:[0-9]{1,6}");
            var m = rg.Matches(str);
            if (m.Count > 0)
            {
                foreach (Match mi in m)
                {
                    var indexOfComma = mi.Value.IndexOf(":", StringComparison.Ordinal);
                    var addr = mi.Value.Substring(0, indexOfComma);
                    var port = mi.Value.Substring(indexOfComma + 1, mi.Value.Length - indexOfComma - 1);
                    result.Add(new AddressPort() {Address = addr, Port = Convert.ToInt32(port)});
                }
                return true;
            }
            return false;
        }


        /// <summary>
        /// 保存单个key value
        /// </summary>
        /// <param name="key">Redis Key</param>
        /// <param name="value">保存的值</param>
        /// <param name="expiry">过期时间</param>
        /// <returns></returns>
        public bool StringSet(string key, string value, TimeSpan? expiry = default(TimeSpan?))
        {
            key = AddSysCustomKey(key);
            return Do(db => db.StringSet(key, value, expiry));
        }

        /// <summary>
        /// 保存多个key value
        /// </summary>
        /// <param name="keyValues">键值对</param>
        /// <returns></returns>
        public bool StringSet(List<KeyValuePair<RedisKey, RedisValue>> keyValues)
        {
            List<KeyValuePair<RedisKey, RedisValue>> newkeyValues =
                keyValues.Select(p => new KeyValuePair<RedisKey, RedisValue>(AddSysCustomKey(p.Key), p.Value)).ToList();
            return Do(db => db.StringSet(newkeyValues.ToArray()));
        }

        /// <summary>
        /// 保存一个对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public bool StringSet<T>(string key, T obj, TimeSpan? expiry = default(TimeSpan?))
        {
            key = AddSysCustomKey(key);
            string json = ConvertJson(obj);
            return Do(db => db.StringSet(key, json, expiry));
        }

        /// <summary>
        /// 获取单个key的值
        /// </summary>
        /// <param name="key">Redis Key</param>
        /// <returns></returns>
        public string StringGet(string key)
        {
            key = AddSysCustomKey(key);
            return Do(db => db.StringGet(key));
        }
     

        /// <summary>
        /// 获取一个key的对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T StringGet<T>(string key)
        {
            key = AddSysCustomKey(key);
            return Do(db => ConvertObj<T>(db.StringGet(key)));
        }

        /// <summary>
        /// 为数字增长val
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val">可以为负</param>
        /// <returns>增长后的值</returns>
        public double StringIncrement(string key, double val = 1)
        {
            key = AddSysCustomKey(key);
            return Do(db => db.StringIncrement(key, val));
        }

        /// <summary>
        /// 为数字减少val
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val">可以为负</param>
        /// <returns>减少后的值</returns>
        public double StringDecrement(string key, double val = 1)
        {
            key = AddSysCustomKey(key);
            return Do(db => db.StringDecrement(key, val));
        }


        /// <summary>
        /// 移除指定ListId的内部List的值
        /// </summary>
        /// <param name="key"></param>
        public void ListRemove<T>(string key)
        {
            key = AddSysCustomKey(key);
            var all = ListGet<T>(key);
            foreach (var single in all)
            {
                var s = single;
                Do(db => db.ListRemove(key, ConvertJson(s)));
            }
        }
        /// <summary>
        /// 入栈
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void ListSet<T>(string key, List<T> value)
        {
            key = AddSysCustomKey(key);
            StringSet(key, ConvertJson(value));
        }
        /// <summary>
        /// 获取指定key的List
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public List<T> ListGet<T>(string key)
        {
            key = AddSysCustomKey(key);
            return ConvetList<T>(StringGet(key));
        }

        public string AddSysCustomKey(string key)
        {
            return SystemKey + key;
        }

        public enum RedisServerStatus
        {
            Stoped,
            Runing,
            ConnectionFailed
        }

        private ConfigurationOptions BuildOptions(List<AddressPort> ap)
        {
            ConfigurationOptions options = new ConfigurationOptions();
            ap.ForEach(r =>
            {
                options.EndPoints.Add(r.Address, r.Port);
            });
            return options;
        }

        public void Start()
        {

            _conn = ConnectionMultiplexer.Connect(BuildOptions(_serverConf));
            var sentineloption = BuildOptions(_sentinelConf);
            sentineloption.TieBreaker = ""; //sentinel模式一定要写
            sentineloption.CommandMap = CommandMap.Sentinel;
            sentineloption.ServiceName = "mymaster";
            _sentinel = ConnectionMultiplexer.Connect(sentineloption);
            _sentinelsub = _sentinel.GetSubscriber();
            _sentinelsub.Subscribe("+switch-master", (channel, message) =>
            {
//                WriteWarn((string)message);
            });
        }

        public struct AddressPort
        {
            public string Address;
            public int Port;
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }

        #region Private Method

        private string ConvertJson<T>(T value)
        {
            string result = value is string ? value.ToString() : JsonConvert.SerializeObject(value);
            return result;
        }

        private T ConvertObj<T>(RedisValue value)
        {
            return JsonConvert.DeserializeObject<T>(value);
        }

        private List<T> ConvetList<T>(string values)
        {
            return ConvertObj<List<T>>(values);
        }
        private List<T> ConvetList<T>(RedisValue[] values)
        {
            List<T> result = new List<T>();
            foreach (var item in values)
            {
                var model = ConvertObj<T>(item);
                result.Add(model);
            }
            return result;
        }



        private T Do<T>(Func<IDatabase, T> func)
        {
            var database = _conn.GetDatabase(1);
            return func(database);
        }
        #endregion
    }

    /// <summary>
    /// 
    /// </summary>
    public static class RedisHelper
    {
        
    }
}
