using SSO.Persistence.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSO.Application.Interfaces
{
    public interface IRoleService
    {
        List<VTC_Roles> GetRole();
        VTC_Roles GetRoleByID(String id);

        List<VTC_GroupRole> GetGroupRoleByGroupId(String id);
        String AddRole(VTC_Roles roles);
        String UpdateRole(VTC_Roles roles);
        void DeleteRole(String id);
    }
}
