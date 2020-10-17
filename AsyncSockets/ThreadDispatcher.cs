using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncSockets
{
	public interface IThreadDispatcher
	{
		void StartDispatching( TcpListener listener, ILogger logger, IProtocolFactory protocolFactory );
	}

	public class ClientThreadDispatcher : IThreadDispatcher
	{
		public void StartDispatching( TcpListener tcpListener, ILogger logger, IProtocolFactory protocolFactory )
		{
			IServerProtocol serverProtocol;
			tcpListener.Start();

			//	Run forever, accepting and spawning threads to service each connection.
			for(; ; )
			{
				try
				{
					//	Block waiting for a connection.
					Socket clientSocket = tcpListener.AcceptSocket();
					serverProtocol = protocolFactory.CreateServerProtocol( clientSocket, logger );

					//	A client has connected so create a thread to handle it.
					ThreadStart clientThreadStart = new ThreadStart( serverProtocol.HandleClientConnection );
					Thread clientThread = new Thread( clientThreadStart )
					{
						IsBackground = true
					};
					clientThread.Start();
					logger.WriteEntry( "Created and started Thread = " + clientThread.GetHashCode() );
				}
				catch( System.IO.IOException ioException )
				{
					logger.WriteEntry( "Exception = " + ioException.Message );
				}
			}
			//	Unreachable.
		}
	}

	public class PoolThreadDispatcher : IThreadDispatcher
	{
		private readonly int m_SizeThreadPool;

		public PoolThreadDispatcher()
		{
			this.m_SizeThreadPool = Properties.Settings.Default.SizeThreadPool;
		}
		public PoolThreadDispatcher( int sizeThreadPool )
		{
			this.m_SizeThreadPool = sizeThreadPool;
		}

		public void StartDispatching( TcpListener tcpListener, ILogger logger, IProtocolFactory protocolFactory )
		{
			tcpListener.Start();

			//	Create the limited number of threads accepting connections.
			for( int indexThread = 0; indexThread < this.m_SizeThreadPool; indexThread++ )
			{
				DispatchLoop dispatchLoop = new DispatchLoop( tcpListener, logger, protocolFactory );
				ThreadStart clientThreadStart = new ThreadStart( dispatchLoop.RunDispatcher );
				Thread clientThread = new Thread( clientThreadStart );
				clientThread.Start();
				clientThread.IsBackground = true;
				logger.WriteEntry( "Created and started Thread = " + clientThread.GetHashCode() );
			}
		}
	}

	public class DispatchLoop
	{
		private readonly TcpListener m_TcpListener;
		private readonly ILogger m_Logger;
		private readonly IProtocolFactory m_ProtocolFactory;

		public DispatchLoop( TcpListener tcpListener, ILogger logger, IProtocolFactory protocolFactory )
		{
			this.m_TcpListener = tcpListener;
			this.m_Logger = logger;
			this.m_ProtocolFactory = protocolFactory;
		}

		public void RunDispatcher()
		{
			IServerProtocol serverProtocol;

			//	Run forever, accepting connections to service.
			for(; ; )
			{
				try
				{
					//	Block waiting for a connection.
					Socket clientSocket = m_TcpListener.AcceptSocket();
					serverProtocol = this.m_ProtocolFactory.CreateServerProtocol( clientSocket, m_Logger );

					//	A client has connected so create a thread to handle it.
					serverProtocol.HandleClientConnection();
				}
				catch( System.IO.IOException ioException )
				{
					m_Logger.WriteEntry( "Exception = " + ioException.Message );
				}
			}
			//	Unreachable.
		}
	}
}

