using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace AsyncSockets
{
	public class BrowserServerProtocol : IServerProtocol
	{
		public BrowserServerProtocol( Socket clientSocket, ILogger logger )
		{
			ClientSocket = clientSocket;
			Logger = logger;
		}

		public Socket ClientSocket { get; protected set; }
		public ILogger Logger { get; protected set; }

		//	Do application specific work here.
		//	It could be a looped protocol exchanging a series of instructions.
		//	Or it could be a simple receive and reply.
		//	The client should close the socket.
		//	However, in HTTP, it is the server that closes the connection.
		//	That is why HTTP is a connectionless protocol.
		public void HandleClientConnection()
		{
			ILogger logger;
			Socket clientSocket;

			clientSocket = ClientSocket;
			logger = Logger;

			//	Get the input buffer.
			AsyncReceive asyncReceive = new AsyncReceive( clientSocket, logger );
			string receiveText = asyncReceive.Receive();
			if( receiveText.Length > 0 )
			{
				logger.WriteEntry( receiveText );
			}

			//	Decode the received text into an application specific command.

			//	Depending on the application specific protocol,
			//	Shutdown() can be used to close receive/send/both actions.

			//	Bottom line is that business logic determines who closes a socket.
			//	Do work here and build a response.

			//	QNX maintained an open socket with send/receive/reply as the protocol.
			//	In QNX, the client determines when a socket is closed, not the server.

			string sendText = "This is a message from the server.";

			AsyncSend asyncSend = new AsyncSend( clientSocket, logger );
			asyncSend.Send( sendText );
		}
	}

	public class BrowserServerProtocolFactory : IProtocolFactory
	{
		public BrowserServerProtocolFactory() { }
		public IServerProtocol CreateServerProtocol( Socket clientSocket, ILogger logger )
		{
			IServerProtocol serverProtocol = new BrowserServerProtocol( clientSocket, logger );
			return ( serverProtocol );
		}
	}
}
