Imports System.Data.SqlClient
Imports System.net.Mail
Imports System.Net.Mail.MailMessage
Imports System.Net.NetworkCredential
Imports System.Web.Security
Imports WebGestor
Imports System.Data
Partial Class AdminProblemas_Reportar
    Inherits System.Web.UI.Page
    Dim NoMouse As Boolean
    Dim Sql As String
#Region " Código generado por el Diseñador de Web Forms "
    'El Diseñador de Web Forms requiere esta llamada.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    'NOTA: el Diseñador de Web Forms necesita la siguiente declaración del marcador de posición.
    'No se debe eliminar o mover.

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: el Diseñador de Web Forms requiere esta llamada de método
        'No la modifique con el editor de código.
        InitializeComponent()
    End Sub

#End Region
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load, Me.Load
        'Introducir aquí el código de usuario para inicializar la página
        If Not Page.IsPostBack Then
            lblUsuarioCodigo.Text = User.Identity.Name
            If Session("ParamPage") = "AP" Then
                Me.Hyperlink2.NavigateUrl = "AdminProblemas_Creacion.aspx"
            ElseIf Session("ParamPage") = "AP2" Then
                Me.Hyperlink2.NavigateUrl = "AdminProblemas_Creacion2.aspx"
            End If
            Call Nuevo_Click(sender, e)
        End If
    End Sub
    Private Sub Nuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Nuevo.Click
        On Error GoTo Nuevo
        Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        Dim Rs As SqlClient.SqlDataReader
        cboRep_Prioridad.SelectedIndex = 0
        lblMensaje.Text = ""
        lblMensaje2.Text = ""
        lblMsj1.Visible = False
        lblMsj2.Visible = False
        lblMsj3.Visible = False
        lblTipoProb.Text = ""
        Call LLenaComboItemTabEsp(cboRep_Problema, "", "", "TBESP_PRO1", "TBESP_PRO2", "TBESP_PRO3", 1)
        Call cboRep_Problema_SelectedIndexChanged(sender, e)
        txtRep_Fecha.Text = FormatoFecha(FechaActual())
        txtRep_Hora.Text = FormatoHora(HoraActual())
        Cn.Open()
        CmdGlobal.Connection = Cn
        CmdGlobal.CommandText = "SELECT MAX(APROB_CODIGO) FROM TBADMIN_PROBLEMAS WHERE EMPRESA_CODIGO='" & Session("CodEmpresa") & "'"
        Rs = CmdGlobal.ExecuteReader
        If Rs.HasRows Then
            While Rs.Read
                txtRep_Codigo.Text = Format(CLng(Nz(Rs(0))) + 1, "00000")
            End While
        Else
            txtRep_Codigo.Text = "00001"
        End If
        Rs.Close()
        txtPeAnex1.Text = ""
        txtPeAnex2.Text = ""
        txtPeArea.Text = ""
        txtPeCargo.Text = ""
        txtPeCodInterno.Text = ""
        txtPeTelf1.Text = ""
        txtPeTelf2.Text = ""
        txtRep_Descrip.Text = ""
        lblRep_Personal.Text = lblUsuarioCodigo.Text
        lblRep_Nombres.Text = ""
        CmdGlobal.CommandText = "SELECT PERSON_APEPAT+' '+PERSON_APEMAT+', '+PERSON_NOMBRES,PERSON_COD_INTERNO,PE.PERSON_TELF1_EMP,PE.PERSON_TELF2_EMP,PE.PERSON_ANEXO1_EMP,PE.PERSON_ANEXO2_EMP, " _
                              & "(SELECT CARGO_NOMBRE FROM BDGRUPOEMPRESAS.DBO.TBPERSONAL_DEFINE_CARGO CD WHERE CD.CARGO_CODIGO=PE.PERSON_CARGO) AS CARGOP1 " _
                              & " FROM BDGRUPOEMPRESAS.DBO.TBPERSONAL P INNER JOIN BDGRUPOEMPRESAS.DBO.TBPERSONAL_EMPRESAS PE ON P.PERSON_CODIGO=PE.PERSONAL_CODIGO WHERE PERSON_CODIGO='" & lblUsuarioCodigo.Text & "' AND PE.EMPRESA_CODIGO='" & Session("CodEmpresa") & "' AND PE.GRPOEMPRESA_CODIGO=" & Session("CodGrupoEmpresa")
        Rs = CmdGlobal.ExecuteReader
        If Rs.HasRows Then
            While Rs.Read
                lblRep_Nombres.Text = Nu(Rs(0))
                txtPeCodInterno.Text = Nu(Rs!PERSON_COD_INTERNO)
                txtPeCargo.Text = Nu(Rs!CARGOP1)
                txtPeTelf1.Text = Nu(Rs!PERSON_TELF1_EMP)
                txtPeTelf2.Text = Nu(Rs!PERSON_TELF2_EMP)
                txtPeAnex1.Text = Nu(Rs!PERSON_ANEXO1_EMP)
                txtPeAnex2.Text = Nu(Rs!PERSON_ANEXO2_EMP)
            End While
            Rs.Close()
            CmdGlobal.CommandText = "SELECT DISTINCT PA.AREA_CODIGO, DA.AREA_NOMBRE FROM BDGRUPOEMPRESAS.DBO.TBPERSONAL_AREAS PA INNER JOIN BDGRUPOEMPRESAS.DBO.TBPERSONAL_DEFINE_AREA DA ON PA.AREA_CODIGO = DA.AREA_CODIGO " _
                                 & " WHERE (DA.EMPRESA_CODIGO = '" & Session("CodEmpresa") & "') AND (DA.GRPOEMPRESA_CODIGO = " & Session("CodGrupoEmpresa") & ") AND (PA.PERSON_AREA_SYS_EST = '0') AND (PA.PERSON_PERSONAL = '" & lblUsuarioCodigo.Text & "') AND (DA.AREA_SYS_EST = '0') ORDER BY DA.AREA_NOMBRE"
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    If txtPeArea.Text <> "" Then txtPeArea.Text = txtPeArea.Text & "; "
                    txtPeArea.Text = txtPeArea.Text & Nu(Rs!AREA_NOMBRE)
                End While
            End If
            Rs.Close()
        Else
            Rs.Close()
            CmdGlobal.CommandText = "SELECT USUARI_APEPAT,USUARI_APEMAT,USUARI_NOMBRES From BDSEGURIDADGRUPOEMPS.DBO.TBUSUARI WHERE (USUARI_SYS_EST = '0') AND USUARI_CODIGO='" & lblUsuarioCodigo.Text & "'" ' AND USUARI_TIPO='" & TipoIngrUsuSis & "'" QUITADO GESTO EMP
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    lblRep_Nombres.Text = Nu(Rs(0)) + " " + Nu(Rs(1)) + ", " + Nu(Rs(2))
                End While
            End If
        End If
        Cn.Close()
        Exit Sub
Nuevo:
        If Cn.State = ConnectionState.Open Then Cn.Close()
    End Sub
    Public Sub LLenaComboItemTabEsp(ByVal cbo As DropDownList, ByVal Valor1 As String, ByVal Valor2 As String, _
                             ByVal Tb1 As String, ByVal Tb2 As String, ByVal Tb3 As String, ByVal Ntb As Integer)
        On Error GoTo TE
        Dim CnTE As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim RsTE As SqlClient.SqlDataReader
        Dim CmdTE As New SqlClient.SqlCommand
        CnTE.Open()
        CmdTE.Connection = CnTE
        cbo.Items.Clear()
        Dim Item1 As New ListItem
        Item1.Text = "< Seleccionar >"
        Item1.Value = "0"
        cbo.Items.Add(Item1)
        If Ntb = 1 Then
            CmdTE.CommandText = "SELECT NIVEL1_DESCRIP,NIVEL1_CODIGO From " & Tb1 & " WHERE (NIVEL1_SYS_EST = '0') AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "')"
            CmdTE.CommandText = CmdTE.CommandText & " ORDER BY NIVEL1_DESCRIP"
        ElseIf Ntb = 2 Then
            CmdTE.CommandText = "SELECT TB2.NIVEL2_DESCRIP, TB2.NIVEL2_CODIGO fROM " & Tb2 & " TB2 INNER JOIN " & Tb1 & " TB1 ON TB1.EMPRESA_CODIGO=TB2.EMPRESA_CODIGO AND Tb2.NIVEL1_CODIGO = Tb1.NIVEL1_CODIGO " _
            & "WHERE (TB1.NIVEL1_SYS_EST = '0') AND (TB2.NIVEL1_CODIGO = '" & Valor1 & "') AND (TB2.NIVEL2_SYS_EST = '0') AND (TB1.EMPRESA_CODIGO='" & Session("CodEmpresa") & "')"
            CmdTE.CommandText = CmdTE.CommandText & " ORDER BY TB2.NIVEL2_DESCRIP"
        ElseIf Ntb = 3 Then
            CmdTE.CommandText = "SELECT TB3.NIVEL3_DESCRIP, TB3.NIVEL3_CODIGO FROM " & Tb2 & " TB2 INNER JOIN " & Tb1 & " TB1 ON TB2.EMPRESA_CODIGO=TB1.EMPRESA_CODIGO AND TB2.NIVEL1_CODIGO = TB1.NIVEL1_CODIGO " _
            & "INNER JOIN " & Tb3 & " TB3 ON TB2.EMPRESA_CODIGO=TB3.EMPRESA_CODIGO AND  Tb2.NIVEL2_CODIGO = Tb3.NIVEL2_CODIGO WHERE (TB1.NIVEL1_SYS_EST = '0') AND (TB2.NIVEL2_SYS_EST = '0') AND " _
            & "(TB3.NIVEL3_SYS_EST = '0') AND (TB2.NIVEL1_CODIGO = '" & Valor1 & "') AND (TB2.NIVEL2_CODIGO = '" & Valor2 & "') AND (TB1.EMPRESA_CODIGO='" & Session("CodEmpresa") & "')"
            CmdTE.CommandText = CmdTE.CommandText & " ORDER BY TB3.NIVEL3_DESCRIP"
        End If
        RsTE = CmdTE.ExecuteReader
        If RsTE.HasRows Then
            While RsTE.Read
                Dim Item As New ListItem
                Item.Text = Nu(RsTE(0))
                Item.Value = Nu(RsTE(1))
                cbo.Items.Add(Item)
            End While
        End If
        RsTE.Close()
        CnTE.Close()
        Exit Sub
TE:
        If CnTE.State = ConnectionState.Open Then CnTE.Close()
    End Sub
    Private Sub cboRep_Problema_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboRep_Problema.SelectedIndexChanged
        If NoMouse = True Then Exit Sub
        lblMsj1.Visible = False
        cboRep_P2.Items.Clear()
        cboRep_P3.Items.Clear()
        If cboRep_Problema.SelectedIndex = -1 Or cboRep_Problema.Items.Count = 0 Then Exit Sub
        If cboRep_Problema.Items(cboRep_Problema.SelectedIndex).Value = "0" Then Exit Sub
        lblTipoProb.Text = cboRep_Problema.Items(cboRep_Problema.SelectedIndex).Value
        Call LLenaComboItemTabEsp(cboRep_P2, cboRep_Problema.Items(cboRep_Problema.SelectedIndex).Value, "", "TBESP_PRO1", "TBESP_PRO2", "TBESP_PRO3", 2)
    End Sub
    Private Sub cboRep_P2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboRep_P2.SelectedIndexChanged
        If NoMouse = True Then Exit Sub
        lblMsj2.Visible = False
        cboRep_P3.Items.Clear()
        If cboRep_P2.SelectedIndex = -1 Or cboRep_P2.Items.Count = 0 Then Exit Sub
        If cboRep_P2.Items(cboRep_P2.SelectedIndex).Value = "0" Then Exit Sub
        Call LLenaComboItemTabEsp(cboRep_P3, cboRep_Problema.Items(cboRep_Problema.SelectedIndex).Value, cboRep_P2.Items(cboRep_P2.SelectedIndex).Value, "TBESP_PRO1", "TBESP_PRO2", "TBESP_PRO3", 3)
    End Sub
    Private Sub Enviar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Enviar.Click
        lblMensaje2.Text = ""
        If lblUltProb.Text = txtRep_Codigo.Text Then lblMensaje.Text = "Ya se guardó el Problema, hacer click sobre Reportar Nuevo Problema " : Exit Sub
        If cboRep_Problema.SelectedIndex = -1 Or cboRep_Problema.Items.Count = 0 Then lblMsj1.Visible = True : Exit Sub
        If cboRep_Problema.Items(cboRep_Problema.SelectedIndex).Value = "0" Then lblMsj1.Visible = True : Exit Sub
        If cboRep_P2.SelectedIndex = -1 Or cboRep_P2.Items.Count = 0 Then lblMsj2.Visible = True : Exit Sub
        If cboRep_P2.Items(cboRep_P2.SelectedIndex).Value = "0" Then lblMsj2.Visible = True : Exit Sub
        If cboRep_P3.SelectedIndex = -1 Or cboRep_P3.Items.Count = 0 Then lblMsj3.Visible = True : Exit Sub
        If cboRep_P3.Items(cboRep_P3.SelectedIndex).Value = "0" Then lblMsj3.Visible = True : Exit Sub
        On Error GoTo Guardar
        Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        Dim Rs As SqlClient.SqlDataReader
        Dim NumAnt As String = txtRep_Codigo.Text, G As Integer
        Dim CuerpoEmail As String
        G = 0
        Dim ValorSys As String = lblUsuarioCodigo.Text & FechaActual() & HoraActual()
        G = 1
        Cn.Open()
        CmdGlobal.Connection = Cn
        CmdGlobal.CommandText = "SELECT MAX(APROB_CODIGO) FROM TBADMIN_PROBLEMAS WHERE EMPRESA_CODIGO='" & Session("CodEmpresa") & "'"
        Rs = CmdGlobal.ExecuteReader
        If Rs.HasRows Then
            While Rs.Read
                txtRep_Codigo.Text = Format(CLng(Nz(Rs(0))) + 1, "00000")
            End While
        Else
            txtRep_Codigo.Text = "00001"
        End If
        Rs.Close()
        G = 3
        lblTipoProb.Text = cboRep_Problema.Items(cboRep_Problema.SelectedIndex).Value
        CmdGlobal.CommandText = "INSERT INTO TBADMIN_PROBLEMAS(EMPRESA_CODIGO,APROB_TIPO, APROB_USUARIO_REPORTA,APROB_CODIGO, APROB_PRIORIDAD, APROB_PROBLEMA1, APROB_PROBLEMA2,APROB_PROBLEMA_DESCRIPCION," _
                               & "APROB_FECHA_REPORTA, APROB_HORA_REPORTA,APROB_ESTADO, APROB_SYS_CRE,APROB_SYS_EST) " _
                               & "VALUES('" & Session("CodEmpresa") & "'," & lblTipoProb.Text & ",'" & lblRep_Personal.Text & "'," & txtRep_Codigo.Text & "," _
                               & "'" & cboRep_Prioridad.Items(cboRep_Prioridad.SelectedIndex).Value & "'," & cboRep_P2.Items(cboRep_P2.SelectedIndex).Value & "," & cboRep_P3.Items(cboRep_P3.SelectedIndex).Value & ",'" & Solo_Texto(txtRep_Descrip.Text) & "'," _
                               & "'" & Right(txtRep_Fecha.Text, 4) & Mid(txtRep_Fecha.Text, 4, 2) & Left(txtRep_Fecha.Text, 2) & "','" & Left(txtRep_Hora.Text, 2) & Right(txtRep_Hora.Text, 2) & "','1','" & ValorSys & "','0')" '" & TipoIngrUsuSis & "')"
        If CmdGlobal.ExecuteNonQuery() = 0 Then
            Cn.Close()
            lblMensaje.Text = "Ha ocurrido un error, no se ha podido enviar el problema."
            Exit Sub
        End If
        CmdGlobal.CommandText = "UPDATE TBADMIN_PROBLEMAS SET APROB_TIPO_ORIG=" & lblTipoProb.Text & ",APROB_PROBLEMA1_ORIG=" & cboRep_P2.Items(cboRep_P2.SelectedIndex).Value & ",APROB_PROBLEMA2_ORIG=" & cboRep_P3.Items(cboRep_P3.SelectedIndex).Value & " WHERE APROB_CODIGO=" & txtRep_Codigo.Text & " AND EMPRESA_CODIGO='" & Session("CodEmpresa") & "'"
        CmdGlobal.ExecuteNonQuery()
        G = 4
        Cn.Close()
        If NumAnt <> txtRep_Codigo.Text Then
            lblMensaje.Text = "Ya se había guardado un Problema con el Nº " & NumAnt & ", el nuevo Nº de Problema guardado fué " & txtRep_Codigo.Text & "."
        Else
            lblMensaje.Text = "El Problema ha sido guardado satisfactoriamente."
        End If
        Call Enviar_Email()
        lblUltProb.Text = txtRep_Codigo.Text
        Exit Sub
Guardar:
        If G <> 4 Then lblMensaje.Text = "Ha ocurrido un error, no se ha podido enviar el problema."
        If Cn.State = ConnectionState.Open Then Cn.Close()
    End Sub
    Private Sub cboRep_P3_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboRep_P3.SelectedIndexChanged
        lblMsj3.Visible = False
        lblMensaje2.Text = ""
    End Sub
    Private Sub Enviar_Email()
        lblMensaje2.Text = ""
        Dim Cn As New SqlConnection(Session("Ruta_Emp"))
        Dim Rs As SqlDataReader
        Dim cmdSql As New SqlCommand
        Dim i As Integer, Ok As Boolean = False
        Dim nConsola As Integer, nEnviar_Email As Boolean = False, nEmail As String
        nConsola = 1 'parametro de la consola 1 (maestra) pa todos los tipos de ad. prob.
        Try
            Cn.Open()
            cmdSql.Connection = Cn
            cmdSql.CommandText = "SELECT PPARA_ENV_EMAIL_NUEPROB FROM TBADMIN_PROBLEMAS_PARAMETROS WHERE PPARA_CONSOLA='" & nConsola & "'"
            Rs = cmdSql.ExecuteReader
            If Rs.HasRows Then
                Do While Rs.Read
                    If Nu(Rs!PPARA_ENV_EMAIL_NUEPROB) = "S" Then nEnviar_Email = True
                Loop
                Rs.Close()
                If nEnviar_Email = True Then
                    lstCorreos.Items.Clear()
                    cmdSql.CommandText = "SELECT PERSONA_CODIGO," _
                    & "(SELECT PERSON_EMAIL FROM BDGRUPOEMPRESAS.dbo.TBPERSONAL P WHERE P.PERSON_CODIGO = AV.PERSONA_CODIGO) AS CORREO1," _
                    & "(SELECT USUARI_CORREO FROM BDSEGURIDADGRUPOEMPS.DBO.TBUSUARI U WHERE U.USUARI_CODIGO = AV.PERSONA_CODIGO) AS CORREO2 " _
                    & " FROM TBADMIN_PROBLEMAS_AVISOCORREO AV WHERE (AV.EMPRESA_CODIGO = '" & Session("CodEmpresa") & "') AND PPARA_CONSOLA='" & nConsola & "'"
                    Rs = cmdSql.ExecuteReader
                    If Rs.HasRows Then
                        Do While Rs.Read
                            nEmail = ""
                            If Left(Nu(Rs!PERSONA_CODIGO), 4) <> "1111" And Nu(Rs!CORREO1) <> "" Then nEmail = Nu(Rs!CORREO1)
                            If Left(Nu(Rs!PERSONA_CODIGO), 4) = "1111" And Nu(Rs!CORREO2) <> "" Then nEmail = Nu(Rs!CORREO2)
                            If nEmail <> "" Then
                                Dim Item As New ListItem
                                Item.Text = nEmail
                                Item.Value = Nu(Rs!PERSONA_CODIGO)
                                lstCorreos.Items.Add(Item)
                            End If
                        Loop
                    End If
                    Rs.Close()
                    For i = 0 To lstCorreos.Items.Count - 1
                        Call sEnvia_Email(lstCorreos.Items(i).Text)
                    Next
                End If
            End If
        Catch Ex As SqlException
            lblMensaje.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & ex.Message
        Catch Ex As Exception
            lblMensaje.Text = "Ha ocurrido un error la Aplicacion:<br>" & ex.Message
        Finally
            Cn.Close()
        End Try
    End Sub
    Private Sub sEnvia_Email(ByVal Email As String)
        Dim correo As New MailMessage()
        Dim smtp As New SmtpClient
        Dim i As Integer
        correo.From = New MailAddress("slimorales.27@gmail.com")
        correo.To.Add(Email)
        correo.Subject = "WebTrimega - Su Usuario"
        correo.Body = "<BLOCKQUOTE ><font style='FONT-SIZE:11px;FONT-FAMILY:tahoma,sans-serif'><hr color=#A0C6E5 size=1>" _
                    & "<br><div><font color='#373e68'><strong>Nuevo Problema Reportado </strong><u><strong>Nº " & txtRep_Codigo.Text & "</strong></u></font></div><div> </div>" _
                    & "<div><strong>- Fecha:</strong> " & txtRep_Fecha.Text & "</div>" _
                    & "<div><strong>- Hora:</strong> " & txtRep_Hora.Text & "</div>" _
                    & "<div><strong>- Prioridad:</strong>  " & cboRep_Prioridad.Items(cboRep_Prioridad.SelectedIndex).Value & "</div>" _
                    & "<div><strong>- Tipo de Problema:</strong> " & cboRep_Problema.SelectedItem.Text & "</div>" _
                    & "<div><strong>- Concepto de Problema:</strong> " & cboRep_P2.SelectedItem.Text & ", " & cboRep_P3.SelectedItem.Text & "</div>" _
                    & "<div><strong>- Persona que lo reportó</strong>: " & lblRep_Nombres.Text & "<br></font></BLOCKQUOTE>"
        correo.IsBodyHtml = True

        smtp.Host = "smtp.gmail.com"
        smtp.Port = 25
        smtp.EnableSsl = True
        smtp.Credentials = New System.Net.NetworkCredential("slimorales.27@gmail.com", "")

        Try
            smtp.Send(correo)
            'lblError.Text = "Mensaje enviado satisfactoriamente"
        Catch ehttp As System.Web.HttpException
            i = 1
            lblMensaje2.Text = "Se ha producido un error al intentar enviar Correo Electrónico a las personas encargadas."
        End Try
        If i = 0 Then
            lblMensaje2.Text = "Se ha enviado Correo Electrónico a las personas encargadas."
        End If
    End Sub
End Class
