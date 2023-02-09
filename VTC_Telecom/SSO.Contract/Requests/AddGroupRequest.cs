using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSO.Contract.Requests
{
    public class AddGroupRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string RoleId { get; set; }
    }
}
