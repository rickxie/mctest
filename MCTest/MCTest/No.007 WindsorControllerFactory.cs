using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace MCTest
{
    public class WindsorControllerFactory 
    {
        private readonly IKernel kernel;
        public WindsorControllerFactory(IKernel kernel)
        {
            this.kernel = kernel;
        }
        public Type[] GetPublicClasssFromApplicationAssembly(Predicate<Type> where)
        {
            return typeof(BirdFly).Assembly.GetExportedTypes()
                .Where(t => t.IsClass)
                .Where(r => !r.IsAbstract)
                .Where(where.Invoke).OrderBy(t => t.Name).ToArray();
        }

        public IHandler[] GetHandlersFor(Type type, IWindsorContainer container)
        {
            return container.Kernel.GetAssignableHandlers(type);
        } 
    }

    public class FlyInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
//            container.Register(Classes.FromThisAssembly().BasedOn<IFly>().LifestyleTransient());
//            container.Register(Classes.FromThisAssembly().BasedOn<IBirdfly>().LifestyleTransient());
            container.Register(Component.For<IMyfly>().ImplementedBy<MyFly>().LifestyleTransient());
            container.Register(Component.For<IBirdfly>().ImplementedBy<BirdFly>().LifestyleTransient());
        }

       
    }
}
