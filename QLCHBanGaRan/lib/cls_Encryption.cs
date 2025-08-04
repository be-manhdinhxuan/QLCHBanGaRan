using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace QLCHBanGaRan.lib
{
    public static class cls_Encryption
    {
        // ✅ Khóa bí mật và vector khởi tạo (16 ký tự = 128 bit)
        private static readonly string key = "BongChickenKey12"; // Bạn có thể đổi
        private static readonly string iv = "InitVector123456";  // Cũng 16 ký tự

        /// <summary>
        /// Mã hóa chuỗi văn bản bằng AES
        /// </summary>
        public static string Encrypt(string plainText)
        {
            try
            {
                using (Aes aes = Aes.Create())
                {
                    aes.Key = Encoding.UTF8.GetBytes(key);
                    aes.IV = Encoding.UTF8.GetBytes(iv);
                    aes.Padding = PaddingMode.PKCS7;

                    ICryptoTransform encryptor = aes.CreateEncryptor();
                    byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
                    byte[] encryptedBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);

                    return Convert.ToBase64String(encryptedBytes);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Encryption error: " + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Giải mã chuỗi đã được mã hóa AES
        /// </summary>
        public static string Decrypt(string encryptedText)
        {
            try
            {
                using (Aes aes = Aes.Create())
                {
                    aes.Key = Encoding.UTF8.GetBytes(key);
                    aes.IV = Encoding.UTF8.GetBytes(iv);
                    aes.Padding = PaddingMode.PKCS7;

                    ICryptoTransform decryptor = aes.CreateDecryptor();
                    byte[] encryptedBytes = Convert.FromBase64String(encryptedText);
                    byte[] decryptedBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);

                    return Encoding.UTF8.GetString(decryptedBytes);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Decryption error: " + ex.Message);
                return null;
            }
        }
    }
}
