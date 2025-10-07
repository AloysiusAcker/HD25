Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data
Imports WebGestor
Imports System.Web.Security
Public Class ClsGtp_Procesos
    Dim objSeg As New ModuloSeguridad
    Public Sub GTP_LlenaComboItem_Proceso(ByVal psCampo As String, ByVal Ddl As DropDownList, ByVal psCodProceso_Est As String,
                                          ByVal psSigla As String, Optional ByRef psTabla As String = "", Optional ByRef psCodEst_Actual As String = "")
        Dim CnTE As New SqlConnection(Ruta_GrEmp)
        Dim cmdGlobalTE As New SqlCommand
        Dim RsTE As SqlClient.SqlDataReader
        Dim Sql As String = ""
        Try
            CnTE.Open() : cmdGlobalTE.Connection = CnTE
            Ddl.Items.Clear()
            Dim n As Integer = 0
            If psTabla <> "" Then
                Sql = " SELECT ELEMEN_CODIGO, ELEMEN_VALOR FROM TBCELEMEN " _
                    & " inner join BDGEmpresa" & psSigla & ".dbo." & psTabla & "  "
                If psTabla = "TBTICKET_RELACION_PROCESO_ESTADO" Then Sql = Sql & " on elemen_codigo = estado_codigo "
                If psCodEst_Actual <> "" Then
                    Sql = Sql & " inner JOIN BDGEmpresa" & psSigla & ".dbo.TBTICKET_ESTADO_RELACION AS B  " _
                & " ON B.TICKET_ESTADO_RELACION = estado_codigo "
                End If
                If psTabla = "TBTICKET_CLIENTE_RELACION_PROCESO" Then Sql = Sql & " on elemen_codigo = proceso_codigo  "
                Sql = Sql & " WHERE ELEMEN_TABLA='" & psCampo & "' AND ELEMEN_SYS_EST='0' "
                If psTabla = "TBTICKET_RELACION_PROCESO_ESTADO" Then Sql = Sql & " and proceso_codigo = '" & psCodProceso_Est & "' "
                If psCodEst_Actual <> "" Then Sql = Sql & " AND TICKET_ESTADO = '" & psCodEst_Actual & "' "
                If psTabla = "TBTICKET_CLIENTE_RELACION_PROCESO" Then Sql = Sql & " and estado_codigo = '" & psCodProceso_Est & "' "
                Sql = Sql & " order by elemen_codigo"
                n = 2
            End If

            cmdGlobalTE.CommandText = Sql
            RsTE = cmdGlobalTE.ExecuteReader
            If RsTE.HasRows Then
                While RsTE.Read
                    Dim Item As New ListItem
                    Item.Text = Nu(RsTE(1)).ToString
                    Item.Value = Nu(RsTE(0)).ToString
                    Ddl.Items.Add(Item)
                End While
            End If
            RsTE.Close()

        Catch Ex As SqlClient.SqlException
        Catch Ex As Exception
        Finally
            CnTE.Close()
        End Try
    End Sub

    Public Sub LLenaComboItemTabEspRelacionProceso(ByVal psConexion As String, ByVal Ddl As DropDownList, ByVal valor1 As String, ByVal valor2 As String,
                                                   ByVal Tb1 As String, ByVal psCodProceso As String, ByVal CodGEE As String, Optional ByRef Mostrar_Codigo As String = "")
        Dim CnTE As New SqlConnection(psConexion)
        Dim cmdGlobalTE As New SqlCommand
        Dim RsTE As SqlClient.SqlDataReader
        Dim Sql As String = ""
        Try
            CnTE.Open() : cmdGlobalTE.Connection = CnTE
            Ddl.Items.Clear()
            Dim n As Integer = 0

            Sql = " SELECT NIVEL1_DESCRIP,NIVEL1_CODIGO From " & Tb1 & " " _
              & " INNER JOIN TBTICKET_RELACION_PROCESO_GTP1 on NIVEL1_CODIGO = GTP1_CODIGO " _
              & " WHERE (NIVEL1_SYS_EST = '0') AND (EMPRESA_CODIGO='" & CodGEE & "') AND PROCESO_CODIGO = '" & psCodProceso & "' "
            If Mostrar_Codigo = "S" Then
                Sql = Sql & " ORDER BY NIVEL1_CODIGO"
            Else
                Sql = Sql & " ORDER BY NIVEL1_DESCRIP"
            End If
            'End If
            cmdGlobalTE.CommandText = Sql
            RsTE = cmdGlobalTE.ExecuteReader
            If RsTE.HasRows Then
                While RsTE.Read
                    Dim Item As New ListItem
                    Item.Text = Nu(RsTE(0)).ToString
                    Item.Value = Nu(RsTE(1)).ToString
                    Ddl.Items.Add(Item)
                End While
            End If
            RsTE.Close()
            Ddl.Items.Add("< Seleccionar >")
            Ddl.SelectedValue = "< Seleccionar >"
        Catch Ex As SqlClient.SqlException
        Catch Ex As Exception
        Finally
            CnTE.Close()
        End Try
    End Sub
    Public Sub Llenar_Usuario(ByVal ddl As DropDownList)
        ddl.Items.Clear()
        ddl.DataSource = objSeg.Listar_Usuarios()
        ddl.DataTextField = "nombre"
        ddl.DataValueField = "Codigo"
        ddl.DataBind()
        ddl.Items.Add("< Seleccionar >")
        ddl.SelectedValue = "< Seleccionar >"
    End Sub

End Class
