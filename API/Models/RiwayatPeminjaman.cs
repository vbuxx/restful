using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class RiwayatPeminjaman
    {
        [Key]
        public int Id { get; set; }
        public Barang Barang { get; set; }

        [ForeignKey("Barang")]
        public int BarangId { get; set; }
        public Employee Employee { get; set; }

        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        public string Tgl { get; set; }
        public int Qty { get; set; }
        public string Status { get; set; }

    }
}
