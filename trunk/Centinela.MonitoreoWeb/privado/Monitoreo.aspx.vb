Public Partial Class Monitoreo
    Inherits System.Web.UI.Page

    Private Sub Monitoreo_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.AppendHeader("Refresh", "10; URL=Monitoreo.aspx")
    End Sub
End Class