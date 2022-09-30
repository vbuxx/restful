using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Context;
using API.Models;
using API.Repositories.Data;
using API.ViewModels;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BarangController : ControllerBase
    {
        BarangRepository barangRepository;

        public BarangController(BarangRepository barangRepository)
        {
            this.barangRepository = barangRepository;
        }

        // GET: api/Barangs
        [HttpGet]
        public IActionResult Get()
        {
            var data = barangRepository.Get();
            if (data != null)
            {
                return Ok(new { message = "Sukses Get", statusCode = 200, data = data });
            }
            else
            {
                return Ok(new { message = "Sukses Get", statusCode = 200, data = "null" });
            }
        }

        // GET: api/Barangs/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var data = barangRepository.Get(id);

            if(data != null)
            {
                return Ok(new { message = "Sukses GetById", statusCode = 200, data = data });
            }
            else
            {
                return Ok(new { message = "Data tidak ditemukan", statusCode = 200, data = "null" });
            }
        }

        [HttpPost]
        public IActionResult Post(BarangVM barang)
        {
            var result = barangRepository.Post(barang);
            if (result > 0)
            {
                return Ok(new { message = "Sukses tambah data", statusCode = 200 });
            }
            else
            {
                return BadRequest(new { message = "Gagal tambah data", statusCode = 400 });
            }

        }

        [HttpPost("pengadaan")]
        public IActionResult Pengadaan(PengadaanVM pengadaan)
        {
            var result = barangRepository.Pengadaan(pengadaan);
            if (result > 0)
            {
                return Ok(new { message = "Sukses tambah pengadaan baru, Tabel Barang & Rowayat Pengadaan telah diperbarui", statusCode = 200});
            }
            else
            {
                return BadRequest(new { message = "Gagal tambah pengadaan baru", statusCode = 400 });
            }

        }

        [HttpPost("peminjaman")]
        public IActionResult Peminjamn(PeminjamanVM peminjaman)
        {
            var result = barangRepository.Peminjaman(peminjaman);
            if (result > 0)
            {
                return Ok(new { message = "Sukses pinjam barang, Tabel Barang & Rowayat Peminjaman telah diperbarui", statusCode = 200 });
            }
            else
            {
                return BadRequest(new { message = "Gagal tambah pinjam barang", statusCode = 400});
            }

        }

        [HttpPost("pengembalian")]
        public IActionResult Pengembalian(PengembalianVM pengembalian)
        {
            var result = barangRepository.Pengembalian(pengembalian);
            if (result > 0)
            {
                return Ok(new { message = "Sukses kembalikan barang, Tabel Barang & Rowayat Peminjaman telah diperbarui", statusCode = 200});
            }
            else
            {
                return BadRequest(new { message = "Gagal tambah pinjam barang", statusCode = 400});
            }

        }
     


    }
}
