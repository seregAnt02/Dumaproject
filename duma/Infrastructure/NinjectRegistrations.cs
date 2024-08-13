using System;
using duma.Models;
using Ninject.Modules;

namespace duma.Infrastructure
{
    public class NinjectRegistrations: NinjectModule
    {
        public override void Load()
        {
            Bind<IRespository>().To<MySqlRepository>().WithPropertyValue("Context", new DumContext());
        }
    }
}