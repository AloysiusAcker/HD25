Imports System.Data
Imports System.Data.SqlClient
Public Class Cls_Clasificacion

    Public Function NumeroNodo(ByVal psConexion As String, ByVal codigo As Integer) As DataTable
        Dim objConn As New SqlConnection(psConexion)
        Dim objComand As New SqlCommand(" SELECT CLAS_COD_NIVEL, CLAS_NIVEL1, CLAS_NIVEL2, CLAS_NIVEL3, CLAS_NIVEL4, CLAS_NIVEL5," +
                                        " CLAS_NIVEL6, CLAS_NIVEL7, CLAS_NIVEL8, CLAS_NIVEL9, CLAS_NIVEL10" +
                                        " FROM TBINV_ARTICULO_CLASIFICACION" +
                                        " WHERE CLAS_CODIGO = @CODIGO " +
                                        " AND CLAS_SYS_EST = 0 ", objConn)
        objComand.Parameters.Add("@CODIGO", SqlDbType.Int).Value = codigo
        Dim da As New SqlDataAdapter(objComand)
        Dim dt As New DataTable()
        da.Fill(dt)
        Return dt
    End Function

    Public Function NodosHijos1(ByVal psConexion As String, ByVal nivel1 As Integer, ByVal nodoHijo As Integer) As DataTable
        Dim objConn As New SqlConnection(psConexion)
        Dim objComand As New SqlCommand(" SELECT CLAS_CODIGO as CODIGO," +
                                        " CLAS_NUMERO +' - '+ CLAS_NOMBRE as clasificacion," +
                                        " (SELECT count(clas_codigo)" +
                                        " FROM TBINV_ARTICULO_CLASIFICACION c2  WHERE c2.CLAS_NIVEL2=c1.CLAS_CODIGO And clas_cod_nivel = 3 ) as CountHijos" +
                                        " FROM TBINV_ARTICULO_CLASIFICACION c1" +
                                        " WHERE CLAS_NIVEL1 = @NIVEL1 " +
                                        " And clas_cod_nivel = @NODO" +
                                        " and clas_sys_est = '0' " +
                                        " ORDER BY CLAS_NUMERACION", objConn)
        objComand.Parameters.Add("@NIVEL1", SqlDbType.Int).Value = nivel1
        objComand.Parameters.Add("@NODO", SqlDbType.Int).Value = nodoHijo
        Dim da As New SqlDataAdapter(objComand)
        Dim dt As New DataTable()
        da.Fill(dt)
        Return dt
    End Function

    Public Function NodosHijos(ByVal psConexion As String, ByVal nivel1 As Integer, ByVal nodoHijo As Integer,
                               ByVal nodoHijoAyuda As Integer, ByVal codigo As Integer) As DataTable
        Dim objConn As New SqlConnection(psConexion)
        Dim objComand As New SqlCommand(" SELECT CLAS_CODIGO as CODIGO," +
                                        " CLAS_NUMERO +' - '+ CLAS_NOMBRE as clasificacion," +
                                        " (SELECT count(clas_codigo)" +
                                        " FROM TBINV_ARTICULO_CLASIFICACION c2  WHERE c2.CLAS_NIVEL" + CStr(nodoHijo) + "=c1.CLAS_CODIGO And clas_cod_nivel = " + CStr(nodoHijo + 1) + " ) as CountHijos" +
                                        " FROM TBINV_ARTICULO_CLASIFICACION c1" +
                                        " WHERE CLAS_NIVEL1 = @NIVEL1 " +
                                        " And clas_cod_nivel = @NODO" +
                                        " and CLAS_NIVEL" + CStr(nodoHijoAyuda) + " = @CODIGO " +
                                        " and clas_sys_est = '0' " +
                                        " ORDER BY CLAS_NUMERACION", objConn)
        objComand.Parameters.Add("@NIVEL1", SqlDbType.Int).Value = nivel1
        objComand.Parameters.Add("@NODO", SqlDbType.Int).Value = nodoHijo
        objComand.Parameters.Add("@CODIGO", SqlDbType.Int).Value = codigo
        Dim da As New SqlDataAdapter(objComand)
        Dim dt As New DataTable()
        da.Fill(dt)
        Return dt
    End Function

    Public Function PopularRootLevel(ByVal psConexion As String) As DataTable
        Dim objConn As New SqlConnection(psConexion)
        Dim objComand As New SqlCommand(" Select CLAS_CODIGO As CODIGO, " +
                                        " CLAS_NUMERO +' - '+ CLAS_NOMBRE as clasificacion, " +
                                        " (SELECT count(clas_codigo) " +
                                        " FROM TBINV_ARTICULO_CLASIFICACION c2  WHERE c2.CLAS_NIVEL1=c1.CLAS_CODIGO and clas_cod_nivel = 2 ) as CountHijos " +
                                        " FROM TBINV_ARTICULO_CLASIFICACION c1  WHERE CLAS_COD_NIVEL=1 and clas_sys_est = '0' ORDER BY CLAS_NUMERACION", objConn)
        Dim da As New SqlDataAdapter(objComand)
        Dim dt As New DataTable()
        da.Fill(dt)
        Return dt
    End Function

    Public Sub NodosPopulares(ByVal dt As DataTable, ByVal nodes As TreeNodeCollection)
        nodes.Clear()
        For Each dr As DataRow In dt.Rows
            Dim tn As New TreeNode()
            tn.Text = dr("clasificacion").ToString()
            tn.Value = dr("CODIGO").ToString()
            nodes.Add(tn)
            tn.PopulateOnDemand = (CInt(dr("CountHijos")) > 0)
        Next
    End Sub
End Class
