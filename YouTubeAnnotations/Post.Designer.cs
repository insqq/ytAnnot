namespace YouTubeAnnotations
{
    partial class Post
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblContent = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lpbPicture = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.lpbPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // lblContent
            // 
            this.lblContent.AutoSize = true;
            this.lblContent.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblContent.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblContent.Location = new System.Drawing.Point(0, 0);
            this.lblContent.MaximumSize = new System.Drawing.Size(450, 100);
            this.lblContent.Name = "lblContent";
            this.lblContent.Size = new System.Drawing.Size(46, 17);
            this.lblContent.TabIndex = 0;
            this.lblContent.Text = "label1";
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblTime.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.lblTime.Location = new System.Drawing.Point(0, 17);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(35, 13);
            this.lblTime.TabIndex = 5;
            this.lblTime.Text = "label1";
            this.lblTime.Click += new System.EventHandler(this.lblTime_Click);
            this.lblTime.MouseLeave += new System.EventHandler(this.lblTime_MouseLeave);
            this.lblTime.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lblTime_MouseMove);
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(0, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1063, 2);
            this.label1.TabIndex = 3;
            // 
            // lpbPicture
            // 
            this.lpbPicture.Dock = System.Windows.Forms.DockStyle.Top;
            this.lpbPicture.Location = new System.Drawing.Point(0, 32);
            this.lpbPicture.Name = "lpbPicture";
            this.lpbPicture.Size = new System.Drawing.Size(1063, 50);
            this.lpbPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.lpbPicture.TabIndex = 3;
            this.lpbPicture.TabStop = false;
            // 
            // Post
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.lpbPicture);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.lblContent);
            this.MinimumSize = new System.Drawing.Size(1050, 0);
            this.Name = "Post";
            this.Size = new System.Drawing.Size(1063, 550);
            ((System.ComponentModel.ISupportInitialize)(this.lpbPicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblContent;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox lpbPicture;
    }
}
