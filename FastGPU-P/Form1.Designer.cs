namespace FastGPU_P
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
            this.gpuBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.vmBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.addButton = new System.Windows.Forms.Button();
            this.AllocationLabel = new System.Windows.Forms.Label();
            this.allocationBar = new MetroFramework.Controls.MetroTrackBar();
            this.allocLabel = new System.Windows.Forms.Label();
            this.installDriver = new System.Windows.Forms.Button();
            this.RemoveButton = new System.Windows.Forms.Button();
            this.mountVHD = new System.Windows.Forms.Button();
            this.vhdBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // gpuBox
            // 
            this.gpuBox.FormattingEnabled = true;
            this.gpuBox.Location = new System.Drawing.Point(25, 174);
            this.gpuBox.Name = "gpuBox";
            this.gpuBox.Size = new System.Drawing.Size(315, 28);
            this.gpuBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 151);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "GPU";
            // 
            // vmBox
            // 
            this.vmBox.FormattingEnabled = true;
            this.vmBox.Location = new System.Drawing.Point(23, 107);
            this.vmBox.Name = "vmBox";
            this.vmBox.Size = new System.Drawing.Size(317, 28);
            this.vmBox.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "VM";
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(25, 301);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(144, 29);
            this.addButton.TabIndex = 4;
            this.addButton.Text = "Add";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // AllocationLabel
            // 
            this.AllocationLabel.AutoSize = true;
            this.AllocationLabel.Location = new System.Drawing.Point(27, 214);
            this.AllocationLabel.Name = "AllocationLabel";
            this.AllocationLabel.Size = new System.Drawing.Size(156, 20);
            this.AllocationLabel.TabIndex = 6;
            this.AllocationLabel.Text = "Allocation percentage";
            // 
            // allocationBar
            // 
            this.allocationBar.BackColor = System.Drawing.Color.Transparent;
            this.allocationBar.Location = new System.Drawing.Point(27, 246);
            this.allocationBar.Name = "allocationBar";
            this.allocationBar.Size = new System.Drawing.Size(255, 29);
            this.allocationBar.TabIndex = 7;
            this.allocationBar.Text = "null";
            this.allocationBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.allocationBar_Scroll);
            // 
            // allocLabel
            // 
            this.allocLabel.AutoSize = true;
            this.allocLabel.Location = new System.Drawing.Point(297, 247);
            this.allocLabel.Name = "allocLabel";
            this.allocLabel.Size = new System.Drawing.Size(37, 20);
            this.allocLabel.TabIndex = 8;
            this.allocLabel.Text = "50%";
            // 
            // installDriver
            // 
            this.installDriver.Location = new System.Drawing.Point(91, 372);
            this.installDriver.Name = "installDriver";
            this.installDriver.Size = new System.Drawing.Size(243, 29);
            this.installDriver.TabIndex = 9;
            this.installDriver.Text = "install/update driver";
            this.installDriver.UseVisualStyleBackColor = true;
            this.installDriver.Click += new System.EventHandler(this.installDriver_Click);
            // 
            // RemoveButton
            // 
            this.RemoveButton.Location = new System.Drawing.Point(175, 301);
            this.RemoveButton.Name = "RemoveButton";
            this.RemoveButton.Size = new System.Drawing.Size(159, 29);
            this.RemoveButton.TabIndex = 10;
            this.RemoveButton.Text = "Remove";
            this.RemoveButton.UseVisualStyleBackColor = true;
            this.RemoveButton.Click += new System.EventHandler(this.RemoveButton_Click);
            // 
            // mountVHD
            // 
            this.mountVHD.Location = new System.Drawing.Point(27, 338);
            this.mountVHD.Name = "mountVHD";
            this.mountVHD.Size = new System.Drawing.Size(307, 29);
            this.mountVHD.TabIndex = 11;
            this.mountVHD.Text = "Mount VHD";
            this.mountVHD.UseVisualStyleBackColor = true;
            this.mountVHD.Click += new System.EventHandler(this.mountVHD_Click);
            // 
            // vhdBox
            // 
            this.vhdBox.FormattingEnabled = true;
            this.vhdBox.Location = new System.Drawing.Point(27, 373);
            this.vhdBox.Name = "vhdBox";
            this.vhdBox.Size = new System.Drawing.Size(58, 28);
            this.vhdBox.TabIndex = 12;
            this.vhdBox.Click += new System.EventHandler(this.vhdBox_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(91, 429);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(176, 20);
            this.label3.TabIndex = 13;
            this.label3.Text = "with ❤ by @b1on1cdog";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(360, 465);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.vhdBox);
            this.Controls.Add(this.mountVHD);
            this.Controls.Add(this.RemoveButton);
            this.Controls.Add(this.installDriver);
            this.Controls.Add(this.allocLabel);
            this.Controls.Add(this.allocationBar);
            this.Controls.Add(this.AllocationLabel);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.vmBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gpuBox);
            this.Name = "Form1";
            this.Text = "Fast GPU-P";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ComboBox gpuBox;
        private Label label1;
        private ComboBox vmBox;
        private Label label2;
        private Button addButton;
        private Label AllocationLabel;
        private MetroFramework.Controls.MetroTrackBar allocationBar;
        private Label allocLabel;
        private Button installDriver;
        private Button RemoveButton;
        private Button mountVHD;
        private ComboBox vhdBox;
        private Label label3;
    }
}