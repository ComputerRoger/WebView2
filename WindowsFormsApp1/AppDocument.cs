using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
	public class AppDocument
	{

		BrowserDocument m_BrowserDocument;

		public AppDocument()
		{
			m_BrowserDocument = new BrowserDocument();
		}

		public BrowserDocument BrowserDocument
		{
			get
			{
				return ( m_BrowserDocument );
			}
		}
	}
}
