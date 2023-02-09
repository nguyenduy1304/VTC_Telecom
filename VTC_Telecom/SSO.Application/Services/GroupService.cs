using SSO.Application.Interfaces;
using SSO.Contract.Requests;
using SSO.Persistence.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSO.Application.Services
{
    public class GroupService : IGroupService
    {
        public string AddGroup(AddGroupRequest addGroupRequest)
        {
            using(Connection connection = new Connection())
            {
                VTC_Groups vTC_Groups = new VTC_Groups();
                vTC_Groups.Id = Guid.NewGuid().ToString();
                vTC_Groups.Name = addGroupRequest.Name;
                vTC_Groups.Description = addGroupRequest.Description;
                connection.VTC_Groups.Add(vTC_Groups);
                connection.SaveChanges();

                var roleid = addGroupRequest.RoleId;
                var vTC_Roles = connection.VTC_Roles;
                foreach(var item in vTC_Roles)
                {
                    if (roleid.Contains(item.Id) == true)
                    {
                        VTC_GroupRole vTC_GroupRole = new VTC_GroupRole();
                        vTC_GroupRole.Id = Guid.NewGuid().ToString();
                        vTC_GroupRole.GroupId = vTC_Groups.Id;
                        vTC_GroupRole.RoleId = item.Id;
                        connection.VTC_GroupRole.Add(vTC_GroupRole);
                    }
                }
                connection.SaveChanges();
                return "successful";
            }
        }

        public void DeleteGroup(string id)
        {
            using (Connection connection = new Connection())
            {
                var grouprole = connection.VTC_GroupRole.Where(g => g.GroupId == id).ToList();
                foreach (var gouprole in grouprole)
                {
                    connection.VTC_GroupRole.Remove(gouprole);
                }
                var groupuser = connection.VTC_GroupUser.Where(g => g.GroupId == id).ToList();
                foreach (var gu in groupuser)
                {
                    connection.VTC_GroupUser.Remove(gu);
                }
                connection.SaveChanges();
                var group = connection.VTC_Groups.FirstOrDefault(u => u.Id == id);
                connection.VTC_Groups.Remove(group);
                connection.SaveChanges();
            }
        }

        public List<VTC_Groups> GetGroup()
        {
            using (Connection connection = new Connection())
            {
                var group = connection.VTC_Groups.ToList();
                return group;
            }
        }

        public VTC_Groups GetGroupByID(string id)
        {
            using (Connection connection = new Connection())
            {
                VTC_Groups vTC_Groups = connection.VTC_Groups.FirstOrDefault(a => a.Id == id);
                return vTC_Groups;
            }
        }

        public string UpdateGroup(UpdateGroupRequest updateGroupRequest)
        {
            using (Connection connection = new Connection())
            {
                var updategoup = connection.VTC_Groups.FirstOrDefault(u => u.Id == updateGroupRequest.Id);
                if (updategoup != null)
                {
                    updategoup.Name = updateGroupRequest.Name;
                    updategoup.Description = updateGroupRequest.Description;
                    connection.SaveChanges();

                    var rolegroup = connection.VTC_GroupRole.Where(g => g.GroupId == updateGroupRequest.Id).ToList();
                    if(rolegroup != null)
                    {
                        foreach(var role in rolegroup)
                        {
                            connection.VTC_GroupRole.Remove(role);
                        }
                        connection.SaveChanges();
                    }

                    var roleid = updateGroupRequest.RoleId;
                    var vTC_Roles = connection.VTC_Roles;
                    foreach (var item in vTC_Roles)
                    {
                        if (roleid.Contains(item.Id) == true)
                        {
                            VTC_GroupRole vTC_GroupRole = new VTC_GroupRole();
                            vTC_GroupRole.Id = Guid.NewGuid().ToString();
                            vTC_GroupRole.GroupId = updateGroupRequest.Id;
                            vTC_GroupRole.RoleId = item.Id;
                            connection.VTC_GroupRole.Add(vTC_GroupRole);
                        }
                    }
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
