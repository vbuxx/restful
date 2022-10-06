using Client.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Client.Controllers
{
    public class BarangController : Controller
    {
        HttpClient HttpClient;
        string pengadaanAddress;

        public BarangController()
        {
            this.pengadaanAddress = "https://localhost:5001/api/Barang/pengadaan";


        }

        public IActionResult Index(Pengadaan pengadaan)
        {
            return View();
        }
    }
}
