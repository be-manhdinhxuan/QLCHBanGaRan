using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace QLCHBanGaRan.lib
{
    public class cls_EnCrypt
    {
        private static string key = "Hungdeptrai!@#1412io";

        //ma hoa mat khau
        public static string EncryptMD5(string plainText)
        {
            string encrypted = null;

            try
            {
                byte[] inputBytes = ASCIIEncoding.ASCII.GetBytes(plainText);
                byte[] pwdhash = null;
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();

                //Tạo mã băm hash từ mật khẩu
                //băm mã hash là mã hóa 1 chiều
                //hàm băm hash, không thể dịch ngược
                pwdhash = hashmd5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(key));
                hashmd5 = null;

                //Tao 1 TripleDES moi
                TripleDESCryptoServiceProvider tdesProvider = new TripleDESCryptoServiceProvider();
                tdesProvider.Key = pwdhash;
                tdesProvider.Mode = CipherMode.ECB;

                encrypted = Convert.ToBase64String(tdesProvider.CreateEncryptor().TransformFinalBlock(inputBytes, 0, inputBytes.Length));

            }
            catch
            {
                throw;
            }
            return encrypted;
        }

        //giai ma matkhau

        public static string DecryptMD5(string DecryptString)
        {
            try
            {
                byte[] keyArray;
                //lay byte tu chuoi

                byte[] toEncryptArray = Convert.FromBase64String(DecryptString);

                //neu hash duoc su dung, lay hash cua key;
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));

                hashmd5.Clear();

                TripleDESCryptoServiceProvider tdesProvider = new TripleDESCryptoServiceProvider();
                tdesProvider.Key = keyArray;
                //co 4 mode
                //su dung ECB(Electronic Code Book)

                tdesProvider.Mode = CipherMode.ECB;
                tdesProvider.Padding = PaddingMode.PKCS7;

                ICryptoTransform transform = tdesProvider.CreateDecryptor();
                byte[] resultArray = transform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

                tdesProvider.Clear();

                return UTF8Encoding.UTF8.GetString(resultArray);

            }
            catch
            {
                return " ";
            }
        }
    }
}
