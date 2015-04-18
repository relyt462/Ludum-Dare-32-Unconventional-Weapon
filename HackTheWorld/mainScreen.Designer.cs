namespace HackTheWorld
{
	partial class mainScreen
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
			this.inputTextBox = new System.Windows.Forms.TextBox();
			this.output = new HackTheWorld.ConsoleStyleLabel();
			this.SuspendLayout();
			// 
			// inputTextBox
			// 
			this.inputTextBox.BackColor = System.Drawing.SystemColors.WindowText;
			this.inputTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.inputTextBox.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.inputTextBox.Font = new System.Drawing.Font("Consolas", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.inputTextBox.ForeColor = System.Drawing.Color.LimeGreen;
			this.inputTextBox.Location = new System.Drawing.Point(0, 504);
			this.inputTextBox.Name = "inputTextBox";
			this.inputTextBox.Size = new System.Drawing.Size(512, 30);
			this.inputTextBox.TabIndex = 1;
			this.inputTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
			// 
			// output
			// 
			this.output.Dock = System.Windows.Forms.DockStyle.Fill;
			this.output.Font = new System.Drawing.Font("Consolas", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.output.ForeColor = System.Drawing.Color.LimeGreen;
			this.output.Location = new System.Drawing.Point(0, 0);
			this.output.Name = "output";
			this.output.Size = new System.Drawing.Size(512, 504);
			this.output.TabIndex = 2;
			// 
			// mainScreen
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.WindowText;
			this.ClientSize = new System.Drawing.Size(512, 534);
			this.Controls.Add(this.output);
			this.Controls.Add(this.inputTextBox);
			this.Name = "mainScreen";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "mainScreen";
			this.Activated += new System.EventHandler(this.mainScreen_Activated);
			this.Shown += new System.EventHandler(this.mainScreen_Shown);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox inputTextBox;
		private ConsoleStyleLabel output;
	}
}