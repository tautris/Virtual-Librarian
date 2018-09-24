using System.Windows.Forms;

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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.imgCamUser = new Emgu.CV.UI.ImageBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.learnNewFace = new System.Windows.Forms.Button();
            this.userName = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgCamUser)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 66.55738F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.44262F));
            this.tableLayoutPanel1.Controls.Add(this.imgCamUser, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(610, 366);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // imgCamUser
            // 
            this.imgCamUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imgCamUser.FunctionalMode = Emgu.CV.UI.ImageBox.FunctionalModeOption.Minimum;
            this.imgCamUser.Location = new System.Drawing.Point(409, 3);
            this.imgCamUser.Name = "imgCamUser";
            this.imgCamUser.Size = new System.Drawing.Size(198, 177);
            this.imgCamUser.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imgCamUser.TabIndex = 2;
            this.imgCamUser.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.learnNewFace);
            this.panel1.Controls.Add(this.userName);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(409, 186);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(198, 177);
            this.panel1.TabIndex = 5;
            // 
            // learnNewFace
            // 
            this.learnNewFace.Dock = System.Windows.Forms.DockStyle.Top;
            this.learnNewFace.Location = new System.Drawing.Point(0, 20);
            this.learnNewFace.Name = "learnNewFace";
            this.learnNewFace.Size = new System.Drawing.Size(198, 23);
            this.learnNewFace.TabIndex = 3;
            this.learnNewFace.Text = "Learn my face!";
            this.learnNewFace.UseVisualStyleBackColor = true;
            this.learnNewFace.Click += new System.EventHandler(this.LearnNewFace_Click);
            // 
            // userName
            // 
            this.userName.Dock = System.Windows.Forms.DockStyle.Top;
            this.userName.Location = new System.Drawing.Point(0, 0);
            this.userName.Name = "userName";
            this.userName.Size = new System.Drawing.Size(198, 20);
            this.userName.TabIndex = 4;
            this.userName.Text = "Username";
            this.userName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.userName.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(610, 366);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MainForm";
            this.Text = "Virtual Librarian";
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imgCamUser)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Emgu.CV.UI.ImageBox imgCamUser;
        private System.Windows.Forms.Button learnNewFace;
        private TextBox userName;
        private Panel panel1;
    }
}

