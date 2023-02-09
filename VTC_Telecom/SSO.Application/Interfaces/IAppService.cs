using SSO.Persistence.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSO.Application.Interfaces
{
    public interface IAppService
    {
        List<VTC_Apps> GetApp();
        VTC_Apps GetAppByID(String id);
        VTC_Apps GetAppByAppId(String id);
        String AddApp(VTC_Apps apps);
        String UpdateApp(VTC_Apps apps);
        void DeleteApp(String id);
    }
}
