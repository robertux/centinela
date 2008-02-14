Imports System.Windows.Forms
Imports System.Drawing

Public Class MinimizarABandeja
    Public WithEvents menuBandeja As New ContextMenu
    'Estado por defecto Normal
    Public restaurarEstadoVentana As FormWindowState = FormWindowState.Maximized
    Private WithEvents iconoBandeja As New NotifyIcon
    Private WithEvents frm As New Form
    'La Aplicacion es visible?
    Private visible As Boolean = True

    'Constructor
    'Toma 3 parametros
    '1)El Main Form de la aplicacion (Me)           --mandatorio
    '2)Texto que se muestra en la bandeja           --optional
    '3)Icono de la app que se muestra en la bandeja --optional
    Sub New(ByVal elFormulario As Form, Optional ByVal textoIcono As String = "", _
    Optional ByVal icono As Icon = Nothing)
        'assign passed form to global var
        frm = elFormulario
        'add two main menu items to traymenu and event handlers
        menuBandeja.MenuItems.Add("Restaurar", AddressOf restaurar)
        menuBandeja.MenuItems.Add("Cerrar", AddressOf cerrar)
        'add event handler for passed main form to execute
        AddHandler frm.SizeChanged, AddressOf ejecutar
        'add event handler for double clicking the icon in the tray (same as menu item) 
        AddHandler iconoBandeja.DoubleClick, AddressOf restaurar
        'properties for trayicon
        '-hide
        '-if icon passed, assign - otherwise assign default icon
        '-assign passed text - if none then will be blank
        '-assign context menu
        With iconoBandeja
            .Visible = False
            If IsNothing(icono) Then
                .Icon = frm.Icon
            Else
                .Icon = icono
            End If
            .Text = textoIcono
            .ContextMenu = menuBandeja
        End With
    End Sub

    Private Sub restaurar(ByVal sender As System.Object, ByVal e As System.EventArgs)
        frm.ShowInTaskbar = True
        visible = True
        iconoBandeja.Visible = False
        frm.WindowState = restaurarEstadoVentana
    End Sub

    Private Sub cerrar(ByVal sender As System.Object, ByVal e As System.EventArgs)
        iconoBandeja.Visible = False
        frm.Close()
    End Sub

    Private Sub ejecutar(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'This fires on form size changine so...
        'If form is visible and is being minimized, then 
        '-hide app in taskbar
        '-show icon in system tray
        If visible And frm.WindowState = FormWindowState.Minimized Then
            visible = False
            frm.ShowInTaskbar = False
            iconoBandeja.Visible = True
        End If
    End Sub

End Class
