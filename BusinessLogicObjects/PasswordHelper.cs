using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace CAPRES.BusinessLogicObjects
{
    public class PasswordHelper
    {
        public string GenerateRandomPassword()
        {
            try
            {
                char[] chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
                byte[] data = new byte[1];
                using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
                {
                    crypto.GetNonZeroBytes(data);
                    data = new byte[10];
                    crypto.GetNonZeroBytes(data);
                }
                StringBuilder result = new StringBuilder(10);
                foreach (byte b in data)
                {
                    result.Append(chars[b % (chars.Length)]);
                }
                return result.ToString();
            }

            catch (Exception e)
            {
                Debug.WriteLine("Generate Random Password Exception Type: " + e.GetType() +
                    "\nMessage: " + e.Message +
                    "\nStack Trace: " + e.StackTrace);
                return null;
            }
        }

        public string EncryptPassword(string plainPassword)
        {
            try
            {
                string encryptionKey = "ONAEJ0907TRAMS";
                byte[] clearBytes = Encoding.Unicode.GetBytes(plainPassword);
                using (Aes encryptor = Aes.Create())
                {
                    Rfc2898DeriveBytes rfcdb = new Rfc2898DeriveBytes(encryptionKey,
                        new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65,
                        0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                    encryptor.Key = rfcdb.GetBytes(32);
                    encryptor.IV = rfcdb.GetBytes(16);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(),
                            CryptoStreamMode.Write))
                        {
                            cs.Write(clearBytes, 0, clearBytes.Length);
                            cs.Close();
                        }
                        return Convert.ToBase64String(ms.ToArray());
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Encrypt Password Exception Type: " + e.GetType() +
                    "\nMessage: " + e.Message +
                    "\nStack Trace: " + e.StackTrace);
                return null;
            }
        }

        public string DecryptPassword(string encryptedPassword)
        {
            try
            {
                string encryptionKey = "ONAEJ0907TRAMS";
                byte[] cipherBytes = Convert.FromBase64String(encryptedPassword);
                using (Aes encryptor = Aes.Create())
                {
                    Rfc2898DeriveBytes rfcdb = new Rfc2898DeriveBytes(encryptionKey,
                        new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65,
                        0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                    encryptor.Key = rfcdb.GetBytes(32);
                    encryptor.IV = rfcdb.GetBytes(16);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(),
                            CryptoStreamMode.Write))
                        {
                            cs.Write(cipherBytes, 0, cipherBytes.Length);
                            cs.Close();
                        }
                        return Encoding.Unicode.GetString(ms.ToArray());
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Decrypt Password Exception Type: " + e.GetType() +
                    "\nMessage: " + e.Message +
                    "\nStack Trace: " + e.StackTrace);
                return null;
            }
        }
    }
}