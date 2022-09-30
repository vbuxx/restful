using Client.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Client.Controllers
{
    public class UserController : Controller
    {
        HttpClient HttpClient;
        string loginAddress, registerAddress;

        public UserController()
        {
            this.loginAddress = "https://localhost:5001/api/user/login";
            this.registerAddress = "https://localhost:5001/api/user/register";
            
            
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Login login)
        {
            HttpClient = new HttpClient
            {
                BaseAddress = new Uri(loginAddress)
            };
            StringContent content = new StringContent(JsonConvert.SerializeObject(login), Encoding.UTF8, "application/json");
            var result = HttpClient.PostAsync(loginAddress, content).Result;
            if (result.IsSuccessStatusCode)
            {
                var data = JsonConvert.DeserializeObject<ResponseClient>(await result.Content.ReadAsStringAsync());
                HttpContext.Session.SetString("Role", data.data.Role);
                return RedirectToAction("Index", "AdminPanel");
            }
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(Register register)
        {
            HttpClient = new HttpClient
            {
                BaseAddress = new Uri(registerAddress)
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(register), Encoding.UTF8, "application/json");
            var result = HttpClient.PostAsync(registerAddress, content).Result;
            if (result.IsSuccessStatusCode)
            {

                return RedirectToAction("Login", "User");
            }
            return View();
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }

       
    }
}
