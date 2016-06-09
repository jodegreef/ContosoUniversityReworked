using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using ContosoUniversity.DAL;
using System.Data.Entity.Infrastructure.Interception;
using ContosoUniversity.Infrastructure;
using Autofac.Integration.Mvc;

namespace ContosoUniversity
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            DbInterception.Add(new SchoolInterceptorTransientErrors());
            DbInterception.Add(new SchoolInterceptorLogging());

            UseFeatureFolders();

            RegisterDependencies();
        }

        private void UseFeatureFolders()
        {
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new FeatureViewLocationRazorViewEngine());
        }

        private void RegisterDependencies()
        {
            var container = Bootstrap.Create(builder => {
                builder.RegisterControllers(typeof(MvcApplication).Assembly);
            });

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
