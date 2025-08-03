using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Configuration; // Thêm namespace này

namespace QLCHBanGaRan.lib
{
    public class cls_AESEncryption
    {
        // Đọc key và IV từ App.config
        private static readonly string aesKey = ConfigurationManager.AppSettings["AES_Key"];
        private static readonly string aesIV = ConfigurationManager.AppSettings["AES_IV"];

        public static string Encrypt(string plainText)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(aesKey.PadRight(32).Substring(0, 32)); // AES-256
                aesAlg.IV = Encoding.UTF8.GetBytes(aesIV.PadRight(16).Substring(0, 16));    // 16 bytes

                aesAlg.Mode = CipherMode.CBC;
                aesAlg.Padding = PaddingMode.PKCS7;

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                using (MemoryStream msEncrypt = new MemoryStream())
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                {
                    swEncrypt.Write(plainText);
                    swEncrypt.Flush();
                    csEncrypt.FlushFinalBlock();
                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }

        public static string Decrypt(string cipherText)
        {
            try
            {
                using (Aes aesAlg = Aes.Create())
                {
                    aesAlg.Key = Encoding.UTF8.GetBytes(aesKey.PadRight(32).Substring(0, 32));
                    aesAlg.IV = Encoding.UTF8.GetBytes(aesIV.PadRight(16).Substring(0, 16));

                    aesAlg.Mode = CipherMode.CBC;
                    aesAlg.Padding = PaddingMode.PKCS7;

                    ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                    byte[] buffer = Convert.FromBase64String(cipherText);

                    using (MemoryStream msDecrypt = new MemoryStream(buffer))
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {
                        return srDecrypt.ReadToEnd();
                    }
                }
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
