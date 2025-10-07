Imports System.Data.SqlClient
Imports System.Web.Security
Imports WebGestor.Funciones
Imports System.Data
Imports WebGestor
Partial Class Cas_RedireccionarIncidencia
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            txtNIncidente.Focus()
            Me.Page.Session.Timeout = 1080
        End If
    End Sub
    Protected Sub btnRegresar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegresar.Click
        Call Tipos_Criterio("2", cboNImportancia, Session("CodEmpresa"), Session("Ruta_Emp"))
        Call Tipos_Criterio("1", cboNTipo, Session("CodEmpresa"), Session("Ruta_Emp"))
        Call Cargar_Informacion(sender, e)
        Me.Page.Session.Timeout = 1080
    End Sub
    Private Sub Cargar_Informacion(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim P1 As String = "0"
        Dim P2 As String = "0"
        Dim P3 As String = "0"
        Dim pCodIncidente As Double
        Dim dt As DataTable
        Dim obj As New ModuloCas
        Dim pCodGrupo As Double = 0
        If txtNIncidente.Text.Trim = "" Then Exit Sub
        pCodIncidente = txtNIncidente.Text.Trim
        Session("IncCodigo") = txtNIncidente.Text.Trim
        txtIniLlamada.Text = ""
        txtIniLlamada.Text = FormatoHoraSeg(HoraActual(True))
        Try
            dt = obj.CasLista_xIncidente(Session("CodEmpresa"), pCodIncidente,Session("Ruta_Emp"))
            If dt.Rows.Count = 1 Then
                For Each dr As Data.DataRow In dt.Rows
                    txtNUsuario.Text = " " & Nu(dr("APROB_USUARIO_REPORTA"))
                    txtNOficina.Text = " " & IIf(Nu(dr("BANCO_OFICINA")) = "", Nu(dr("BANCO_OFICINA2")), Nu(dr("BANCO_OFICINA")))
                    txtNNombre.Text = " " & Nu(dr("TBCAS_PERSONA_APELLIDOS")) & ", " & Nu(dr("TBCAS_PERSONA_NOMBRE"))
                    txtNTelefono.Text = " " & IIf(Nu(dr("INC_TELEFONO")) = "", Nu(dr("TBCAS_TELEFONO")) & " - " & Nu(dr("TBCAS_ANEXO")), Nu(dr("INC_TELEFONO")))
                    txtNDescripcion.Text = " " & Nu(dr("APROB_PROBLEMA_DESCRIPCION"))
                    Session("IncDescripcion") = " " & Nu(dr("APROB_PROBLEMA_DESCRIPCION"))
                    cboNImportancia.SelectedValue = Nu(dr("APROB_PRIORIDAD")) : cboNImportancia.Enabled = False
                    cboNTipo.SelectedValue = Nu(dr("INC_TIPO")) : cboNTipo.Enabled = False
                    Call LLenaComboItemTabEsp(cboNComponente, "", "", "TBESP_CAS1", "TBESP_CAS2", "TBESP_CAS3", 1, Session("CodEmpresa"), Session("Ruta_Emp")) '"&#241;"
                    'cboNComponente.Items.Add("< Seleccionar >") : cboNComponente.SelectedValue = "< Seleccionar >"
                    If Nu(dr("APROB_TIPO")) <> "" Then cboNComponente.SelectedValue = Nu(dr("APROB_TIPO")) : cboNComponente_SelectedIndexChanged(sender, e)
                    If Not IsDBNull(dr("APROB_PROBLEMA1")) Then
                        If Nu(dr("APROB_PROBLEMA1")) <> "" Then
                            If Nu(dr("APROB_PROBLEMA1")) <> 0.0 Then
                                cboNElemento.SelectedValue = Nu(dr("APROB_PROBLEMA1")) : cboNElemento_SelectedIndexChanged(sender, e)
                            End If
                        End If
                    End If
                    If Not IsDBNull(dr("APROB_PROBLEMA2")) Then
                        If Nu(dr("APROB_PROBLEMA2")) <> "" Then
                            If Nu(dr("APROB_PROBLEMA2")) <> 0.0 Then
                                cboNElemento2.SelectedValue = Nu(dr("APROB_PROBLEMA2"))
                            End If
                        End If
                    End If
                    cboNComponente.Enabled = False : cboNElemento.Enabled = False : cboNElemento2.Enabled = False
                Next
            End If
            dt = Nothing
            dt = obj.CasLista_xIncidente_Solucion(Session("CodEmpresa"), pCodIncidente,Session("Ruta_Emp"))
            If dt.Rows.Count = 1 Then
                For Each dr As Data.DataRow In dt.Rows
                    txtNSolucion.Text = " " & Nu(dr("DPROB_ACCION_DESCRIPCION"))
                Next
            End If
            dt = Nothing
        Catch Ex As SqlException
            lblError.Visible = True
            lblError.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
        Catch Ex As Exception
            lblError.Visible = True
            lblError.Text = "Ha ocurrido un error en la Aplicacion :<br>" & Ex.Message
        Finally
        End Try
        Me.Page.Session.Timeout = 1080
    End Sub
    Protected Sub cboNComponente_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        lblError.Visible = False
        cboNElemento.Items.Clear()
        cboNElemento2.Items.Clear()
        cboNElemento.Enabled = False
        cboNElemento2.Enabled = False
        Call LLenaComboItemTabEsp(cboNElemento, cboNComponente.SelectedValue.Trim, "", "TBESP_CAS1", "TBESP_CAS2", "TBESP_CAS3", 2, Session("CodEmpresa"), Session("Ruta_Emp"))
        If cboNComponente.SelectedValue = "< Seleccionar >" Then
            cboNElemento.Enabled = False
        Else
            cboNElemento.Enabled = True
        End If
        Me.Page.Session.Timeout = 1080
    End Sub
    Protected Sub cboNElemento_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        lblError.Visible = False
        cboNElemento2.Enabled = False
        cboNElemento2.Items.Clear()
        Call LLenaComboItemTabEsp(cboNElemento2, cboNComponente.SelectedValue.Trim, cboNElemento.SelectedValue.Trim, "TBESP_CAS1", "TBESP_CAS2", "TBESP_CAS3", 3, "0001", strConexion)
        If cboNElemento.SelectedValue = "< Seleccionar >" Then
            cboNElemento2.Enabled = False
        Else
            cboNElemento2.Enabled = True
        End If
        Me.Page.Session.Timeout = 1080
    End Sub
End Class
