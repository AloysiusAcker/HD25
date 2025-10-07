Imports System.Data.SqlClient
Imports WebGestor
Imports System.Data
Partial Class Person_Control_Define_Horarios
    Inherits System.Web.UI.Page
    Protected Sub btnListar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListar.Click
        Dim obj As New clsControlPersonal
        Try
            lblError.Text = ""
            Flex.DataSource = obj.Lista_Horario_xCargo(Session("CodEmpresa"), Session("CodGrupoEmpresa"))
            Flex.DataBind()
            lblRegistro.Text = "Se encontrarón " & Flex.Rows.Count & " registros."
        Catch ex As SqlException
            lblError.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & ex.Message
        Catch ex As Exception
            lblError.Text = "Ha ocurrido un error en la aplicación:<br>" & ex.Message
        Finally
        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            lblRegistro.Text = ""
            lblError.Text = ""
            btnListar_Click(sender, e)
        End If
    End Sub
    Protected Sub btnNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        lblIngreso.Visible = True
        Try
            lblError.Text = ""
            lblIngreso.Visible = True
            Call Listar_Cargo()
            txtHEntrada.Text = "__:__" : txtHSalida.Text = "__:__"
            lblIngDatos.Text = "Ingresar Datos"
        Catch ex As SqlException
            lblError.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & ex.Message
        Catch ex As Exception
            lblError.Text = "Ha ocurrido un error en la aplicación:<br>" & ex.Message
        Finally
        End Try
    End Sub
    Private Sub Listar_Cargo()
        Dim obj As New clsControlPersonal
        cboCargo.Items.Clear()
        cboCargo.DataSource = obj.Lista_Cargo(Session("CodEmpresa"), Session("CodGrupoEmpresa"))
        cboCargo.DataTextField = "CARGO_NOMBRE"
        cboCargo.DataValueField = "CARGO_CODIGO"
        cboCargo.DataBind()
        cboCargo.Items.Add("< Seleccionar >") : cboCargo.SelectedValue = "< Seleccionar >"
    End Sub
    Protected Sub Flex_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Flex.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        Try
            lblError.Text = ""
            Call Listar_Cargo()
            lblIngreso.Visible = True
            cboCargo.SelectedValue = Nz(Flex.Rows(Index).Cells(1).Text.Trim)
            txtHEntrada.Text = Flex.Rows(Index).Cells(3).Text.Trim
            txtHSalida.Text = Flex.Rows(Index).Cells(4).Text.Trim
            lblIngDatos.Text = "Editar Datos"
        Catch ex As SqlException
            lblError.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & ex.Message
        Catch ex As Exception
            lblError.Text = "Ha ocurrido un error en la aplicación:<br>" & ex.Message
        Finally
        End Try
    End Sub
    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            lblError.Text = ""
            lblIngreso.Visible = False
            Call Listar_Cargo()
            txtHEntrada.Text = "__:__" : txtHSalida.Text = "__:__"
            lblIngDatos.Text = "Ingresar Datos"
        Catch ex As SqlException
            lblError.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & ex.Message
        Catch ex As Exception
            lblError.Text = "Ha ocurrido un error en la aplicación:<br>" & ex.Message
        Finally
        End Try
    End Sub
    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If cboCargo.SelectedValue = "< Seleccionar >" Then lblError.Text = "Debe de seleccionar al tipo de Cargo" : Exit Sub
        Try
            Dim Cn As New SqlConnection(Ruta_GrEmp)
            Dim Cn2 As New SqlConnection(Ruta_GrEmp)
            Dim CmdGlobal As New SqlCommand
            Dim CmdGlobal2 As New SqlCommand
            Dim Rs As SqlClient.SqlDataReader
            Dim ValorSys As String = FechaActual() & HoraActual() & Session("User")
            Cn.Open() : CmdGlobal.Connection = Cn
            Cn2.Open() : CmdGlobal2.Connection = Cn2
            If lblIngDatos.Text = "Ingresar Datos" Then
                CmdGlobal.CommandText = "SELECT * FROM TBHORARIOS_ENTSAL WHERE HOR_SYS_EST='0' AND HOR_CARGO='" & Llenar_Ceros(cboCargo.SelectedValue.Trim, 2) & "'"
                Rs = CmdGlobal.ExecuteReader
                If Rs.HasRows Then
                    While Rs.Read
                        lblError.Text = "Ya se definió los horarios del cargo seleccionado,<br>" & "quizá solo deba editarlo."
                        Exit Sub
                    End While
                End If
                Rs.Close()
                CmdGlobal.CommandText = "INSERT INTO TBHORARIOS_ENTSAL(GRPOEMPRESA_CODIGO,EMPRESA_CODIGO,HOR_CARGO,HOR_HORA_ENTRADA,HOR_HORA_SALIDA,HOR_SYS_EST,HOR_SYS_CRE) " _
                         & " VALUES(" & Session("CodGrupoEmpresa") & ",'" & Session("CodEmpresa") & "','" & Llenar_Ceros(cboCargo.SelectedValue.Trim, 2) & "','" & Left(txtHEntrada.Text.Trim, 2) & Right(txtHEntrada.Text.Trim, 2) & "'," _
                         & "'" & Left(txtHSalida.Text.Trim, 2) & Right(txtHSalida.Text.Trim, 2) & "','0','" & ValorSys & "')"
                CmdGlobal.ExecuteNonQuery()
                CmdGlobal.CommandText = "UPDATE TBINTEGRAN_ASISTENCIA SET IA_HORA_ENTRADA='" & Left(txtHEntrada.Text.Trim, 2) & Right(txtHEntrada.Text.Trim, 2) & "',IA_HORA_SALIDA='" & Left(txtHSalida.Text.Trim, 2) & Right(txtHSalida.Text.Trim, 2) & "' " _
                         & " WHERE IA_INTEGRA='S' AND IA_SYS_EST='0' AND " _
                         & "(SELECT PERSON_CARGO FROM TBPERSONAL_EMPRESAS WHERE PERSONAL_CODIGO=IA_CODIGO)='" & Llenar_Ceros(cboCargo.SelectedValue.Trim, 2) & "' AND GRPOEMPRESA_CODIGO=" & Session("CodGrupoEmpresa") & " AND EMPRESA_CODIGO = '" & Session("CodEmpresa") & "'"
                CmdGlobal.ExecuteNonQuery()
            ElseIf lblIngDatos.Text = "Editar Datos" Then
                CmdGlobal.CommandText = "UPDATE TBHORARIOS_ENTSAL SET HOR_HORA_ENTRADA='" & Left(txtHEntrada.Text.Trim, 2) & Right(txtHEntrada.Text.Trim, 2) & "'," _
                         & "HOR_HORA_SALIDA='" & Left(txtHSalida.Text.Trim, 2) & Right(txtHSalida.Text.Trim, 2) & "',HOR_SYS_MOD='" & ValorSys & "' " _
                         & "WHERE HOR_CARGO='" & Llenar_Ceros(cboCargo.SelectedValue.Trim, 2) & "'AND HOR_SYS_EST='0' AND GRPOEMPRESA_CODIGO=" & Session("CodGrupoEmpresa") & " AND EMPRESA_CODIGO = '" & Session("CodEmpresa") & "'"
                CmdGlobal.ExecuteNonQuery()
                CmdGlobal.CommandText = "UPDATE TBINTEGRAN_ASISTENCIA SET IA_HORA_ENTRADA='" & Left(txtHEntrada.Text.Trim, 2) & Right(txtHEntrada.Text.Trim, 2) & "' " _
                      & " WHERE IA_INTEGRA='S'  AND IA_SYS_EST='0' AND " _
                      & "(SELECT PERSON_CARGO FROM TBPERSONAL_EMPRESAS WHERE PERSONAL_CODIGO=IA_CODIGO)='" & Llenar_Ceros(cboCargo.SelectedValue.Trim, 2) & "' AND GRPOEMPRESA_CODIGO=" & Session("CodGrupoEmpresa") & " AND EMPRESA_CODIGO = '" & Session("CodEmpresa") & "'"
                CmdGlobal.ExecuteNonQuery()
                CmdGlobal.CommandText = "UPDATE TBINTEGRAN_ASISTENCIA SET IA_HORA_SALIDA='" & Left(txtHSalida.Text.Trim, 2) & Right(txtHSalida.Text.Trim, 2) & "' " _
                      & " WHERE IA_INTEGRA='S'  AND IA_SYS_EST='0' AND " _
                      & "(SELECT PERSON_CARGO FROM TBPERSONAL_EMPRESAS WHERE PERSONAL_CODIGO=IA_CODIGO)='" & Llenar_Ceros(cboCargo.SelectedValue.Trim, 2) & "' AND GRPOEMPRESA_CODIGO=" & Session("CodGrupoEmpresa") & " AND EMPRESA_CODIGO = '" & Session("CodEmpresa") & "'"
                CmdGlobal.ExecuteNonQuery()
            End If
            btnListar_Click(sender, e)
            btnCancelar_Click(sender, e)
        Catch ex As SqlException
            lblError.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & ex.Message
        Catch ex As Exception
            lblError.Text = "Ha ocurrido un error en la aplicación:<br>" & ex.Message
        Finally
        End Try
    End Sub
End Class
