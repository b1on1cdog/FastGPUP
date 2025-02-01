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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            gpuBox = new ComboBox();
            gpuLabel = new Label();
            vmBox = new ComboBox();
            vmLabel = new Label();
            addButton = new Button();
            AllocationLabel = new Label();
            allocationBar = new MetroFramework.Controls.MetroTrackBar();
            allocPercent = new Label();
            installDriverBtn = new Button();
            RemoveButton = new Button();
            creditLabel = new Label();
            SuspendLayout();
            // 
            // gpuBox
            // 
            gpuBox.DropDownStyle = ComboBoxStyle.DropDownList;
            gpuBox.FormattingEnabled = true;
            gpuBox.Location = new Point(29, 235);
            gpuBox.Margin = new Padding(4, 7, 4, 7);
            gpuBox.Name = "gpuBox";
            gpuBox.Size = new Size(393, 33);
            gpuBox.TabIndex = 0;
            // 
            // gpuLabel
            // 
            gpuLabel.AutoSize = true;
            gpuLabel.Location = new Point(29, 193);
            gpuLabel.Margin = new Padding(4, 0, 4, 0);
            gpuLabel.Name = "gpuLabel";
            gpuLabel.Size = new Size(46, 25);
            gpuLabel.TabIndex = 1;
            gpuLabel.Text = "GPU";
            // 
            // vmBox
            // 
            vmBox.DropDownStyle = ComboBoxStyle.DropDownList;
            vmBox.FormattingEnabled = true;
            vmBox.Location = new Point(29, 132);
            vmBox.Margin = new Padding(4, 7, 4, 7);
            vmBox.Name = "vmBox";
            vmBox.Size = new Size(395, 33);
            vmBox.TabIndex = 2;
            // 
            // vmLabel
            // 
            vmLabel.AutoSize = true;
            vmLabel.Location = new Point(29, 87);
            vmLabel.Margin = new Padding(4, 0, 4, 0);
            vmLabel.Name = "vmLabel";
            vmLabel.Size = new Size(39, 25);
            vmLabel.TabIndex = 3;
            vmLabel.Text = "VM";
            // 
            // addButton
            // 
            addButton.Location = new Point(31, 397);
            addButton.Margin = new Padding(4, 7, 4, 7);
            addButton.Name = "addButton";
            addButton.Size = new Size(180, 60);
            addButton.TabIndex = 4;
            addButton.Text = "Allocate";
            addButton.UseVisualStyleBackColor = true;
            addButton.Click += addButton_Click;
            // 
            // AllocationLabel
            // 
            AllocationLabel.AutoSize = true;
            AllocationLabel.Location = new Point(29, 291);
            AllocationLabel.Margin = new Padding(4, 0, 4, 0);
            AllocationLabel.Name = "AllocationLabel";
            AllocationLabel.Size = new Size(184, 25);
            AllocationLabel.TabIndex = 6;
            AllocationLabel.Text = "Allocation percentage";
            // 
            // allocationBar
            // 
            allocationBar.BackColor = Color.Transparent;
            allocationBar.Location = new Point(31, 323);
            allocationBar.Margin = new Padding(4, 7, 4, 7);
            allocationBar.Minimum = 5;
            allocationBar.Name = "allocationBar";
            allocationBar.Size = new Size(319, 60);
            allocationBar.SmallChange = 5;
            allocationBar.TabIndex = 7;
            allocationBar.Text = "null";
            allocationBar.Scroll += allocationBar_Scroll;
            // 
            // allocPercent
            // 
            allocPercent.AutoSize = true;
            allocPercent.Location = new Point(371, 340);
            allocPercent.Margin = new Padding(4, 0, 4, 0);
            allocPercent.Name = "allocPercent";
            allocPercent.Size = new Size(47, 25);
            allocPercent.TabIndex = 8;
            allocPercent.Text = "50%";
            // 
            // installDriverBtn
            // 
            installDriverBtn.Location = new Point(31, 471);
            installDriverBtn.Margin = new Padding(4, 7, 4, 7);
            installDriverBtn.Name = "installDriverBtn";
            installDriverBtn.Size = new Size(387, 60);
            installDriverBtn.TabIndex = 9;
            installDriverBtn.Text = "Update driver";
            installDriverBtn.UseVisualStyleBackColor = true;
            installDriverBtn.Click += installDriverBtn_Click;
            // 
            // RemoveButton
            // 
            RemoveButton.Location = new Point(219, 397);
            RemoveButton.Margin = new Padding(4, 7, 4, 7);
            RemoveButton.Name = "RemoveButton";
            RemoveButton.Size = new Size(199, 60);
            RemoveButton.TabIndex = 10;
            RemoveButton.Text = "Remove";
            RemoveButton.UseVisualStyleBackColor = true;
            RemoveButton.Click += RemoveButton_Click;
            // 
            // creditLabel
            // 
            creditLabel.AutoSize = true;
            creditLabel.Location = new Point(119, 545);
            creditLabel.Margin = new Padding(4, 0, 4, 0);
            creditLabel.Name = "creditLabel";
            creditLabel.Size = new Size(215, 25);
            creditLabel.TabIndex = 13;
            creditLabel.Text = "with ❤ by @b1on1cdog";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(450, 612);
            Controls.Add(creditLabel);
            Controls.Add(RemoveButton);
            Controls.Add(installDriverBtn);
            Controls.Add(allocPercent);
            Controls.Add(allocationBar);
            Controls.Add(AllocationLabel);
            Controls.Add(addButton);
            Controls.Add(vmLabel);
            Controls.Add(vmBox);
            Controls.Add(gpuLabel);
            Controls.Add(gpuBox);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 7, 4, 7);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form1";
            Padding = new Padding(25, 125, 25, 42);
            Text = "Fast GPU-P";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox gpuBox;
        private Label gpuLabel;
        private ComboBox vmBox;
        private Label vmLabel;
        private Button addButton;
        private Label AllocationLabel;
        private MetroFramework.Controls.MetroTrackBar allocationBar;
        private Label allocPercent;
        private Button installDriverBtn;
        private Button RemoveButton;
        private Label creditLabel;
    }
}