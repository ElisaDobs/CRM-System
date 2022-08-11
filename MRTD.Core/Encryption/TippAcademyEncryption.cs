using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace MRTD.Core.Encryption
{
    public static class TippAcademyEncryptionEngine
    {
        private const int saltSize = 16;
        private const int keySize = 256;
        private const int iterations = 1000;
        public static string Encrypt(string input, string password)
        {
            byte[] textBytes = Encoding.UTF8.GetBytes(input);
            byte[] passPhraseBytes = Encoding.UTF8.GetBytes(password);
            passPhraseBytes = SHA256.Create().ComputeHash(passPhraseBytes);
            byte[] saltBytes = new byte[saltSize];
            new RNGCryptoServiceProvider().GetBytes(saltBytes);
            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;
                    AES.Mode = CipherMode.CBC;

                    var key = new Rfc2898DeriveBytes(passPhraseBytes, saltBytes, iterations);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);
                    using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(textBytes, 0, textBytes.Length);
                        cs.Close();
                    }
                    byte[] encryptedTextBytes = saltBytes.Concat(ms.ToArray()).ToArray();
                    return Convert.ToBase64String(encryptedTextBytes);
                }
            }
        }
        public static string Decrypt(string input, string password)
        {
            byte[] cipherBytes = Convert.FromBase64String(input);
            byte[] saltBytes = cipherBytes.Take(saltSize).ToArray();
            byte[] textBytes = cipherBytes.Skip(saltSize).ToArray();
            byte[] passPhraseBytes = Encoding.UTF8.GetBytes(password);
            passPhraseBytes = SHA256.Create().ComputeHash(passPhraseBytes);
            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;
                    AES.Mode = CipherMode.CBC;
                    var key = new Rfc2898DeriveBytes(passPhraseBytes, saltBytes, 1000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);
                    using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(textBytes, 0, textBytes.Length);
                        cs.Close();
                    }

                    byte[] decryptedBytes = ms.ToArray();

                    return Encoding.UTF8.GetString(decryptedBytes);
                }
            }
        }
    }
}
