using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac;
using Nop.Core.Configuration;
using Nop.Core.Infrastructure;
using Nop.Core.Infrastructure.DependencyManagement;

namespace Nop.Web.Themes.PAA.Infrastructure
{
    public class DependencyRegister : IDependencyRegistrar
    {
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, NopConfig config)
        {
            throw new NotImplementedException();
        }

        public int Order => 3;
    }
}