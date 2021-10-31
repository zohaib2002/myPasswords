using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace myPasswords
{
    public partial class NewFile : Form
    {
        public NewFile()
        {
            InitializeComponent();
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            home.Show();

            this.Close();
        }

        private void NewFile_Load(object sender, EventArgs e)
        {
            radioButton1.Checked = true;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            gv.number_of_keys = 1;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            gv.number_of_keys = 2;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            gv.number_of_keys = 3;
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            gv.number_of_keys = 4;
        }

        private void create_Click(object sender, EventArgs e)
        {

            if ((textBox1.Text.Length > 0))
            {

                if (textBox2.Text.Length >= 8)
                {

                    gv.database_name = textBox1.Text;
                    gv.password = textBox2.Text;

                    gv.iv = crypt.MD5_Hash(gv.password);

                    DialogResult dialogResult = folderBrowserDialog1.ShowDialog();

                    if (dialogResult == DialogResult.OK)
                    {
                        this.Hide();
                        gv.file_path = folderBrowserDialog1.SelectedPath;

                        FileStream fileStream;
                        string selected_path = gv.file_path + "\\" + gv.database_name + "\\key(s)\\";
                        Directory.CreateDirectory(Path.GetDirectoryName(selected_path));

                        switch (gv.number_of_keys)
                        {

                            case 1:
                                gv.w_key = crypt.generate_key();
                                gv.cipher_key = gv.w_key;

                                fileStream = File.Create(selected_path + "\\w_key.key");
                                fileStream.Write(crypt.add_key_index((0x00), gv.w_key), 0, 33);
                                fileStream.Close();

                                break;

                            case 2:
                                gv.w_key = crypt.generate_key();
                                gv.r_key = crypt.generate_key();

                                for (int i = 0; i < 32; i++)
                                {

                                    gv.cipher_key[i] = (byte)(gv.w_key[i] ^ gv.r_key[i]);

                                }

                                fileStream = File.Create(selected_path + "\\w_key.key");
                                fileStream.Write(crypt.add_key_index((0x00), gv.w_key), 0, 33);
                                fileStream.Close();

                                fileStream = File.Create(selected_path + "\\r_key.key");
                                fileStream.Write(crypt.add_key_index((0x01), gv.r_key), 0, 33);
                                fileStream.Close();

                                break;

                            case 3:
                                gv.w_key = crypt.generate_key();
                                gv.r_key = crypt.generate_key();
                                gv.g_key = crypt.generate_key();

                                for (int i = 0; i < 32; i++)
                                {

                                    gv.cipher_key[i] = (byte)((gv.w_key[i] ^ gv.r_key[i]) | gv.g_key[i]);

                                }

                                fileStream = File.Create(selected_path + "\\w_key.key");
                                fileStream.Write(crypt.add_key_index((0x00), gv.w_key), 0, 33);
                                fileStream.Close();

                                fileStream = File.Create(selected_path + "\\r_key.key");
                                fileStream.Write(crypt.add_key_index((0x01), gv.r_key), 0, 33);
                                fileStream.Close();

                                fileStream = File.Create(selected_path + "\\g_key.key");
                                fileStream.Write(crypt.add_key_index((0x02), gv.g_key), 0, 33);
                                fileStream.Close();

                                break;

                            case 4:
                                gv.w_key = crypt.generate_key();
                                gv.r_key = crypt.generate_key();
                                gv.g_key = crypt.generate_key();
                                gv.b_key = crypt.generate_key();

                                for (int i = 0; i < 32; i++)
                                {

                                    gv.cipher_key[i] = (byte)(((gv.w_key[i] ^ gv.r_key[i]) | gv.g_key[i]) & gv.b_key[i]);

                                }

                                fileStream = File.Create(selected_path + "\\w_key.key");
                                fileStream.Write(crypt.add_key_index((0x00), gv.w_key), 0, 33);
                                fileStream.Close();

                                fileStream = File.Create(selected_path + "\\r_key.key");
                                fileStream.Write(crypt.add_key_index((0x01), gv.r_key), 0, 33);
                                fileStream.Close();

                                fileStream = File.Create(selected_path + "\\g_key.key");
                                fileStream.Write(crypt.add_key_index((0x02), gv.g_key), 0, 33);
                                fileStream.Close();

                                fileStream = File.Create(selected_path + "\\b_key.key");
                                fileStream.Write(crypt.add_key_index((0x03), gv.b_key), 0, 33);
                                fileStream.Close();

                                break;
                        }

                        gv.file_path = folderBrowserDialog1.SelectedPath + "\\" + gv.database_name + "\\" + gv.database_name + ".crypt";

                        fileStream = File.Create(gv.file_path);
                        fileStream.Write(crypt.append_number_of_keys(gv.number_of_keys), 0, 16);
                        fileStream.Write(crypt.EncryptStringToBytes_Aes("File Decrypted", gv.cipher_key, gv.iv), 0, 16);
                        fileStream.Close();

                        MainUI mainUI = new MainUI();
                        mainUI.Show();

                        this.Close();

                    }

                }
                else crypt.error_message(2);

            }
            else crypt.error_message(3);


        }
    }
}
