using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsSimple
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void label1_Click( object sender, EventArgs e )
		{

		}

		private void button1_Click( object sender, EventArgs e )
		{
			Thread thread = new Thread( new ThreadStart( this.ThreadLoop ) );
			thread.IsBackground = true;
			thread.Start();
		}

		private void ThreadLoop()
		{
			//int stp;
			//int newval;
			//Random rnd = new Random();

			//while( true )
			//{
			//	stp = this.progressBar1.Step * rnd.Next( -1, 2 );
			//	newval = this.progressBar1.Value + stp;
			//	if( newval > this.progressBar1.Maximum )
			//		newval = this.progressBar1.Maximum;
			//	else if( newval < this.progressBar1.Minimum )
			//		newval = this.progressBar1.Minimum;
			//	this.progressBar1.Value = newval;
			//	Thread.Sleep( 100 );
			//}
		}

		private void webView2Control1_Click( object sender, EventArgs e )
		{

		}
	}
}
