using Autofac;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pachyderm.UI.Autofac
{
    public class MainModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterAssemblyTypes(typeof(MainModule).Assembly)
                .Where(x => x.Name.EndsWith("Service"))
                .AsImplementedInterfaces()
                .AsSelf()
                .SingleInstance();
        }
    }
}
