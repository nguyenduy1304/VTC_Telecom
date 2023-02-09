using Microsoft.IdentityModel.Tokens;
using SSO.Application.Interfaces;
using SSO.Contract.Constant;
using SSO.Contract.Requests;
using SSO.Persistence.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static SSO.Contract.Constant.Hash;

namespace SSO.Application.Services
{
    public class UserService : IUserService
    {
        public void DeleteUser(string id)
        {
            using (Connection connection = new Connection())
            {
                var groupuser = connection.VTC_GroupUser.Where(u => u.UserId == id).ToList();
                if (groupuser != null)
                {
                    foreach (var item in groupuser)
                    {
                        connection.VTC_GroupUser.Remove(item);
                    }
                    connection.SaveChanges();

                }
                var token = connection.VTC_Token.FirstOrDefault(u => u.UserId == id);
                if (token != null)
                {
                    connection.VTC_Token.Remove(token);
                    connection.SaveChanges();
                }
                var user = connection.VTC_Users.SingleOrDefault(u => u.Id == id);
                if (user != null)
                {
                    connection.VTC_Users.Remove(user);
                    connection.SaveChanges();
                }
            }
        }
        public List<VTC_GroupUser> GetGroupUserByUserId(string id)
        {
            using (Connection connection = new Connection())
            {
                var user = connection.VTC_GroupUser.Where(x => x.UserId == id).ToList();
                if (user != null)
                {
                    return user;
                }
                else
                {
                    return null;
                }
            }
        }

        public VTC_Token GetTokenByUser(string id)
        {
            using (Connection connection = new Connection())
            {
                var token = connection.VTC_Token.SingleOrDefault(u => u.UserId == id);

                if (token == null)
                {
                    return null;
                }
                else
                {
                    return token;
                }
            }
        }

        public List<VTC_Users> GetUsers()
        {
            using (Connection connection = new Connection())
            {
                return connection.VTC_Users.ToList();
            }
        }

        public VTC_Users GetUsersById(string id)
        {
            using(Connection connection = new Connection())
            {
                var user = connection.VTC_Users.FirstOrDefault(x => x.Id == id);
                if (user != null)
                {
                    return user;
                }
                else
                {
                    return null;
                }
            }
        }

        public string LogIn(LoginRequest loginRequest)
        {
            using (Connection connection = new Connection())
            {
                var userName = connection.VTC_Users.FirstOrDefault(u => u.UserName == loginRequest.UserName);
                if (userName != null)
                {
                    if (userName.PassWord == Hash.GetHash(loginRequest.PassWord, HashType.MD5))
                    {
                        List<VTC_Roles> role = new List<VTC_Roles>();

                        var groupuser = connection.VTC_GroupUser.Where(gu => gu.UserId == userName.Id).ToList();
                        foreach (VTC_GroupUser item in groupuser)
                        {
                            var grouprole = connection.VTC_GroupRole.Where(gl => gl.GroupId == item.GroupId).ToList();
                            foreach (VTC_GroupRole gr in grouprole)
                            {
                                var roles = connection.VTC_Roles.FirstOrDefault(gl => gl.Id == gr.RoleId);
                                if ((roles.AppId).Contains(loginRequest.AppId))
                                {
                                    VTC_Roles vTC_Roles = new VTC_Roles();
                                    vTC_Roles.Permission = roles.Permission;
                                    role.Add(vTC_Roles);
                                }
                            }
                        }
                        String permission = "";
                        foreach (VTC_Roles item in role)
                        {
                            string addpermission = "," + item.Permission;
                            int i = permission.Length;
                            permission = permission.Insert(i, addpermission);
                        }
                        permission = permission.ToLower();
                        string[] arr = permission.Split(',');
                        HashSet<string> result = new HashSet<string>(arr);
                        string outputpermission = string.Join(",", result);
                        if (outputpermission.StartsWith(","))
                        {
                            outputpermission = outputpermission.Substring(1);
                        }

                        var key = ConfigurationManager.AppSettings["JwtKey"];
                        var issuer = ConfigurationManager.AppSettings["JwtIssuer"];
                        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
                        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                        var permClaims = new List<Claim>();
                        permClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
                        permClaims.Add(new Claim("userid", userName.Id));
                        permClaims.Add(new Claim("username", userName.UserName));
                        permClaims.Add(new Claim("permission", outputpermission));
                        var token = new JwtSecurityToken(issuer,
                                        issuer,
                                        permClaims,
                                        expires: DateTime.Now.AddDays(1),
                                        signingCredentials: credentials);
                        var jwt_token = new JwtSecurityTokenHandler().WriteToken(token);
                        var userToken = connection.VTC_Token.SingleOrDefault(a => a.UserId == userName.Id);
                        var app = connection.VTC_Apps.FirstOrDefault(a => a.AppId == loginRequest.AppId);
                        if (userToken == null && app != null)
                        {
                            VTC_Token vTC_Token = new VTC_Token();
                            vTC_Token.Id = Guid.NewGuid().ToString();
                            vTC_Token.UserId = userName.Id;
                            vTC_Token.Token = jwt_token;
                            vTC_Token.LoginDate = DateTime.Now;
                            vTC_Token.Expired = DateTime.Now.AddHours(24);
                            vTC_Token.AppId = app.Id;
                            connection.VTC_Token.Add(vTC_Token);
                            connection.SaveChanges();
                        }
                        else
                        {
                            userToken.Token = jwt_token;
                            userToken.LoginDate = DateTime.Now;
                            userToken.Expired = DateTime.Now.AddHours(24);
                            connection.SaveChanges();
                        }
                        //byte[] bytes = Encoding.UTF8.GetBytes("B68A4717A0BE21353585823D2532E6AD");
                        //string encryptedText = AESEncryption.Encrypt(jwt_token, bytes);
                        //string decryptedText = AESEncryption.Decrypt(encryptedText, bytes);

                        return jwt_token;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
        }

        public string Register(RegisterRequest registerRequest)
        {
            using (Connection connection = new Connection())
            {
                var userName = connection.VTC_Users.FirstOrDefault(u => u.UserName == registerRequest.UserName);
                if (userName == null)
                {
                    VTC_Users vTC_Users = new VTC_Users();
                    vTC_Users.Id = Guid.NewGuid().ToString();
                    vTC_Users.UserName = registerRequest.UserName;
                    vTC_Users.PassWord = Hash.GetHash(registerRequest.PassWord, HashType.MD5);
                    vTC_Users.FullName = registerRequest.FullName;
                    vTC_Users.Birthday = null;
                    vTC_Users.DateCreated = DateTime.Now;
                    vTC_Users.Email = registerRequest.Email;
                    vTC_Users.Phone = registerRequest.Phone;
                    vTC_Users.Address = null;
                    connection.VTC_Users.Add(vTC_Users);
                    connection.SaveChanges();

                    VTC_GroupUser vTC_GroupUser = new VTC_GroupUser();
                    vTC_GroupUser.Id = Guid.NewGuid().ToString();
                    vTC_GroupUser.UserId = vTC_Users.Id;
                    vTC_GroupUser.GroupId = "09711268-7146-4e96-814c-52ab8795b7a6";
                    connection.VTC_GroupUser.Add(vTC_GroupUser);
                    connection.SaveChanges();
                    return "successful";
                }
                else
                {
                    return "ER_Username";
                }
            }
        }

        public string SignOut(string id)
        {
            throw new NotImplementedException();
        }

        public void UpdateUser(EditUserRequest editUserRequest)
        {
            using (Connection connection = new Connection())
            {
                var user = connection.VTC_Users.FirstOrDefault(u=>u.Id == editUserRequest.Id);
                if(user != null)
                {
                    user.FullName = editUserRequest.FullName;
                    if(editUserRequest.Birthday != null)
                    {
                        user.Birthday = editUserRequest.Birthday;
                    }
                    else
                    {
                        user.Birthday = null;
                    }
                    user.Email = editUserRequest.Email;
                    user.Phone = editUserRequest.Phone;
                    user.Address = editUserRequest.Address;
                }
                var groupuser = connection.VTC_GroupUser.Where(g => g.UserId== editUserRequest.Id).ToList();
                if (groupuser != null)
                {
                    foreach (var gu in groupuser)
                    {
                        connection.VTC_GroupUser.Remove(gu);
                    }
                    connection.SaveChanges();
                }

                var groupid = editUserRequest.GroupId;
                var vTC_Group = connection.VTC_Groups;
                foreach (var item in vTC_Group)
                {
                    if (groupid.Contains(item.Id) == true)
                    {
                        VTC_GroupUser vTC_GroupUser = new VTC_GroupUser();
                        vTC_GroupUser.Id = Guid.NewGuid().ToString();
                        vTC_GroupUser.UserId = editUserRequest.Id;
                        vTC_GroupUser.GroupId = item.Id;
                        connection.VTC_GroupUser.Add(vTC_GroupUser);
                    }
                }
                connection.SaveChanges();
            }
        }
    }
}
