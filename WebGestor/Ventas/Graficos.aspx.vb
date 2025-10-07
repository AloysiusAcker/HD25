Imports WebGestor
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing

Partial Class Graficos
    Inherits System.Web.UI.Page


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
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Try
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
                chartBarras.Visible = False : ChartArea.Visible = False
                ChartPie.Visible = False : chartLinea.Visible = False : chartBarras2.Visible = False
                'Call Grafico()
            Catch Ex As SqlException
                lblError.Visible = True
                lblError.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
            Catch Ex As Exception
                lblError.Visible = True
                lblError.Text = "Ha ocurrido un error en el registro de la APLICACION:<br>" & Ex.Message
            Finally
            End Try
        End If
    End Sub

    Private Sub GraficoBarras() 'Barras
        Dim testdata As New System.Collections.Generic.Dictionary(Of String, Integer)
        Dim testdata1 As New System.Collections.Generic.Dictionary(Of String, Integer)
        Dim obj As New clsGeneral_Proceso
        Dim dt As New DataTable

        dt = TablaCombo ' obj.Prc_Ventas_xMes(Session("Ruta_Emp"), "2018")
        For Each r As DataRow In dt.Rows
            testdata.Add(Convert.ToString(r("mes")), Convert.ToInt32(r(Serie1)))
            testdata1.Add(Convert.ToString(r("mes")), Convert.ToInt32(r(Serie2)))
        Next
        dt.Columns.Add(New DataColumn("COLOR", Type.GetType("System.Object")))

        chartBarras2.Series("VentasMes").BorderColor = Color.Black
        chartBarras2.Series("VentasMes").Points.DataBind(testdata, "Key", "Value", String.Empty)
        chartBarras2.Series("VentasMes2").Points.DataBind(testdata1, "Key", "Value", String.Empty)

        Tabla = dt
        repLeyenda.DataSource = Tabla
        repLeyenda.DataBind()
    End Sub

    Private Sub Grafico2() 'Barras
        Dim testdata As New System.Collections.Generic.Dictionary(Of String, Integer)
        Dim testdata1 As New System.Collections.Generic.Dictionary(Of String, Integer)
        Dim obj As New clsGeneral_Proceso
        Dim dt As New DataTable

        dt = TablaCombo ' obj.Prc_Ventas_xMes(Session("Ruta_Emp"), "2018")

        chartBarras.DataSource = dt
        chartBarras.Series("VentasMes").XValueMember = "mes"
        chartBarras.Series("VentasMes").YValueMembers = Serie1 ' "precio_total_soles"
        chartBarras.Series("VentasMes").Palette = System.Web.UI.DataVisualization.Charting.ChartColorPalette.Pastel
        chartBarras.Series("VentasMes").LabelBackColor = Color.White
        chartBarras.DataBind()
        Tabla = dt

        repLeyenda.DataSource = Tabla
        repLeyenda.DataBind()
    End Sub
    Private Sub Grafico3() 'pIE
        Dim testdata As New System.Collections.Generic.Dictionary(Of String, Integer)
        Dim testdata1 As New System.Collections.Generic.Dictionary(Of String, Integer)
        Dim obj As New clsGeneral_Proceso
        Dim dt As New DataTable

        dt = TablaCombo ' obj.Prc_Ventas_xMes(Session("Ruta_Emp"), "2018")
        ChartPie.DataSource = dt
        ChartPie.Series("VentasMes").XValueMember = "mes"
        ChartPie.Series("VentasMes").YValueMembers = Serie1 ' "precio_total_soles"
        ChartPie.Series("VentasMes").Palette = System.Web.UI.DataVisualization.Charting.ChartColorPalette.Pastel

        ChartPie.DataBind()

        Tabla = dt

        repLeyenda.DataSource = Tabla
        repLeyenda.DataBind()
    End Sub
    Private Sub Grafico4() 'Linea
        Dim testdata As New System.Collections.Generic.Dictionary(Of String, Integer)
        Dim testdata1 As New System.Collections.Generic.Dictionary(Of String, Integer)
        Dim obj As New clsGeneral_Proceso
        Dim dt As New DataTable

        dt = TablaCombo ' obj.Prc_Ventas_xMes(Session("Ruta_Emp"), "2018")
        chartLinea.DataSource = dt
        chartLinea.Series("VentasMes").XValueMember = "mes"
        chartLinea.Series("VentasMes").YValueMembers = Serie1 ' "precio_total_soles"
        chartLinea.Series("VentasMes").Palette = System.Web.UI.DataVisualization.Charting.ChartColorPalette.Pastel

        chartLinea.DataBind()

        Tabla = dt

        repLeyenda.DataSource = Tabla
        repLeyenda.DataBind()
    End Sub
    Private Sub GraficoArea() 'Linea
        Dim testdata As New System.Collections.Generic.Dictionary(Of String, Integer)
        Dim testdata1 As New System.Collections.Generic.Dictionary(Of String, Integer)
        Dim obj As New clsGeneral_Proceso
        Dim dt As New DataTable

        dt = TablaCombo ' obj.Prc_Ventas_xMes(Session("Ruta_Emp"), "2018")
        ChartArea.DataSource = dt
        ChartArea.Series("VentasMes").XValueMember = "mes"
        ChartArea.Series("VentasMes").YValueMembers = Serie1 ' "precio_total_soles"
        ChartArea.Series("VentasMes").Palette = System.Web.UI.DataVisualization.Charting.ChartColorPalette.Pastel

        ChartArea.DataBind()

        Tabla = dt

        repLeyenda.DataSource = Tabla
        repLeyenda.DataBind()
    End Sub
    Private Function Color_Aleatorio(ByVal Ind As Integer) As System.Drawing.Color
        Try
            Return Colores(Ind)
        Catch ex As Exception
            Return Color.White
        End Try
    End Function

    Protected Sub repLeyenda_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles repLeyenda.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim Fila = CType(DirectCast(e.Item.DataItem, System.Data.DataRowView).Row, System.Data.DataRow)
            If Not Fila Is Nothing Then
                Dim lblColor As Label = e.Item.FindControl("lblColor")
                Dim lblDescripcion As Label = e.Item.FindControl("lblDescripcion")
                Dim lblCantidad As Label = e.Item.FindControl("lblCantidad")
                'If ddlTipoGrafica.SelectedValue = 2 Then lblColor.BackColor = CType(Fila("COLOR"), Color)
                lblDescripcion.Text = Convert.ToString(Fila("MES"))
                lblCantidad.Text = Convert.ToString(Fila(Serie1))
            End If
        End If
    End Sub
    Protected Sub ddlTipoGrafica_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTipoGrafica.SelectedIndexChanged
        repLeyenda.DataSource = Nothing
        repLeyenda.DataBind()
        lblError.Text = ""
        chartBarras.Visible = False : ChartPie.Visible = False : chartLinea.Visible = False : ChartArea.Visible = False : chartBarras2.Visible = False
        If ddlTipoDato.SelectedValue = "< Seleccionar >" Then lblError.Text = "Seleccionar Tipo de Datos" : Exit Sub
        If ddlTipoGrafica.SelectedValue = "< Seleccionar >" Then lblError.Text = lblError.Text & "<br>" & "Seleccionar Tipo de Gráfica" : Exit Sub
        If ddlTipoGrafica.SelectedValue = 1 Then ChartPie.Visible = True : Call Grafico3()
        If ddlTipoGrafica.SelectedValue = 2 Then chartBarras.Visible = True : Call Grafico2()
        If ddlTipoGrafica.SelectedValue = 3 Then chartLinea.Visible = True : Call Grafico4()
        If ddlTipoGrafica.SelectedValue = 4 Then ChartArea.Visible = True : Call GraficoArea()
        If ddlTipoGrafica.SelectedValue = 5 Then chartBarras2.Visible = True : Call GraficoBarras()
    End Sub

    Private Sub ddlTipoDato_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTipoDato.SelectedIndexChanged
        repLeyenda.DataSource = Nothing
        repLeyenda.DataBind()
        Dim obj As New clsGeneral_Proceso
        Dim dt As New DataTable
        lblError.Text = ""
        chartBarras.Visible = False : ChartPie.Visible = False : chartLinea.Visible = False : ChartArea.Visible = False
        If ddlTipoDato.SelectedValue = "< Seleccionar >" Then lblError.Text = "Seleccionar Tipo de Datos" : Exit Sub
        If ddlTipoDato.SelectedValue = 1 Then TablaCombo = obj.Prc_Ventas_xMes(Session("Ruta_Emp"), "2018") : Serie1 = "precio_total_soles" : Serie2 = "precio_total_dolar"
        If ddlTipoDato.SelectedValue = 2 Then TablaCombo = obj.Prc_Ventas_xProductos(Session("Ruta_Emp"), "2018") : Serie1 = "precio_total_soles" : Serie2 = "cant"
    End Sub


End Class
