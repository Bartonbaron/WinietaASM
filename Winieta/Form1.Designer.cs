﻿namespace Winieta
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pictureBox1 = new PictureBox();
            BtnLoadImg = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(33, 33);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(161, 74);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // BtnLoadImg
            // 
            BtnLoadImg.Location = new Point(324, 167);
            BtnLoadImg.Name = "BtnLoadImg";
            BtnLoadImg.Size = new Size(157, 49);
            BtnLoadImg.TabIndex = 0;
            BtnLoadImg.Text = "Wczytaj obraz";
            BtnLoadImg.Click += BtnLoadImg_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(600, 450);
            Controls.Add(BtnLoadImg);
            Controls.Add(pictureBox1);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pictureBox1;
        private Button BtnLoadImg;
    }
}
