/*
    Author: Christopher Angulo
    Description: This file contains is used to create a DLL that will encrypt and decrypt a user passwords using
        AES encryption.
    Class: ASU CSE-445 - Service Oriented Computing - Fall 2024
    Professor: Yinong Chen
    Last Date Modified: 12/1/2024
 */

// libraries
using System;
using System.Security.Cryptography; // going to use built-in .NET classes for encryption
using System.Text;

namespace LocalHash
{
    public class CredentialEncrypt
    {
        private readonly Aes _aes; // built-in AES object for encryption

        // constructor to create AES object using an arbitrary key and initialization vector
        public CredentialEncrypt()
        {
            _aes = Aes.Create(); // create AES object

            // ensure this key is 32 characters long (private key)
            // used https://www.roboform.com/password-generator for generating random 32 character key
            _aes.Key = Encoding.UTF8.GetBytes("QyR!gUZQane#XHm#et2pVbLbWnMwrmpv");

            // ensure this IV is 16 characters long (initialization vector)
            // used https://www.roboform.com/password-generator for generating random 16 character IV
            _aes.IV = Encoding.UTF8.GetBytes("tnso9u682DesRGu^");
        }

        // encrypt plaintext password using AES encryption
        public string EncryptString(string plainText)
        {
            byte[] encrypted; // byte array to store encrypted password
            using (var encryptor = _aes.CreateEncryptor(_aes.Key, _aes.IV)) // create encryptor object
            {
                byte[] plainBytes = Encoding.UTF8.GetBytes(plainText); // convert plaintext to byte array
                encrypted = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length); // encrypt password
            }
            return Convert.ToBase64String(encrypted); // return encrypted password as a base64 string (plaintext)
        }
    }

    // decrypt encrypted password using AES encryption
    // this class is not used in this project since we encrypt password only as a 1-way hash for added security
    // however, this might be useful for future implementations
    public class CredentialDecrypt
    {
        private readonly Aes _aes; // built-in AES object for decryption

        // constructor to create AES object using an arbitrary key and initialization vector
        public CredentialDecrypt()
        {
            _aes = Aes.Create(); // create AES object

            // ensure this key is 32 characters long
            // used https://www.roboform.com/password-generator for generating random 32 character key
            // same as used for encryption
            _aes.Key = Encoding.UTF8.GetBytes("QyR!gUZQane#XHm#et2pVbLbWnMwrmpv");

            // ensure this IV is 16 characters long
            // used https://www.roboform.com/password-generator for generating random 16 character IV
            // same as used for encryption
            _aes.IV = Encoding.UTF8.GetBytes("tnso9u682DesRGu^");
        }

        // decrypt encrypted password using AES encryption
        public string DecryptString(string cipherText)
        {
            byte[] decrypted; // byte array to store decrypted password
            using (var decryptor = _aes.CreateDecryptor(_aes.Key, _aes.IV)) // create decryptor object
            {
                byte[] cipherBytes = Convert.FromBase64String(cipherText); // convert base64 string to byte array
                decrypted = decryptor.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length); // decrypt password
            }
            return Encoding.UTF8.GetString(decrypted); // return decrypted password as a string (plaintext)
        }
    }
}
