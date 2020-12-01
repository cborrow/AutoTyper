using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace AutoTyper
{
    public class AutoTypeEntry
    {
        string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        string encryptedPassword;
        public string EncryptedPassword
        {
            get { return encryptedPassword; }
            set { encryptedPassword = value; }
        }

        public AutoTypeEntry()
        {

        }

        public AutoTypeEntry(string name, string password)
        {
            this.name = name;
            this.encryptedPassword = Encrypt(password);
        }

        public string Encrypt(string str)
        {
            try
            {
                string encPwd = Convert.ToBase64String(ProtectedData.Protect(
                    Encoding.Unicode.GetBytes(str), null, DataProtectionScope.CurrentUser));
                return encPwd;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to encrypt str : " + ex.Message);
            }
            //TODO : Decide if we should use unprotected password, notify user, give user option to not encrypt passwords?
            return string.Empty;
        }

        public string Decrypt(string encryptedStr)
        {
            try
            {
                string pwd = Encoding.Unicode.GetString(
                    ProtectedData.Unprotect(Convert.FromBase64String(encryptedStr), null, DataProtectionScope.CurrentUser));
                return pwd;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to decrypt str : " + ex.Message);
            }
            return string.Empty;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
