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
    public partial class OpenFile : Form
    {
        public string[] key_path = new string[4];
        public bool[] is_key_selected = { true, true, true, true };
        public bool[] is_key_entered = { false, false, false, false };
        public bool is_key_in_range = true;
        public bool is_key_generated = false;
       
        public OpenFile()
        {
            InitializeComponent();
        }

        private void OpenFile_Load(object sender, EventArgs e)
        {
            switch (gv.number_of_keys) 
            {
                case 1:
                    comboBox1.DropDownStyle = ComboBoxStyle.Simple;
                    comboBox1.Items.Add("Key Path");

                    is_key_selected[0] = false;

                    comboBox1.SelectedIndex = 0;
                    break;
                case 2:
                    comboBox1.Items.Add("Key Path 1");
                    comboBox1.Items.Add("Key Path 2");

                    is_key_selected[0] = false;
                    is_key_selected[1] = false;

                    comboBox1.SelectedIndex = 0;
                    break;
                case 3:
                    comboBox1.Items.Add("Key Path 1");
                    comboBox1.Items.Add("Key Path 2");
                    comboBox1.Items.Add("Key Path 3");

                    is_key_selected[0] = false;
                    is_key_selected[1] = false;
                    is_key_selected[2] = false;

                    comboBox1.SelectedIndex = 0;
                    break;
                case 4:
                    comboBox1.Items.Add("Key Path 1");
                    comboBox1.Items.Add("Key Path 2");
                    comboBox1.Items.Add("Key Path 3");
                    comboBox1.Items.Add("Key Path 4");

                    is_key_selected[0] = false;
                    is_key_selected[1] = false;
                    is_key_selected[2] = false;
                    is_key_selected[3] = false;

                    comboBox1.SelectedIndex = 0;
                    break;
            }
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            home.Show();

            this.Close();
        }

        private void open_Click(object sender, EventArgs e)
        {
            gv.password = textBox1.Text;
            gv.iv = crypt.MD5_Hash(gv.password);

            if (is_key_selected[0] && is_key_selected[1] && is_key_selected[2] && is_key_selected[3])
            {
                byte[] raw_data = new byte[33];

                for (int i = 0; i < gv.number_of_keys; i++)
                {

                    switch (gv.number_of_keys)
                    {
                        case 1:
                            raw_data = File.ReadAllBytes(key_path[i]);
                            if (raw_data[0] == (0x00))
                            {
                                gv.w_key = crypt.return_key(raw_data);
                                is_key_entered[0] = true;
                            }
                            else
                            {
                                is_key_in_range = false;
                            }
                            break;

                        case 2:
                            raw_data = File.ReadAllBytes(key_path[i]);
                            if (raw_data[0] == (0x00))
                            {
                                gv.w_key = crypt.return_key(raw_data);
                                is_key_entered[0] = true;
                            }
                            else if (raw_data[0] == (0x01))
                            {
                                gv.r_key = crypt.return_key(raw_data);
                                is_key_entered[1] = true;
                            }
                            else
                            {
                                is_key_in_range = false;
                            }
                            break;

                        case 3:
                            raw_data = File.ReadAllBytes(key_path[i]);
                            if (raw_data[0] == (0x00))
                            {
                                gv.w_key = crypt.return_key(raw_data);
                                is_key_entered[0] = true;
                            }
                            else if (raw_data[0] == (0x01))
                            {
                                gv.r_key = crypt.return_key(raw_data);
                                is_key_entered[1] = true;
                            }
                            else if (raw_data[0] == (0x02))
                            {
                                gv.g_key = crypt.return_key(raw_data);
                                is_key_entered[2] = true;
                            }
                            else
                            {
                                is_key_in_range = false;
                            }
                            break;

                        case 4:
                            raw_data = File.ReadAllBytes(key_path[i]);
                            if (raw_data[0] == (0x00))
                            {
                                gv.w_key = crypt.return_key(raw_data);
                                is_key_entered[0] = true;
                            }
                            else if (raw_data[0] == (0x01))
                            {
                                gv.r_key = crypt.return_key(raw_data);
                                is_key_entered[1] = true;
                            }
                            else if (raw_data[0] == (0x02))
                            {
                                gv.g_key = crypt.return_key(raw_data);
                                is_key_entered[2] = true;
                            }
                            else if (raw_data[0] == (0x03))
                            {
                                gv.b_key = crypt.return_key(raw_data);
                                is_key_entered[3] = true;
                            }
                            else
                            {
                                is_key_in_range = false;
                            }
                            break;
                    }

                    if (!is_key_in_range) break;

                }

                if (!is_key_in_range) crypt.error_message(5);
                else
                {
                    switch (gv.number_of_keys) 
                    {
                        case 1:
                            if (is_key_entered[0])
                            {
                                gv.cipher_key = gv.w_key;
                                is_key_generated = true;

                            }
                            else crypt.error_message(5);
                            break;
                            
                        case 2:


                            if (is_key_entered[0] && is_key_entered[1])
                            {
                                for (int i = 0; i < 32; i++)
                                {

                                    gv.cipher_key[i] = (byte)(gv.w_key[i] ^ gv.r_key[i]);

                                }
                                is_key_generated = true;
                            }
                            else crypt.error_message(5);
                            break;

                        case 3:


                            if(is_key_entered[0] && is_key_entered[1] && is_key_entered[2]){
                                for (int i = 0; i < 32; i++)
                                {

                                    gv.cipher_key[i] = (byte)((gv.w_key[i] ^ gv.r_key[i]) | gv.g_key[i]);

                                }
                                is_key_generated = true;
                            }
                            else crypt.error_message(5);
                            break;

                        case 4:
                            if(is_key_entered[0] && is_key_entered[1] && is_key_entered[2] && is_key_entered[3]){
                                for (int i = 0; i < 32; i++)
                                {

                                    gv.cipher_key[i] = (byte)(((gv.w_key[i] ^ gv.r_key[i]) | gv.g_key[i]) & gv.b_key[i]);

                                }
                                is_key_generated = true;
                            }
                            else crypt.error_message(5);
                            break;
                    }

                    if (is_key_generated)
                    {
                        byte[] data = File.ReadAllBytes(gv.file_path);
                        byte[] encrypted_header = new byte[16];


                        for (int i = 16; i < 32; i++)
                        {
                            encrypted_header[i - 16] = data[i];
                        }


                        try
                        {   if (crypt.DecryptStringFromBytes_Aes(encrypted_header, gv.cipher_key, gv.iv) == "File Decrypted")
                            {
                                MainUI mainUI = new MainUI();
                                mainUI.Show();

                                this.Close();
                            }
                        }

                        catch
                        { crypt.error_message(5); }

                    }
                }
            }
            else crypt.error_message(4);
        }

        private void browse_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Key File|*.key";
            openFileDialog1.Title = "Select a Key File";
            openFileDialog1.FileName = "";
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            switch (comboBox1.SelectedIndex) 
            {
                case 0:
                    key_path[0] = openFileDialog1.FileName;
                    comboBox1.Items[0] = openFileDialog1.FileName;
                    is_key_selected[0] = true;
                    break;
                case 1:
                    key_path[1] = openFileDialog1.FileName;
                    comboBox1.Items[1] = openFileDialog1.FileName;
                    is_key_selected[1] = true;
                    break;
                case 2:
                    key_path[2] = openFileDialog1.FileName;
                    comboBox1.Items[2] = openFileDialog1.FileName;
                    is_key_selected[2] = true;
                    break;
                case 3:
                    key_path[3] = openFileDialog1.FileName;
                    comboBox1.Items[3] = openFileDialog1.FileName;
                    is_key_selected[3] = true;
                    break;
            }
        }
    }
}
