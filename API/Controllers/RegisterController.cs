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
    public class RegisterController : ControllerBase
    {
        RegisterRepository registerRepository;
        public RegisterController(RegisterRepository registerRepository)
        {
            this.registerRepository = registerRepository;
        }

        [HttpPost]
        public IActionResult Register(Register register)
        {
            var data = registerRepository.Register(register);

            if (data == null)
            {
                return BadRequest(new { message = "Gagal Register!", StatusCode = 400 });
            }
            return Ok(new { message = "Berhasil registrasi!", StatusCode = 200, data = data });
        }
    }
}
