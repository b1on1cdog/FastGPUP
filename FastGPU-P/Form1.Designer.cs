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
            gpuBox = new ComboBox();
            label1 = new Label();
            vmBox = new ComboBox();
            label2 = new Label();
            addButton = new Button();
            AllocationLabel = new Label();
            allocationBar = new MetroFramework.Controls.MetroTrackBar();
            allocLabel = new Label();
            installDriverBtn = new Button();
            RemoveButton = new Button();
            label3 = new Label();
            SuspendLayout();
            // 
            // gpuBox
            // 
            gpuBox.FormattingEnabled = true;
            gpuBox.Location = new Point(22, 130);
            gpuBox.Margin = new Padding(3, 2, 3, 2);
            gpuBox.Name = "gpuBox";
            gpuBox.Size = new Size(276, 23);
            gpuBox.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(22, 113);
            label1.Name = "label1";
            label1.Size = new Size(30, 15);
            label1.TabIndex = 1;
            label1.Text = "GPU";
            // 
            // vmBox
            // 
            vmBox.FormattingEnabled = true;
            vmBox.Location = new Point(20, 80);
            vmBox.Margin = new Padding(3, 2, 3, 2);
            vmBox.Name = "vmBox";
            vmBox.Size = new Size(278, 23);
            vmBox.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(22, 61);
            label2.Name = "label2";
            label2.Size = new Size(25, 15);
            label2.TabIndex = 3;
            label2.Text = "VM";
            // 
            // addButton
            // 
            addButton.Location = new Point(22, 226);
            addButton.Margin = new Padding(3, 2, 3, 2);
            addButton.Name = "addButton";
            addButton.Size = new Size(126, 22);
            addButton.TabIndex = 4;
            addButton.Text = "Add";
            addButton.UseVisualStyleBackColor = true;
            addButton.Click += addButton_Click;
            // 
            // AllocationLabel
            // 
            AllocationLabel.AutoSize = true;
            AllocationLabel.Location = new Point(24, 160);
            AllocationLabel.Name = "AllocationLabel";
            AllocationLabel.Size = new Size(123, 15);
            AllocationLabel.TabIndex = 6;
            AllocationLabel.Text = "Allocation percentage";
            // 
            // allocationBar
            // 
            allocationBar.BackColor = Color.Transparent;
            allocationBar.Location = new Point(24, 184);
            allocationBar.Margin = new Padding(3, 2, 3, 2);
            allocationBar.Name = "allocationBar";
            allocationBar.Size = new Size(223, 22);
            allocationBar.TabIndex = 7;
            allocationBar.Text = "null";
            allocationBar.Scroll += allocationBar_Scroll;
            // 
            // allocLabel
            // 
            allocLabel.AutoSize = true;
            allocLabel.Location = new Point(260, 185);
            allocLabel.Name = "allocLabel";
            allocLabel.Size = new Size(29, 15);
            allocLabel.TabIndex = 8;
            allocLabel.Text = "50%";
            // 
            // installDriverBtn
            // 
            installDriverBtn.Location = new Point(24, 252);
            installDriverBtn.Margin = new Padding(3, 2, 3, 2);
            installDriverBtn.Name = "installDriverBtn";
            installDriverBtn.Size = new Size(268, 22);
            installDriverBtn.TabIndex = 9;
            installDriverBtn.Text = "install/update driver";
            installDriverBtn.UseVisualStyleBackColor = true;
            installDriverBtn.Click += installDriverBtn_Click;
            // 
            // RemoveButton
            // 
            RemoveButton.Location = new Point(153, 226);
            RemoveButton.Margin = new Padding(3, 2, 3, 2);
            RemoveButton.Name = "RemoveButton";
            RemoveButton.Size = new Size(139, 22);
            RemoveButton.TabIndex = 10;
            RemoveButton.Text = "Remove";
            RemoveButton.UseVisualStyleBackColor = true;
            RemoveButton.Click += RemoveButton_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(80, 276);
            label3.Name = "label3";
            label3.Size = new Size(135, 15);
            label3.TabIndex = 13;
            label3.Text = "with ❤ by @b1on1cdog";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(315, 303);
            Controls.Add(label3);
            Controls.Add(RemoveButton);
            Controls.Add(installDriverBtn);
            Controls.Add(allocLabel);
            Controls.Add(allocationBar);
            Controls.Add(AllocationLabel);
            Controls.Add(addButton);
            Controls.Add(label2);
            Controls.Add(vmBox);
            Controls.Add(label1);
            Controls.Add(gpuBox);
            Margin = new Padding(3, 2, 3, 2);
            Name = "Form1";
            Padding = new Padding(18, 60, 18, 15);
            Text = "Fast GPU-P";
            ResumeLayout(false);
            PerformLayout();
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
        private Button installDriverBtn;
        private Button RemoveButton;
        private Label label3;
    }
}