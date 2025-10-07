Imports WebGestor
Imports System.Data.SqlClient
Imports System.Data
Partial Class Person_Control_Integran_Asistencia
    Inherits System.Web.UI.Page
    Protected Sub btnListar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListar.Click
        lblError.Text = ""
        Dim obj As New clsControlPersonal
        Dim dt As New DataTable
        Dim dtListado As New DataTable
        Dim drT As DataRow
        Dim i As Integer = 0
        Dim cmdGlobal As New SqlCommand
        Dim cn As New SqlConnection(Ruta_GrEmp)
        Dim Rs As SqlDataReader
        dtListado.Columns.Add("c0")
        dtListado.Columns.Add("c1")
        dtListado.Columns.Add("c2")
        dtListado.Columns.Add("c3")
        dtListado.Columns.Add("c4")
        dtListado.Columns.Add("c5")
        dtListado.Columns.Add("c6")
        dtListado.Columns.Add("c7")
        dtListado.Columns.Add("c8")
        dtListado.Columns.Add("c9")
        dtListado.Columns.Add("c10")
        dtListado.Columns.Add("c11")
        dtListado.Columns.Add("c12")
        Try
            cn.Open() : cmdGlobal.Connection = cn
            dt = obj.Listar_Personal_Asisencia(Session("CodEmpresa"), Session("CodGrupoEmpresa"))
            If dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    i = i + 1
                    drT = dtListado.NewRow()
                    drT("c0") = i
                    drT("c1") = Nu(dr("NOMBRE_P"))
                    drT("c2") = Nu(dr("PERSON_CODIGO"))
                    drT("c3") = Nu(dr("NOM_CARGO"))
                    drT("c4") = IIf(Nu(dr("INTEGRA_A")) = "S", "Sí", IIf(Nu(dr("INTEGRA_A")) = "N", "No", ""))
                    drT("c5") = Nu(dr("HOR_FIJO"))
                    drT("c6") = Nu(dr("HOR_VARIABLE"))
                    If drT("c6") = "X" Then
                        cmdGlobal.CommandText = " SELECT GRPOEMPRESA_CODIGO, EMPRESA_CODIGO, HV_AÑO, HV_PERSONAL, " _
                                              & " HV_NRO_DIA, HV_HORA_ENTRADA, HV_HORA_SALIDA, HV_SYS_EST, " _
                                              & " HV_MINUTOS_TOLERANCIA, HV_MINUTOS_REFRIGERIO " _
                                              & " From TBINTEGRAN_ASISTENCIA_VARIABLE " _
                                              & " WHERE (HV_SYS_EST = '0') " _
                                              & " AND (HV_PERSONAL = '" & Nu(dr("PERSON_CODIGO")) & "') " _
                                              & " AND GRPOEMPRESA_CODIGO=" & Session("CodGrupoEmpresa") & " " _
                                              & " AND EMPRESA_CODIGO = '" & Session("CodEmpresa") & "'"
                        Rs = cmdGlobal.ExecuteReader
                        If Rs.HasRows Then
                            While Rs.Read
                                drT("c7") = "«»"
                                drT("c8") = "«»"
                                drT("c9") = "«»"
                                drT("c10") = "«»"
                            End While
                        End If
                        Rs.Close()
                    Else
                        drT("c7") = IIf(Nu(dr("HOR_ENTRADA_PER")) = "", "", Nu(dr("HOR_ENTRADA_PER")))
                        If drT("c7") <> "" Then drT("c7") = Left(drT("c7"), 2) + ":" + Right(drT("c7"), 2)
                        drT("c8") = IIf(Nu(dr("HOR_SALIDA_PER")) = "", "", Nu(dr("HOR_SALIDA_PER")))
                        If drT("c8") <> "" Then drT("c10") = Left(drT("c8"), 2) + ":" + Right(drT("c8"), 2)
                        drT("c9") = IIf(Nu(dr("MIN_TOLE")) = "", "", Format(Nu(dr("MIN_TOLE")), "00"))
                        drT("c10") = IIf(Nu(dr("MIN_REFRIGERIO")) = "", "", Format(Nu(dr("MIN_REFRIGERIO")), "00"))
                    End If
                    drT("c11") = Nu(dr("PERSON_CARGO"))
                    dtListado.Rows.Add(drT)
                Next
            End If
            Flex.DataSource = dtListado
            Flex.DataBind()
            lblRegistro.Text = "Se ha encontrado " & Flex.Rows.Count & " registros."
        Catch ex As SqlException
            lblError.Text = "Ha ocurrido un error en la base de datos: <br>" & ex.Message
        Catch ex As Exception
            lblError.Text = "Ha ocurrido un error en la aplicación: <br>" & ex.Message
        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            lblRegistro.Text = ""
            lblError.Text = ""
        End If
    End Sub
End Class
