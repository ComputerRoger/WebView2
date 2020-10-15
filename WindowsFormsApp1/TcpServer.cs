using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace WindowsFormsApp1
{
	public class TcpServer
	{
		public TcpServer( IAppDocument appDocument, IThreadDispatcher threadDispatcher )
		{
			AppDocument = appDocument;
			BrowserServerPort = Properties.Settings.Default.BrowserServerPort;
			ThreadDispatcher = threadDispatcher;
		}

		#region Properties.
		
		public IAppDocument AppDocument { get; protected set; }
		public int BrowserServerPort { get; protected set; }
		public IThreadDispatcher ThreadDispatcher { get; protected set; }
		#endregion

		//	Service Tcp/IP requests.
		public void StartMethod()
		{
			//	Stopping the main thread will terminate the program.
			System.Threading.Thread.CurrentThread.IsBackground = true;

			ILogger logger = new ConsoleLogger();
			logger.writeEntry( "The TCP Server is starting." );

			//	Start listening for connections.
			TcpListener tcpListener = new TcpListener( IPAddress.Any, BrowserServerPort );
			tcpListener.Start();

			//	Dispatch service to each connection.
			IProtocolFactory protocolFactory;
			protocolFactory = new ServerProtocolFactory();
			ThreadDispatcher.StartDispatching( tcpListener, logger, protocolFactory );
		}
	}
}
