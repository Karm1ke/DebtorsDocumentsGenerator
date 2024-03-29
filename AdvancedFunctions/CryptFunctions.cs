﻿using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace AdvancedFunctions
{
    public static partial class Util
    {
        public static string GetHashString(string s)
        {
            byte[] ByteData = Encoding.ASCII.GetBytes(s);
            //MD5 creating MD5 object.
            MD5 oMd5 = MD5.Create();
            //Hash değerini hesaplayalım.
            byte[] HashData = oMd5.ComputeHash(ByteData);

            //convert byte array to hex format
            StringBuilder oSb = new StringBuilder();

            for (int x = 0; x < HashData.Length; x++)
            {
                //hexadecimal string value
                oSb.Append(HashData[x].ToString("x2"));
            }
            return (oSb.ToString());
        }

        public static class AESSha1Crypter
        {
            public static string Encrypt(string plainText, string password,
                string salt = "Kosher", string hashAlgorithm = "SHA1",
                int passwordIterations = 2, string initialVector = "OFRna73m*aze01xY",
                int keySize = 256)
            {
                if (string.IsNullOrEmpty(plainText))
                    return "";

                byte[] initialVectorBytes = Encoding.ASCII.GetBytes(initialVector);
                byte[] saltValueBytes = Encoding.ASCII.GetBytes(salt);
                byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

                PasswordDeriveBytes derivedPassword = new PasswordDeriveBytes(password, saltValueBytes, hashAlgorithm, passwordIterations);
                byte[] keyBytes = derivedPassword.GetBytes(keySize / 8);
                RijndaelManaged symmetricKey = new RijndaelManaged();
                symmetricKey.Mode = CipherMode.CBC;

                byte[] cipherTextBytes = null;

                using (ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initialVectorBytes))
                {
                    using (MemoryStream memStream = new MemoryStream())
                    {
                        using (CryptoStream cryptoStream = new CryptoStream(memStream, encryptor, CryptoStreamMode.Write))
                        {
                            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                            cryptoStream.FlushFinalBlock();
                            cipherTextBytes = memStream.ToArray();
                            memStream.Close();
                            cryptoStream.Close();
                        }
                    }
                }

                symmetricKey.Clear();
                return Convert.ToBase64String(cipherTextBytes);
            }

            public static string Decrypt(string cipherText, string password,
            string salt = "Kosher", string hashAlgorithm = "SHA1",
            int passwordIterations = 2, string initialVector = "OFRna73m*aze01xY",
            int keySize = 256)
            {
                if (string.IsNullOrEmpty(cipherText))
                    return "";

                byte[] initialVectorBytes = Encoding.ASCII.GetBytes(initialVector);
                byte[] saltValueBytes = Encoding.ASCII.GetBytes(salt);
                byte[] cipherTextBytes = Convert.FromBase64String(cipherText);

                PasswordDeriveBytes derivedPassword = new PasswordDeriveBytes(password, saltValueBytes, hashAlgorithm, passwordIterations);
                byte[] keyBytes = derivedPassword.GetBytes(keySize / 8);

                RijndaelManaged symmetricKey = new RijndaelManaged();
                symmetricKey.Mode = CipherMode.CBC;

                byte[] plainTextBytes = new byte[cipherTextBytes.Length];
                int byteCount = 0;

                using (ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initialVectorBytes))
                {
                    using (MemoryStream memStream = new MemoryStream(cipherTextBytes))
                    {
                        using (CryptoStream cryptoStream = new CryptoStream(memStream, decryptor, CryptoStreamMode.Read))
                        {
                            byteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                            memStream.Close();
                            cryptoStream.Close();
                        }
                    }
                }

                symmetricKey.Clear();
                return Encoding.UTF8.GetString(plainTextBytes, 0, byteCount);
            }
        }
    }
}
