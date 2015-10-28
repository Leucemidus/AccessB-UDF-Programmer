namespace AccessB_UDF_Programmer
{
    partial class frmAccbUDFProg
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
            this.btnDetect = new System.Windows.Forms.Button();
            this.btnLoadHEX = new System.Windows.Forms.Button();
            this.btnHEXProg = new System.Windows.Forms.Button();
            this.lblDetect = new System.Windows.Forms.Label();
            this.lblFileName = new System.Windows.Forms.Label();
            this.lblProgramStatus = new System.Windows.Forms.Label();
            this.opnHEXFile = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // btnDetect
            // 
            this.btnDetect.Location = new System.Drawing.Point(12, 10);
            this.btnDetect.Name = "btnDetect";
            this.btnDetect.Size = new System.Drawing.Size(106, 23);
            this.btnDetect.TabIndex = 0;
            this.btnDetect.Text = "AccessB Detect";
            this.btnDetect.UseVisualStyleBackColor = true;
            this.btnDetect.Click += new System.EventHandler(this.btnDetect_Click);
            // 
            // btnLoadHEX
            // 
            this.btnLoadHEX.Enabled = false;
            this.btnLoadHEX.Location = new System.Drawing.Point(12, 39);
            this.btnLoadHEX.Name = "btnLoadHEX";
            this.btnLoadHEX.Size = new System.Drawing.Size(106, 23);
            this.btnLoadHEX.TabIndex = 0;
            this.btnLoadHEX.Text = "Load HEX";
            this.btnLoadHEX.UseVisualStyleBackColor = true;
            this.btnLoadHEX.Click += new System.EventHandler(this.btnLoadHEX_Click);
            // 
            // btnHEXProg
            // 
            this.btnHEXProg.Enabled = false;
            this.btnHEXProg.Location = new System.Drawing.Point(12, 68);
            this.btnHEXProg.Name = "btnHEXProg";
            this.btnHEXProg.Size = new System.Drawing.Size(106, 23);
            this.btnHEXProg.TabIndex = 0;
            this.btnHEXProg.Text = "Program HEX";
            this.btnHEXProg.UseVisualStyleBackColor = true;
            this.btnHEXProg.Click += new System.EventHandler(this.btnHEXProg_Click);
            // 
            // lblDetect
            // 
            this.lblDetect.AutoSize = true;
            this.lblDetect.Location = new System.Drawing.Point(124, 15);
            this.lblDetect.Name = "lblDetect";
            this.lblDetect.Size = new System.Drawing.Size(141, 13);
            this.lblDetect.TabIndex = 1;
            this.lblDetect.Text = "Board status: Disconnected.";
            // 
            // lblFileName
            // 
            this.lblFileName.AutoSize = true;
            this.lblFileName.Location = new System.Drawing.Point(124, 44);
            this.lblFileName.Name = "lblFileName";
            this.lblFileName.Size = new System.Drawing.Size(55, 13);
            this.lblFileName.TabIndex = 1;
            this.lblFileName.Text = "File name:";
            // 
            // lblProgramStatus
            // 
            this.lblProgramStatus.AutoSize = true;
            this.lblProgramStatus.Location = new System.Drawing.Point(124, 73);
            this.lblProgramStatus.Name = "lblProgramStatus";
            this.lblProgramStatus.Size = new System.Drawing.Size(40, 13);
            this.lblProgramStatus.TabIndex = 1;
            this.lblProgramStatus.Text = "Status:";
            // 
            // opnHEXFile
            // 
            this.opnHEXFile.FileName = "sdsdf";
            // 
            // frmAccbUDFProg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(341, 99);
            this.Controls.Add(this.lblProgramStatus);
            this.Controls.Add(this.lblFileName);
            this.Controls.Add(this.lblDetect);
            this.Controls.Add(this.btnHEXProg);
            this.Controls.Add(this.btnLoadHEX);
            this.Controls.Add(this.btnDetect);
            this.Name = "frmAccbUDFProg";
            this.Text = "AccessB UDF Programmer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnDetect;
        private System.Windows.Forms.Button btnLoadHEX;
        private System.Windows.Forms.Button btnHEXProg;
        private System.Windows.Forms.Label lblDetect;
        private System.Windows.Forms.Label lblFileName;
        private System.Windows.Forms.Label lblProgramStatus;
        private System.Windows.Forms.OpenFileDialog opnHEXFile;
    }
}

