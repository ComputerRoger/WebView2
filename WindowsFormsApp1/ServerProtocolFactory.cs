﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AsyncSockets;
using GeneralClassLibrary;

namespace BrowserFormServer
{
	public class ServerProtocolFactory : IProtocolFactory
	{
		public IServerProtocol CreateServerProtocol( Socket clientSocket, ILogger logger )
		{
			return new BrowserServerProtocol( clientSocket, logger );
		}
	}
}

