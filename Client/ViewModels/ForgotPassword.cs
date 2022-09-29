using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.ViewModels
{
    public class ForgotPassword
    {
        public string Email { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
    }
}
