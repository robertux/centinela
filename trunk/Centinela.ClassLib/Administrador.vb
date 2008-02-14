Namespace ClassLib

	''' <summary>
    ''' Representa un objeto usuario con privilegios de administrador
    ''' </summary>
    Public Class Administrador
        Inherits Usuario

		''' <summary>
        ''' Crea una nueva instancia de la clase Administrador
        ''' </summary>
        ''' <param name="id">El id del administrador</param>
        ''' <param name="nomComp">El nombre completo del administrador</param>
        ''' <param name="clv">La clave de acceso del administrador</param>
        ''' <param name="nomUsr">El nombre de usuario del administrador</param>
        ''' <param name="vis">Indica si el administrador se encuentra visible o no</param>
        Public Sub New(ByVal id As String, ByVal nomComp As String, ByVal clv As String, ByVal nomUsr As String, ByVal vis As Boolean)
            MyBase.New(id, nomComp, clv, nomUsr, vis)
        End Sub
    End Class
End Namespace