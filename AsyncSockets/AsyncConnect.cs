using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;

namespace AsyncSockets
{
	public class AsyncConnect
	{
		public AsyncConnect( string serverName, int serverPort, ILogger logger )
		{
			ServerName = serverName;
			ServerPort = serverPort;
			Logger = logger;
			ClientSocket = null;
			ConnectDoneEvent = new ManualResetEvent( false );
		}

		#region Properties.

		public ManualResetEvent ConnectDoneEvent { get; protected set; } = new ManualResetEvent( false );
		public Socket ClientSocket { get; protected set; } = null;
		public string ServerName { get; protected set; }
		public int ServerPort { get; protected set; }
		public ILogger Logger { get; protected set; }

		#endregion

		protected void Connect()
		{
			Socket clientSocket;

			// Connect to a remote device.  
			try
			{
				// Establish the remote endpoint for the socket.  
				IPHostEntry ipHostInfo = Dns.GetHostEntry( ServerName );
				IPAddress ipAddress = ipHostInfo.AddressList[ 0 ];
				IPEndPoint serverEndPoint = new IPEndPoint( ipAddress, ServerPort );

				// Create a TCP/IP socket. 
				clientSocket = new Socket( ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp );

				// Connect to the remote endpoint.  
				clientSocket.BeginConnect( serverEndPoint, new AsyncCallback( ConnectCallback ), clientSocket );
				ConnectDoneEvent.WaitOne();
			}
			catch( Exception e )
			{
				Logger.WriteEntry( e.ToString() );
			}
		}

		private void ConnectCallback( IAsyncResult asyncResult )
		{
			Socket clientSocket;
			try
			{
				// Retrieve the socket from the state object.  
				clientSocket = ( Socket ) asyncResult.AsyncState;

				// Complete the connection.  
				clientSocket.EndConnect( asyncResult );

				//	Save the result as a property.
				this.ClientSocket = clientSocket;

				// Signal that the connection has been made.  
				ConnectDoneEvent.Set();
			}
			catch( Exception e )
			{
				Console.WriteLine( e.ToString() );
			}
		}
	}
}
