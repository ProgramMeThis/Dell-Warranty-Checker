namespace Dell_Warranty_Checker
{
    partial class MainForm
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
            this.label3 = new System.Windows.Forms.Label();
            this.ipAddressTextBox = new System.Windows.Forms.TextBox();
            this.resetButton = new System.Windows.Forms.Button();
            this.serviceTagTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.computerNameTextBox = new System.Windows.Forms.TextBox();
            this.lookupButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.resultsTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(126, 200);
            this.label3.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(160, 32);
            this.label3.TabIndex = 25;
            this.label3.Text = "IP Address:";
            // 
            // ipAddressTextBox
            // 
            this.ipAddressTextBox.Location = new System.Drawing.Point(313, 197);
            this.ipAddressTextBox.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.ipAddressTextBox.Name = "ipAddressTextBox";
            this.ipAddressTextBox.Size = new System.Drawing.Size(623, 38);
            this.ipAddressTextBox.TabIndex = 2;
            this.ipAddressTextBox.TextChanged += new System.EventHandler(this.ipAddressTextBox_TextChanged);
            // 
            // resetButton
            // 
            this.resetButton.Enabled = false;
            this.resetButton.Location = new System.Drawing.Point(958, 147);
            this.resetButton.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(200, 103);
            this.resetButton.TabIndex = 4;
            this.resetButton.Text = "Reset";
            this.resetButton.UseVisualStyleBackColor = true;
            this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
            // 
            // serviceTagTextBox
            // 
            this.serviceTagTextBox.Location = new System.Drawing.Point(313, 115);
            this.serviceTagTextBox.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.serviceTagTextBox.Name = "serviceTagTextBox";
            this.serviceTagTextBox.Size = new System.Drawing.Size(623, 38);
            this.serviceTagTextBox.TabIndex = 1;
            this.serviceTagTextBox.TextChanged += new System.EventHandler(this.serviceTagTextBox_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(107, 118);
            this.label2.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(174, 32);
            this.label2.TabIndex = 21;
            this.label2.Text = "Service Tag:";
            // 
            // computerNameTextBox
            // 
            this.computerNameTextBox.Location = new System.Drawing.Point(313, 33);
            this.computerNameTextBox.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.computerNameTextBox.Name = "computerNameTextBox";
            this.computerNameTextBox.Size = new System.Drawing.Size(623, 38);
            this.computerNameTextBox.TabIndex = 0;
            this.computerNameTextBox.TextChanged += new System.EventHandler(this.computerNameTextBox_TextChanged);
            // 
            // lookupButton
            // 
            this.lookupButton.Enabled = false;
            this.lookupButton.Location = new System.Drawing.Point(958, 16);
            this.lookupButton.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.lookupButton.Name = "lookupButton";
            this.lookupButton.Size = new System.Drawing.Size(200, 103);
            this.lookupButton.TabIndex = 3;
            this.lookupButton.Text = "Lookup";
            this.lookupButton.UseVisualStyleBackColor = true;
            this.lookupButton.Click += new System.EventHandler(this.lookupButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(59, 36);
            this.label1.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(229, 32);
            this.label1.TabIndex = 18;
            this.label1.Text = "Computer Name:";
            // 
            // resultsTextBox
            // 
            this.resultsTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.resultsTextBox.Location = new System.Drawing.Point(36, 280);
            this.resultsTextBox.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.resultsTextBox.Multiline = true;
            this.resultsTextBox.Name = "resultsTextBox";
            this.resultsTextBox.ReadOnly = true;
            this.resultsTextBox.Size = new System.Drawing.Size(1122, 467);
            this.resultsTextBox.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1185, 777);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ipAddressTextBox);
            this.Controls.Add(this.resetButton);
            this.Controls.Add(this.serviceTagTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.computerNameTextBox);
            this.Controls.Add(this.lookupButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.resultsTextBox);
            this.Name = "Form1";
            this.Text = "Dell Warranty Checker";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox ipAddressTextBox;
        private System.Windows.Forms.Button resetButton;
        private System.Windows.Forms.TextBox serviceTagTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox computerNameTextBox;
        private System.Windows.Forms.Button lookupButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox resultsTextBox;
    }
}

