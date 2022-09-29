using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.ViewModels
{
    public class ResponseRegisterClient
    {
        public string Message { get; set; }
        public int StatusCode { get; set; }
        public ResponseRegisterClient data { get; set; }
    }
}
