using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSO.Contract.Requests
{
    public class RegisterRequest
    {
        public String UserName { get; set; }
        public String PassWord { get; set; }
        public String FullName { get; set; }
        public String Email { get; set; }
        public String Phone { get; set; }
    }
}
