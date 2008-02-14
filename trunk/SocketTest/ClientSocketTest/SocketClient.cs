/*
 * Created by SharpDevelop.
 * User: Jayan Nair
 * Date: 7/9/2004
 * Time: 2:27 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;


namespace DefaultNamespace
{
	/// <summary>
	/// Description of SocketClient.	
	/// </summary>
	public class SocketClient : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button buttonDisconnect;
		private System.Windows.Forms.Button buttonConnect;
		private System.Windows.Forms.RichTextBox richTextRxMessage;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.RichTextBox richTextTxMessage;
		private System.Windows.Forms.Button buttonSendMessage;
		
		byte[] m_dataBuffer = new byte [10];
		IAsyncResult m_result;
		public AsyncCallback m_pfnCallBack ;
		public Socket m_clientSocket;		
		
		private string iP;
		private string port;
		
		public SocketClient()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			this.iP = "192.168.1.2";
			this.port = "8000";
			//textBoxIP.Text = GetIP();
		}
		
		[STAThread]
		public static void Main(string[] args)
		{
			Application.Run(new SocketClient());
		}
		
		#region Windows Forms Designer generated code
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent() {
			this.buttonSendMessage = new System.Windows.Forms.Button();
			this.richTextTxMessage = new System.Windows.Forms.RichTextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.richTextRxMessage = new System.Windows.Forms.RichTextBox();
			this.buttonConnect = new System.Windows.Forms.Button();
			this.buttonDisconnect = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// buttonSendMessage
			// 
			this.buttonSendMessage.Location = new System.Drawing.Point(8, 184);
			this.buttonSendMessage.Name = "buttonSendMessage";
			this.buttonSendMessage.Size = new System.Drawing.Size(240, 24);
			this.buttonSendMessage.TabIndex = 14;
			this.buttonSendMessage.Text = "Send Message";
			// 
			// richTextTxMessage
			// 
			this.richTextTxMessage.Location = new System.Drawing.Point(8, 80);
			this.richTextTxMessage.Name = "richTextTxMessage";
			this.richTextTxMessage.Size = new System.Drawing.Size(240, 96);
			this.richTextTxMessage.TabIndex = 2;
			this.richTextTxMessage.Text = "";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(8, 64);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(120, 16);
			this.label4.TabIndex = 9;
			this.label4.Text = "Message To Server";
			// 
			// richTextRxMessage
			// 
			this.richTextRxMessage.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
			this.richTextRxMessage.Location = new System.Drawing.Point(256, 80);
			this.richTextRxMessage.Name = "richTextRxMessage";
			this.richTextRxMessage.ReadOnly = true;
			this.richTextRxMessage.Size = new System.Drawing.Size(248, 128);
			this.richTextRxMessage.TabIndex = 1;
			this.richTextRxMessage.Text = "";
			// 
			// buttonConnect
			// 
			this.buttonConnect.BackColor = System.Drawing.SystemColors.HotTrack;
			this.buttonConnect.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.buttonConnect.ForeColor = System.Drawing.Color.Yellow;
			this.buttonConnect.Location = new System.Drawing.Point(344, 8);
			this.buttonConnect.Name = "buttonConnect";
			this.buttonConnect.Size = new System.Drawing.Size(72, 48);
			this.buttonConnect.TabIndex = 7;
			this.buttonConnect.Text = "Connect To Server";
			this.buttonConnect.UseVisualStyleBackColor = false;
			this.buttonConnect.Click += new System.EventHandler(this.ButtonConnectClick);
			// 
			// buttonDisconnect
			// 
			this.buttonDisconnect.BackColor = System.Drawing.Color.Red;
			this.buttonDisconnect.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.buttonDisconnect.ForeColor = System.Drawing.Color.Yellow;
			this.buttonDisconnect.Location = new System.Drawing.Point(432, 8);
			this.buttonDisconnect.Name = "buttonDisconnect";
			this.buttonDisconnect.Size = new System.Drawing.Size(72, 48);
			this.buttonDisconnect.TabIndex = 15;
			this.buttonDisconnect.Text = "Disconnet From Server";
			this.buttonDisconnect.UseVisualStyleBackColor = false;
			this.buttonDisconnect.Click += new System.EventHandler(this.ButtonDisconnectClick);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(256, 64);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(192, 16);
			this.label3.TabIndex = 8;
			this.label3.Text = "Message From Server";
			// 
			// SocketClient
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(512, 249);
			this.Controls.Add(this.buttonDisconnect);
			this.Controls.Add(this.buttonSendMessage);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.buttonConnect);
			this.Controls.Add(this.richTextTxMessage);
			this.Controls.Add(this.richTextRxMessage);
			this.Name = "SocketClient";
			this.Text = "Socket Client";
			this.ResumeLayout(false);
		}
		#endregion
		void Close()
		{
			if ( m_clientSocket != null )
			{
				m_clientSocket.Close ();
				m_clientSocket = null;
			}		
		}

        private void ButtonDisconnectClick(object sender, EventArgs e)
        {
            Close();
        }
		
		private void ButtonConnectClick(object sender, EventArgs e)
		{
			try
			{
				// Create the socket instance
				m_clientSocket = new Socket (AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp );
				
				// Cet the remote IP address
				IPAddress ip = IPAddress.Parse (this.iP);
				int iPortNo = System.Convert.ToInt16 (this.port);
				// Create the end point 
				IPEndPoint ipEnd = new IPEndPoint (ip,iPortNo);
				// Connect to the remote host
				m_clientSocket.Connect ( ipEnd );
				if(m_clientSocket.Connected) {
					
					//Wait for data asynchronously 
					WaitForData();
				}
			}
			catch(SocketException se)
			{
				string str;
				str = "\nConnection failed, is the server running?\n" + se.Message;
				MessageBox.Show (str);
			}		
		}
		
		public void SendMessage(string msg)
		{
			try
			{				
				// New code to send strings
				NetworkStream networkStream = new NetworkStream(m_clientSocket);
				System.IO.StreamWriter streamWriter = new System.IO.StreamWriter(networkStream);
				streamWriter.WriteLine(msg);
				streamWriter.Flush();
                //------------------------------------------------------------------------------------------
                byte[] data = new byte[500];                
                m_clientSocket.Receive(data);
                char[] chars = new char[data.Length + 1];
                System.Text.Decoder d = System.Text.Encoding.UTF8.GetDecoder();
                int charLen = d.GetChars(data, 0, data.Length, chars, 0);
                System.String szData = new System.String(chars);
                richTextRxMessage.Text = richTextRxMessage.Text + szData.ToString();

                //------------------------------------------------------------------------------------------

				/* Use the following code to send bytes
				byte[] byData = System.Text.Encoding.ASCII.GetBytes(objData.ToString ());
				if(m_clientSocket != null){
					m_clientSocket.Send (byData);
				}
				*/
			}
			catch(SocketException se)
			{
				MessageBox.Show (se.Message );
			}	
		}
		
		public void WaitForData()
		{
			try
			{
				if  ( m_pfnCallBack == null ) 
				{
					m_pfnCallBack = new AsyncCallback (OnDataReceived);
				}
				SocketPacket theSocPkt = new SocketPacket ();
				theSocPkt.thisSocket = m_clientSocket;
				// Start listening to the data asynchronously
				m_result = m_clientSocket.BeginReceive (theSocPkt.dataBuffer,
				                                        0, theSocPkt.dataBuffer.Length,
				                                        SocketFlags.None, 
				                                        m_pfnCallBack, 
				                                        theSocPkt);
			}
			catch(SocketException se)
			{
				MessageBox.Show (se.Message );
			}

		}
		
		public class SocketPacket
		{
			public System.Net.Sockets.Socket thisSocket;
			public byte[] dataBuffer = new byte[1024];
		}

		public  void OnDataReceived(IAsyncResult asyn)
		{
			try
			{
				SocketPacket theSockId = (SocketPacket)asyn.AsyncState ;
				int iRx  = theSockId.thisSocket.EndReceive (asyn);
				char[] chars = new char[iRx +  1];
				System.Text.Decoder d = System.Text.Encoding.UTF8.GetDecoder();
				int charLen = d.GetChars(theSockId.dataBuffer, 0, iRx, chars, 0);
				System.String szData = new System.String(chars);
				//richTextRxMessage.Text = richTextRxMessage.Text + szData.ToString();
				WaitForData();
			}
			catch (ObjectDisposedException )
			{
				System.Diagnostics.Debugger.Log(0,"1","\nOnDataReceived: Socket has been closed\n");
			}
			catch(SocketException se)
			{
				MessageBox.Show (se.Message );
			}
		}

   	   String GetIP()
	   {	   
	   		String strHostName = Dns.GetHostName();
		
		   	// Find host by name
		   	IPHostEntry iphostentry = Dns.GetHostEntry(strHostName);		   	
		
		   	// Grab the first IP addresses
		   	String IPStr = "";
		   	foreach(IPAddress ipaddress in iphostentry.AddressList){
		        IPStr = ipaddress.ToString();
		   		return IPStr;
		   	}
		   	return IPStr;
	   }	
	}
}
