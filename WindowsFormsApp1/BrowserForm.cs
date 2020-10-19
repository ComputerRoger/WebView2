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

namespace BrowserFormServer
{
	public partial class BrowserForm : Form
	{
		private readonly IAppDocument m_AppDocument;

		public BrowserForm( IAppDocument appDocument )
		{
			m_AppDocument = appDocument;

			InitializeComponent();
			HookupEvents();

			//BrowserControl.Size = new Size( 0,0 );
			Navigate( @"https://this-page-intentionally-left-blank.org/" );
			//Navigate( @"https://www.amazon.com" );
		}

		#region Propterties.

		protected IAppDocument AppDocument => ( m_AppDocument );

		#endregion

		#region Methods.

		public void Navigate( string url )
		{
			BrowserControl.Navigate( url );
		}

		public string HtmlToXml( string htmlText )
		{
			HtmlAgilityPack.HtmlDocument htmlDocument;
			string xmlText;

			//	Create an HTML document.
			htmlDocument = new HtmlAgilityPack.HtmlDocument();
			htmlDocument.LoadHtml( htmlText );

			//	Use HTML Agility Pack to transform the HTML to XML.
			htmlDocument.OptionOutputAsXml = true;
			System.IO.TextWriter stringWriter;
			stringWriter = new System.IO.StringWriter();
			htmlDocument.Save( stringWriter );
			xmlText = stringWriter.ToString();

			return xmlText;
		}

		public void GetDomText( ExecuteScriptCompletedEventArgs executeScriptCompletedEventArgs )
		{
			IBrowserDocument browserDocument;
			string jsonText;

			browserDocument = AppDocument.BrowserDocument;

			jsonText = executeScriptCompletedEventArgs.ResultAsJson;

			//	Replace the escaped text with normal characters.
			string unescapedText = System.Text.RegularExpressions.Regex.Unescape( jsonText );

			//	Remove the surrounding quotes.
			string htmlText = unescapedText.Substring( 1, unescapedText.Length - 2 );
			browserDocument.DomText = htmlText;

			string xmlText;
			xmlText = HtmlToXml( htmlText );
			browserDocument.XmlText = xmlText;

			//string clearHtml = BrowserDocument.RemoveNonContent( htmlText );
			//string clearXml = BrowserDocument.RemoveNonContent( xmlText );
		}

		protected void NavigationCompleted( object sender, MtrDev.WebView2.Wrapper.NavigationCompletedEventArgs e )
		{
			Action<ExecuteScriptCompletedEventArgs> scriptAction;
			IBrowserDocument browserDocument;
			string javascriptText;

			browserDocument = AppDocument.BrowserDocument;
			scriptAction = new Action<ExecuteScriptCompletedEventArgs>( GetDomText );
			javascriptText = "document.documentElement.innerHTML";
			this.BrowserControl.ExecuteScript( javascriptText, scriptAction );
		}
		#endregion

		#region Events.

		protected void HookupEvents()
		{
			BrowserControl.NavigationCompleted += BrowserControl_NavigationCompleted;
		}

		private void BrowserForm_Load( object sender, EventArgs e )
		{
		}

		private void BrowserControl_NavigationCompleted( object sender, MtrDev.WebView2.Wrapper.NavigationCompletedEventArgs e )
		{
			NavigationCompleted( sender, e );
		}
		#endregion
	}
}
