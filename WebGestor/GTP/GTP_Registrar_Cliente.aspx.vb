Imports WebGestor
Imports System.Data.SqlClient
Imports System.Data
Partial Class GTP_GTP_Registrar_Cliente
    Inherits System.Web.UI.Page

    Protected Sub BtnLimpiar_Click(sender As Object, e As EventArgs) Handles BtnLimpiar.Click
        TxtRuc.Text = ""
        TxtApePat.Text = ""
        TxtApeMat.Text = ""
        TxtNombres.Text = ""
        TxtTelefono.Text = ""
        TxtEmail.Text = ""
        BtnRegistrar.Enabled = True
        BtnBuscar.Visible = False
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            LblError.Text = ""
        End If
    End Sub

    Private Sub BtnRegistrar_Click(sender As Object, e As EventArgs) Handles BtnRegistrar.Click
        Dim Cn As New SqlConnection(Session("Ruta_Emp"))
        Dim Cn2 As New SqlConnection(Session("Ruta_Emp"))
        Dim Cn3 As New SqlConnection(Session("Ruta_Emp"))
        Dim cmdSql As New SqlCommand
        Dim cmdSql2 As New SqlCommand
        Dim cmdSql3 As New SqlCommand
        Dim ObjCliente As New Cls_Cliente
        Dim Rs As SqlClient.SqlDataReader
        Dim Rs2 As SqlClient.SqlDataReader
        Dim lsCodigoOperador As Double
        Dim pdCodCliente As String = ""
        Try
            Cn.Open()
            cmdSql.Connection = Cn
            Cn2.Open()
            cmdSql2.Connection = Cn2

            Cn3.Open()
            cmdSql3.Connection = Cn3


            cmdSql.CommandText = "SELECT MAX(PERSONA_CODIGO) FROM TBDATA_PERSONAS WHERE (EMPRESA_CODIGO='" & Session("CodEmpresa") & "')"
            Rs = cmdSql.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    pdCodCliente = Nz(Rs(0)) + 1
                End While
            Else
                pdCodCliente = "1"
            End If
            Rs.Close()
            Dim psCodContacto As String = ""
            Dim psRazonsocial As String = ""
            psRazonsocial = TxtApePat.Text & " " & TxtApeMat.Text & " " & TxtNombres.Text
            Dim psClienteExiste As String = "NO"


            cmdSql.CommandText = "SELECT PERSONA_CODIGO FROM TBDATA_PERSONAS WHERE (EMPRESA_CODIGO='" & Session("CodEmpresa") & "') and PERSONA_RUC = '" & TxtRuc.Text & "'"
            Rs = cmdSql.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    pdCodCliente = Nz(Rs(0))
                    psClienteExiste = "SI"
                End While
                Rs.Close()
            Else
                Rs.Close()
                psClienteExiste = "NO"
                cmdSql2.CommandText = "insert into TBDATA_PERSONAS ( PERSONA_CODIGO,PERSONA_RUC, PERSONA_RAZON_SOCIAL,PERSONA_APEPAT, PERSONA_APEMAT," _
                           & " PERSONA_NOMBRES, PERSONA_NOMBRE_CONTACTO,PERSONA_TIPO, PERSONA_EMAIL, PERSONA_SYS_CRE, PERSONA_SYS_EST,EMPRESA_CODIGO, PERSONA_TELF1, PERSONA_TIPO_CLIENTE, PERSONA_PROVIENE) " _
                           & " VALUES('" & CInt(pdCodCliente) & "','" & TxtRuc.Text & "','" & QuitaComilla(Trim(psRazonsocial)) & "','" & QuitaComilla(Trim(TxtApePat.Text)) & "','" & QuitaComilla(Trim(TxtApeMat.Text)) & "','" & QuitaComilla(Trim(TxtNombres.Text)) & "'," _
                           & " '" & QuitaComilla(Solo_Texto(psRazonsocial)) & "','1', '" & Trim(TxtEmail.Text) & "','" & Session("user") & FechaActual() & HoraActual() & "','0','" & Session("CodEmpresa") & "','" & Trim(TxtTelefono.Text) & "', '1','W')"
                cmdSql2.ExecuteNonQuery()

                cmdSql2.CommandText = " SELECT MAX(CONTACTO_CODIGO) FROM TBDATA_PERSONAS_CONTACTO "
                Rs2 = cmdSql2.ExecuteReader
                If Rs2.HasRows Then
                    While Rs2.Read
                        psCodContacto = Nz(Rs2(0)) + 1
                    End While
                Else
                    psCodContacto = "1"
                End If
                Rs2.Close()
                cmdSql2.CommandText = " INSERT INTO TBDATA_PERSONAS_CONTACTO (EMPRESA_CODIGO, PERSONA_CODIGO, CONTACTO_CODIGO, " _
                                      & " CONTACTO_APEPAT, CONTACTO_APEMAT, CONTACTO_NOMBRES, CONTACTO_EMAIL, CONTACTO_TELEF1, CONTACTO_SYS_EST) " _
                                      & " VALUES('" & Session("CodEmpresa") & "'," & CInt(pdCodCliente) & "," & psCodContacto & ", " _
                                      & " '" & QuitaComilla(Trim(TxtApePat.Text)) & "', '" & QuitaComilla(Trim(TxtApeMat.Text)) & "', '" & QuitaComilla(Trim(TxtNombres.Text)) & "', " _
                                      & " '" & QuitaComilla(Trim(TxtEmail.Text)) & "', '" & QuitaComilla(Trim(TxtTelefono.Text)) & "', '0')"
                cmdSql2.ExecuteNonQuery()
            End If

            Dim psCod As String = ""

            cmdSql.CommandText = " SELECT MAX(CALL_NUMREG) FROM TBCALLCENTER_DATOS_VARIOS "
            Rs = cmdSql.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    psCod = Nz(Rs(0)) + 1
                End While
            Else
                psCod = "1"
            End If
            Rs.Close()

            cmdSql.CommandText = "Exec TBLIS_OPERADOR_DISPONIBLE "
            Rs = cmdSql.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    lsCodigoOperador = Nu(Rs(0))
                End While
            End If
            Rs.Close()

            cmdSql.CommandText = " SELECT *,(SELECT ELEMEN_VALOR FROM BDGRUPOEMPRESAS.DBO.TBCELEMEN WHERE ELEMEN_TABLA = 'TBOPC004' AND ELEMEN_CODIGO = PERSONA_DIST) as didtrito FROM TBDATA_PERSONAS WHERE PERSONA_SYS_EST = '0' AND PERSONA_CODIGO = " & pdCodCliente & " AND PERSONA_TIPO = '1'"
            Rs = cmdSql.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    cmdSql2.CommandText = " SELECT * FROM TBCALLCENTER_DATOS_VARIOS where CALL_COD_CENTRAL = " & pdCodCliente
                    Rs2 = cmdSql2.ExecuteReader
                    If Rs2.HasRows = False Then
                        cmdSql3.CommandText = " INSERT INTO TBCALLCENTER_DATOS_VARIOS ( EMPRESA_CODIGO, CALL_NUMREG, CALL_REG_FECHA, CALL_REG_HORA, CALL_REG_USER, CALL_OPERADOR, CALL_FECHA_CARGA, " _
                                              & " CALL_FECHA_ACTUALIZAR, CALL_FECHA_LLAMADA, CALL_HORA_LLAMADA, CALL_SYS_EST, CALL_ESTADO_CARTERA, CALL_ESTADO, CALL_ESTADO_ATENCION, CALL_OPERACION_EST, " _
                                              & " CALL_VECES_LLAMADAS, CALL_FECHA_PROCESO, CALL_COD_CENTRAL, CALL_TIPO_PERSONA, CALL_RUC, CALL_RAZON_SOCIAL, CALL_DIRECCION, CALL_TELEFONO, " _
                                              & " CALL_COD_POSTAL, CALL_COD_ESTADO, CALL_COD_DPTO, CALL_COD_PROVINCIA, CALL_COD_DISTRITO, CALL_UBIGEO, " _
                                              & " CALL_FONO_1, CALL_FONO_2, CALL_FONO_3, CALL_FONO_4 , CALL_FONO_5, CALL_PROCESADO_SERVIDOR, CALL_TIPO_NEGOCIO, CALL_ESTADO_PROCESO,CALL_ESTADO_CERRADO)" _
                                              & " VALUES ( '" & Session("CodEmpresa") & "', " & psCod & ", '" & FechaActual() & "', '" & HoraActual() & "', '" & Session("User") & "', '" & lsCodigoOperador & "', '" & FechaActual() & "', " _
                                              & " '" & FechaActual() & "', '" & FechaActual() & "', '" & HoraActual() & "', '0', '1', '1', '0', '0', " _
                                              & " 0, '" & FechaActual() & "', " & pdCodCliente & ", '1', '" & Nu(Rs!PERSONA_RUC) & "', '" & Nu(Rs!PERSONA_RAZON_SOCIAL) & "', '" & Nu(Rs!PERSONA_DIRECCION) & "', '" & Nu(Rs!PERSONA_TELF1) & "' , " _
                                              & " '', '1', '" & Nu(Rs!PERSONA_DPTO) & "', '" & Nu(Rs!PERSONA_PROV) & "', '" & Nu(Rs!PERSONA_DIST) & "', '" & Left(Nu(Rs!PERSONA_DPTO), 2) + Mid(Nu(Rs!PERSONA_PROV), 3, 2) + Right(Nu(Rs!PERSONA_DIST), 2) & "', " _
                                              & " '" & Nu(Rs!PERSONA_TELF2) & "', '" & Nu(Rs!PERSONA_TELF_OF) & "', '" & Nu(Rs!PERSONA_TELF_CELULAR) & "', '', '', '0', '1', '1','0')"
                        cmdSql3.ExecuteNonQuery()
                    End If
                    Rs2.Close()
                End While
            End If
            Rs.Close()

            cmdSql.CommandText = "exec TBUPD_ESTADO_OPERADOR_VARIOS '1', '" & lsCodigoOperador & "', '1'"
            cmdSql.ExecuteNonQuery()

            cmdSql.CommandText = " UPDATE TBCALLCENTER_OPERADOR_ESTADO Set ESTADO_OPERATIVO = '0' WHERE OPERADOR_CODIGO = '" & lsCodigoOperador & "'  "
            cmdSql.ExecuteNonQuery()

            cmdSql.CommandText = " UPDATE TBCALLCENTER_DATOS_VARIOS SET CALL_ESTADO_ATENCION  = '1'  WHERE CALL_NUMREG = '" & psCod & "'  "
            cmdSql.ExecuteNonQuery()

            cmdSql.CommandText = "Exec TBUPD_OPERADOR_DISPONIBLE '1'," & lsCodigoOperador & ",'1','1', " & pdCodCliente
            cmdSql.ExecuteNonQuery()

            BtnLimpiar_Click(sender, e)

            If psClienteExiste = "NO" Then LblError.Text = "DATOS GUARDADOS"

        Catch ex As SqlException
            LblError.Text = "Ha ocurrido un error en la base de datos: " & ex.Message
        Catch ex As Exception
            LblError.Text = "Ha ocurrido un error en la aplicación: " & ex.Message
        Finally
        End Try
    End Sub

    Private Sub TxtRuc_TextChanged(sender As Object, e As EventArgs) Handles TxtRuc.TextChanged
        Dim Cn As New SqlConnection(Session("Ruta_Emp"))
        Dim cmdSql As New SqlCommand
        Dim cmdSql2 As New SqlCommand
        Dim cmdSql3 As New SqlCommand
        Dim ObjCliente As New Cls_Cliente
        Dim Rs As SqlClient.SqlDataReader
        Dim pdCodCliente As String = ""

        Try
            Cn.Open()
            cmdSql.Connection = Cn

            cmdSql.CommandText = "SELECT PERSONA_CODIGO,PERSONA_RUC, PERSONA_RAZON_SOCIAL,PERSONA_APEPAT, PERSONA_APEMAT," _
                              & " PERSONA_NOMBRES, PERSONA_NOMBRE_CONTACTO,PERSONA_TIPO, PERSONA_EMAIL, PERSONA_TELF1, " _
                              & " PERSONA_TIPO_CLIENTE, PERSONA_PROVIENE " _
                              & " FROM TBDATA_PERSONAS " _
                              & "WHERE (EMPRESA_CODIGO='" & Session("CodEmpresa") & "') and PERSONA_RUC = '" & TxtRuc.Text & "'"
            Rs = cmdSql.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    lblCodCliente.Text = Nz(Rs("PERSONA_CODIGO"))
                    TxtRuc.Text = Nu(Rs("PERSONA_RUC"))
                    TxtApePat.Text = Nu(Rs("PERSONA_APEPAT"))
                    TxtApeMat.Text = Nu(Rs("PERSONA_APEMAT"))
                    TxtNombres.Text = Nu(Rs("PERSONA_NOMBRES"))
                    TxtEmail.Text = Nu(Rs("PERSONA_EMAIL"))
                    TxtTelefono.Text = Nu(Rs("PERSONA_TELF1"))
                    BtnRegistrar.Enabled = False
                    BtnBuscar.Visible = True
                End While
            End If
            Rs.Close()

        Catch ex As SqlException
            LblError.Text = "Ha ocurrido un error en la base de datos: " & ex.Message
        Catch ex As Exception
            LblError.Text = "Ha ocurrido un error en la aplicación: " & ex.Message
        Finally
        End Try
    End Sub
    Protected Sub BtnBuscar_Click(sender As Object, e As EventArgs) Handles BtnBuscar.Click
        Dim Cn As New SqlConnection(Session("Ruta_Emp"))
        Dim Cn2 As New SqlConnection(Session("Ruta_Emp"))
        Dim Cn3 As New SqlConnection(Session("Ruta_Emp"))
        Dim cmdSql As New SqlCommand
        Dim cmdSql2 As New SqlCommand
        Dim cmdSql3 As New SqlCommand
        Dim ObjCliente As New Cls_Cliente
        Dim Rs As SqlClient.SqlDataReader
        Dim Rs2 As SqlClient.SqlDataReader
        Dim lsCodigoOperador As Double
        Dim pdCodCliente As String = ""
        Try
            Cn.Open()
            cmdSql.Connection = Cn
            Cn2.Open()
            cmdSql2.Connection = Cn2

            Cn3.Open()
            cmdSql3.Connection = Cn3

            cmdSql.CommandText = "Exec TBLIS_CODIGO_OPERADOR " & Session("User")
            Rs = cmdSql.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    lsCodigoOperador = Nu(Rs("OPERADOR_CODIGO"))
                End While
            End If
            Rs.Close()

            pdCodCliente = lblCodCliente.Text

            Dim psCodContacto As String = ""
            Dim psRazonsocial As String = ""
            psRazonsocial = TxtApePat.Text & " " & TxtApeMat.Text & " " & TxtNombres.Text

            Dim psCod As String = ""

            cmdSql.CommandText = " SELECT MAX(CALL_NUMREG) FROM TBCALLCENTER_DATOS_VARIOS "
            Rs = cmdSql.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    psCod = Nz(Rs(0)) + 1
                End While
            Else
                psCod = "1"
            End If
            Rs.Close()

            cmdSql.CommandText = " SELECT *,(SELECT ELEMEN_VALOR FROM BDGRUPOEMPRESAS.DBO.TBCELEMEN WHERE ELEMEN_TABLA = 'TBOPC004' AND ELEMEN_CODIGO = PERSONA_DIST) as didtrito FROM TBDATA_PERSONAS WHERE PERSONA_SYS_EST = '0' AND PERSONA_CODIGO = " & pdCodCliente & " AND PERSONA_TIPO = '1'"
            Rs = cmdSql.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    cmdSql2.CommandText = " SELECT * FROM TBCALLCENTER_DATOS_VARIOS where CALL_COD_CENTRAL = " & pdCodCliente
                    Rs2 = cmdSql2.ExecuteReader
                    If Rs2.HasRows = False Then
                        cmdSql3.CommandText = " INSERT INTO TBCALLCENTER_DATOS_VARIOS ( EMPRESA_CODIGO, CALL_NUMREG, CALL_REG_FECHA, CALL_REG_HORA, CALL_REG_USER, CALL_OPERADOR, CALL_FECHA_CARGA, " _
                                              & " CALL_FECHA_ACTUALIZAR, CALL_FECHA_LLAMADA, CALL_HORA_LLAMADA, CALL_SYS_EST, CALL_ESTADO_CARTERA, CALL_ESTADO, CALL_ESTADO_ATENCION, CALL_OPERACION_EST, " _
                                              & " CALL_VECES_LLAMADAS, CALL_FECHA_PROCESO, CALL_COD_CENTRAL, CALL_TIPO_PERSONA, CALL_RUC, CALL_RAZON_SOCIAL, CALL_DIRECCION, CALL_TELEFONO, " _
                                              & " CALL_COD_POSTAL, CALL_COD_ESTADO, CALL_COD_DPTO, CALL_COD_PROVINCIA, CALL_COD_DISTRITO, CALL_UBIGEO, " _
                                              & " CALL_FONO_1, CALL_FONO_2, CALL_FONO_3, CALL_FONO_4 , CALL_FONO_5, CALL_PROCESADO_SERVIDOR, CALL_TIPO_NEGOCIO, CALL_ESTADO_PROCESO)" _
                                              & " VALUES ( '" & Session("CodEmpresa") & "', " & psCod & ", '" & FechaActual() & "', '" & HoraActual() & "', '" & Session("User") & "', '" & lsCodigoOperador & "', '" & FechaActual() & "', " _
                                              & " '" & FechaActual() & "', '" & FechaActual() & "', '" & HoraActual() & "', '0', '1', '1', '0', '0', " _
                                              & " 0, '" & FechaActual() & "', " & pdCodCliente & ", '1', '" & Nu(Rs!PERSONA_RUC) & "', '" & Nu(Rs!PERSONA_RAZON_SOCIAL) & "', '" & Nu(Rs!PERSONA_DIRECCION) & "', '" & Nu(Rs!PERSONA_TELF1) & "' , " _
                                              & " '', '1', '" & Nu(Rs!PERSONA_DPTO) & "', '" & Nu(Rs!PERSONA_PROV) & "', '" & Nu(Rs!PERSONA_DIST) & "', '" & Left(Nu(Rs!PERSONA_DPTO), 2) + Mid(Nu(Rs!PERSONA_PROV), 3, 2) + Right(Nu(Rs!PERSONA_DIST), 2) & "', " _
                                              & " '" & Nu(Rs!PERSONA_TELF2) & "', '" & Nu(Rs!PERSONA_TELF_OF) & "', '" & Nu(Rs!PERSONA_TELF_CELULAR) & "', '', '', '0', '1', '1')"
                        cmdSql3.ExecuteNonQuery()
                    End If
                    Rs2.Close()
                End While
            End If
            Rs.Close()

            cmdSql.CommandText = "exec TBUPD_ESTADO_OPERADOR_VARIOS '1', '" & lsCodigoOperador & "', '1'"
            cmdSql.ExecuteNonQuery()

            cmdSql.CommandText = " UPDATE TBCALLCENTER_OPERADOR_ESTADO Set ESTADO_OPERATIVO = '0' WHERE OPERADOR_CODIGO = '" & lsCodigoOperador & "'  "
            cmdSql.ExecuteNonQuery()

            cmdSql.CommandText = " UPDATE TBCALLCENTER_DATOS_VARIOS SET CALL_ESTADO_ATENCION  = '1'  WHERE CALL_NUMREG = '" & psCod & "'  "
            cmdSql.ExecuteNonQuery()

            cmdSql.CommandText = "Exec TBUPD_OPERADOR_DISPONIBLE '1'," & lsCodigoOperador & ",'1','1', " & pdCodCliente
            cmdSql.ExecuteNonQuery()

            BtnLimpiar_Click(sender, e)

        Catch ex As SqlException
            LblError.Text = "Ha ocurrido un error en la base de datos: " & ex.Message
        Catch ex As Exception
            LblError.Text = "Ha ocurrido un error en la aplicación: " & ex.Message
        Finally
        End Try
    End Sub
End Class
