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

namespace WindowsFormsApp1
{
	public partial class Form1 : Form
	{
		private AppDocument m_AppDocument;

		public Form1( AppDocument appDocument )
		{
			m_AppDocument = appDocument;

			InitializeComponent();
			HookupEvents();
			WebView.Navigate( "https:\\www.amazon.com" );
		}

		#region Propterties.

		protected AppDocument AppDocument => ( m_AppDocument );

		protected WebView2Control WebView => ( this.webView2Control1 );

		#endregion

		protected void HookupEvents()
		{
			WebView.NavigationCompleted += WebView_NavigationCompleted;
		}

		private void WebView_NavigationCompleted( object sender, MtrDev.WebView2.Wrapper.NavigationCompletedEventArgs e )
		{
			NavigationCompleted( sender, e );
		}

		private void Form1_Load( object sender, EventArgs e )
		{

		}

		protected void NavigationCompleted( object sender, MtrDev.WebView2.Wrapper.NavigationCompletedEventArgs e )
		{
			Action<ExecuteScriptCompletedEventArgs> scriptAction;
			BrowserDocument browserDocument;
			string javascriptText;

			browserDocument = AppDocument.BrowserDocument;
			scriptAction = new Action<ExecuteScriptCompletedEventArgs>( GetDomText );
			javascriptText = "document.documentElement.innerHTML";
			this.webView2Control1.ExecuteScript( javascriptText, scriptAction );
		}

		public void GetDomText( ExecuteScriptCompletedEventArgs executeScriptCompletedEventArgs )
		{
			BrowserDocument browserDocument;
			string jsonText;

			browserDocument = AppDocument.BrowserDocument;

			jsonText = executeScriptCompletedEventArgs.ResultAsJson;

			//	Replace the escaped text with normal characters.
			string unescapedText = System.Text.RegularExpressions.Regex.Unescape( jsonText );

			//	Remove the surrounding quotes.
			string htmlText = unescapedText.Substring( 1, unescapedText.Length - 2 );
			browserDocument.DomText = htmlText;

			string xmlText;
			xmlText = htmlToXml( htmlText );
			browserDocument.XmlText = xmlText;

			string clearHtml = BrowserDocument.RemoveNonContent( htmlText );
			string clearXml = BrowserDocument.RemoveNonContent( xmlText );
		}

		public string htmlToXml( string htmlText )
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

		//	Start navigation to the URI of the textbox.
		private void button1_Click( object sender, EventArgs e )
		{
			BrowserDocument browserDocument;
			Uri documentUri;
			bool isNavigate;
			string controlText = textBox1.Text;

			browserDocument = AppDocument.BrowserDocument;
			documentUri = browserDocument.Uri;
			if( documentUri == null )
			{
				if( Uri.IsWellFormedUriString( controlText, UriKind.Absolute ) )
				{
					documentUri = new Uri( controlText );
					isNavigate = true;
				}
				else
				{
					isNavigate = false;
				}
			}
			else
			{
				isNavigate = controlText.CompareTo( documentUri.OriginalString ) != 0;
			}
			if( isNavigate )
			{
				if( controlText.Length > 0 )
				{
					if( Uri.IsWellFormedUriString( controlText, UriKind.Absolute ) )
					{
						//	Tell the browser to navigate which will update the state of the DOM.
						browserDocument.Uri = new Uri( controlText );
						WebView.Navigate( controlText );

						//	NavigateToString is used to load the browser with HTML.
						//WebView.NavigateToString( controlText );
					}
					else
					{
						//	Ignore the badly formed URI.
					}
				}
			}
		}

		private void button2_Click( object sender, EventArgs e )
		{

		}


	}



	//public static string FilterHtml( string rawHtmlText, bool isBrowserSource, bool isKeepScriptCommentBlock )
	//{
	//	string text;
	//	string xmlText;

	//	text = rawHtmlText;

	//	text = WebPageLibrary.ConvertIso8859ToUtf8( text );

	//	//	Filter escaped white spaces.
	//	text = Regex.Replace( text, @"&nbsp;", @" " );
	//	text = Regex.Replace( text, @"\t", @" " );

	//	////	Replace Diacritics in UTF-8 with normalized characters.
	//	//text = World.RemoveDiacritics( text );

	//	//	Filter newlines.
	//	text = Regex.Replace( text, @"\r", @" ", RegexOptions.Singleline );
	//	text = Regex.Replace( text, @"\n", @" ", RegexOptions.Singleline );

	//	//	Filter script blocks.
	//	//	StartKey, Any number of characters but as few as possible between the the keys, EndKey.
	//	text = Regex.Replace( text, @"<script(.+?)/script>", @"", RegexOptions.Singleline );

	//	//	Filter style blocks.
	//	//	StartKey, Any number of characters but as few as possible between the the keys, EndKey.
	//	text = Regex.Replace( text, @"<style(.+?)/style>", @"", RegexOptions.Singleline );

	//	//	Remove HTML tags.
	//	text = Regex.Replace( text, @"<META(.+?)>", @"", RegexOptions.IgnoreCase | RegexOptions.Singleline );
	//	if( isKeepScriptCommentBlock )
	//	{
	//		//	Keep the script comments and transform contents to XML.
	//		text = Regex.Replace( text, @"<!--", @"", RegexOptions.IgnoreCase | RegexOptions.Singleline );
	//		text = Regex.Replace( text, @"-->", @"", RegexOptions.IgnoreCase | RegexOptions.Singleline );
	//	}
	//	else
	//	{
	//		//	Remove script comments.
	//		text = Regex.Replace( text, @"<\!--(.+?)-->", @"", RegexOptions.IgnoreCase | RegexOptions.Singleline );
	//	}
	//	text = Regex.Replace( text, @"<link(.+?)>", @"", RegexOptions.IgnoreCase | RegexOptions.Singleline );

	//	//				//	Remove Escaped quotes.
	//	//				string pat;
	//	//				pat = Regex.Escape( @"\\" );
	//	//				text = Regex.Replace( text, pat,						@"", RegexOptions.IgnoreCase );

	//	//	Filter Http formatting attributes.
	//	text = Regex.Replace( text, "valign=\"(.*?)\"", @"", RegexOptions.IgnoreCase );

	//	text = Regex.Replace( text, "align=\"(.*?)\"", @"", RegexOptions.IgnoreCase );
	//	text = Regex.Replace( text, "bgcolor=\"(.*?)\"", @"", RegexOptions.IgnoreCase );
	//	text = Regex.Replace( text, "border=\"(.*?)\"", @"", RegexOptions.IgnoreCase );
	//	text = Regex.Replace( text, "cellspacing=\"(.*?)\"", @"", RegexOptions.IgnoreCase );
	//	text = Regex.Replace( text, "cellpadding=\"(.*?)\"", @"", RegexOptions.IgnoreCase );
	//	text = Regex.Replace( text, "marginheight=\"(.*?)\"", @"", RegexOptions.IgnoreCase );
	//	text = Regex.Replace( text, "marginwidth=\"(.*?)\"", @"", RegexOptions.IgnoreCase );
	//	text = Regex.Replace( text, "leftmargin=\"(.*?)\"", @"", RegexOptions.IgnoreCase );
	//	text = Regex.Replace( text, "topmargin=\"(.*?)\"", @"", RegexOptions.IgnoreCase );
	//	text = Regex.Replace( text, "width=\"(.*?)\"", @"", RegexOptions.IgnoreCase );
	//	text = Regex.Replace( text, "src=\"(.*?)\"", @"", RegexOptions.IgnoreCase );
	//	text = Regex.Replace( text, "height=\"(.*?)\"", @"", RegexOptions.IgnoreCase );
	//	text = Regex.Replace( text, "colspan=\"(.*?)\"", @"", RegexOptions.IgnoreCase );
	//	text = Regex.Replace( text, "background=\"(.*?)\"", @"", RegexOptions.IgnoreCase );
	//	text = Regex.Replace( text, "nowrap=\"(.*?)\"", @"", RegexOptions.IgnoreCase );
	//	text = Regex.Replace( text, "onmouseout=\"(.*?)\"", @"", RegexOptions.IgnoreCase );
	//	text = Regex.Replace( text, "onmouseover=\"(.*?)\"", @"", RegexOptions.IgnoreCase );
	//	text = Regex.Replace( text, "onclick=\"(.*?)\"", @"", RegexOptions.IgnoreCase );
	//	text = Regex.Replace( text, "target=\"(.*?)\"", @"", RegexOptions.IgnoreCase );

	//	//	Remove local hrefs (#) but keep external hrefs.
	//	text = Regex.Replace( text, "href=\"#(.+?)\"", @"", RegexOptions.IgnoreCase );

	//	//	November 2006, DRF Version.
	//	text = Regex.Replace( text, "summary=\"(.*?)\"", @"", RegexOptions.IgnoreCase );
	//	text = Regex.Replace( text, "scope=\"(.*?)\"", @"", RegexOptions.IgnoreCase );

	//	//	Style attributes needed when scaling an image.
	//	//text = Regex.Replace( text, "style=\"(.*?)\"", @"", RegexOptions.IgnoreCase );

	//	//	Remove image tags.
	//	//text = Regex.Replace( text, "<img(.+?)>", @"", RegexOptions.IgnoreCase );

	//	//	Remove bold tags.
	//	text = Regex.Replace( text, "<b>", @"", RegexOptions.IgnoreCase );
	//	text = Regex.Replace( text, "</b>", @"", RegexOptions.IgnoreCase );

	//	//	Replace vertical bar separators with known delimiter string.
	//	text = Regex.Replace( text, "[|]", Delimiter, RegexOptions.IgnoreCase );

	//	//	Remove commas separating anchors.
	//	text = Regex.Replace( text, "/a>,( *)<a", @"/a><a", RegexOptions.IgnoreCase );

	//	//	Remove anchors containing JavaScript.
	//	//	StartKey, Any number of characters but as few as possible between the the keys, EndKey.
	//	text = Regex.Replace( text, @"<Script(.+?)/Script>", @"", RegexOptions.IgnoreCase );
	//	//text = Regex.Replace( text, @"<a(.+?)JavaScript(.+?)/a>", @"", RegexOptions.IgnoreCase );

	//	//	Remove breaks.
	//	text = Regex.Replace( text, @"<br>", @" ", RegexOptions.IgnoreCase );

	//	//	Remove space between tags.
	//	text = Regex.Replace( text, @">( +?)<", @"><", RegexOptions.IgnoreCase );
	//	//text = Regex.Replace( text, @"<td>( *)</td>",			@"<td></td>", RegexOptions.Singleline );

	//	//	Remove multiple spaces.
	//	text = Regex.Replace( text, @"(\ +)", @" ", RegexOptions.IgnoreCase );

	//	//	Remove null divisions.
	//	text = Regex.Replace( text, "<div >", @"<div>", RegexOptions.IgnoreCase );
	//	text = Regex.Replace( text, "<div>( *?)</div>", @"", RegexOptions.IgnoreCase );

	//	//	Remove space between tags.
	//	text = Regex.Replace( text, @">( +?)<", @"><", RegexOptions.IgnoreCase );

	//	//	Remove null spans.
	//	text = Regex.Replace( text, "<span >", @"<span>", RegexOptions.IgnoreCase );
	//	text = Regex.Replace( text, "<span>( *?)</span>", @"", RegexOptions.IgnoreCase );

	//	//	Remove space between tags.
	//	text = Regex.Replace( text, @">( +?)<", @"><", RegexOptions.IgnoreCase );

	//	//	Remove null divisions.
	//	text = Regex.Replace( text, "<div >", @"<div>", RegexOptions.IgnoreCase );
	//	text = Regex.Replace( text, "<div>( *?)</div>", @"", RegexOptions.IgnoreCase );

	//	//	Remove space between tags.
	//	text = Regex.Replace( text, @">( +?)<", @"><", RegexOptions.IgnoreCase );

	//	//	December 2010.

	//	//	Keep Id and Class attributes.
	//	//				text = Regex.Replace( text, "class=\"(.*?)\"",			@"", RegexOptions.IgnoreCase );
	//	//				text = Regex.Replace( text, "id=\"(.*?)\"",				@"", RegexOptions.IgnoreCase );

	//	//	Transform encoded single-quote.
	//	string s;
	//	string singleQuote;
	//	singleQuote = @"&#039;";
	//	text = Regex.Replace( text, singleQuote, "'", RegexOptions.IgnoreCase );
	//	s = Regex.Match( text, "#039" ).Value;

	//	//	Transform to an HtmlDocument.
	//	try
	//	{
	//		HtmlAgilityPack.HtmlDocument htmlDocument;

	//		htmlDocument = new HtmlAgilityPack.HtmlDocument();
	//		if( isBrowserSource )
	//		{
	//			Stream stream;

	//			stream = GenerateStreamFromString( text );
	//			htmlDocument.OptionDefaultStreamEncoding = Encoding.UTF8;
	//			htmlDocument.Load( stream );
	//		}
	//		else
	//		{
	//			StringReader stringReader;

	//			stringReader = new System.IO.StringReader( text );
	//			htmlDocument.Load( stringReader );
	//		}

	//		//	Transform to XML string.
	//		System.IO.StringWriter stringWriter;
	//		stringWriter = new System.IO.StringWriter();
	//		htmlDocument.OptionOutputAsXml = true;
	//		htmlDocument.Save( stringWriter );
	//		xmlText = stringWriter.GetStringBuilder().ToString();

	//		//	Remove nulls.
	//		xmlText = Regex.Replace( xmlText,
	//			@"</html>(.+?)</span>",
	//			@"</html></span>",
	//			RegexOptions.IgnoreCase | RegexOptions.Singleline );
	//	}
	//	catch( System.Exception e )
	//	{
	//		text = e.Message;
	//		xmlText = "";
	//	}
	//	return ( xmlText );
	//}

	//	}
}
