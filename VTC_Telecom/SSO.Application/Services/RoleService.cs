using SSO.Application.Interfaces;
using SSO.Persistence.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSO.Application.Services
{
    public class RoleService : IRoleService
    {
        public string AddRole(VTC_Roles roles)
        {
            using (Connection connection = new Connection())
            {
                roles.Id = Guid.NewGuid().ToString();
                connection.VTC_Roles.Add(roles);
                connection.SaveChanges();
                return "successful";
            }
        }

        public void DeleteRole(string id)
        {
            using (Connection connection = new Connection())
            {
                var grouprole = connection.VTC_GroupRole.Where(g=>g.GroupId == id).ToList();
                foreach (var gouprole in grouprole)
                {
                    connection.VTC_GroupRole.Remove(gouprole);
                }
                connection.SaveChanges();

                var role = connection.VTC_Roles.FirstOrDefault(u => u.Id == id);
                connection.VTC_Roles.Remove(role);
                connection.SaveChanges();
            }
        }

        public List<VTC_GroupRole> GetGroupRoleByGroupId(string id)
        {
            using (Connection connection = new Connection())
            { 
                var groupRole = connection.VTC_GroupRole.Where(u => u.GroupId == id).ToList();
                if(groupRole != null)
                {
                    return groupRole;

                }
                else
                {
                    return null;
                }
            }
        }
        public List<VTC_Roles> GetRole()
        {
            Connection connection = new Connection();
            var roles = connection.VTC_Roles.ToList();
            return roles;

        }

        public VTC_Roles GetRoleByID(string id)
        {
            using (Connection connection = new Connection())
            {
                VTC_Roles vTC_Roles = connection.VTC_Roles.FirstOrDefault(a => a.Id == id);
                return vTC_Roles;
            }
        }

        public string UpdateRole(VTC_Roles roles)
        {
            using (Connection connection = new Connection())
            {
                var updateRole = connection.VTC_Roles.FirstOrDefault(u => u.Id == roles.Id);
                if (updateRole != null)
                {
                    updateRole.Name = roles.Name;
                    updateRole.AppId = roles.AppId;
                    updateRole.Permission = roles.Permission;
                    updateRole.Description = roles.Description;
                    connection.SaveChanges();
                    return "successful";
                }
                else
                {
                    return "error";

                }
            }
        }
    }
}
