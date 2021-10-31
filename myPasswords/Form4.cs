using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net.Http.Headers;
using System.Text;
using System.Windows.Forms;

namespace myPasswords
{
    public partial class MainUI : Form
    {
        List<entry> record = new List<entry>();
        int number_of_entries = 0;
        string selected_title;
        public MainUI()
        {
            InitializeComponent();
        }

        private void main_UI_Load(object sender, EventArgs e)
        {
            this.Text = "Password Manager" + " - " + gv.database_name;
            byte[] raw_data = File.ReadAllBytes(gv.file_path);


            if (raw_data.Length > 32)
            {
                byte[] enc_data = new byte[raw_data.Length - 32];

                for (int i = 0; i < raw_data.Length - 32; i++)
                {
                    enc_data[i] = raw_data[i + 32];
                }

                string dec_data = crypt.DecryptStringFromBytes_Aes(enc_data, gv.cipher_key, gv.iv);

                string[] dec_record = dec_data.Split('\n');


                for (int i = 0; i < dec_record.Length; i++)
                {
                    if (dec_record[i].Length >= 13 && (dec_record[i].Split('\t').Length - 1) == 6)
                    {
                        record.Add(entry.get_record(dec_record[i]));
                        list.Items.Add(record[number_of_entries++].title);
                    }
                }

            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            list.Items.Clear();
            for (int i = 0; i < number_of_entries; i++) 
            {
                if (record[i].title.ToLower().Contains(textBox1.Text.ToLower())) 
                {
                    list.Items.Add(record[i].title);
                }
            }
        }

        private void menu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void show_password_Click(object sender, EventArgs e)
        {
            textBox6.UseSystemPasswordChar = false;
            show_password.Visible = false;
            hide_password.Visible = true;
        }

        private void hide_password_Click(object sender, EventArgs e)
        {
            textBox6.UseSystemPasswordChar = true;
            show_password.Visible = true;
            hide_password.Visible = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            System.Windows.Forms.Clipboard.SetText(" ");
            timer1.Stop();
        }

        private void copy_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Clipboard.SetText(textBox6.Text);
            timer1.Start();
        }

        private void list_SelectedIndexChanged(object sender, EventArgs e)
        {
            for(int i = 0; i < number_of_entries; i++)
            {
                if (record[i].title.ToLower() == list.Items[list.SelectedIndex].ToString().ToLower())
                {
                    textBox2.Text = record[i].group;
                    textBox3.Text = record[i].title;
                    textBox4.Text = record[i].username;
                    textBox5.Text = record[i].email;
                    textBox6.Text = record[i].password;
                    textBox7.Text = record[i].u_r_l;
                    textBox8.Text = record[i].notes;
                }
            }
        }

        private void add_entry_Click(object sender, EventArgs e)
        {
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();

            textBox2.ReadOnly = false;
            textBox3.ReadOnly = false;
            textBox4.ReadOnly = false;
            textBox5.ReadOnly = false;
            textBox6.ReadOnly = false;
            textBox7.ReadOnly = false;
            textBox8.ReadOnly = false;


            textBox2.PlaceholderText = "Enter a group.";
            textBox3.PlaceholderText = "Enter a title.";
            textBox4.PlaceholderText = "Enter a username.";
            textBox5.PlaceholderText = "Enter an email.";
            textBox6.PlaceholderText = "Enter a password.";
            textBox7.PlaceholderText = "Enter a URL.";
            textBox8.PlaceholderText = "Enter notes, if any.";

            add_entry.Visible = false;
            modify_entry.Visible = false;
            delete_entry.Visible = false;

            cancel_append.Visible = true;
            append_entry.Visible = true;

        }

        private void append_entry_Click(object sender, EventArgs e)
        {
            bool is_title_taken = false;
            if(textBox3.Text.Length == 0 || textBox6.Text.Length == 0)
            {
                crypt.error_message(6);
            }
            else
            {
                for (int i = 0; i < number_of_entries; i++)
                {
                    if (record[i].title.ToLower() == textBox3.Text.ToLower())
                    {
                        crypt.error_message(7);
                        is_title_taken = true;
                        break;
                    }
                }

                if(!is_title_taken)
                {
                    number_of_entries++;

                    if (textBox2.Text.Length == 0) textBox2.Text = "Unsorted";
                    if (textBox4.Text.Length == 0) textBox4.Text = " ";
                    if (textBox5.Text.Length == 0) textBox5.Text = " ";
                    if (textBox7.Text.Length == 0) textBox7.Text = " ";
                    if (textBox8.Text.Length == 0) textBox8.Text = " ";

                    entry new_entry = new entry(textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, textBox6.Text, textBox7.Text, textBox8.Text);
                    record.Add(new_entry);
                    list.Items.Add(new_entry.title);

                    textBox2.Clear();
                    textBox3.Clear();
                    textBox4.Clear();
                    textBox5.Clear();
                    textBox6.Clear();
                    textBox7.Clear();
                    textBox8.Clear();

                    textBox2.ReadOnly = true;
                    textBox3.ReadOnly = true;
                    textBox4.ReadOnly = true;
                    textBox5.ReadOnly = true;
                    textBox6.ReadOnly = true;
                    textBox7.ReadOnly = true;
                    textBox8.ReadOnly = true;


                    textBox2.PlaceholderText = " ";
                    textBox3.PlaceholderText = " ";
                    textBox4.PlaceholderText = " ";
                    textBox5.PlaceholderText = " ";
                    textBox6.PlaceholderText = " ";
                    textBox7.PlaceholderText = " ";
                    textBox8.PlaceholderText = " ";

                    add_entry.Visible = true;
                    modify_entry.Visible = true;
                    delete_entry.Visible = true;

                    cancel_append.Visible = false;
                    append_entry.Visible = false;

                }

            }
        }

        private void cancel_append_Click(object sender, EventArgs e)
        {
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();

            textBox2.ReadOnly = true;
            textBox3.ReadOnly = true;
            textBox4.ReadOnly = true;
            textBox5.ReadOnly = true;
            textBox6.ReadOnly = true;
            textBox7.ReadOnly = true;
            textBox8.ReadOnly = true;


            textBox2.PlaceholderText = " ";
            textBox3.PlaceholderText = " ";
            textBox4.PlaceholderText = " ";
            textBox5.PlaceholderText = " ";
            textBox6.PlaceholderText = " ";
            textBox7.PlaceholderText = " ";
            textBox8.PlaceholderText = " ";

            add_entry.Visible = true;
            modify_entry.Visible = true;
            delete_entry.Visible = true;

            cancel_append.Visible = false;
            append_entry.Visible = false;
        }

        private void modify_entry_Click(object sender, EventArgs e)
        {
            if (textBox3.Text.Length == 0) crypt.error_message(8);
            else
            {
                selected_title = textBox3.Text.ToLower();

                textBox2.ReadOnly = false;
                textBox3.ReadOnly = false;
                textBox4.ReadOnly = false;
                textBox5.ReadOnly = false;
                textBox6.ReadOnly = false;
                textBox7.ReadOnly = false;
                textBox8.ReadOnly = false;

                if (textBox2.Text == " ") textBox2.Clear();
                if (textBox4.Text == " ") textBox4.Clear();
                if (textBox5.Text == " ") textBox5.Clear();
                if (textBox7.Text == " ") textBox7.Clear();
                if (textBox8.Text == " ") textBox8.Clear();

                textBox2.PlaceholderText = "Enter a group.";
                textBox3.PlaceholderText = "Enter a title.";
                textBox4.PlaceholderText = "Enter a username.";
                textBox5.PlaceholderText = "Enter an email.";
                textBox6.PlaceholderText = "Enter a password.";
                textBox7.PlaceholderText = "Enter a URL.";
                textBox8.PlaceholderText = "Enter notes, if any.";

                add_entry.Visible = false;
                modify_entry.Visible = false;
                delete_entry.Visible = false;

                cancel_changes.Visible = true;
                save_changes.Visible = true;
                }
            }

        private void save_changes_Click(object sender, EventArgs e)
        {
            int i,j;
            bool is_title_taken = false;

            for (i = 0; i < number_of_entries; i++)
            {
                
                if (record[i].title.ToLower() == selected_title) break;
                    
  
            }

            for (j = 0; j < number_of_entries; j++) 
            {
                if ((record[j].title.ToLower() == textBox3.Text.ToLower()) && j!=i) 
                {
                    crypt.error_message(7);
                    is_title_taken = true;
                    break;
                }
            }
                       
            if (is_title_taken == false)
            {
                record[i].group = textBox2.Text;
                record[i].title = textBox3.Text;
                record[i].username = textBox4.Text;
                record[i].email = textBox5.Text;
                record[i].password = textBox6.Text;
                record[i].u_r_l = textBox7.Text;
                record[i].notes = textBox8.Text;

                list.Items.Add(record[i].title);
                list.Items.Remove(selected_title);

                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                textBox5.Clear();
                textBox6.Clear();
                textBox7.Clear();
                textBox8.Clear();

                textBox2.ReadOnly = true;
                textBox3.ReadOnly = true;
                textBox4.ReadOnly = true;
                textBox5.ReadOnly = true;
                textBox6.ReadOnly = true;
                textBox7.ReadOnly = true;
                textBox8.ReadOnly = true;


                textBox2.PlaceholderText = " ";
                textBox3.PlaceholderText = " ";
                textBox4.PlaceholderText = " ";
                textBox5.PlaceholderText = " ";
                textBox6.PlaceholderText = " ";
                textBox7.PlaceholderText = " ";
                textBox8.PlaceholderText = " ";

                add_entry.Visible = true;
                modify_entry.Visible = true;
                delete_entry.Visible = true;

                cancel_changes.Visible = false;
                save_changes.Visible = false;
            }
           
        }

        private void cancel_changes_Click(object sender, EventArgs e)
        {
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();

            textBox2.ReadOnly = true;
            textBox3.ReadOnly = true;
            textBox4.ReadOnly = true;
            textBox5.ReadOnly = true;
            textBox6.ReadOnly = true;
            textBox7.ReadOnly = true;
            textBox8.ReadOnly = true;


            textBox2.PlaceholderText = " ";
            textBox3.PlaceholderText = " ";
            textBox4.PlaceholderText = " ";
            textBox5.PlaceholderText = " ";
            textBox6.PlaceholderText = " ";
            textBox7.PlaceholderText = " ";
            textBox8.PlaceholderText = " ";

            add_entry.Visible = true;
            modify_entry.Visible = true;
            delete_entry.Visible = true;

            cancel_changes.Visible = false;
            save_changes.Visible = false;
        }

        private void delete_entry_Click(object sender, EventArgs e)
        {


            if (textBox3.Text.Length == 0) crypt.error_message(9);
            else
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete the entry?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.Yes)
                {
                    for (int i = 0; i < number_of_entries; i++)
                    {
                        if (record[i].title.ToLower() == textBox3.Text.ToLower())
                        {
                            record.RemoveAt(i);
                            number_of_entries--;

                            list.Items.Clear();

                            for (int j = 0; j < number_of_entries; j++)
                            {
                                list.Items.Add(record[j].title);
                            }

                            textBox2.Clear();
                            textBox3.Clear();
                            textBox4.Clear();
                            textBox5.Clear();
                            textBox6.Clear();
                            textBox7.Clear();
                            textBox8.Clear();

                            list.ClearSelected();

                            break;
                        }
                    }
                }

            }

        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            string dec_data = "";

            for(int i=0; i<number_of_entries; i++)
            {
                dec_data += record[i].return_record();
            }
            if (dec_data.Length > 0)
            {
                byte[] enc_data = crypt.EncryptStringToBytes_Aes(dec_data, gv.cipher_key, gv.iv);


                FileStream fileStream = File.Create(gv.file_path);
                fileStream.Write(crypt.append_number_of_keys(gv.number_of_keys), 0, 16);
                fileStream.Write(crypt.EncryptStringToBytes_Aes("File Decrypted", gv.cipher_key, gv.iv), 0, 16);
                fileStream.Write(enc_data, 0, enc_data.Length);
                fileStream.Close();


            }
        }

        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Do you want to save changes to " + gv.database_name + ".crypt", "", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);

            if(dialogResult == DialogResult.Yes)
            {
                toolStripMenuItem6.PerformClick();
                this.Close();

                Home home = new Home();
                home.Show();
            }

            else if (dialogResult == DialogResult.No)
            { 
                this.Close();

                Home home = new Home();
                home.Show();
            }
        }

        private void MainUI_FormClosed(object sender, FormClosedEventArgs e)
        {
            toolStripMenuItem9.PerformClick();
        }
    }


}
