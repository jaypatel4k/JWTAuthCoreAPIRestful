using JWTAuthCoreAPIRestful.Data;
using JWTAuthCoreAPIRestful.Interface;
using JWTAuthCoreAPIRestful.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace JWTAuthCoreAPIRestful.Repository
{
    public class UserRepository : IUserRepository
    {
        private JWTAuthCRUDContext _dbContext;
        public UserRepository(JWTAuthCRUDContext dbContext)
        {
            _dbContext = dbContext;
        }

        public UserModel GetUser(string username,string password)
        {
            
            //loginmodel = _dbContext.Users.Where(x => x.Username == username && x.Password == password).First();
            var loginmodel = _dbContext.Users.Where(x => x.Username == username && x.Password == password).FirstOrDefault();
            return loginmodel;
        }
        
    }
}
