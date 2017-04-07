using Autofac;
using Autofac.Integration.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using CarRental.Business;
using CarRental.Business.Implementations;
using CarRental.Business.Interfaces;
using CarRental.DataAccess;

namespace CarRental.WebApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            this.RegisterAutofac();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void RegisterAutofac()
        {
            var builder = new ContainerBuilder();

            // Register your MVC controllers.
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            // Register types
            builder.RegisterType<ApplicationDbContext>().As<IApplicationDbContext>().SingleInstance();
            builder.RegisterType<UserBusiness>().As<IUserBusiness>().SingleInstance();
            builder.RegisterType<ReservationBusiness>().As<IReservationBusiness>().SingleInstance();
            builder.RegisterType<SearchBusiness>().As<ISearchBusiness>().SingleInstance();
            builder.RegisterType<AvailabilityBusiness>().As<IAvailabilityBusiness>().SingleInstance();
            builder.RegisterType<VehicleBusiness>().As<IVehicleBusiness>().SingleInstance();
            builder.RegisterType<VehicleImageBusiness>().As<IVehicleImageBusiness>().SingleInstance();
            builder.RegisterType<VehicleTypeBusiness>().As<IVehicleTypeBusiness>().SingleInstance();
            builder.RegisterType<LocationBusiness>().As<ILocationBusiness>().SingleInstance();
            builder.RegisterType<SearchBusiness>().As<ISearchBusiness>().SingleInstance();
            builder.RegisterType<EquipmentBusiness>().As<IEquipmentBusiness>().SingleInstance();
            builder.RegisterType<ReservationEquipmentBusiness>().As<IReservationEquipmentBusiness>().SingleInstance();
            builder.RegisterType<DefaultValuesBusiness>().As<IDefaultValuesBusiness>().SingleInstance();

            // OPTIONAL: Register model binders that require DI.
            builder.RegisterModelBinders(Assembly.GetExecutingAssembly());
            builder.RegisterModelBinderProvider();

            // OPTIONAL: Register web abstractions like HttpContextBase.
            builder.RegisterModule<AutofacWebTypesModule>();

            // OPTIONAL: Enable property injection in view pages.
            builder.RegisterSource(new ViewRegistrationSource());

            // OPTIONAL: Enable property injection into action filters.
            builder.RegisterFilterProvider();

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            //HttpConfiguration config = GlobalConfiguration.Configuration;
            //config.DependencyResolver = new AutoFacContainer(new AutofacDependencyResolver(container));
        }
    }
}
