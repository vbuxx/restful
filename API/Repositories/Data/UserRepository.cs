using API.Context;
using API.Models;
using API.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repositories.Data
{
    public class UserRepository
    {
        MyContext myContext;

        public UserRepository(MyContext myContext)
        {
            this.myContext = myContext;
        }

        public ResponseLogin Login(Login login)
        {
            var data = myContext.UserRoles.
               Include(x => x.User.Employee).
               Include(x => x.User).
               Include(x => x.Role).
               Where(x => x.User.Employee.Email == login.Email).FirstOrDefault();

            if (data == null)
                return null;
            else if (data != null && !login.Password.Equals(data.User.Password))
                return null;

            return new ResponseLogin
            {
                Id = data.UserId,
                Email = data.User.Employee.Email,
                FullName = data.User.Employee.FullName,
                Role = data.Role.Name

            };
        }

        



            //if (data == null)
            //    return null;
            //else if (data != null && !login.Password.Equals(data.User.Password))
            //    return null;


    }
}

