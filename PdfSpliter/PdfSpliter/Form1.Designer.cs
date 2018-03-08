namespace PdfSpliter
{
    partial class Form1
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
            this.fileselect = new System.Windows.Forms.Button();
            this.inputfilepath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.outputinformation = new System.Windows.Forms.TextBox();
            this.run = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // fileselect
            // 
            this.fileselect.Location = new System.Drawing.Point(13, 27);
            this.fileselect.Name = "fileselect";
            this.fileselect.Size = new System.Drawing.Size(259, 23);
            this.fileselect.TabIndex = 0;
            this.fileselect.Text = "Select File";
            this.fileselect.UseVisualStyleBackColor = true;
            this.fileselect.Click += new System.EventHandler(this.fileselect_Click);
            // 
            // inputfilepath
            // 
            this.inputfilepath.Location = new System.Drawing.Point(13, 72);
            this.inputfilepath.Name = "inputfilepath";
            this.inputfilepath.Size = new System.Drawing.Size(259, 20);
            this.inputfilepath.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(42, 126);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 3;
            // 
            // outputinformation
            // 
            this.outputinformation.BackColor = System.Drawing.SystemColors.Control;
            this.outputinformation.Location = new System.Drawing.Point(138, 162);
            this.outputinformation.Name = "outputinformation";
            this.outputinformation.Size = new System.Drawing.Size(134, 20);
            this.outputinformation.TabIndex = 4;
            // 
            // run
            // 
            this.run.Location = new System.Drawing.Point(177, 217);
            this.run.Name = "run";
            this.run.Size = new System.Drawing.Size(95, 47);
            this.run.TabIndex = 5;
            this.run.Text = "Run";
            this.run.UseVisualStyleBackColor = true;
            this.run.Click += new System.EventHandler(this.run_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(13, 116);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(259, 23);
            this.progressBar1.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(287, 276);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.run);
            this.Controls.Add(this.outputinformation);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.inputfilepath);
            this.Controls.Add(this.fileselect);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button fileselect;
        private System.Windows.Forms.TextBox inputfilepath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox outputinformation;
        private System.Windows.Forms.Button run;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}

