﻿using System.Windows.Forms;

namespace Virtual_Librarian
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.MainFormLogInAdminButton = new System.Windows.Forms.Button();
            this.learnNewFace = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.imgCamUser = new Emgu.CV.UI.ImageBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgCamUser)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.MainFormLogInAdminButton);
            this.panel1.Controls.Add(this.learnNewFace);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(364, 171);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(243, 192);
            this.panel1.TabIndex = 5;
            // 
            // MainFormLogInAdminButton
            // 
            this.MainFormLogInAdminButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.MainFormLogInAdminButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.MainFormLogInAdminButton.FlatAppearance.BorderSize = 0;
            this.MainFormLogInAdminButton.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.MainFormLogInAdminButton.Location = new System.Drawing.Point(0, 55);
            this.MainFormLogInAdminButton.Name = "MainFormLogInAdminButton";
            this.MainFormLogInAdminButton.Size = new System.Drawing.Size(243, 60);
            this.MainFormLogInAdminButton.TabIndex = 4;
            this.MainFormLogInAdminButton.Text = "Log In As Admin";
            this.MainFormLogInAdminButton.UseVisualStyleBackColor = false;
            this.MainFormLogInAdminButton.Click += new System.EventHandler(this.MainFormLogInAdminButton_Click);
            // 
            // learnNewFace
            // 
            this.learnNewFace.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(153)))), ((int)(((byte)(0)))));
            this.learnNewFace.Dock = System.Windows.Forms.DockStyle.Top;
            this.learnNewFace.FlatAppearance.BorderSize = 0;
            this.learnNewFace.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.learnNewFace.Location = new System.Drawing.Point(0, 0);
            this.learnNewFace.Name = "learnNewFace";
            this.learnNewFace.Size = new System.Drawing.Size(243, 55);
            this.learnNewFace.TabIndex = 3;
            this.learnNewFace.Text = "Log In";
            this.learnNewFace.UseVisualStyleBackColor = false;
            this.learnNewFace.Click += new System.EventHandler(this.LearnNewFace_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(255)))), ((int)(((byte)(253)))));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 59.34426F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40.65574F));
            this.tableLayoutPanel1.Controls.Add(this.imgCamUser, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.textBox1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45.90164F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 54.09836F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(610, 366);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // imgCamUser
            // 
            this.imgCamUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imgCamUser.FunctionalMode = Emgu.CV.UI.ImageBox.FunctionalModeOption.Minimum;
            this.imgCamUser.Location = new System.Drawing.Point(364, 3);
            this.imgCamUser.Name = "imgCamUser";
            this.imgCamUser.Size = new System.Drawing.Size(243, 162);
            this.imgCamUser.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imgCamUser.TabIndex = 2;
            this.imgCamUser.TabStop = false;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(255)))), ((int)(((byte)(253)))));
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Font = new System.Drawing.Font("Arial", 24.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.textBox1.ForeColor = System.Drawing.Color.Black;
            this.textBox1.Location = new System.Drawing.Point(3, 3);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(280, 161);
            this.textBox1.TabIndex = 6;
            this.textBox1.Text = "  Welcome\r\n  To\r\n  Virtual\r\n  Librarian";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(610, 366);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "MainForm";
            this.Text = "Virtual Librarian";
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgCamUser)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel panel1;
        private Button learnNewFace;
        private TableLayoutPanel tableLayoutPanel1;
        private Emgu.CV.UI.ImageBox imgCamUser;
        private TextBox textBox1;
        private Button MainFormLogInAdminButton;
    }
}

