using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Castle.Facilities.Startable;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;

namespace MCTest
{
    public class Program
    {
       

        static void Main(string[] args)
        {


            IOutput obj = null;
            const string currAss = "MCTest";
            obj = 工厂.通过反射获取实例(currAss + ".显式隐式转换");
            obj = 工厂.通过反射获取实例(currAss + ".特性");
            obj = 工厂.通过反射获取实例(currAss + ".Json和对象相互转换");
            obj = 工厂.通过反射获取实例(currAss + ".依赖注入");
            obj = 工厂.通过反射获取实例(currAss + ".十六进制表示");
            obj = 工厂.通过反射获取实例(currAss + ".AnyscAwait");
            obj = 工厂.通过反射获取实例(currAss + ".FuncTest");
            obj = 工厂.通过反射获取实例(currAss + ".ThreadTest");
            obj = 工厂.通过反射获取实例(currAss + ".IisSiteReader");
            obj = 工厂.通过反射获取实例(currAss + ".Workplace");
            obj = 工厂.通过反射获取实例(currAss + ".CorssDomain");
            obj = 工厂.通过反射获取实例(currAss + ".Clone");
            obj = 工厂.通过反射获取实例(currAss + ".BasicAuthTest");
            obj = 工厂.通过反射获取实例(currAss + ".RegexExcract");

            obj.Main();
            Console.Read();
        }

}

 
}
