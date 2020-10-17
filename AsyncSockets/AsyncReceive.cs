using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;

namespace AsyncSockets
{
	public class AsyncReceive
	{
		// State object for receiving data.  
		protected class AsyncReceiveState
		{
			public int SizeTcpByteArray { get; protected set; }
			public byte[] ReceiveBuffer { get; protected set; }
			public StringBuilder Sb { get; protected set; }

			public AsyncReceiveState()
			{
				InitializeAsyncReceiveState();
			}

			public void InitializeAsyncReceiveState()
			{
				SizeTcpByteArray = Properties.Settings.Default.SizeTcpByteArray;
				Sb = new StringBuilder();
				ReceiveBuffer = new byte[ SizeTcpByteArray ];
			}
		}

		public AsyncReceive( Socket connectedSocket, ILogger logger )
		{
			ConnectedSocket = connectedSocket;
			Logger = logger;
			InitializeAsyncReceive();
		}

		#region Properties.
		public Socket ConnectedSocket { get; protected set; }
		public ILogger Logger { get; protected set; }

		protected ManualResetEvent ReceiveDoneEvent { get; set; } = new ManualResetEvent( false );
		public string ReceivedText { get; protected set; } = "";
		protected AsyncReceiveState ReceiveState { get; set; }

		#endregion

		protected void InitializeAsyncReceive()
		{
			ReceivedText = "";
			ReceiveDoneEvent.Reset();
			ReceiveState = new AsyncReceiveState();
		}

		private void BeginReceive()
		{
			SocketFlags socketFlags;
			int offset;

			offset = 0;
			socketFlags = SocketFlags.None;
			ConnectedSocket.BeginReceive( ReceiveState.ReceiveBuffer, offset, ReceiveState.SizeTcpByteArray, socketFlags,
				new AsyncCallback( ReceiveCallback ), ReceiveState );
		}

		public string Receive()
		{
			try
			{
				//	Initialize the receive state.
				InitializeAsyncReceive();

				//	Begin receiving the data from the remote device.
				BeginReceive();

				//	Wait for the final callback signal.
				ReceiveDoneEvent.WaitOne();
			}
			catch( Exception e )
			{
				Logger.WriteEntry( e.ToString() );
			}
			return ( this.ReceivedText );
		}


		private void ReceiveCallback( IAsyncResult asyncResult )
		{
			try
			{
				// Retrieve the state object and the clientSocket socket from the asynchronous state object.  
				AsyncReceiveState asyncReceiveState = ( AsyncReceiveState ) asyncResult.AsyncState;

				// Read data from the remote device.  
				int bytesRead = ConnectedSocket.EndReceive( asyncResult );

				if( bytesRead > 0 )
				{
					// Accumulate the received text. 
					string receivedText = Encoding.ASCII.GetString( asyncReceiveState.ReceiveBuffer, 0, bytesRead );
					asyncReceiveState.Sb.Append( receivedText );

					// Recursively get the rest of the data.  
					BeginReceive();
				}
				else
				{
					//	All the text has arrived.  
					if( asyncReceiveState.Sb.Length > 1 )
					{
						this.ReceivedText = asyncReceiveState.Sb.ToString();
					}
					//	Signal the completion of the message.  
					ReceiveDoneEvent.Set();
				}
			}
			catch( Exception e )
			{
				Logger.WriteEntry( e.ToString() );
			}
		}
	}
}
