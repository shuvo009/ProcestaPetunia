//Title :           Encrypt and Descript.
//Version :         1.0.0.1
//Copyright :       Copyright (c) 2010
//Author :          Md.Hasanuzzaman (shuvo009@live.com)
//Company :         procesta (http://www.procesta.com/)
//Description :     Supports String Encrypt and Descript.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using ProcestaVariables;

namespace ProcestaEncryptAndDescript
{
    public class EncryptAndDescript
    {
        // Encrypt
        public static string Encrypt(string plainText)
        {
            try
            {
                if (!plainText.Equals(string.Empty))
                {
                    byte[] initVectorBytes = Encoding.ASCII.GetBytes(ProcestaVariables.Variables.initVector);
                    byte[] saltValueBytes = Encoding.ASCII.GetBytes(ProcestaVariables.Variables.saltValue);
                    byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
                    PasswordDeriveBytes password = new PasswordDeriveBytes(ProcestaVariables.Variables.passPhrase, saltValueBytes, ProcestaVariables.Variables.hashAlgorithm, ProcestaVariables.Variables.passwordIterations);
                    byte[] keyBytes = password.GetBytes(ProcestaVariables.Variables.keySize / 8);
                    RijndaelManaged symmetricKey = new RijndaelManaged();
                    symmetricKey.Mode = CipherMode.CBC;
                    ICryptoTransform encrypor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
                    MemoryStream memoryStream = new MemoryStream();
                    CryptoStream cryptoStream = new CryptoStream(memoryStream, encrypor, CryptoStreamMode.Write);
                    cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                    cryptoStream.FlushFinalBlock();
                    byte[] cipherTextBytes = memoryStream.ToArray();
                    memoryStream.Close();
                    cryptoStream.Close();
                    string ciperText = Convert.ToBase64String(cipherTextBytes);
                    return ciperText;
                }
                else
                {
                    return plainText;
                }
            }
            catch
            {
                return "";
            }

        }
        //Descript
        public static string Descript(string chipherText)
        {
            try
            {
                if (!chipherText.Equals(string.Empty))
                {
                    byte[] initVentoryBytes = Encoding.ASCII.GetBytes(ProcestaVariables.Variables.initVector);
                    byte[] saltValueBytes = Encoding.ASCII.GetBytes(ProcestaVariables.Variables.saltValue);
                    byte[] chiperTextBytes = Convert.FromBase64String(chipherText);
                    PasswordDeriveBytes password = new PasswordDeriveBytes(ProcestaVariables.Variables.passPhrase, saltValueBytes, ProcestaVariables.Variables.hashAlgorithm, ProcestaVariables.Variables.passwordIterations);
                    byte[] keyBytes = password.GetBytes(ProcestaVariables.Variables.keySize / 8);
                    RijndaelManaged symmetricKey = new RijndaelManaged();
                    symmetricKey.Mode = CipherMode.CBC;
                    ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVentoryBytes);
                    MemoryStream memoryStream = new MemoryStream(chiperTextBytes);
                    CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
                    byte[] plainTextByte = new byte[chiperTextBytes.Length];
                    int decrptedByteCount = cryptoStream.Read(plainTextByte, 0, plainTextByte.Length);
                    memoryStream.Close();
                    cryptoStream.Close();
                    string plainText = Encoding.UTF8.GetString(plainTextByte, 0, decrptedByteCount);
                    return plainText;
                }
                else
                {
                    return chipherText;
                }
            }
            catch
            {
                return "";
            }
        }
       
    }
}
