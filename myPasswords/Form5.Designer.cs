namespace myPasswords
{
    partial class UpdateConfirmation
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpdateConfirmation));
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.update_changes = new System.Windows.Forms.Button();
            this.cancel_update = new System.Windows.Forms.Button();
            this.show_password = new System.Windows.Forms.Button();
            this.hide_password = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(18)))));
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox1.Font = new System.Drawing.Font("Roboto", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBox1.ForeColor = System.Drawing.Color.White;
            this.textBox1.Location = new System.Drawing.Point(115, 35);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(466, 28);
            this.textBox1.TabIndex = 0;
            this.textBox1.UseSystemPasswordChar = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Roboto", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(23, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Password";
            // 
            // update_changes
            // 
            this.update_changes.AutoSize = true;
            this.update_changes.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.update_changes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.update_changes.Font = new System.Drawing.Font("Roboto", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.update_changes.ForeColor = System.Drawing.Color.White;
            this.update_changes.Location = new System.Drawing.Point(398, 91);
            this.update_changes.Name = "update_changes";
            this.update_changes.Size = new System.Drawing.Size(100, 40);
            this.update_changes.TabIndex = 2;
            this.update_changes.Text = "Update";
            this.update_changes.UseVisualStyleBackColor = true;
            // 
            // cancel_update
            // 
            this.cancel_update.AutoSize = true;
            this.cancel_update.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancel_update.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancel_update.Font = new System.Drawing.Font("Roboto", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cancel_update.ForeColor = System.Drawing.Color.White;
            this.cancel_update.Location = new System.Drawing.Point(513, 91);
            this.cancel_update.Name = "cancel_update";
            this.cancel_update.Size = new System.Drawing.Size(100, 40);
            this.cancel_update.TabIndex = 2;
            this.cancel_update.Text = "Cancel";
            this.cancel_update.UseVisualStyleBackColor = true;
            // 
            // show_password
            // 
            this.show_password.BackColor = System.Drawing.Color.WhiteSmoke;
            this.show_password.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("show_password.BackgroundImage")));
            this.show_password.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.show_password.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.show_password.Location = new System.Drawing.Point(583, 34);
            this.show_password.Name = "show_password";
            this.show_password.Size = new System.Drawing.Size(30, 30);
            this.show_password.TabIndex = 3;
            this.show_password.UseVisualStyleBackColor = false;
            // 
            // hide_password
            // 
            this.hide_password.BackColor = System.Drawing.Color.WhiteSmoke;
            this.hide_password.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("hide_password.BackgroundImage")));
            this.hide_password.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.hide_password.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.hide_password.Location = new System.Drawing.Point(583, 34);
            this.hide_password.Name = "hide_password";
            this.hide_password.Size = new System.Drawing.Size(30, 30);
            this.hide_password.TabIndex = 3;
            this.hide_password.UseVisualStyleBackColor = false;
            // 
            // UpdateConfirmation
            // 
            this.AcceptButton = this.update_changes;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(18)))));
            this.CancelButton = this.cancel_update;
            this.ClientSize = new System.Drawing.Size(659, 176);
            this.Controls.Add(this.hide_password);
            this.Controls.Add(this.show_password);
            this.Controls.Add(this.cancel_update);
            this.Controls.Add(this.update_changes);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "UpdateConfirmation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Update Database";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button update_changes;
        private System.Windows.Forms.Button cancel_update;
        private System.Windows.Forms.Button show_password;
        private System.Windows.Forms.Button hide_password;
    }
}