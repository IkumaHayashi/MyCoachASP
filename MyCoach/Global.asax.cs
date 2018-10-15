using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Data.Entity;
using MyCoach.Models;

namespace MyCoach
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //Database.SetInitializer(
            //    new MigrateDatabaseToLatestVersion<TrainingModels, Configuration>()
            //    );

            //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<MyCoach.Models.TrainingModels>());
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MyCoachDatabaseContext, Configuration>());
            Database.SetInitializer(new MyCoachDatabaseInitializer());
        }
    }
}
