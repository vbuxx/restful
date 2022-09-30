using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.ViewModels
{
    public class PeminjamanVM
    {
        public int PetugasId { get; set; }
        public int BarangId { get; set; }
        public int EmployeeId { get; set; }
        public string Tgl { get; set; }
        public int Qty { get; set; }
    }
}
