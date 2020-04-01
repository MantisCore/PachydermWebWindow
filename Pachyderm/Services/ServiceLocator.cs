using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Autofac;

namespace Pachyderm.Services
{
    public sealed class ServiceLocator
    {
        private static readonly Lazy<ServiceLocator> _lazy = new Lazy<ServiceLocator>(() => new ServiceLocator());
        private IContainer _container;

        private ServiceLocator()
        {
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyModules(typeof(ServiceLocator).Assembly);
            _container = builder.Build();
        }

        public static ServiceLocator Current
        {
            get
            {
                return _lazy.Value;
            }
        }

        public T GetService<T>()
        {
            return _container.Resolve<T>();
        }
    }
}
