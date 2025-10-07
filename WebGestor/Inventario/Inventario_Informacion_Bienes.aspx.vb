Imports WebGestor
Imports System.Data.SqlClient
Imports System.Data
Partial Class Inventario_Inventario_Informacion_Bienes
    Inherits System.Web.UI.Page

    Dim obj As New clsInv_Listados
    Dim objCat As New Cls_Catalogo
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            lblError.Text = ""
            lblRecepcion.Visible = False
            lblSalida.Visible = False
            LblSalidaCC.Visible = False
        End If
    End Sub

    Private Sub BtnListar_Click(sender As Object, e As EventArgs) Handles BtnListar.Click
        Dim obj As New clsInv_Listados
        Dim objProceso As New clsInv_Procesos
        lblError.Text = ""
        Dim pCodArt As Integer = 0
        Dim TipoLista As String = "0"
        Dim pdCodRelacionado As String = ""
        Dim psNroPlaca As Double = 0
        Dim pdCodAlmacen As Double = 0
        lblRecepcion.Visible = False
        lblSalida.Visible = False
        LblSalidaCC.Visible = False
        Dim dtDatos As New DataTable
        dtDatos = Nothing
        gridSalida.DataSource = dtDatos
        gridSalida.DataBind()
        gridCC.DataSource = dtDatos
        gridCC.DataBind()
        gridRecepcion.DataSource = dtDatos
        gridRecepcion.DataBind()
        Flex.DataSource = dtDatos
        Flex.DataBind()
        Dim psConexion As String = Session("Ruta_Emp") ' ConfigurationManager.AppSettings("cnTecnicos")
        objProceso.Almacen_Autorizado(Session("Ruta_Emp"), Session("CodEmpresa"), HttpContext.Current.User.Identity.Name)
        'If txtCodArt.Text.Trim <> "" Then
        '    pCodArt = txtCodArt.Text.Trim : TipoLista = "1"
        'Else
        '    pCodArt = 0 : TipoLista = "0"
        'End If
        Dim dt As DataTable
        'If txtUbicacion.Text.Trim <> "" Then pdCodAlmacen = txtUbicacion.Text.Trim
        'If txtPlaca.Text.Trim <> "" Then psNroPlaca = txtPlaca.Text.Trim
        'If TxtCodRelacionado.Text.Trim <> "" Then pdCodRelacionado = TxtCodRelacionado.Text

        Dim psTipoUbicacion As String = "0"
        'If RBAlmacen.Checked = True Then psTipoUbicacion = "1"
        'If RBCentroC.Checked = True Then psTipoUbicacion = "2"
        If txtPlaca.Text <> "" Then
            psNroPlaca = Nz(txtPlaca.Text)
        End If
        Dim pdSerie_Numerar As Double = 0
        Try
            If txtNroSerie.Text = "" And txtPlaca.Text = "" Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingresar Nro. de serie o nro. de placa');", True)
            Else
                dt = obj.Lista_EquiposAlmacen(Session("Ruta_Emp"), Session("CodEmpresa"), pCodArt, psTipoUbicacion, pdCodAlmacen, TipoLista, txtNroSerie.Text.Trim, psNroPlaca, pdCodRelacionado)
                Flex.DataSource = dt
                Flex.DataBind()
                For Each dr As DataRow In dt.Rows
                    pdSerie_Numerar = Nu(dr("serie_numerar"))
                Next
                gridSalida.DataSource = obj.SalidaAlmacen_xSerieNumerar(Session("Ruta_Emp"), Session("CodEmpresa"), pdSerie_Numerar)
                gridSalida.DataBind()
                If gridSalida.Rows.Count > 0 Then lblSalida.Visible = True
                gridCC.DataSource = obj.SalidaCC_xSerieNumerar(Session("Ruta_Emp"), Session("CodEmpresa"), pdSerie_Numerar)
                gridCC.DataBind()
                If gridCC.Rows.Count > 0 Then LblSalidaCC.Visible = True
                gridRecepcion.DataSource = obj.SalidaRecepciones_xSerieNumerar(Session("Ruta_Emp"), Session("CodEmpresa"), pdSerie_Numerar)
                gridRecepcion.DataBind()
                If gridRecepcion.Rows.Count > 0 Then lblRecepcion.Visible = True
            End If

        Catch ex As SqlException
            lblError.Text = ex.Message
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
        End Try
    End Sub

    Private Sub gridSalida_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gridSalida.RowCommand
        If e.CommandName = "Select" Then
            ' Obtener el ID del CommandArgument
            Dim id As String = e.CommandArgument.ToString()
            Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
            ' Obtener los datos del elemento seleccionado
            Dim dt As New DataTable
            Dim psTicket As Double = 0
            psTicket = id

            DetalleTicket.DataSource = obj.Datos_xTicket(Session("Ruta_Emp"), Session("CodEmpresa"), psTicket)
            DetalleTicket.DataBind()
        End If
    End Sub
End Class
