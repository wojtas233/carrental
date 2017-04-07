using CarRental.Business.Implementations;
using CarRental.Business.Interfaces;
using CarRental.DataAccess;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CarRental.WebApp.Startup))]
namespace CarRental.WebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            var dbContext = new ApplicationDbContext();
            var defaultValuesBusiness = new DefaultValuesBusiness(dbContext);
            defaultValuesBusiness.AddDefaultRoles();
        }
    }
}
