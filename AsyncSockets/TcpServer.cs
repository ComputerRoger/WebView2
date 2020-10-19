using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using GeneralClassLibrary;

namespace AsyncSockets
{
	public class TcpServer
	{
		public TcpServer( int serverPort, IThreadDispatcher threadDispatcher, IProtocolFactory protocolFactory, ILogger logger )
		{
			ServerPort = serverPort;
			ThreadDispatcher = threadDispatcher;
			ProtocolFactory = protocolFactory;
			Logger = logger;
		}

		#region Properties.
		
		public int ServerPort { get; protected set; }
		public IThreadDispatcher ThreadDispatcher { get; protected set; }
		public ILogger Logger { get; protected set; }
		public IProtocolFactory ProtocolFactory { get; protected set; }
		#endregion

		//	Service Tcp/IP requests.
		public void StartMethod()
		{
			//	Stopping the main thread will terminate the program.
			System.Threading.Thread.CurrentThread.IsBackground = true;

			Logger.WriteEntry( "The TCP Server is starting." );

			//	Start listening for connections.
			TcpListener tcpListener = new TcpListener( IPAddress.Any, ServerPort );
			tcpListener.Start();

			//	Dispatch service to each connection.
			ThreadDispatcher.StartDispatching( tcpListener, Logger, ProtocolFactory );
		}
	}
}
