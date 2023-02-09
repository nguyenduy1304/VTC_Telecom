using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SSO.Contract.Constant
{
    public class RSAEncryption
    {
        public static string Encrypt(string data, RSAParameters RSAKey, bool DoOAEPPadding)
        {
            byte[] encryptedData;
            using (var RSA = new RSACryptoServiceProvider(2048))
            {
                RSA.ImportParameters(RSAKey);
                encryptedData = RSA.Encrypt(Encoding.UTF8.GetBytes(data), DoOAEPPadding);
            }
            return Convert.ToBase64String(encryptedData);
        }

        public static string Decrypt(string data, RSAParameters RSAKey, bool DoOAEPPadding)
        {
            string decryptedData;
            using (var RSA = new RSACryptoServiceProvider(2048))
            {
                RSA.ImportParameters(RSAKey);
                decryptedData = Encoding.UTF8.GetString(RSA.Decrypt(Convert.FromBase64String(data), DoOAEPPadding));
            }
            return decryptedData;
        }
    }
}
