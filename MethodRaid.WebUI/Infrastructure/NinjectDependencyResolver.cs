using MethodRaid.WebUI.Interface;
using Ninject;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MethodRaid.WebUI.Infrastructure
{
    public class NinjectDependencyResolver: IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            kernel.Bind<IBookRepository>().To<BookRepository>();

            kernel.Bind<IClientRepository>().To<ClientRepository>();

            kernel.Bind<ISummarydata>().To<Summarydata>();
        }
    }
}