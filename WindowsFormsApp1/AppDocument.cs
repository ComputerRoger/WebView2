using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncSockets
{
	public interface IAppDocument
	{
		IBrowserDocument BrowserDocument { get; }
	}

	public class AppDocument : IAppDocument
	{
		readonly IBrowserDocument m_BrowserDocument;
		readonly MainForm m_MainForm;

		public AppDocument( IBrowserDocument browserDocument )
		{
			m_BrowserDocument = browserDocument;
			m_MainForm = new MainForm( this );
		}

		public IBrowserDocument BrowserDocument
		{
			get
			{
				return ( m_BrowserDocument );
			}
		}
		public MainForm MainForm
		{
			get
			{
				return m_MainForm;
			}
		}
	}
}
