namespace WindowsFormsApp1
{
	partial class BrowserForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose( bool disposing )
		{
			if( disposing && ( components != null ) )
			{
				components.Dispose();
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.BrowserControl = new MtrDev.WebView2.Winforms.WebView2Control();
			this.SuspendLayout();
			// 
			// BrowserControl
			// 
			this.BrowserControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.BrowserControl.Location = new System.Drawing.Point(0, 0);
			this.BrowserControl.Name = "BrowserControl";
			this.BrowserControl.Size = new System.Drawing.Size(552, 509);
			this.BrowserControl.TabIndex = 0;
			// 
			// BrowserForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(552, 509);
			this.Controls.Add(this.BrowserControl);
			this.Name = "BrowserForm";
			this.Text = "BrowserForm";
			this.Load += new System.EventHandler(this.BrowserForm_Load);
			this.ResumeLayout(false);

		}



		#endregion

		private MtrDev.WebView2.Winforms.WebView2Control BrowserControl;
	}
}

