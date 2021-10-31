using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace myPasswords
{
    class crypt
    {
        public static bool check_sum(byte[] file_header_array)
        {
            byte[] md5_hash = MD5_Hash("CRYPT");

            for (int i = 0; i < 15; i++)
            {
                if (md5_hash[i] != file_header_array[i])
                {
                    return false;
                }
            }
            return true;
        }

        public static byte[] add_key_index(byte key_index, byte[] key)
        {
            byte[] key_with_index = new byte[33];
            key_with_index[0] = key_index;

            for (int i = 0; i < key.Length; i++)
            {
                key_with_index[i + 1] = key[i];
            }

            return key_with_index;
        }

        public static byte[] append_number_of_keys(int number_of_keys) 
        {
            byte[] hash = MD5_Hash("CRYPT");
            byte[] result = new byte[16];

            for (int i = 0; i < 15; i++) 
            {
                result[i] = hash[i];
            }
            
            result[15] = (byte)number_of_keys;

            return result;
        }

        public static byte[] return_key(byte[] key_with_index)
        {
            byte[] key = new byte[32];

            for (int i = 0; i<32; i++)
            {
                key[i] = key_with_index[i+1];
            }

            return key;
        }

        public static byte[] MD5_Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            return md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));
        }

        public static byte[] generate_key() {
            Aes myAes = Aes.Create();
            return myAes.Key;
        } 

        public static byte[] EncryptStringToBytes_Aes(string plainText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");
            byte[] encrypted;

            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create an encryptor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            // Return the encrypted bytes from the memory stream.
            return encrypted;
        }

        public static string DecryptStringFromBytes_Aes(byte[] cipherText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create a decryptor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {

                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }

            return plaintext;
        }

        public static void error_message(int exception) 
        {
            switch (exception) 
            {
                case 1:
                    MessageBox.Show("The chosen file is not a crypt file", "Invalid File", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;

                case 2:
                    MessageBox.Show("Password must be atleast 8 characters.", "Invalid Password", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;

                case 3:
                    MessageBox.Show("Please enter a database name.", "Database Name Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;

                case 4:
                    MessageBox.Show("Please provide path to all keys.", "Key Path Missing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;

                case 5:
                    MessageBox.Show("Wrong combination of key(s) and password.", "Decryption Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;

                case 6:
                    MessageBox.Show("Title and password cannot be empty.", "Missing Required Fields", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;

                case 7:
                    MessageBox.Show("Title already exist in database.", "Unique Title Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;

                case 8:
                    MessageBox.Show("Please select an entry to modify.", "No Entry Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;

                case 9:
                    MessageBox.Show("Please select an entry to delete.", "No Entry Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
            }
        }

    }
}
