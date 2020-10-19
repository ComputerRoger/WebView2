using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using GeneralClassLibrary;

namespace AsyncSockets
{
	public class AsyncSend
	{
		public AsyncSend( Socket connectedSocket, ILogger logger )
		{
			ConnectedSocket = connectedSocket;
			Logger = logger;
			InitializeAsyncSend();
		}

		#region Properties.
		public Socket ConnectedSocket { get; protected set; }
		public ILogger Logger { get; protected set; }
		public ManualResetEvent SendDoneEvent { get; protected set; } = new ManualResetEvent( false );

		#endregion

		protected void InitializeAsyncSend()
		{
			SendDoneEvent = new ManualResetEvent( false );
		}

		public void Send( String sendText )
		{
			// Convert the text to byte data using ASCII encoding.  
			byte[] byteData = Encoding.ASCII.GetBytes( sendText );

			// Begin sending the data to the remote device.  
			SocketFlags socketFlags;
			int offset;

			offset = 0;
			socketFlags = SocketFlags.None;
			ConnectedSocket.BeginSend( byteData, offset, byteData.Length, socketFlags,
				new AsyncCallback( SendCallback ), ConnectedSocket );

			//	Wait for the completion signal.
			SendDoneEvent.WaitOne();
		}

		private void SendCallback( IAsyncResult asyncResult )
		{
			try
			{
				// Retrieve the socket from the state object.  
				Socket connectedSocket = ( Socket ) asyncResult.AsyncState;

				// Complete sending the data to the remote device.  
				int bytesSent = connectedSocket.EndSend( asyncResult );

				string logMessage = String.Format( "Sent {0} bytes to server.", bytesSent );
				Logger.WriteEntry( logMessage );

				// Signal that all bytes have been sent.  
				SendDoneEvent.Set();
			}
			catch( Exception e )
			{
				Logger.WriteEntry( e.ToString() );
			}
		}
	}
}
