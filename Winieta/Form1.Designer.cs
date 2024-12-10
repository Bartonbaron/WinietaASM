namespace Winieta
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            pictureBox1 = new PictureBox();
            BtnLoadImg = new Button();
            PreEffect = new Label();
            pictureBox2 = new PictureBox();
            label2 = new Label();
            BtnApplyVignette = new Button();
            label1 = new Label();
            IntensityTB = new TrackBar();
            label3 = new Label();
            label4 = new Label();
            btnSaveImage = new Button();
            label5 = new Label();
            trackBar2 = new TrackBar();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)IntensityTB).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBar2).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = SystemColors.Control;
            pictureBox1.BackgroundImageLayout = ImageLayout.None;
            pictureBox1.Location = new Point(33, 36);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(244, 130);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // BtnLoadImg
            // 
            BtnLoadImg.Location = new Point(86, 187);
            BtnLoadImg.Name = "BtnLoadImg";
            BtnLoadImg.Size = new Size(125, 32);
            BtnLoadImg.TabIndex = 0;
            BtnLoadImg.Text = "Load image";
            BtnLoadImg.Click += BtnLoadImg_Click;
            // 
            // PreEffect
            // 
            PreEffect.Location = new Point(0, 0);
            PreEffect.Name = "PreEffect";
            PreEffect.Size = new Size(100, 23);
            PreEffect.TabIndex = 5;
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = SystemColors.Control;
            pictureBox2.BackgroundImageLayout = ImageLayout.None;
            pictureBox2.Location = new Point(360, 33);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(244, 130);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 2;
            pictureBox2.TabStop = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(444, 169);
            label2.Name = "label2";
            label2.Size = new Size(78, 15);
            label2.TabIndex = 3;
            label2.Text = "With vignette";
            label2.Click += label1_Click;
            // 
            // BtnApplyVignette
            // 
            BtnApplyVignette.Location = new Point(418, 187);
            BtnApplyVignette.Name = "BtnApplyVignette";
            BtnApplyVignette.Size = new Size(125, 32);
            BtnApplyVignette.TabIndex = 4;
            BtnApplyVignette.Text = "Apply vignette";
            BtnApplyVignette.UseVisualStyleBackColor = true;
            BtnApplyVignette.Click += BtnApplyVignette_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(110, 169);
            label1.Name = "label1";
            label1.Size = new Size(85, 15);
            label1.TabIndex = 6;
            label1.Text = "Original image";
            label1.Click += label1_Click_1;
            // 
            // IntensityTB
            // 
            IntensityTB.Location = new Point(70, 265);
            IntensityTB.Minimum = 1;
            IntensityTB.Name = "IntensityTB";
            IntensityTB.Size = new Size(125, 45);
            IntensityTB.TabIndex = 7;
            IntensityTB.Value = 2;
            IntensityTB.Scroll += IntensityTB_Scroll;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 275);
            label3.Name = "label3";
            label3.Size = new Size(52, 15);
            label3.TabIndex = 8;
            label3.Text = "Intensity";
            label3.Click += label3_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(126, 312);
            label4.Name = "label4";
            label4.Size = new Size(0, 15);
            label4.TabIndex = 9;
            label4.Click += label4_Click;
            // 
            // btnSaveImage
            // 
            btnSaveImage.Location = new Point(418, 237);
            btnSaveImage.Name = "btnSaveImage";
            btnSaveImage.Size = new Size(125, 34);
            btnSaveImage.TabIndex = 10;
            btnSaveImage.Text = "Save Image";
            btnSaveImage.UseVisualStyleBackColor = true;
            btnSaveImage.Click += btnSaveImage_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(12, 369);
            label5.Name = "label5";
            label5.Size = new Size(107, 15);
            label5.TabIndex = 12;
            label5.Text = "Number of threads";
            label5.Click += label5_Click;
            // 
            // trackBar2
            // 
            trackBar2.Location = new Point(125, 357);
            trackBar2.Name = "trackBar2";
            trackBar2.Size = new Size(125, 45);
            trackBar2.TabIndex = 14;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(640, 486);
            Controls.Add(trackBar2);
            Controls.Add(label5);
            Controls.Add(btnSaveImage);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(IntensityTB);
            Controls.Add(label1);
            Controls.Add(BtnApplyVignette);
            Controls.Add(label2);
            Controls.Add(pictureBox2);
            Controls.Add(PreEffect);
            Controls.Add(BtnLoadImg);
            Controls.Add(pictureBox1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            Text = "VignetteEffect";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)IntensityTB).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBar2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private Button BtnLoadImg;
        private Label PreEffect;
        private PictureBox pictureBox2;
        private Label label2;
        private Button BtnApplyVignette;
        private Label label1;
        private Label label3;
        private Label label4;
        private TrackBar IntensityTB;
        private Button btnSaveImage;
        private Label label5;
        private TrackBar trackBar2;
    }
}
