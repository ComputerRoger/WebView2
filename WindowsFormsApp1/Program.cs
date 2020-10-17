using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Threading;

namespace AsyncSockets
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			TrialAndError.TestTrialAndError();

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault( false );

			//	Create the application document to maintain state.
			AppDocument appDocument;
			BrowserDocument browserDocument;
			MainForm mainForm;

			browserDocument = new BrowserDocument();
			appDocument = new AppDocument( browserDocument );
			mainForm = appDocument.MainForm;

			//	Provide a container for the TCP/IP server.
			int serverPort = Properties.Settings.Default.BrowserServerPort;
			int sizeThreadPool = Properties.Settings.Default.SizeThreadPool;
			IProtocolFactory protocolFactory = new BrowserServerProtocolFactory();
			ILogger logger = new ConsoleLogger();
			IThreadDispatcher threadDispatcher = new PoolThreadDispatcher( sizeThreadPool );
			TcpServer tcpServer = new TcpServer( serverPort, threadDispatcher, protocolFactory, logger );

			//	Serve TCP/IP on a separate thread.
			System.Threading.Thread serverThread;
			ThreadStart threadStart = new ThreadStart( tcpServer.StartMethod );
			serverThread = new Thread( threadStart );
			serverThread.Start();

			//	The MainForm, as the root control container, provides user interface capability.
			Application.Run( mainForm );
		}
	}
}

