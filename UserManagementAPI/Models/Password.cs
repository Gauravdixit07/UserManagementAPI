using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace UserManagementAPI.Models
{
    public class Password
    {
        public static string Decrypt(string cipherText)
        {
            string EncryptionKey = "Put Yor Key";
            SHA256 mySHA256 = SHA256Managed.Create();
            byte[] key = mySHA256.ComputeHash(Encoding.ASCII.GetBytes(EncryptionKey));
            //byte[] iv = new byte[16] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0 };
            byte[] iv = new byte[16] { 0x49, 0x76, 0x64, 0x65, 0x4d, 0x4e, 0x65, 0x61, 0x65, 0x43, 0x2d, 0x4e, 0x25, 0x45, 0x67, 0x76 };
            Aes encryptor = Aes.Create();
            encryptor.Mode = CipherMode.CBC;
            byte[] aesKey = new byte[32];
            Array.Copy(key, 0, aesKey, 0, 32);
            encryptor.Key = aesKey;
            encryptor.IV = iv;
            MemoryStream memoryStream = new MemoryStream();
            ICryptoTransform aesDecryptor = encryptor.CreateDecryptor();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, aesDecryptor, CryptoStreamMode.Write);
            string plainText = String.Empty;

            try
            {
                byte[] cipherBytes = Convert.FromBase64String(cipherText);
                cryptoStream.Write(cipherBytes, 0, cipherBytes.Length);
                cryptoStream.FlushFinalBlock();
                byte[] plainBytes = memoryStream.ToArray();
                plainText = Encoding.ASCII.GetString(plainBytes, 0, plainBytes.Length);
            }
            finally
            {
                memoryStream.Close();
                cryptoStream.Close();
            }
            return plainText;
        }

        public static string Encrypt(string clearText)
        {
            string EncryptionKey = "Put Yor Key";
            SHA256 mySHA256 = SHA256Managed.Create();
            byte[] key = mySHA256.ComputeHash(Encoding.ASCII.GetBytes(EncryptionKey));

            //{ 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 }
            byte[] iv = new byte[16] { 0x49, 0x76, 0x64, 0x65, 0x4d, 0x4e, 0x65, 0x61, 0x65, 0x43, 0x2d, 0x4e, 0x25, 0x45, 0x67, 0x76 };
            Aes encryptor = Aes.Create();
            encryptor.Mode = CipherMode.CBC;
            byte[] aesKey = new byte[32];
            Array.Copy(key, 0, aesKey, 0, 32);
            encryptor.Key = aesKey;
            encryptor.IV = iv;
            MemoryStream memoryStream = new MemoryStream();
            ICryptoTransform aesEncryptor = encryptor.CreateEncryptor();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, aesEncryptor, CryptoStreamMode.Write);

            try
            {
                byte[] plainBytes = Encoding.ASCII.GetBytes(clearText);
                cryptoStream.Write(plainBytes, 0, plainBytes.Length);
                cryptoStream.FlushFinalBlock();
                byte[] cipherBytes = memoryStream.ToArray();
                clearText = Convert.ToBase64String(cipherBytes, 0, cipherBytes.Length);
            }

            finally
            {
                memoryStream.Close();
                cryptoStream.Close();
            }
            return clearText;
        }
    }
}
