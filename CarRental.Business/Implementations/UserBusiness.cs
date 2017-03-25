using CarRental.Business.Interfaces;
using CarRental.Business.Models;
using CarRental.DataAccess;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Business.Implementations
{
    public class UserBusiness : IUserBusiness
    {
        private readonly IApplicationDbContext _dbContext;
        public UserBusiness(IApplicationDbContext db)
        {
            _dbContext = db;
        }
        public List<UserModel> GetAll()
        {
            var model = _dbContext.Users.Select(x => new UserModel
            {
                Id = x.Id,
                Email = x.Email,
                FirstName = x.FirstName,
                LastName = x.LastName,
                IsEnabled = x.IsEnabled
            }).ToList();
            return model;
        }
    }
}
