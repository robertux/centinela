'
' * Created by SharpDevelop.
' * User: Jayan Nair
' * Date: 7/9/2004
' * Time: 2:27 PM
' * 
' * To change this template use Tools | Options | Coding | Edit Standard Headers.
' 

Imports System
Imports System.Windows.Forms
Imports System.Net
Imports System.Net.Sockets


	''' <summary>
	''' Description of SocketClient.	
	''' </summary>
	Public Class SocketClient

        Private m_dataBuffer As Byte() = New Byte(9) {}
        Private m_result As IAsyncResult
        Public m_pfnCallBack As AsyncCallback
        Public m_clientSocket As Socket
    'Public Event ReceiveData(ByVal data As String)
    'Public Event ReceiveImage(ByVal img As System.Drawing.Image)
    Public Event ReceiveData(ByVal data As Byte())

        Private iP As String
    Private port As String
    'Public recImgs As Boolean

        Public Sub New()
            Me.iP = "192.168.1.2"
            'textBoxIP.Text = GetIP();
        Me.port = "8000"
        'Me.recImgs = False
        End Sub

    Public Sub Close()
        If m_clientSocket IsNot Nothing Then
            m_clientSocket.Close()
            m_clientSocket = Nothing
        End If
    End Sub

        Public Sub Connect()
            Try
                ' Create the socket instance
                m_clientSocket = New Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)

                ' Cet the remote IP address
                Dim ip As IPAddress = IPAddress.Parse(Me.iP)
                Dim iPortNo As Integer = System.Convert.ToInt16(Me.port)
                ' Create the end point 
                Dim ipEnd As New IPEndPoint(ip, iPortNo)
                ' Connect to the remote host
                m_clientSocket.Connect(ipEnd)
                If m_clientSocket.Connected Then

                    'Wait for data asynchronously 
                    WaitForData()
                End If
            Catch se As SocketException
                Dim str As String
                str = "" & Chr(10) & "Connection failed, is the server running?" & Chr(10) & "" + se.Message
                MessageBox.Show(str)
            End Try
        End Sub

    Public Sub SendData(ByVal data As Byte())
        Try
            ' New code to send strings
            m_clientSocket.Send(data)
            'Dim networkStream As New NetworkStream(m_clientSocket)
            'Dim streamWriter As New System.IO.StreamWriter(networkStream)
            'streamWriter.WriteLine(msg)

            ' Use the following code to send bytes
            '				byte[] byData = System.Text.Encoding.ASCII.GetBytes(objData.ToString ());
            '				if(m_clientSocket != null){
            '					m_clientSocket.Send (byData);
            '				}
            '				

            'streamWriter.Flush()
        Catch se As SocketException
            MessageBox.Show(se.Message)
        End Try
    End Sub

    Public Function SendAndRecDataSync(ByVal data As Byte()) As Byte()
        Try
            m_clientSocket.Send(data)
            Dim received As Byte()
            m_clientSocket.Receive(received)
            Return received
        Catch se As SocketException
            MessageBox.Show(se.Message)
            Return Nothing
        End Try
    End Function

        Public Sub WaitForData()
            Try
                If m_pfnCallBack Is Nothing Then
                m_pfnCallBack = New AsyncCallback(AddressOf OnDataReceived)
                End If
                Dim theSocPkt As New SocketPacket()
                theSocPkt.thisSocket = m_clientSocket
            ' Start listening to the data asynchronously
            m_result = m_clientSocket.BeginReceive(theSocPkt.dataBuffer, 0, theSocPkt.dataBuffer.Length, SocketFlags.None, m_pfnCallBack, theSocPkt)
            Catch se As SocketException
                MessageBox.Show(se.Message)
            End Try

        End Sub

        Public Class SocketPacket
            Public thisSocket As System.Net.Sockets.Socket
            Public dataBuffer As Byte() = New Byte(1023) {}
        End Class

    Public Sub OnDataReceived(ByVal asyn As IAsyncResult)
        Try
            Dim theSockId As SocketPacket = DirectCast(asyn.AsyncState, SocketPacket)
            RaiseEvent ReceiveData(theSockId.dataBuffer)
            'Dim ms As New System.IO.MemoryStream(theSockId.dataBuffer)
            'Dim imagen As System.Drawing.Image = System.Drawing.Image.FromStream(ms)
            'RaiseEvent ReceiveImage(imagen)
            WaitForData()
        Catch ex As Exception

        End Try
        'Try
        'Dim theSockId As SocketPacket = DirectCast(asyn.AsyncState, SocketPacket)
        'Dim iRx As Integer = theSockId.thisSocket.EndReceive(asyn)

        'Dim chars As Char() = New Char(iRx) {}
        'Dim d As System.Text.Decoder = System.Text.Encoding.UTF8.GetDecoder()
        'Dim charLen As Integer = d.GetChars(theSockId.dataBuffer, 0, iRx, chars, 0)
        'Dim szData As New String(chars)
        '                'richTextRxMessage.Text = richTextRxMessage.Text + szData.ToString()
        'RaiseEvent ReceiveData(szData)
        'WaitForData()
        'Catch generatedExceptionName As ObjectDisposedException
        'System.Diagnostics.Debugger.Log(0, "1", "" & Chr(10) & "OnDataReceived: Socket has been closed" & Chr(10) & "")
        'Catch se As SocketException
        'MessageBox.Show(se.Message)
        'Catch ex As Exception
        'pass
        'End Try
    End Sub

    Private Function GetIP() As String
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
End Class
