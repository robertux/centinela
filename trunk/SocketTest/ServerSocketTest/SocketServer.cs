/*
 * Created by SharpDevelop.
 * User: Jayan Nair
 * Date: 02/01/2005
 * Time: 2:54 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Collections;
using System.Threading;

namespace DefaultNamespace
{
	/// <summary>
	/// Description of SocketServer.	
	/// </summary>
	public class SocketServer 
	{
		public delegate void UpdateRichEditCallback(string text);
		public delegate void UpdateClientListCallback();
				
		public AsyncCallback pfnWorkerCallBack ;
		private  Socket m_mainSocket;

		// An ArrayList is used to keep track of worker sockets that are designed
		// to communicate with each connected client. Make it a synchronized ArrayList
		// For thread safety
		private System.Collections.ArrayList m_workerSocketList = 
				ArrayList.Synchronized(new System.Collections.ArrayList());

		// The following variable will keep track of the cumulative 
		// total number of clients connected at any time. Since multiple threads
		// can access this variable, modifying this variable should be done
		// in a thread safe manner
		private int m_clientCount = 0;


		public SocketServer()
		{
			// Display the local IP address on the GUI
			//textBoxIP.Text = GetIP();
			this.StartListen();
		}
		
		public void StartListen()
		{
			try
			{				
				int port = 8000;
				// Create the listening socket...
				m_mainSocket = new Socket(AddressFamily.InterNetwork, 
					SocketType.Stream, 
					ProtocolType.Tcp);
				IPEndPoint ipLocal = new IPEndPoint (IPAddress.Any, port);
				// Bind to local IP Address...
				m_mainSocket.Bind( ipLocal );
				// Start listening...
				m_mainSocket.Listen(4);
				// Create the call back for any client connections...
				m_mainSocket.BeginAccept(new AsyncCallback (OnClientConnect), null);
				
			}
			catch(SocketException se)
			{
				MessageBox.Show ( se.Message );
			}

		}
	
		// This is the call back function, which will be invoked when a client is connected
		public void OnClientConnect(IAsyncResult asyn)
		{
			try
			{
				// Here we complete/end the BeginAccept() asynchronous call
				// by calling EndAccept() - which returns the reference to
				// a new Socket object
				Socket workerSocket = m_mainSocket.EndAccept (asyn);

				// Now increment the client count for this client 
				// in a thread safe manner
				Interlocked.Increment(ref m_clientCount);
				
				// Add the workerSocket reference to our ArrayList
				m_workerSocketList.Add(workerSocket);

				// Let the worker Socket do the further processing for the 
				// just connected client
				WaitForData(workerSocket, m_clientCount);
							
				// Since the main Socket is now free, it can go back and wait for
				// other clients who are attempting to connect
				m_mainSocket.BeginAccept(new AsyncCallback ( OnClientConnect ),null);
			}
			catch(ObjectDisposedException)
			{
				System.Diagnostics.Debugger.Log(0,"1","\n OnClientConnection: Socket has been closed\n");
			}
			catch(SocketException se)
			{
				MessageBox.Show ( se.Message );
			}
			
		}
		//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
		public class SocketPacket
		{
			// Constructor which takes a Socket and a client number
			public SocketPacket(System.Net.Sockets.Socket socket, int clientNumber)
			{
				m_currentSocket = socket;
				m_clientNumber  = clientNumber;
			}
			public System.Net.Sockets.Socket m_currentSocket;
			public int m_clientNumber;
			// Buffer to store the data sent by the client
			public byte[] dataBuffer = new byte[1024];
		}
		
		//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
		// Start waiting for data from the client
		public void WaitForData(System.Net.Sockets.Socket soc, int clientNumber)
		{
			try
			{
				if  ( pfnWorkerCallBack == null )
				{		
					// Specify the call back function which is to be 
					// invoked when there is any write activity by the 
					// connected client
					pfnWorkerCallBack = new AsyncCallback (OnDataReceived);
				}
				SocketPacket theSocPkt = new SocketPacket (soc, clientNumber);

				soc.BeginReceive (theSocPkt.dataBuffer, 0, 
					theSocPkt.dataBuffer.Length,
					SocketFlags.None,
					pfnWorkerCallBack,
					theSocPkt);
			}
			catch(SocketException se)
			{
				MessageBox.Show (se.Message );
			}
		}
		// This the call back function which will be invoked when the socket
		// detects any client writing of data on the stream
		public  void OnDataReceived(IAsyncResult asyn)
		{
			SocketPacket socketData = (SocketPacket)asyn.AsyncState ;
			try
			{
				// Complete the BeginReceive() asynchronous call by EndReceive() method
				// which will return the number of characters written to the stream 
				// by the client
				int iRx  = socketData.m_currentSocket.EndReceive (asyn);
				char[] chars = new char[iRx +  1];
				// Extract the characters as a buffer
				System.Text.Decoder d = System.Text.Encoding.UTF8.GetDecoder();
				int charLen = d.GetChars(socketData.dataBuffer, 
					0, iRx, chars, 0);

				System.String szData = new System.String(chars);
				string msg = "" + socketData.m_clientNumber + ":";
//	//	//	//	//AppendToRichEditControl(msg + szData);

				// Send back the reply to the client
				string replyMsg = "Server Reply:" + szData.ToUpper(); 
				// Convert the reply to byte array
				byte[] byData = System.Text.Encoding.ASCII.GetBytes(replyMsg);

				Socket workerSocket = (Socket)socketData.m_currentSocket;
				workerSocket.Send(byData);
	
				// Continue the waiting for data on the Socket
				WaitForData(socketData.m_currentSocket, socketData.m_clientNumber );

			}
			catch (ObjectDisposedException )
			{
				System.Diagnostics.Debugger.Log(0,"1","\nOnDataReceived: Socket has been closed\n");
			}
			catch(SocketException se)
			{
				if(se.ErrorCode == 10054) // Error code for Connection reset by peer
				{	
					string msg = "Client " + socketData.m_clientNumber + " Disconnected" + "\n";					

					// Remove the reference to the worker socket of the closed client
					// so that this object will get garbage collected
					m_workerSocketList[socketData.m_clientNumber - 1] = null;					
				}
				else
				{
					MessageBox.Show (se.Message );
				}
			}
		}
	
		String GetIP()
		{	   
			String strHostName = Dns.GetHostName();
		
			// Find host by name
			IPHostEntry iphostentry = Dns.GetHostEntry(strHostName);
		
			// Grab the first IP addresses
			String IPStr = "";
			foreach(IPAddress ipaddress in iphostentry.AddressList)
			{
				IPStr = ipaddress.ToString();
				return IPStr;
			}
			return IPStr;
		}
		
		void CloseSockets()
		{
			if(m_mainSocket != null)
			{
				m_mainSocket.Close();
			}
			Socket workerSocket = null;
			for(int i = 0; i < m_workerSocketList.Count; i++)
			{
				workerSocket = (Socket)m_workerSocketList[i];
				if(workerSocket != null)
				{
					workerSocket.Close();
					workerSocket = null;
				}
			}	
		}
		
		void SendMsgToClient(string msg, int clientNumber)
		{
			// Convert the reply to byte array
			byte[] byData = System.Text.Encoding.ASCII.GetBytes(msg);

			Socket workerSocket = (Socket)m_workerSocketList[clientNumber - 1];
			workerSocket.Send(byData);
		}
	}
}
