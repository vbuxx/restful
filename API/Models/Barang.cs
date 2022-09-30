using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Barang
    {
        [Key]
        public int Id { get; set; }

        public string Nama { get; set; }
        public string Satuan { get; set; }
        public int Stock { get; set; }
    }
}
