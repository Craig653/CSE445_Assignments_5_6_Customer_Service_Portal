using System;
using System.Security.Cryptography; // gonna use built-in .NET classes for encryption
using System.Text;

namespace LocalHash
{
    public class CredentialEncrypt
    {
        private readonly Aes _aes; // built-in AES object for encryption

        // constructor
        public CredentialEncrypt()
        {
            _aes = Aes.Create(); // create an AES object

            // Ensure this key is 32 characters long (private key)
            // used https://www.roboform.com/password-generator for generating random 32 character key
            _aes.Key = Encoding.UTF8.GetBytes("QyR!gUZQane#XHm#et2pVbLbWnMwrmpv");

            // Ensure this IV is 16 characters long (initialization vector)
            // used https://www.roboform.com/password-generator for generating random 16 character IV
            _aes.IV = Encoding.UTF8.GetBytes("tnso9u682DesRGu^");
        }

        // encrypt a string
        public string EncryptString(string plainText)
        {
            byte[] encrypted; // byte array for encrypted text
            using (var encryptor = _aes.CreateEncryptor(_aes.Key, _aes.IV)) // create an encryptor object
            {
                byte[] plainBytes = Encoding.UTF8.GetBytes(plainText); // convert the plain text to bytes
                encrypted = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length); // encrypt the bytes
            }
            return Convert.ToBase64String(encrypted); // return the encrypted string
        }
    }

    // WE ARE NOT USING THE DECRYPT SINCE WE ARE COMPARING PASSWORDS IN A 1-WAY HASH/CIPHERTEXT MANNER, BUT MAYBE IT CAN BE USEFUL IN THE FUTURE
    // decrypt a string
    public class CredentialDecrypt
    {
        private readonly Aes _aes; // built-in AES object for decryption

        public CredentialDecrypt()
        {
            _aes = Aes.Create(); // create an AES object
            _aes.Key = Encoding.UTF8.GetBytes("QyR!gUZQane#XHm#et2pVbLbWnMwrmpv"); // Ensure this key is 32 characters long
            _aes.IV = Encoding.UTF8.GetBytes("tnso9u682DesRGu^");  // Ensure this IV is 16 characters long
        }

        // decrypt a string
        public string DecryptString(string cipherText)
        {
            byte[] decrypted; // byte array for decrypted text
            using (var decryptor = _aes.CreateDecryptor(_aes.Key, _aes.IV)) // create a decryptor object
            {
                byte[] cipherBytes = Convert.FromBase64String(cipherText); // convert the cipher text to bytes
                decrypted = decryptor.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length); // decrypt the bytes
            }
            return Encoding.UTF8.GetString(decrypted); // return the decrypted string
        }
    }
}
