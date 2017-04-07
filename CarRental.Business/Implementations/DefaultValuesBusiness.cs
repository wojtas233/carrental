using CarRental.Business.Interfaces;
using CarRental.DataAccess;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Business.Implementations
{
    public class DefaultValuesBusiness : IDefaultValuesBusiness
    {
        private readonly IApplicationDbContext _dbContext;
        public DefaultValuesBusiness(IApplicationDbContext db)
        {
            _dbContext = db;
        }

        public void AddDefaultRoles()
        {
            if (!_dbContext.Roles.Any())
            {
                var adminRole = new IdentityRole
                {
                    Name = "Admin"
                };
                _dbContext.Roles.Add(adminRole);

                var userRole = new IdentityRole
                {
                    Name = "User"
                };
                _dbContext.Roles.Add(userRole);

                _dbContext.SaveChanges();
            }
        }
    }
}
