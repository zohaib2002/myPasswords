using System;
using System.Collections.Generic;
using System.Text;

namespace myPasswords
{
    class gv
    {


        public static int number_of_keys = 1;
        public static string database_name;
        public static string password;
        public static string file_path;
        public static bool new_file = false;

        public static byte[] w_key, r_key, g_key, b_key;
        public static byte[] cipher_key = new byte[32];
        public static byte[] iv = new byte[16];


    }
}
