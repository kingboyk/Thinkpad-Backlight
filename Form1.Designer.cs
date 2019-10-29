namespace Thinkpad_Backlight
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        
        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.BrightnessLabel = new System.Windows.Forms.Label();
            this.BrightnessComboBox = new System.Windows.Forms.ComboBox();
            this.OnStartupCheckBox = new System.Windows.Forms.CheckBox();
            this.OnKeyPressCheckBox = new System.Windows.Forms.CheckBox();
            this.TimerCheckBox = new System.Windows.Forms.CheckBox();
            this.SecondsLabel = new System.Windows.Forms.Label();
            this.SecondsNumeric = new System.Windows.Forms.NumericUpDown();
            this.SecondsDisplayLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.SecondsNumeric)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.Timer1Tick);
            // 
            // BrightnessLabel
            // 
            this.BrightnessLabel.AutoSize = true;
            this.BrightnessLabel.Location = new System.Drawing.Point(12, 6);
            this.BrightnessLabel.Name = "BrightnessLabel";
            this.BrightnessLabel.Size = new System.Drawing.Size(56, 13);
            this.BrightnessLabel.TabIndex = 1;
            this.BrightnessLabel.Text = "Brightness";
            // 
            // BrightnessComboBox
            // 
            this.BrightnessComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.BrightnessComboBox.FormattingEnabled = true;
            this.BrightnessComboBox.Items.AddRange(new object[] {
            "Low",
            "High"});
            this.BrightnessComboBox.Location = new System.Drawing.Point(101, 3);
            this.BrightnessComboBox.Name = "BrightnessComboBox";
            this.BrightnessComboBox.Size = new System.Drawing.Size(121, 21);
            this.BrightnessComboBox.TabIndex = 2;
            // 
            // OnStartupCheckBox
            // 
            this.OnStartupCheckBox.AutoSize = true;
            this.OnStartupCheckBox.Location = new System.Drawing.Point(15, 30);
            this.OnStartupCheckBox.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.OnStartupCheckBox.Name = "OnStartupCheckBox";
            this.OnStartupCheckBox.Size = new System.Drawing.Size(207, 17);
            this.OnStartupCheckBox.TabIndex = 4;
            this.OnStartupCheckBox.Text = "Turn backlight on when program starts";
            this.OnStartupCheckBox.UseVisualStyleBackColor = true;
            // 
            // OnKeyPressCheckBox
            // 
            this.OnKeyPressCheckBox.AutoSize = true;
            this.OnKeyPressCheckBox.Location = new System.Drawing.Point(15, 53);
            this.OnKeyPressCheckBox.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.OnKeyPressCheckBox.Name = "OnKeyPressCheckBox";
            this.OnKeyPressCheckBox.Size = new System.Drawing.Size(217, 17);
            this.OnKeyPressCheckBox.TabIndex = 5;
            this.OnKeyPressCheckBox.Text = "Turn backlight on when a key is pressed";
            this.OnKeyPressCheckBox.UseVisualStyleBackColor = true;
            // 
            // TimerCheckBox
            // 
            this.TimerCheckBox.AutoSize = true;
            this.TimerCheckBox.Location = new System.Drawing.Point(15, 76);
            this.TimerCheckBox.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.TimerCheckBox.Name = "TimerCheckBox";
            this.TimerCheckBox.Size = new System.Drawing.Size(238, 17);
            this.TimerCheckBox.TabIndex = 6;
            this.TimerCheckBox.Text = "Turn backlight off when keyboard is inactive:";
            this.TimerCheckBox.UseVisualStyleBackColor = true;
            // 
            // SecondsLabel
            // 
            this.SecondsLabel.AutoSize = true;
            this.SecondsLabel.Location = new System.Drawing.Point(46, 101);
            this.SecondsLabel.Name = "SecondsLabel";
            this.SecondsLabel.Size = new System.Drawing.Size(49, 13);
            this.SecondsLabel.TabIndex = 7;
            this.SecondsLabel.Text = "Seconds";
            // 
            // SecondsNumeric
            // 
            this.SecondsNumeric.Location = new System.Drawing.Point(101, 99);
            this.SecondsNumeric.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.SecondsNumeric.Name = "SecondsNumeric";
            this.SecondsNumeric.Size = new System.Drawing.Size(104, 20);
            this.SecondsNumeric.TabIndex = 8;
            this.SecondsNumeric.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // SecondsDisplayLabel
            // 
            this.SecondsDisplayLabel.AutoSize = true;
            this.SecondsDisplayLabel.Location = new System.Drawing.Point(211, 101);
            this.SecondsDisplayLabel.Name = "SecondsDisplayLabel";
            this.SecondsDisplayLabel.Size = new System.Drawing.Size(0, 13);
            this.SecondsDisplayLabel.TabIndex = 9;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(329, 122);
            this.Controls.Add(this.SecondsDisplayLabel);
            this.Controls.Add(this.SecondsNumeric);
            this.Controls.Add(this.SecondsLabel);
            this.Controls.Add(this.TimerCheckBox);
            this.Controls.Add(this.OnKeyPressCheckBox);
            this.Controls.Add(this.OnStartupCheckBox);
            this.Controls.Add(this.BrightnessComboBox);
            this.Controls.Add(this.BrightnessLabel);
            this.Margin = new System.Windows.Forms.Padding(1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.SecondsNumeric)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label BrightnessLabel;
        private System.Windows.Forms.ComboBox BrightnessComboBox;
        private System.Windows.Forms.CheckBox OnStartupCheckBox;
        private System.Windows.Forms.CheckBox OnKeyPressCheckBox;
        private System.Windows.Forms.CheckBox TimerCheckBox;
        private System.Windows.Forms.Label SecondsLabel;
        private System.Windows.Forms.NumericUpDown SecondsNumeric;
        private System.Windows.Forms.Label SecondsDisplayLabel;
    }
}