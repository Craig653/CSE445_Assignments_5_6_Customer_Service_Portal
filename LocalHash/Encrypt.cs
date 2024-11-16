using System;
using System.Security.Cryptography; // gonna use built-in .NET classes for encryption
using System.Text;

namespace LocalHash
{
    public class Encrypt
    {
        private readonly Aes _aes; // built-in AES object for encryption

        public Encrypt()
        {
            _aes = Aes.Create();

            // Ensure this key is 32 characters long (private key)
            // used https://www.roboform.com/password-generator for generating random 32 character key
            _aes.Key = Encoding.UTF8.GetBytes("QyR!gUZQane#XHm#et2pVbLbWnMwrmpv");

            // Ensure this IV is 16 characters long (initialization vector)
            // used https://www.roboform.com/password-generator for generating random 16 character IV
            _aes.IV = Encoding.UTF8.GetBytes("tnso9u682DesRGu^");
        }

        public string EncryptString(string plainText)
        {
            byte[] encrypted;
            using (var encryptor = _aes.CreateEncryptor(_aes.Key, _aes.IV))
            {
                byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
                encrypted = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);
            }
            return Convert.ToBase64String(encrypted);
        }
    }

    public class Decrypt
    {
        private readonly Aes _aes;

        public Decrypt()
        {
            _aes = Aes.Create();
            _aes.Key = Encoding.UTF8.GetBytes("QyR!gUZQane#XHm#et2pVbLbWnMwrmpv"); // Ensure this key is 32 characters long
            _aes.IV = Encoding.UTF8.GetBytes("tnso9u682DesRGu^");  // Ensure this IV is 16 characters long
        }

        public string DecryptString(string cipherText)
        {
            byte[] decrypted;
            using (var decryptor = _aes.CreateDecryptor(_aes.Key, _aes.IV))
            {
                byte[] cipherBytes = Convert.FromBase64String(cipherText);
                decrypted = decryptor.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);
            }
            return Encoding.UTF8.GetString(decrypted);
        }
    }
}
   
}
