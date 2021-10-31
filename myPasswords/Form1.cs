using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace myPasswords
{
    public partial class Home : Form
    {

        public Home()
        {
            InitializeComponent();
        }

        private void exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void new_file_Click(object sender, EventArgs e)
        {
            NewFile newFile = new NewFile();
            newFile.Show();

            this.Hide();
        }

        private void open_file_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Encrypted Password Database Files|*.crypt";
            openFileDialog1.Title = "Open a Password Database";
            openFileDialog1.FileName = "";
            openFileDialog1.ShowDialog();
            
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

            gv.file_path = openFileDialog1.FileName;
            gv.database_name = openFileDialog1.SafeFileName.Substring(0, openFileDialog1.SafeFileName.Length - 6);

 
            byte[] file_header = new byte[16];

            byte[] raw_data = File.ReadAllBytes(gv.file_path);

            if (raw_data.Length >= 16) 
            {

                for (int i = 0; i < 16; i++)
                {
                    file_header[i] = raw_data[i];
                }

                if (crypt.check_sum(file_header))
                {
                    gv.number_of_keys = (int) file_header[15];
                    OpenFile openFile = new OpenFile();
                    openFile.Show();

                    this.Hide();
                }

                else
                {
                    crypt.error_message(1);
                }

            }
            else 
            {
                crypt.error_message(1);
            }
            



        }

    }
}
