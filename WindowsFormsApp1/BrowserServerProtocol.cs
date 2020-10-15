using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
	public interface IServerProtocol
	{
		void HandleClientConnection();
	}

	public class BrowserServerProtocol : IServerProtocol
	{
		public BrowserServerProtocol( Socket clientSocket, ILogger logger )
		{
			ClientSocket = clientSocket;
			Logger = logger;
		}

		public Socket ClientSocket { get; protected set; }
		public ILogger Logger { get; protected set; }
		public void HandleClientConnection()
		{
			//	Do work here.
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
