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
	public class SocketServer : System.Windows.Forms.Form
	{
		private System.Windows.Forms.RichTextBox richTextBoxReceivedMsg;
		private System.Windows.Forms.Label label5;
		
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
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			// Display the local IP address on the GUI
			//textBoxIP.Text = GetIP();
			this.StartListen();
		}
		
		[STAThread]
		public static void Main(string[] args)
		{
			Application.Run(new SocketServer());
		}
		
		#region Windows Forms Designer generated code
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent() 
		{
			this.label5 = new System.Windows.Forms.Label();
			this.richTextBoxReceivedMsg = new System.Windows.Forms.RichTextBox();
			this.SuspendLayout();
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(138, 9);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(192, 16);
			this.label5.TabIndex = 10;
			this.label5.Text = "Message Received From Clients";
			// 
			// richTextBoxReceivedMsg
			// 
			this.richTextBoxReceivedMsg.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
			this.richTextBoxReceivedMsg.Location = new System.Drawing.Point(138, 28);
			this.richTextBoxReceivedMsg.Name = "richTextBoxReceivedMsg";
			this.richTextBoxReceivedMsg.ReadOnly = true;
			this.richTextBoxReceivedMsg.Size = new System.Drawing.Size(192, 140);
			this.richTextBoxReceivedMsg.TabIndex = 9;
			this.richTextBoxReceivedMsg.Text = "";
			// 
			// SocketServer
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(340, 205);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.richTextBoxReceivedMsg);
			this.Name = "SocketServer";
			this.Text = "SocketServer";
			this.ResumeLayout(false);
		}
		#endregion
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
				AppendToRichEditControl(msg + szData);

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
					AppendToRichEditControl(msg);

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
		// This method could be called by either the main thread or any of the
		// worker threads
		private void AppendToRichEditControl(string msg) 
		{
			// Check to see if this method is called from a thread 
			// other than the one created the control
			if (InvokeRequired) 
			{
				// We cannot update the GUI on this thread.
				// All GUI controls are to be updated by the main (GUI) thread.
				// Hence we will use the invoke method on the control which will
				// be called when the Main thread is free
				// Do UI update on UI thread
				object[] pList = {msg};
				richTextBoxReceivedMsg.BeginInvoke(new UpdateRichEditCallback(OnUpdateRichEdit), pList);
			}
			else
			{
				// This is the main thread which created this control, hence update it
				// directly 
				OnUpdateRichEdit(msg);
			}
		}
		// This UpdateRichEdit will be run back on the UI thread
		// (using System.EventHandler signature
		// so we don't need to define a new
		// delegate type here)
		private void OnUpdateRichEdit(string msg) 
		{
			richTextBoxReceivedMsg.AppendText(msg);
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

		private void btnClear_Click(object sender, System.EventArgs e)
		{
			richTextBoxReceivedMsg.Clear();
		}
	}
}
