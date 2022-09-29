using API.Context;
using API.Models;
using API.Repositories.Data;
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
    public class DivisionController : ControllerBase
    {

        DivisionRepository divisionRepository;

        public DivisionController(DivisionRepository divisionRepository)
        {
            this.divisionRepository = divisionRepository;
        }

        //READ
        [HttpGet]
        public IActionResult Get()
        {

            var data = divisionRepository.Get();
           
            if (data != null)
            {
                return Ok(new { message = "Sukses", statusCode = 200, data = data });
            }
            else
            {
                return Ok(new { message = "Sukses", statusCode = 200, data = "null" });
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var data = divisionRepository.Get(id);

            if (data != null)
            {
                return Ok(new { message = "Sukses", statusCode = 200, data = data });
            }
            else
            {
                return Ok(new { message = "Sukses", statusCode = 200, data = "null" });
            }

        }


        //CREATE
        [HttpPost]
        public IActionResult Post(Division division)
        {

            var result = divisionRepository.Post(division);
            if ( result > 0)
            {
                return Ok(new { message = "Sukses tambah data", statusCode = 200 });
            }
            else
            {
                return BadRequest(new { message = "Gagal tambah data", statusCode = 400 });
            }



        }

        //UPDATE
        [HttpPut]
        public IActionResult Put(Division division)
        {
            var result = divisionRepository.Put(division);
            if (result > 0)
            {
                return Ok(new { message = "Sukses ubah data", statusCode = 200 });
            }
            else
            {
                return BadRequest(new { message = "Gagal ubah data", statusCode = 400 });
            }

        }


        //DELETE
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = divisionRepository.Delete(id);
            if (result > 0)
            {
                return Ok(new { message = "Sukses hapus data", statusCode = 200 });
            }
            else
            {
                return BadRequest(new { message = "Gagal hapus data", statusCode = 400 });
            }

        }

    }
}
