'
' * Created by SharpDevelop.
' * User: Jayan Nair
' * Date: 02/01/2005
' * Time: 2:54 PM
' * 
' * To change this template use Tools | Options | Coding | Edit Standard Headers.
' 

Imports System
Imports System.Windows.Forms
Imports System.Net
Imports System.Net.Sockets
Imports System.Collections
Imports System.Threading
Imports System.Runtime.Serialization.Formatters.Binary
Imports Centinela.ClassLib

	Public Class SocketServer
		Public Delegate Sub UpdateRichEditCallback(ByVal text As String)
		Public Delegate Sub UpdateClientListCallback()
    Public Event DataReceived(ByVal nClient As Integer, ByVal data As Byte())

		Public pfnWorkerCallBack As AsyncCallback
		Private m_mainSocket As Socket

		' An ArrayList is used to keep track of worker sockets that are designed
		' to communicate with each connected client. Make it a synchronized ArrayList
		' For thread safety
		Private m_workerSocketList As System.Collections.ArrayList = ArrayList.Synchronized(New System.Collections.ArrayList())

		' The following variable will keep track of the cumulative 
		' total number of clients connected at any time. Since multiple threads
		' can access this variable, modifying this variable should be done
		' in a thread safe manner
		Private m_clientCount As Integer = 0


		Public Sub New()
			' Display the local IP address on the GUI
			'textBoxIP.Text = GetIP();
			Me.StartListen()
		End Sub

		Public Sub StartListen()
			Try
				Dim port As Integer = 8000
				' Create the listening socket...
				m_mainSocket = New Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
				Dim ipLocal As New IPEndPoint(IPAddress.Any, port)
				' Bind to local IP Address...
				m_mainSocket.Bind(ipLocal)
				' Start listening...
				m_mainSocket.Listen(4)
				' Create the call back for any client connections...

				m_mainSocket.BeginAccept(New AsyncCallback(addressof OnClientConnect), Nothing)
			Catch se As SocketException
				MessageBox.Show(se.Message)
			End Try

		End Sub

		' This is the call back function, which will be invoked when a client is connected
		Public Sub OnClientConnect(ByVal asyn As IAsyncResult)
			Try
				' Here we complete/end the BeginAccept() asynchronous call
				' by calling EndAccept() - which returns the reference to
				' a new Socket object
				Dim workerSocket As Socket = m_mainSocket.EndAccept(asyn)

				' Now increment the client count for this client 
				' in a thread safe manner
				Interlocked.Increment(m_clientCount)

				' Add the workerSocket reference to our ArrayList
				m_workerSocketList.Add(workerSocket)

				' Let the worker Socket do the further processing for the 
				' just connected client
				WaitForData(workerSocket, m_clientCount)

				' Since the main Socket is now free, it can go back and wait for
				' other clients who are attempting to connect
				m_mainSocket.BeginAccept(New AsyncCallback(addressof OnClientConnect), Nothing)
			Catch generatedExceptionName As ObjectDisposedException
				System.Diagnostics.Debugger.Log(0, "1", "" & Chr(10) & " OnClientConnection: Socket has been closed" & Chr(10) & "")
			Catch se As SocketException
				MessageBox.Show(se.Message)
			End Try

		End Sub
		'------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
		Public Class SocketPacket
			' Constructor which takes a Socket and a client number
			Public Sub New(ByVal socket As System.Net.Sockets.Socket, ByVal clientNumber As Integer)
				m_currentSocket = socket
				m_clientNumber = clientNumber
			End Sub
        Public m_currentSocket As System.Net.Sockets.Socket
			Public m_clientNumber As Integer
			' Buffer to store the data sent by the client
			Public dataBuffer As Byte() = New Byte(1023) {}
		End Class

		'------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
		' Start waiting for data from the client
		Public Sub WaitForData(ByVal soc As System.Net.Sockets.Socket, ByVal clientNumber As Integer)
			Try
				If pfnWorkerCallBack Is Nothing Then
					' Specify the call back function which is to be 
					' invoked when there is any write activity by the 
					' connected client
					pfnWorkerCallBack = New AsyncCallback(addressof OnDataReceived)
				End If
				Dim theSocPkt As New SocketPacket(soc, clientNumber)

				soc.BeginReceive(theSocPkt.dataBuffer, 0, theSocPkt.dataBuffer.Length, SocketFlags.None, pfnWorkerCallBack, theSocPkt)
			Catch se As SocketException
				MessageBox.Show(se.Message)
			End Try
		End Sub
		
		' This the call back function which will be invoked when the socket
		' detects any client writing of data on the stream
		Public Sub OnDataReceived(ByVal asyn As IAsyncResult)
			Dim socketData As SocketPacket = DirectCast(asyn.AsyncState, SocketPacket)
			Try
				' Complete the BeginReceive() asynchronous call by EndReceive() method
				' which will return the number of characters written to the stream 
				' by the client
            'Dim iRx As Integer = socketData.m_currentSocket.EndReceive(asyn)

            RaiseEvent DataReceived(socketData.m_clientNumber, socketData.dataBuffer)
            'ENVIAMOS PETICION DE REGRESO A CASA :)
            Dim Respuesta() As Byte = AccesoRemoto.ObjetoABinario(frmMaster.peticion)

            Dim workerSocket As Socket = DirectCast(socketData.m_currentSocket, Socket)
            workerSocket.Send(Respuesta)

            ' Seguimos esperando por los datos del Socket

				WaitForData(socketData.m_currentSocket, socketData.m_clientNumber)
			Catch generatedExceptionName As ObjectDisposedException
				System.Diagnostics.Debugger.Log(0, "1", "" & Chr(10) & "OnDataReceived: Socket has been closed" & Chr(10) & "")
			Catch se As SocketException
				If se.ErrorCode = 10054 Then
					' Error code for Connection reset by peer
					Dim msg As String = "Client " + socketData.m_clientNumber.ToString() + " Disconnected" + "" & Chr(10) & ""

					' Remove the reference to the worker socket of the closed client
					' so that this object will get garbage collected
					m_workerSocketList(socketData.m_clientNumber - 1) = Nothing
				Else
					MessageBox.Show(se.Message)
				End If
			End Try
		End Sub

		public Function GetIP() As String
			Dim strHostName As String = Dns.GetHostName()

			' Find host by name
			Dim iphostentry As IPHostEntry = Dns.GetHostEntry(strHostName)

			' Grab the first IP addresses
			Dim IPStr As String = ""
			For Each ipaddress As IPAddress In iphostentry.AddressList
				IPStr = ipaddress.ToString()
				Return IPStr
			Next
			Return IPStr
		End Function

		public Sub CloseSockets()
			If m_mainSocket IsNot Nothing Then
				m_mainSocket.Close()
			End If
			Dim workerSocket As Socket = Nothing
			For i As Integer = 0 To m_workerSocketList.Count - 1
				workerSocket = DirectCast(m_workerSocketList(i), Socket)
				If workerSocket IsNot Nothing Then
					workerSocket.Close()
					workerSocket = Nothing
				End If
			Next
		End Sub

    Public Sub SendMsgToClient(ByVal msg As String, ByVal clientNumber As Integer)
        ' Convert the reply to byte array
        Dim byData As Byte() = System.Text.Encoding.ASCII.GetBytes(msg)

        Dim workerSocket As Socket = DirectCast(m_workerSocketList(clientNumber - 1), Socket)
        workerSocket.Send(byData)
    End Sub

    Public Sub SendImgToClient(ByVal img As System.Drawing.Image, ByVal clientNumber As Integer)
        Dim ms As New System.IO.MemoryStream()
        img.Save(ms, System.Drawing.Imaging.ImageFormat.Png)
        Dim workerSocket As Socket = DirectCast(m_workerSocketList(clientNumber - 1), Socket)
        workerSocket.Send(ms.ToArray())
    End Sub

    ''AGREGADO: RAMAYAC
    'Public Sub SendReplyToClient(ByVal p As Peticion, ByVal clientNumber As Integer)
    '    Dim ms As New System.IO.MemoryStream()
    '    img.Save(ms, System.Drawing.Imaging.ImageFormat.Png)
    '    Dim workerSocket As Socket = DirectCast(m_workerSocketList(clientNumber - 1), Socket)
    '    workerSocket.Send(ms.ToArray())
    'End Sub

End Class
