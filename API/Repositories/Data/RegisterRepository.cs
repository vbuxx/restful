using API.Context;
using API.Models;
using API.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repositories.Data
{
    public class RegisterRepository
    {
        MyContext myContext;

        public RegisterRepository(MyContext myContext)
        {
            this.myContext = myContext;
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
                myContext.Users.Add(new User() { Id = emp.Id, Password = register.Password });

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
    }
}
