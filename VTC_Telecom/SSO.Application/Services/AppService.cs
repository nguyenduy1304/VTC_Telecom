using SSO.Application.Interfaces;
using SSO.Persistence.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SSO.Application.Services
{
    public class AppService : IAppService
    {
        public string AddApp(VTC_Apps apps)
        {
            using (Connection connection = new Connection())
            {
                var key = new byte[16];
                using (var rng = new RNGCryptoServiceProvider())
                {
                    rng.GetBytes(key);
                }
                var secretKey = BitConverter.ToString(key).Replace("-", string.Empty);


                var checkapp = connection.VTC_Apps.FirstOrDefault(a=>a.AppId == apps.AppId);
                if (checkapp == null)
                {
                    apps.Id = Guid.NewGuid().ToString();
                    apps.SecretKey = secretKey;
                    apps.AppId = apps.AppId.ToUpper();
                    connection.VTC_Apps.Add(apps);
                    connection.SaveChanges();
                    return "successful";
                }
                else
                {
                    return "ER_AppId";
                }

            }
        }

        public void DeleteApp(string id)
        {
            using (Connection connection = new Connection())
            {
                var app = connection.VTC_Apps.FirstOrDefault(u => u.Id == id);
                connection.VTC_Apps.Remove(app);
                connection.SaveChanges();
            }
        }

        public List<VTC_Apps> GetApp()
        {
            using (Connection connection = new Connection())
            {
                var vTC_Apps = connection.VTC_Apps.ToList();
                return vTC_Apps;
            }
        }

        public VTC_Apps GetAppByAppId(string id)
        {
            using (Connection connection = new Connection())
            {
                VTC_Apps vTC_Apps = connection.VTC_Apps.FirstOrDefault(a => a.AppId == id);
                return vTC_Apps;
            }
        }

        public VTC_Apps GetAppByID(string id)
        {
            using (Connection connection = new Connection())
            {
                VTC_Apps vTC_Apps = connection.VTC_Apps.FirstOrDefault(a => a.Id == id);
                return vTC_Apps;
            }
        }

        public String UpdateApp(VTC_Apps apps)
        {
            using (Connection connection = new Connection())
            {
                var updateApp = connection.VTC_Apps.FirstOrDefault(u => u.Id == apps.Id);
                if(updateApp != null)
                {
                    updateApp.Name = apps.Name;
                    updateApp.AppId = apps.AppId;
                    updateApp.Domain = apps.Domain;
                    updateApp.SecretKey = apps.SecretKey;
                    updateApp.Note = apps.Note;
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
