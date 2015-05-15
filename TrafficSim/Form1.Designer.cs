namespace TrafficSim
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
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.trackBarTrafficFlow = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBoxASK = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarTrafficFlow)).BeginInit();
            this.SuspendLayout();
            // 
            // trackBarTrafficFlow
            // 
            this.trackBarTrafficFlow.Location = new System.Drawing.Point(238, 379);
            this.trackBarTrafficFlow.Maximum = 100;
            this.trackBarTrafficFlow.Minimum = 10;
            this.trackBarTrafficFlow.Name = "trackBarTrafficFlow";
            this.trackBarTrafficFlow.Size = new System.Drawing.Size(104, 45);
            this.trackBarTrafficFlow.TabIndex = 0;
            this.trackBarTrafficFlow.TickFrequency = 5;
            this.trackBarTrafficFlow.Value = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(348, 390);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "TrafficFlow";
            // 
            // checkBoxASK
            // 
            this.checkBoxASK.AutoSize = true;
            this.checkBoxASK.Location = new System.Drawing.Point(440, 390);
            this.checkBoxASK.Name = "checkBoxASK";
            this.checkBoxASK.Size = new System.Drawing.Size(47, 17);
            this.checkBoxASK.TabIndex = 2;
            this.checkBoxASK.Text = "ASK";
            this.checkBoxASK.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(783, 454);
            this.Controls.Add(this.checkBoxASK);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.trackBarTrafficFlow);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.trackBarTrafficFlow)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.TrackBar trackBarTrafficFlow;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBoxASK;
    }
}

