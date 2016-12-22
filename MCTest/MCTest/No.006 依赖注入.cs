using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Core.Internal;
using Castle.DynamicProxy;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;

namespace MCTest
{
    public class 依赖注入: IOutput
    {
       
        public IFly bd { get;set; }
        public void Main()
        {
            IWindsorContainer container = new WindsorContainer();
            container.Install(new FlyInstaller());
//            var factory = new WindsorControllerFactory(null);
//            var handlers = factory.GetHandlersFor(typeof(IFly), container);
//            var abc = factory.GetPublicClasssFromApplicationAssembly(r => r.Is<IFly>());
            ProxyGenerator genrator = new ProxyGenerator();
//            MyFly fly = genrator.CreateClassProxy<MyFly>();
//            IMyfly fly = container.Resolve<IMyfly>();
//            fly.Fly();
            Console.WriteLine("Public Class");
        }


        //利用反射动态注册
        public void RegistDynamic()
        {
            
        }
    }

    public class MyFly : IMyfly
    {
        public IBirdfly BirdFly { get; set; }
        public MyFly()
        {
        }
        public void Fly()
        {
            BirdFly.IcanFly();
        }
    }

    public interface IMyfly
    {
        void Fly();
    }


    public interface IFly
    {
        void Fly();
    }

    public class BirdFly :IBirdfly, IFly
    {
        public void Fly()
        {
            Console.WriteLine("Bird Fly");
        }

        public void IcanFly()
        {
            Console.WriteLine("I Can Fly");
        }
    }

    public interface IBirdfly
    {
        void IcanFly();
    }

    public class ChrikenFly : IFly
    {
        public void Fly()
        {
            Console.WriteLine("Chriken Can't Fly");
        }
    }
}
