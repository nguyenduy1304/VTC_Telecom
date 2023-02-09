using SSO.Contract.Requests;
using SSO.Persistence.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSO.Application.Interfaces
{
    public interface IUserService
    {
        List<VTC_Users> GetUsers();
        VTC_Users GetUsersById(String id);
        List<VTC_GroupUser> GetGroupUserByUserId(String id);
        String LogIn(LoginRequest loginRequest);
        String Register(RegisterRequest registerRequest);
        void UpdateUser(EditUserRequest editUserRequest);
        void DeleteUser(String id);
        VTC_Token GetTokenByUser(String id);
        String SignOut(String id);
    }
}
