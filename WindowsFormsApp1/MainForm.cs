using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MtrDev.WebView2.Winforms;
using MtrDev.WebView2.Wrapper;
using HtmlAgilityPack;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;

namespace WindowsFormsApp1
{
	public class MainForm : Form
	{
		private System.ComponentModel.IContainer components = null;
		private IAppDocument m_AppDocument;

		public MainForm( IAppDocument appDocument )
		{
			m_AppDocument = appDocument;

			InitializeComponent();
			HookupEvents();
		}

		#region Propterties.

		protected IAppDocument AppDocument => ( m_AppDocument );
		#endregion

		#region Methods.

		private void InitializeComponent()
		{
			this.SuspendLayout();

			this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size( 552, 509 );

			this.Name = "MainForm";
			this.Text = "MainForm";
			this.ResumeLayout( false );

		}

		protected override void Dispose( bool disposing )
		{
			if( disposing && ( components != null ) )
			{
				components.Dispose();
			}
			base.Dispose( disposing );
		}
		#endregion

		#region Event Handlers.

		protected void HookupEvents()
		{
			this.Load += MainForm_Load;
		}

		private void MainForm_Load( object sender, EventArgs e )
		{
			this.ClientSize = new Size( 300, 0 );
		}
		#endregion

		#region Methods.

		public BrowserForm StartBrowserForm()
		{
			BrowserForm browserForm;

			//	Create a modeless window.
			browserForm = new BrowserForm( AppDocument );

			browserForm.Owner = this;
			browserForm.Show();
			return ( browserForm );
		}
		#endregion
	}
}

