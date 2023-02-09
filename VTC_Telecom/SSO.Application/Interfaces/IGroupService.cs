using SSO.Contract.Requests;
using SSO.Persistence.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSO.Application.Interfaces
{
    public interface IGroupService
    {
        List<VTC_Groups> GetGroup();
        VTC_Groups GetGroupByID(String id);
        String AddGroup(AddGroupRequest addGroupRequest);
        String UpdateGroup(UpdateGroupRequest updateGroupRequest);
        void DeleteGroup(String id);
    }
}
