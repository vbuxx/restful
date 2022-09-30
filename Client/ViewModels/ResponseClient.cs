using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.ViewModels
{
    public class ResponseClient
    {
        public string Message { get; set; }
        public int StatusCode { get; set; }
        public ResponseLoginClient data { get; set; }
    }
}
