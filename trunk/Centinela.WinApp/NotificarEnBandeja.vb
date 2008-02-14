Imports System.IO
Imports System.Diagnostics
Imports System
Imports System.Windows.Forms
Imports System.Runtime.InteropServices

Namespace NotificarBandeja

#Region "FormTmp"
    Friend Class FormTmp
        Inherits System.Windows.Forms.Form

        Private servicesClass As ShellNot = Nothing
        Friend Sub New(ByVal _servicesClass As ShellNot)
            servicesClass = _servicesClass
        End Sub

        Private Const WM_LBUTTONDOWN As Integer = 513
        Private Const WM_RBUTTONDOWN As Integer = 516
        Private Const WM_MBUTTONDOWN As Integer = 519

        ' for popup the menu
        <DllImport("user32.dll", EntryPoint:="TrackPopupMenu")> _
        Private Shared Function TrackPopupMenu(ByVal hMenu As IntPtr, ByVal wFlags As Integer, ByVal x As Integer, ByVal y As Integer, ByVal nReserved As Integer, ByVal hwnd As IntPtr, _
        ByRef lprc As RECT) As Integer
        End Function

        <StructLayout(LayoutKind.Sequential)> _
        Private Structure RECT
            Friend Left As Integer
            Friend Top As Integer
            Friend Right As Integer
            Friend Bottom As Integer
        End Structure

        Protected Overloads Overrides Sub WndProc(ByRef msg As Message)
            If msg.Msg = servicesClass.WM_NOTIFY_TRAY Then
                If CInt(msg.WParam) = servicesClass.uID Then
                    Dim mb As System.Windows.Forms.MouseButtons = System.Windows.Forms.MouseButtons.None
                    If CInt(msg.LParam) = WM_LBUTTONDOWN Then
                        ' left click
                        mb = System.Windows.Forms.MouseButtons.Left
                    ElseIf CInt(msg.LParam) = WM_MBUTTONDOWN Then
                        ' middle click
                        mb = System.Windows.Forms.MouseButtons.Middle
                    ElseIf CInt(msg.LParam) = WM_RBUTTONDOWN Then
                        ' right click
                        If servicesClass.contextMenuHwnd <> IntPtr.Zero Then
                            ' if connect my menu exist.
                            Dim r As New RECT()
                            r.Left = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Left
                            r.Right = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Right
                            r.Top = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Top
                            r.Bottom = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Right

                            TrackPopupMenu(servicesClass.contextMenuHwnd, 2, System.Windows.Forms.Cursor.Position.X, System.Windows.Forms.Cursor.Position.Y, 0, servicesClass.formHwnd, _
                            r)
                        Else
                            ' else callback mousebuttons.right
                            mb = System.Windows.Forms.MouseButtons.Right
                        End If
                    End If

                    If mb <> System.Windows.Forms.MouseButtons.None AndAlso servicesClass._delegateOfCallBack IsNot Nothing Then
                        servicesClass._delegateOfCallBack(mb)
                        ' run delegate
                        Return
                    End If
                End If
            End If

            MyBase.WndProc(msg)
        End Sub
    End Class
#End Region

    ''' <summary>
    ''' ShellNot Class.
    ''' </summary>
    Public Class ShellNot
        Public Shared ReadOnly myVersion As New System.Version(1, 2)
        'version const
        Private ReadOnly formTmp As FormTmp = Nothing
        ' must here, create message loop
        Private ReadOnly formTmpHwnd As IntPtr = IntPtr.Zero
        ' formtmp.Handle
        Private ReadOnly VersionOk As Boolean = False
        ' version check pass, if false u can use notifyicon with .net framework
        Private forgetDelNotifyBox As Boolean = False
        ' if forget DelNotifyBox we can DelNotifyBox automation
        Friend formHwnd As IntPtr = IntPtr.Zero
        ' for multi-notifyicon
        Friend contextMenuHwnd As IntPtr = IntPtr.Zero
        ' for multi-notifyicon
        Friend Delegate Sub delegateOfCallBack(ByVal mb As System.Windows.Forms.MouseButtons)
        Friend _delegateOfCallBack As delegateOfCallBack = Nothing

        Public Sub New()
            WM_NOTIFY_TRAY += 1
            ' id+1 for message, support multi-notifyicon
            uID += 1
            formTmp = New FormTmp(Me)
            ' create new message loop
            formTmpHwnd = formTmp.Handle
            ' handle
            ' check shell32.dll version, must 5.0 (ie 5.0)
            VersionOk = Me.GetShell32VersionInfo() >= 5
        End Sub
        Protected Overrides Sub Finalize()
            Try

                If forgetDelNotifyBox Then
                    Me.DelNotifyBox()
                    ' forget?
                End If
            Finally
                MyBase.Finalize()
            End Try
        End Sub

#Region "API_Consts"
        Friend ReadOnly WM_NOTIFY_TRAY As Integer = 1024 + 2001
        Friend ReadOnly uID As Integer = 5000

        ' see shellapi.h
        Private Const NIIF_NONE As Integer = 0
        Private Const NIIF_INFO As Integer = 1
        Private Const NIIF_WARNING As Integer = 2
        Private Const NIIF_ERROR As Integer = 3

        Private Const NIF_MESSAGE As Integer = 1
        Private Const NIF_ICON As Integer = 2
        Private Const NIF_TIP As Integer = 4
        Private Const NIF_STATE As Integer = 8
        Private Const NIF_INFO As Integer = 16

        Private Const NIM_ADD As Integer = 0
        Private Const NIM_MODIFY As Integer = 1
        Private Const NIM_DELETE As Integer = 2
        Private Const NIM_SETFOCUS As Integer = 3
        Private Const NIM_SETVERSION As Integer = 4

        Private Const NIS_HIDDEN As Integer = 1
        Private Const NIS_SHAREDICON As Integer = 2

        Private Const NOTIFYICON_OLDVERSION As Integer = 0
        Private Const NOTIFYICON_VERSION As Integer = 3

        ' o, this's master
        <DllImport("shell32.dll", EntryPoint:="Shell_NotifyIcon")> _
        Private Shared Function Shell_NotifyIcon(ByVal dwMessage As Integer, ByRef lpData As NOTIFYICONDATA) As Boolean
        End Function

        ''' <summary>
        ''' if "this.focus()" can't work, u can try this
        ''' </summary>
        ''' <param name="hwnd">this.Handle</param>
        <DllImport("user32.dll", EntryPoint:="SetForegroundWindow")> _
        Public Shared Function SetForegroundWindow(ByVal hwnd As IntPtr) As Integer
        End Function

        <StructLayout(LayoutKind.Sequential)> _
        Private Structure NOTIFYICONDATA
            Friend cbSize As Integer
            Friend hwnd As IntPtr
            Friend uID As Integer
            Friend uFlags As Integer
            Friend uCallbackMessage As Integer
            Friend hIcon As IntPtr
            <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=128)> _
            Friend szTip As String
            Friend dwState As Integer
            Friend dwStateMask As Integer
            <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=255)> _
            Friend szInfo As String
            Friend uTimeoutAndVersion As Integer
            <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=64)> _
            Friend szInfoTitle As String
            Friend dwInfoFlags As Integer
        End Structure
#End Region

        ''' <summary>
        ''' Nueva estructura de NOTIFYICONDATA
        ''' </summary>
        Private Function GetNOTIFYICONDATA(ByVal iconHwnd As IntPtr, ByVal sTip As String, ByVal boxTitle As String, ByVal boxText As String) As NOTIFYICONDATA
            Dim nData As New NOTIFYICONDATA()

            nData.cbSize = System.Runtime.InteropServices.Marshal.SizeOf(nData)
            ' struct size
            nData.hwnd = formTmpHwnd
            ' WndProc form
            nData.uID = uID
            ' message WParam, for callback
            nData.uFlags = NIF_MESSAGE Or NIF_ICON Or NIF_TIP Or NIF_INFO
            ' Flags
            nData.uCallbackMessage = WM_NOTIFY_TRAY
            ' message ID, for call back
            nData.hIcon = iconHwnd
            ' icon handle, for animation icon if u need
            nData.uTimeoutAndVersion = 10 * 1000 Or NOTIFYICON_VERSION
            ' "Balloon Tooltip" timeout
            nData.dwInfoFlags = NIIF_INFO
            ' info type Flag, u can try waring, error, info
            nData.szTip = sTip
            ' tooltip message
            nData.szInfoTitle = boxTitle
            ' "Balloon Tooltip" title
            nData.szInfo = boxText
            ' "Balloon Tooltip" body
            Return nData
        End Function

        Private Function GetShell32VersionInfo() As Integer
            ' getversion of shell32
            Dim fi As New FileInfo(Path.Combine(System.Environment.SystemDirectory, "shell32.dll"))
            If fi.Exists Then
                Dim theVersion As FileVersionInfo = FileVersionInfo.GetVersionInfo(fi.FullName)
                Dim i As Integer = theVersion.FileVersion.IndexOf("."c)
                If i > 0 Then
                    Try
                        Return Integer.Parse(theVersion.FileVersion.Substring(0, i))
                    Catch
                    End Try
                End If
            End If
            Return 0
        End Function

        ''' <summary>
        ''' Agrega una Nueva Notificacion
        ''' </summary>
        ''' <param name="iconHwnd">this.icon.handle</param>
        ''' <param name="sTip">tooltip, 5.0 max: 128 char</param>
        ''' <param name="boxTitle">"Balloon Tooltip" title, max: 64 char</param>
        ''' <param name="boxText">"Balloon Tooltip" body, max: 256 char</param>
        ''' <returns>true,false,error(-1)</returns>
        Public Function AddNotifyBox(ByVal iconHwnd As IntPtr, ByVal sTip As String, ByVal boxTitle As String, ByVal boxText As String) As Integer
            If Not Me.VersionOk Then
                Return -1
            End If

            Dim nData As NOTIFYICONDATA = GetNOTIFYICONDATA(iconHwnd, sTip, boxTitle, boxText)
            If Shell_NotifyIcon(NIM_ADD, nData) Then
                Me.forgetDelNotifyBox = True
                Return 1
            Else
                Return 0
            End If
        End Function

        ''' <summary>
        ''' Elimina el icono de notificacion
        ''' </summary>
        Public Function DelNotifyBox() As Integer
            If Not Me.VersionOk Then
                Return -1
            End If

            Dim nData As NOTIFYICONDATA = GetNOTIFYICONDATA(IntPtr.Zero, Nothing, Nothing, Nothing)
            If Shell_NotifyIcon(NIM_DELETE, nData) Then
                Me.forgetDelNotifyBox = False
                Return 1
            Else
                Return 0
            End If
        End Function

        Public Function ModiNotifyBox(ByVal iconHwnd As IntPtr, ByVal sTip As String, ByVal boxTitle As String, ByVal boxText As String) As Integer
            If Not Me.VersionOk Then
                Return -1
            End If

            Dim nData As NOTIFYICONDATA = GetNOTIFYICONDATA(iconHwnd, sTip, boxTitle, boxText)
            Return CInt(IIf(Shell_NotifyIcon(NIM_MODIFY, nData), 1, 0))
        End Function

#Region "Optional Module"
        ''' <summary>
        ''' connect my contextMenu
        ''' </summary>
        ''' <param name="_formHwnd">main form handle, try: this.handle</param>
        ''' <param name="_contextMenuHwnd">contextMenu.handle</param>
        Public Sub ConnectMyMenu(ByVal _formHwnd As IntPtr, ByVal _contextMenuHwnd As IntPtr)
            formHwnd = _formHwnd
            contextMenuHwnd = _contextMenuHwnd
        End Sub

        Public Sub Dispose()
            _delegateOfCallBack = Nothing
            Me.formTmp.Dispose()
        End Sub

        Public ReadOnly Property VersionPass() As Boolean
            Get
                Return Me.VersionOk
            End Get
        End Property
#End Region
    End Class
End Namespace
