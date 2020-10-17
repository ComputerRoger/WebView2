using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncSockets
{
	public interface IServerProtocol
	{
		void HandleClientConnection();
	}

	public interface IProtocolFactory
	{
		IServerProtocol CreateServerProtocol( Socket clientSocket, ILogger logger );
	}
}

