using API.Repositories.Data;
using API.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        UserRepository userRepository;
        public UserController(UserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        [HttpPost("login")]
        public IActionResult Login(Login login)
        {
            var data = userRepository.Login(login);

            if (data == null)
            {
                return BadRequest(new { message = "Gagal login! Email atau password salah", StatusCode = 400 });
            }
            return Ok(new { message = "Berhasil login!", StatusCode = 200, data = data });
        }

        [HttpPost("register")]
        public IActionResult Register(Register register)
        {
            var data = userRepository.Register(register);

            if (data == null)
            {
                return BadRequest(new { message = "Gagal Register!", StatusCode = 400 });
            }
            return Ok(new { message = "Berhasil registrasi!", StatusCode = 200, data = data });
        }

        //[HttpPost]
        //public IActionResult Register(Register register)
        //{
        //    var data = accountRepository.Register(register);

        //    if (data == null)
        //    {
        //        return BadRequest(new { message = "Gagal Register!", StatusCode = 400 });
        //    }
        //    return Ok(new { message = "Berhasil registrasi!", StatusCode = 200, data = data });
        //}
    }
}
