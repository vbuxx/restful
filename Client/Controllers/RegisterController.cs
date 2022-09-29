using Client.ViewModels;
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
    public class RegisterController : Controller
    {

        HttpClient HttpClient;
        string address;

        public RegisterController()
        {
            this.address = "https://localhost:5001/api/Register/";
            HttpClient = new HttpClient
            {
                BaseAddress = new Uri(address)
            };
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(Register register)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(register), Encoding.UTF8, "application/json");
            var result = HttpClient.PostAsync(address, content).Result;
            if (result.IsSuccessStatusCode)
            {

                return RedirectToAction("Login", "User");
            }
            return View();
        }
    }
}
