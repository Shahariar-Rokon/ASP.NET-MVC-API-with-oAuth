using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestAPIV5N1.Models;

namespace TestAPIV5N1.Repos
{
    public class UserRepo : IDisposable
    {
        AppDbContext db = new AppDbContext();
        public User ValidateUser(string username, string password)
        {
            return db.Users.FirstOrDefault(x => x.UserName.Equals(username, StringComparison.OrdinalIgnoreCase) && x.Password == password);
        }
        public void Dispose()
        {
            db.Dispose();
        }
    }
}