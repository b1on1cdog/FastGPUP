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
            gpuBox.Location = new Point(31, 218);
            gpuBox.Margin = new Padding(4, 4, 4, 4);
            gpuBox.Name = "gpuBox";
            gpuBox.Size = new Size(393, 33);
            gpuBox.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(31, 189);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(46, 25);
            label1.TabIndex = 1;
            label1.Text = "GPU";
            // 
            // vmBox
            // 
            vmBox.FormattingEnabled = true;
            vmBox.Location = new Point(29, 134);
            vmBox.Margin = new Padding(4, 4, 4, 4);
            vmBox.Name = "vmBox";
            vmBox.Size = new Size(395, 33);
            vmBox.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(31, 101);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(39, 25);
            label2.TabIndex = 3;
            label2.Text = "VM";
            // 
            // addButton
            // 
            addButton.Location = new Point(31, 376);
            addButton.Margin = new Padding(4, 4, 4, 4);
            addButton.Name = "addButton";
            addButton.Size = new Size(180, 36);
            addButton.TabIndex = 4;
            addButton.Text = "Add";
            addButton.UseVisualStyleBackColor = true;
            addButton.Click += addButton_Click;
            // 
            // AllocationLabel
            // 
            AllocationLabel.AutoSize = true;
            AllocationLabel.Location = new Point(34, 268);
            AllocationLabel.Margin = new Padding(4, 0, 4, 0);
            AllocationLabel.Name = "AllocationLabel";
            AllocationLabel.Size = new Size(184, 25);
            AllocationLabel.TabIndex = 6;
            AllocationLabel.Text = "Allocation percentage";
            // 
            // allocationBar
            // 
            allocationBar.BackColor = Color.Transparent;
            allocationBar.Location = new Point(34, 308);
            allocationBar.Margin = new Padding(4, 4, 4, 4);
            allocationBar.Name = "allocationBar";
            allocationBar.Size = new Size(319, 36);
            allocationBar.TabIndex = 7;
            allocationBar.Text = "null";
            allocationBar.Scroll += allocationBar_Scroll;
            // 
            // allocLabel
            // 
            allocLabel.AutoSize = true;
            allocLabel.Location = new Point(371, 309);
            allocLabel.Margin = new Padding(4, 0, 4, 0);
            allocLabel.Name = "allocLabel";
            allocLabel.Size = new Size(47, 25);
            allocLabel.TabIndex = 8;
            allocLabel.Text = "50%";
            // 
            // installDriverBtn
            // 
            installDriverBtn.Location = new Point(114, 465);
            installDriverBtn.Margin = new Padding(4, 4, 4, 4);
            installDriverBtn.Name = "installDriver";
            installDriverBtn.Size = new Size(304, 36);
            installDriverBtn.TabIndex = 9;
            installDriverBtn.Text = "install/update driver";
            installDriverBtn.UseVisualStyleBackColor = true;
            installDriverBtn.Click += installDriver_Click;
            // 
            // RemoveButton
            // 
            RemoveButton.Location = new Point(219, 376);
            RemoveButton.Margin = new Padding(4, 4, 4, 4);
            RemoveButton.Name = "RemoveButton";
            RemoveButton.Size = new Size(199, 36);
            RemoveButton.TabIndex = 10;
            RemoveButton.Text = "Remove";
            RemoveButton.UseVisualStyleBackColor = true;
            RemoveButton.Click += RemoveButton_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(114, 536);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(215, 25);
            label3.TabIndex = 13;
            label3.Text = "with ❤ by @b1on1cdog";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(450, 581);
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
            Margin = new Padding(4, 4, 4, 4);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form1";
            Padding = new Padding(25, 75, 25, 25);
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