<%@ Page Language="VB" AutoEventWireup="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server"> 
    Protected Sub lgn1_Authenticate(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.AuthenticateEventArgs)
        Try
            Dim ds As New Data.DataSet
            Dim con As New Data.SqlClient.SqlConnection(Me.SqlDataSource1.ConnectionString)
            con.Close()
            con.Open()
            Dim cmd As New Data.SqlClient.SqlCommand("Validar_Login", con)
            Dim PrmUsuario As Data.SqlClient.SqlParameter = cmd.Parameters.AddWithValue("@Usuario", lgn1.UserName.Trim())
            Dim PrmClave As Data.SqlClient.SqlParameter = cmd.Parameters.AddWithValue("@Clave", lgn1.Password.Trim())
            cmd.CommandType = Data.CommandType.StoredProcedure
            Dim da As New Data.SqlClient.SqlDataAdapter(cmd)
            da.Fill(ds)
            con.Close()
            If ds.Tables(0).Rows.Count > 0 Then
                e.Authenticated = True
                Response.Redirect(".\privado\Monitoreo.aspx")
            Else
                Response.Redirect(".\error\error.htm")
                e.Authenticated = False
            End If
            Exit Sub
        Catch ex As Exception
            'MessageBox.Show("Error While Executing the Stored Procedures", _
            '    "Executing Stored Procedure Error", _
            '    MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub
</script>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Proyecto</title>
    <link href="estilo.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="frm_hogar" runat="server"><br />
<div id="container">
<div id="core_header" style="height: 136px">
     <div id="header_text">
         Proyecto
     </div>
</div>
	<div id="core_container">
	<div id="core_container2">
		<div id="core_left">
		     <div id="navcontainer">
                    <ul>
                    <li><a href="Default.aspx" title="Hogar">Hogar</a></li>
                    <li><a href="Acercade.aspx" title="Acercade">Acerca de...</a></li>
                    </ul>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:dbproyectoConnectionString %>"
                        SelectCommand="SELECT [usuario], [clave], [visible] FROM [Usuario]" DataSourceMode="DataReader"></asp:SqlDataSource>
		     </div>
		</div>
        <div id="core_right">
          <div class="content-box" style="text-align: center">
                  <h3 style="font-weight: bold; text-align: left">
                      Regístrate</h3>
              <strong>Regístrate para accesar al servicio de monitoreo que se brinda esta página.<br />
              </strong>
          </div>   
        <!-- El contenido va aqui... -->
          <div class="content-box">
                 <h3>
                     Login</h3>
              <table width="100%">
                  <tr>
                      <td style="width: 60%; height: 100px;">
                          <div class="dropcap">
                              Acceso a prototipo de monitoreo de sensores....
                          </div>
                      </td>
                      <td style="width: 41%; height: 100px;">
                          &nbsp;<asp:Login ID="lgn1" runat="server" BackColor="#F7F6F3" BorderColor="#E6E2D8" BorderPadding="4" BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="Small" ForeColor="#333333" Height="111px" Width="239px" OnAuthenticate="lgn1_Authenticate">
                              <TitleTextStyle BackColor="#5D7B9D" Font-Bold="True" Font-Size="0.9em" ForeColor="White" />
                              <InstructionTextStyle Font-Italic="True" ForeColor="Black" />
                              <TextBoxStyle Font-Size="0.8em" />
                              <LoginButtonStyle BackColor="#FFFBFF" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px"
                                  Font-Names="Verdana" Font-Size="0.8em" ForeColor="#284775" />
                          </asp:Login>
                      </td>
                  </tr>
              </table>
          </div>
        <!-- Final de el contenido de la pagina -->
        </div>
        </div>
        <div id="footer2"><div id="footer_line"></div>    
             <div id='footer_text'>Copyleft &copy; ...</div>
            </div>
        </div>
</div>
<div id="footer"></div>
</form>
</body>
</html>