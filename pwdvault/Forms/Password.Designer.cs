﻿namespace pwdvault.Forms
{
    partial class Password
    {
        /// <summary> 
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            lbApp = new Label();
            lbUser = new Label();
            btnDelete = new Button();
            btnEdit = new Button();
            lbUserTitle = new Label();
            button1 = new Button();
            label1 = new Label();
            SuspendLayout();
            // 
            // lbApp
            // 
            lbApp.AutoSize = true;
            lbApp.Font = new Font("Bahnschrift SemiBold", 15F, FontStyle.Bold, GraphicsUnit.Point);
            lbApp.ForeColor = Color.FromArgb(25, 155, 226);
            lbApp.Location = new Point(99, 0);
            lbApp.Name = "lbApp";
            lbApp.Size = new Size(143, 36);
            lbApp.TabIndex = 0;
            lbApp.Text = "Facebook";
            lbApp.MouseEnter += Password_MouseEnter;
            lbApp.MouseLeave += Password_MouseLeave;
            // 
            // lbUser
            // 
            lbUser.AutoSize = true;
            lbUser.Location = new Point(267, 37);
            lbUser.Name = "lbUser";
            lbUser.Size = new Size(33, 25);
            lbUser.TabIndex = 1;
            lbUser.Text = "gh";
            lbUser.MouseEnter += Password_MouseEnter;
            lbUser.MouseLeave += Password_MouseLeave;
            // 
            // btnDelete
            // 
            btnDelete.BackgroundImage = Properties.Resources.icons8_delete_48;
            btnDelete.BackgroundImageLayout = ImageLayout.Stretch;
            btnDelete.Location = new Point(976, 14);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(48, 48);
            btnDelete.TabIndex = 3;
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            btnDelete.MouseEnter += Password_MouseEnter;
            btnDelete.MouseLeave += Password_MouseLeave;
            // 
            // btnEdit
            // 
            btnEdit.BackgroundImage = Properties.Resources.icons8_edit_48;
            btnEdit.BackgroundImageLayout = ImageLayout.Stretch;
            btnEdit.Location = new Point(903, 14);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(48, 48);
            btnEdit.TabIndex = 4;
            btnEdit.UseVisualStyleBackColor = true;
            btnEdit.Click += btnEdit_Click;
            btnEdit.MouseEnter += Password_MouseEnter;
            btnEdit.MouseLeave += Password_MouseLeave;
            // 
            // lbUserTitle
            // 
            lbUserTitle.AutoSize = true;
            lbUserTitle.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lbUserTitle.Location = new Point(141, 37);
            lbUserTitle.Name = "lbUserTitle";
            lbUserTitle.Size = new Size(101, 25);
            lbUserTitle.TabIndex = 6;
            lbUserTitle.Text = "Username ";
            lbUserTitle.MouseEnter += Password_MouseEnter;
            lbUserTitle.MouseLeave += Password_MouseLeave;
            // 
            // button1
            // 
            button1.BackgroundImage = Properties.Resources.icons8_copy_48;
            button1.BackgroundImageLayout = ImageLayout.Stretch;
            button1.Location = new Point(830, 14);
            button1.Name = "button1";
            button1.Size = new Size(48, 48);
            button1.TabIndex = 7;
            button1.UseVisualStyleBackColor = true;
            button1.MouseEnter += Password_MouseEnter;
            button1.MouseLeave += Password_MouseLeave;
            // 
            // label1
            // 
            label1.Location = new Point(25, 14);
            label1.Name = "label1";
            label1.Size = new Size(48, 48);
            label1.TabIndex = 8;
            label1.Text = "label1";
            // 
            // Password
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(label1);
            Controls.Add(button1);
            Controls.Add(lbUserTitle);
            Controls.Add(btnEdit);
            Controls.Add(btnDelete);
            Controls.Add(lbUser);
            Controls.Add(lbApp);
            Name = "Password";
            Size = new Size(1050, 80);
            MouseEnter += Password_MouseEnter;
            MouseLeave += Password_MouseLeave;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lbApp;
        private Label lbUser;
        private Button btnDelete;
        private Button btnEdit;
        private Label lbUserTitle;
        private Button button1;
        private Label label1;
    }
}
