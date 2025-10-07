Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports WebGestor
Partial Class CRM_CRM_Lista_CuadroMando
    Inherits System.Web.UI.Page
    Dim objProceso As New ClsGtp_Procesos
    Dim objList As New ClsGtp_Listados
    Public Property Tabla As DataTable
        Get
            If Session("Tabla") Is Nothing Then
                Session("Tabla") = New DataTable()
                Return CType(Session("Tabla"), DataTable)
            Else
                Return CType(Session("Tabla"), DataTable)
            End If
        End Get
        Set(value As DataTable)
            Session("Tabla") = value
        End Set
    End Property

    Public Property TablaCombo As DataTable
        Get
            If Session("TablaCombo") Is Nothing Then
                Session("TablaCombo") = New DataTable()
                Return CType(Session("TablaCombo"), DataTable)
            Else
                Return CType(Session("TablaCombo"), DataTable)
            End If
        End Get
        Set(value As DataTable)
            Session("TablaCombo") = value
        End Set
    End Property
    Public Property TablaFiltros As DataTable
        Get
            If Session("TablaFiltros") Is Nothing Then
                Session("TablaFiltros") = New DataTable()
                Return CType(Session("TablaFiltros"), DataTable)
            Else
                Return CType(Session("TablaFiltros"), DataTable)
            End If
        End Get
        Set(value As DataTable)
            Session("TablaFiltros") = value
        End Set
    End Property
    Public Property Serie1 As String
        Get
            If Session("Serie1") Is Nothing Then
                Session("Serie1") = New DataTable()
                Return CType(Session("Serie1"), String)
            Else
                Return CType(Session("Serie1"), String)
            End If
        End Get
        Set(value As String)
            Session("Serie1") = value
        End Set
    End Property
    Public Property Serie2 As String
        Get
            If Session("Serie2") Is Nothing Then
                Session("Serie2") = New DataTable()
                Return CType(Session("Serie2"), String)
            Else
                Return CType(Session("Serie2"), String)
            End If
        End Get
        Set(value As String)
            Session("Serie2") = value
        End Set
    End Property
    Public Property Colores As Generic.Dictionary(Of Integer, Color)
        Get
            If Session("Colores") Is Nothing Then
                Session("Colores") = New Generic.Dictionary(Of Integer, Color)
                Return CType(Session("Colores"), Generic.Dictionary(Of Integer, Color))
            Else
                Return CType(Session("Colores"), Generic.Dictionary(Of Integer, Color))
            End If
        End Get
        Set(value As Generic.Dictionary(Of Integer, Color))
            Session("Colores") = value
        End Set
    End Property
    Private Function Color_Aleatorio(ByVal Ind As Integer) As System.Drawing.Color
        Try
            Return Colores(Ind)
        Catch ex As Exception
            Return Color.White
        End Try
    End Function
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Try
                Call LlenaComboItem("TBOPC541", DdlTipoLista)
                Call LlenaComboItem("TBOPC473", DdlProceso)
                Call objProceso.Llenar_Usuario(DdlAsesor)
                If Session("User") <> "11119999" Then
                    chkAsesor.Checked = True
                    DdlAsesor.Enabled = True
                    DdlAsesor.SelectedValue = Session("User")
                End If
                DdlProceso.Items.Add("< Seleccionar >")
                DdlProceso.SelectedValue = "< Seleccionar >"
                'DdlProceso_SelectedIndexChanged(sender, e)
                Tabla = New DataTable()
                Dim lst As New Generic.Dictionary(Of Integer, Color)
                lst.Add(0, Color.Yellow)
                lst.Add(1, Color.Blue)
                lst.Add(2, Color.Red)
                lst.Add(3, Color.Brown)
                lst.Add(4, Color.Green)
                lst.Add(5, Color.Magenta)
                lst.Add(6, Color.Orange)
                lst.Add(7, Color.Violet)
                lst.Add(8, Color.Lime)
                lst.Add(9, Color.Gray)
                lst.Add(10, Color.AliceBlue)
                lst.Add(11, Color.AntiqueWhite)
                lst.Add(12, Color.Aqua)
                lst.Add(13, Color.Aquamarine)
                lst.Add(14, Color.Azure)
                lst.Add(15, Color.Beige)
                lst.Add(16, Color.Bisque)
                lst.Add(17, Color.Black)
                lst.Add(18, Color.BlanchedAlmond)
                lst.Add(19, Color.BlueViolet)
                lst.Add(20, Color.BurlyWood)
                lst.Add(21, Color.CadetBlue)
                lst.Add(22, Color.Chartreuse)
                lst.Add(23, Color.Chocolate)
                lst.Add(24, Color.Coral)
                lst.Add(25, Color.CornflowerBlue)
                lst.Add(26, Color.Cornsilk)
                lst.Add(27, Color.Crimson)
                lst.Add(28, Color.Cyan)
                lst.Add(29, Color.DarkBlue)
                lst.Add(30, Color.DarkCyan)
                lst.Add(31, Color.DarkGoldenrod)
                lst.Add(32, Color.DarkGray)
                lst.Add(33, Color.DarkGreen)
                lst.Add(34, Color.DarkKhaki)
                lst.Add(35, Color.DarkMagenta)
                lst.Add(36, Color.DarkOliveGreen)
                lst.Add(37, Color.DarkOrange)
                lst.Add(38, Color.DarkOrchid)
                lst.Add(39, Color.DarkRed)
                lst.Add(40, Color.DarkSalmon)
                lst.Add(41, Color.DarkSeaGreen)
                lst.Add(42, Color.DarkSlateBlue)
                lst.Add(43, Color.DarkSlateGray)
                lst.Add(44, Color.DarkTurquoise)
                lst.Add(45, Color.DarkViolet)
                lst.Add(46, Color.DeepPink)
                lst.Add(47, Color.DeepSkyBlue)
                lst.Add(48, Color.DimGray)
                lst.Add(49, Color.DodgerBlue)
                lst.Add(50, Color.Firebrick)
                lst.Add(51, Color.FloralWhite)
                lst.Add(52, Color.ForestGreen)
                lst.Add(53, Color.Fuchsia)
                lst.Add(54, Color.Gainsboro)
                lst.Add(55, Color.GhostWhite)
                lst.Add(56, Color.Gold)
                lst.Add(57, Color.Goldenrod)
                lst.Add(58, Color.GreenYellow)
                lst.Add(59, Color.Honeydew)
                lst.Add(60, Color.HotPink)
                lst.Add(61, Color.IndianRed)
                lst.Add(62, Color.Indigo)
                lst.Add(63, Color.Ivory)
                lst.Add(64, Color.Khaki)
                lst.Add(65, Color.Lavender)
                lst.Add(66, Color.LavenderBlush)
                lst.Add(67, Color.LawnGreen)
                lst.Add(68, Color.LemonChiffon)
                lst.Add(69, Color.LightBlue)
                lst.Add(70, Color.LightCoral)
                lst.Add(71, Color.LightCyan)
                lst.Add(72, Color.LightGoldenrodYellow)
                lst.Add(73, Color.LightGray)
                lst.Add(74, Color.LightGreen)
                lst.Add(75, Color.LightPink)
                lst.Add(76, Color.LightSalmon)
                lst.Add(77, Color.LightSeaGreen)
                lst.Add(78, Color.LightSkyBlue)
                lst.Add(79, Color.LightSlateGray)
                lst.Add(80, Color.LightSteelBlue)
                lst.Add(81, Color.LightYellow)
                lst.Add(82, Color.LimeGreen)
                lst.Add(83, Color.Linen)
                lst.Add(84, Color.Maroon)
                lst.Add(85, Color.MediumAquamarine)
                lst.Add(86, Color.MediumBlue)
                lst.Add(87, Color.MediumOrchid)
                lst.Add(88, Color.MediumPurple)
                lst.Add(89, Color.MediumSeaGreen)
                lst.Add(90, Color.MediumSlateBlue)
                lst.Add(91, Color.MediumSpringGreen)
                lst.Add(92, Color.MediumTurquoise)
                lst.Add(93, Color.MediumVioletRed)
                lst.Add(94, Color.MidnightBlue)
                lst.Add(95, Color.MintCream)
                lst.Add(96, Color.MistyRose)
                lst.Add(97, Color.Moccasin)
                lst.Add(98, Color.NavajoWhite)
                lst.Add(99, Color.Navy)
                lst.Add(100, Color.OldLace)
                lst.Add(101, Color.Olive)
                lst.Add(102, Color.OliveDrab)
                lst.Add(103, Color.OrangeRed)
                lst.Add(104, Color.Orchid)
                lst.Add(105, Color.PaleGoldenrod)
                lst.Add(106, Color.PaleGreen)
                lst.Add(107, Color.PaleTurquoise)
                lst.Add(108, Color.PaleVioletRed)
                lst.Add(109, Color.PapayaWhip)
                lst.Add(110, Color.PeachPuff)
                lst.Add(111, Color.Peru)
                lst.Add(112, Color.Pink)
                lst.Add(113, Color.Plum)
                lst.Add(114, Color.PowderBlue)
                lst.Add(115, Color.Purple)
                lst.Add(116, Color.RosyBrown)
                lst.Add(117, Color.RoyalBlue)
                lst.Add(118, Color.SaddleBrown)
                lst.Add(119, Color.Salmon)
                lst.Add(120, Color.SandyBrown)
                lst.Add(121, Color.SeaGreen)
                lst.Add(122, Color.SeaShell)
                lst.Add(123, Color.Sienna)
                lst.Add(124, Color.Silver)
                lst.Add(125, Color.SkyBlue)
                lst.Add(126, Color.SlateBlue)
                lst.Add(127, Color.SlateGray)
                lst.Add(128, Color.Snow)
                lst.Add(129, Color.SpringGreen)
                lst.Add(130, Color.SteelBlue)
                lst.Add(131, Color.Tan)
                lst.Add(132, Color.Teal)
                lst.Add(133, Color.Thistle)
                lst.Add(134, Color.Tomato)
                lst.Add(135, Color.Turquoise)
                lst.Add(136, Color.Wheat)
                lst.Add(137, Color.WhiteSmoke)
                lst.Add(138, Color.YellowGreen)
                lst.Add(139, Color.White)
                Colores = lst
                chartBarras.Visible = False
                ChartPie.Visible = False
            Catch Ex As SqlException
                lblError.Visible = True
                lblError.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
            Catch Ex As Exception
                lblError.Visible = True
                lblError.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
            Finally
            End Try
        End If
        Me.Page.Session.Timeout = 1080
    End Sub
    Protected Sub chkAsesor_CheckedChanged(sender As Object, e As EventArgs) Handles chkAsesor.CheckedChanged
        If chkAsesor.Checked = True Then
            DdlAsesor.Enabled = True
            DdlAsesor.SelectedValue = "< Seleccionar >"
        Else
            DdlAsesor.Enabled = False
            DdlAsesor.SelectedValue = "< Seleccionar >"
        End If
    End Sub
    Protected Sub btnListar_Click(sender As Object, e As EventArgs) Handles btnListar.Click
        'GTP_CuadroMando_1
        lblError.Text = ""
        Dim psAsesor As String = ""
        Dim pCodProceso As Integer = 0
        If DdlAsesor.SelectedValue <> "< Seleccionar >" Then psAsesor = DdlAsesor.SelectedValue
        If DdlTipoLista.SelectedValue = "< Seleccionar >" Then lblError.Text = lblError.Text & " <br> - Seleccionar Tipo de Lista"
        If DdlProceso.SelectedValue = "< Seleccionar >" Then lblError.Text = lblError.Text & " <br> - Seleccionar Proceso"
        If chkAsesor.Checked = True And (DdlAsesor.SelectedValue = "<Seleccionar>") Then lblError.Text = lblError.Text & " <br> -Seleccionar Asesor"
        If lblError.Text <> "" Then
            lblError.Text = lblError.Text
            Exit Sub
        End If
        Try
            TablaCombo = Nothing
            TablaFiltros = Nothing
            chartBarras.Visible = False
            ChartPie.Visible = False
            repLeyenda.DataSource = Nothing
            repLeyenda.DataBind()
            FlexGrafico.DataSource = Nothing
            FlexGrafico.DataBind()
            pCodProceso = DdlProceso.SelectedValue
            If DdlTipoLista.SelectedValue = "1" Then
                FlexGrafico.DataSource = objList.GTP_CuadroMando_1(Session("CodEmpresa"), Session("Ruta_Emp"), pCodProceso, psAsesor)
                FlexGrafico.DataBind()
                TablaCombo = objList.GTP_CuadroMando_1(Session("CodEmpresa"), Session("Ruta_Emp"), pCodProceso, psAsesor)
                TablaFiltros = objList.GTP_CuadroMando_1(Session("CodEmpresa"), Session("Ruta_Emp"), pCodProceso, psAsesor)
                Serie1 = "cantidad"
                If objList.GTP_CuadroMando_1(Session("CodEmpresa"), Session("Ruta_Emp"), pCodProceso, psAsesor).Rows.Count > 0 Then
                    lblCount.Text = "Total Registros : " & objList.GTP_CuadroMando_1(Session("CodEmpresa"), Session("Ruta_Emp"), pCodProceso, psAsesor).Rows.Count
                Else
                    lblCount.Text = "No se encontraron registros"
                End If
                chartBarras.Visible = True
                Grafico1()
                ChartPie.Visible = True
                GraficoPie()
            ElseIf DdlTipoLista.SelectedValue = "4" Then
                FlexGrafico.DataSource = objList.GTP_CuadroMando_5(Session("CodEmpresa"), Session("Ruta_Emp"), pCodProceso, psAsesor)
                FlexGrafico.DataBind()
                TablaCombo = objList.GTP_CuadroMando_5(Session("CodEmpresa"), Session("Ruta_Emp"), pCodProceso, psAsesor)
                TablaFiltros = objList.GTP_CuadroMando_5(Session("CodEmpresa"), Session("Ruta_Emp"), pCodProceso, psAsesor)
                Serie1 = "cantidad"
                If objList.GTP_CuadroMando_5(Session("CodEmpresa"), Session("Ruta_Emp"), pCodProceso, psAsesor).Rows.Count > 0 Then
                    lblCount.Text = "Total Registros : " & objList.GTP_CuadroMando_5(Session("CodEmpresa"), Session("Ruta_Emp"), pCodProceso, psAsesor).Rows.Count
                Else
                    lblCount.Text = "No se encontraron registros"
                End If
                chartBarras.Visible = True
                Grafico1()
                ChartPie.Visible = True
                GraficoPie()
            End If
        Catch Ex As SqlException
            lblError.Visible = True
            lblError.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
        Catch Ex As Exception
            lblError.Visible = True
            lblError.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
        Finally
        End Try
    End Sub

    Private Sub Grafico1() 'Barras
        Dim testdata As New System.Collections.Generic.Dictionary(Of String, Integer)
        Dim testdata1 As New System.Collections.Generic.Dictionary(Of String, Integer)
        Dim dt As New DataTable

        dt = TablaCombo ' obj.Prc_Ventas_xMes(Session("Ruta_Emp"), "2018")

        chartBarras.DataSource = dt
        chartBarras.Series("VentasMes").XValueMember = "Pestado"
        chartBarras.Series("VentasMes").YValueMembers = Serie1  ' "precio_total_soles"
        chartBarras.Series("VentasMes").Palette = System.Web.UI.DataVisualization.Charting.ChartColorPalette.Pastel
        chartBarras.Series("VentasMes").LegendText = "#VALY"
        chartBarras.Series("VentasMes").Label = "#PERCENT{P2}"
        'chartBarras.Series("VentasMes").LegendText = "#VALX"
        chartBarras.DataBind()
        Dim colr As System.Drawing.Color
        Dim psColor As String = ""
        Dim x As Integer = 0
        For Each r As DataRow In dt.Rows
            colr = Color_Aleatorio(x)
            psColor = Color_Aleatorio(x).ToArgb
            r("COLOR") = Color_Aleatorio(x).ToArgb
            chartBarras.Series("VentasMes").Points(x).Color = colr
            x += 1
        Next
        Tabla = dt

        repLeyenda.DataSource = Tabla
        repLeyenda.DataBind()
    End Sub
    Private Sub GraficoPie() 'pIE
        Dim testdata As New System.Collections.Generic.Dictionary(Of String, Integer)
        Dim testdata1 As New System.Collections.Generic.Dictionary(Of String, Integer)
        Dim obj As New clsGeneral_Proceso
        Dim dt As New DataTable

        dt = TablaFiltros ' obj.Prc_Ventas_xMes(Session("Ruta_Emp"), "2018")
        ChartPie.DataSource = dt
        ChartPie.Series("VentasMes").XValueMember = "Pestado"
        ChartPie.Series("VentasMes").YValueMembers = Serie1 ' "precio_total_soles
        ChartPie.Series("VentasMes").Palette = System.Web.UI.DataVisualization.Charting.ChartColorPalette.Pastel
        ChartPie.Series("VentasMes")("PieLabelStyle") = "Inside" ' "disabled"
        ChartPie.Series("VentasMes")("PointWidth") = "4.5"
        ChartPie.Series("VentasMes").Label = "#PERCENT{P0}"
        ChartPie.Series("VentasMes").LegendText = "#VALX"
        ChartPie.ChartAreas(0).Area3DStyle.Enable3D = True
        ChartPie.DataBind()

        Dim colr As System.Drawing.Color
        Dim psColor As String = ""
        Dim x As Integer = 0
        For Each r As DataRow In dt.Rows
            colr = Color_Aleatorio(x)
            psColor = Color_Aleatorio(x).ToArgb
            r("COLOR") = Color_Aleatorio(x).ToArgb
            ChartPie.Series("VentasMes").Points(x).Color = colr
            x += 1
        Next
        Tabla = dt

        repLeyenda.DataSource = Tabla
        repLeyenda.DataBind()
    End Sub
    Protected Sub repLeyenda_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles repLeyenda.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim Fila = CType(DirectCast(e.Item.DataItem, System.Data.DataRowView).Row, System.Data.DataRow)
            If Not Fila Is Nothing Then
                Dim lblColor As Label = e.Item.FindControl("lblColor")
                Dim lblDescripcion As Label = e.Item.FindControl("lblDescripcion")
                Dim lblCantidad As Label = e.Item.FindControl("lblCantidad")
                lblColor.BackColor = Color.FromArgb(Fila("COLOR"))
                lblColor.ForeColor = Color.FromArgb(Fila("COLOR"))
                lblColor.Text = "a"
                lblColor.BorderStyle = BorderStyle.Solid
                lblColor.BorderWidth = 1
                lblDescripcion.Text = Convert.ToString(Fila("Pestado"))
                lblCantidad.Text = Convert.ToString(Fila(Serie1))
            End If
        End If
    End Sub
    Protected Sub DdlAsesor_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlAsesor.SelectedIndexChanged
        btnListar_Click(sender, e)
    End Sub
    Protected Sub DdlTipoPeticion_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlTipoPeticion.SelectedIndexChanged
        btnListar_Click(sender, e)
    End Sub
    Protected Sub DdlProceso_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlProceso.SelectedIndexChanged
        If DdlProceso.Items.Count = 0 Then Exit Sub
        If DdlProceso.SelectedValue = "<Seleccionar>" Then Exit Sub
        objProceso.LLenaComboItemTabEspRelacionProceso(Session("Ruta_Emp"), DdlTipoPeticion, "", "", "TBESP_GTP1", DdlProceso.SelectedValue, Session("CodEmpresa"), "1")
        btnListar_Click(sender, e)
    End Sub
    Protected Sub DdlTipoLista_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlTipoLista.SelectedIndexChanged
        btnListar_Click(sender, e)
    End Sub
End Class
