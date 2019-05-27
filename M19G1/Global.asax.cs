﻿using Autofac;
using Autofac.Extras.DynamicProxy;
using Autofac.Integration.Mvc;
using M19G1.Common.Logging;
using M19G1.DAL;
using System;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace M19G1
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<UnitOfWork>().InstancePerRequest();

            builder.Register(c => new Interceptor()).InstancePerRequest();

            builder.RegisterAssemblyTypes(Assembly.Load("M19G1.BLL"))
                .Where(t => t.Name.EndsWith("Service", StringComparison.InvariantCultureIgnoreCase))
                .AsImplementedInterfaces().EnableInterfaceInterceptors().InterceptedBy(typeof(Interceptor)).InstancePerRequest();

            builder.RegisterControllers(typeof(MvcApplication).Assembly).InstancePerRequest();

            builder.RegisterModelBinderProvider();

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
