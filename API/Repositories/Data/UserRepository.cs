﻿using API.Context;
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
            else if (data != null && !ValidatePassword(login.Password, data.User.Password))
                return null;

            return new ResponseLogin
            {
                Id = data.UserId,
                Email = data.User.Employee.Email,
                FullName = data.User.Employee.FullName,
                Role = data.Role.Name

            };
        }


        public ResponseRegister Register(Register register)
        {
            Employee employee = new Employee()
            {
                FullName = register.FullName,
                Email = register.Email
            };
            myContext.Employees.Add(employee);

            if (myContext.SaveChanges() > 0)
            {
                var emp = myContext.Employees.Where(x => x.Email == register.Email).FirstOrDefault();
                myContext.Users.Add(new User() { Id = emp.Id, Password = HashPassword(register.Password) });

                if (myContext.SaveChanges() > 0)
                {

                    myContext.UserRoles.Add(new UserRole() { UserId = emp.Id, RoleId = register.RoleId });
                    if (myContext.SaveChanges() > 0)
                    {
                        return new ResponseRegister
                        {
                            Id = emp.Id,
                            Email = emp.Email,
                            FullName = emp.FullName,
                            RoleId = register.RoleId

                        };
                    }
                }
            }
            return null;
        }



        private static string GetRandomSalt()
        {
            return BCrypt.Net.BCrypt.GenerateSalt(12);
        }

        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, GetRandomSalt());
        }

        public static bool ValidatePassword(string password, string correctHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, correctHash);
        }
    }
}

