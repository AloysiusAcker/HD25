Imports System.Data.SqlClient
Imports System.Data
Public Class Cls_Cliente

    Public Function Contar_Clientes(ByVal psConexion As String, ByVal filtro As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_GTP_CONTAR_CLIENTES", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@FILTRO", filtro)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTP_CONTAR_CLIENTES")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Listar_Tiempo_Estado_Clientes(ByVal psConexion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_GTP_LIST_ACCIONES_CLIENTES", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTP_LIST_ACCIONES_CLIENTES")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Listar_Usuarios(ByVal psConexion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_GTP_LIST_USUARIOS", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTP_LIST_USUARIOS")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Listar_Acciones_XEstado(ByVal psConexion As String, ByVal codigo As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_GTP_LIST_ACCIONES_XESTADO", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@CODIGO", codigo)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTP_LIST_ACCIONES_XESTADO")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Llenar_Acciones_Cliente(ByVal psConexion As String, ByVal codCliente As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_GTP_LLENAR_COMBO_ACCIONES_XCLIENTES", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@COD_CLIENTE", codCliente)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTP_LLENAR_COMBO_ACCIONES_XCLIENTES")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Llenar_Estado(ByVal psConexion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_GTP_LLENAR_COMBO_ESTADO_CLIENTE", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTP_LLENAR_COMBO_ESTADO_CLIENTE")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Llenar_Acciones(ByVal psConexion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_GTP_LLENAR_COMBO_ACCIONES_CLIENTES", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTP_LLENAR_COMBO_ACCIONES_CLIENTES")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Lista_Tracking_Clientes(ByVal psConexion As String, ByVal codigo As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_GTPLISTA_CLIENTE_TRAKING", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@CodCliente", codigo)
        Cmd.Parameters.AddWithValue("@CodEmpresa", "0001")
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTPLISTA_CLIENTE_TRAKING")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Lista_Tracking_Acciones_Clientes(ByVal psConexion As String, ByVal codigo As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_GTPLISTA_CLIENTE_TRAKING_ACCIONES", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@CodCliente", codigo)
        Cmd.Parameters.AddWithValue("@CodEmpresa", "0001")
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTPLISTA_CLIENTE_TRAKING_ACCIONES")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Lista_Datos_Clientes(ByVal psConexion As String, ByVal codigo As String, ByVal ruc As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_GTP_LIST_DATOS_CLIENTE", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@CODIGO", codigo)
        Cmd.Parameters.AddWithValue("@RUC", ruc)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTP_LIST_DATOS_CLIENTE")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Lista_Contacto_Personas(ByVal psConexion As String, ByVal codigo As String, ByVal codContacto As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PROC_GTP_LIST_CONTACTO_PERSONAS", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@CODIGO", codigo)
        Cmd.Parameters.AddWithValue("@COD_CONTACTO", codContacto)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PROC_GTP_LIST_CONTACTO_PERSONAS")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Lista_Contacto_Clientes(ByVal psConexion As String, ByVal codigo As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PROC_GTP_LIST_CONTACTO_CLIENTES", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@CODIGO", codigo)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PROC_GTP_LIST_CONTACTO_CLIENTES")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Lista_Clientes(ByVal psConexion As String, ByVal razonSocial As String, ByVal cif As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_GTP_LIST_CLIENTES", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@RAZONSOCIAL", razonSocial)
        Cmd.Parameters.AddWithValue("@CIF", cif)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTP_LIST_CLIENTES")
        Da.Fill(Dt)
        Return Dt
    End Function


    Public Function Registra_Cliente(ByVal psconexion As String, ByVal cif As String, ByVal gps As String, ByVal adquira As String,
                                     ByVal fecha As String, ByVal nombre As String, ByVal gmi As String, ByVal telf2 As String,
                                     ByVal direccion As String, ByVal telf3 As String, ByVal ciudad As String, ByVal provincia As String,
                                     ByVal pais As String, ByVal codPostal As String, ByVal telfE As String, ByVal modoFacturacion As String,
                                     ByVal grupo As String, ByVal oc As String, ByVal modoEntrada As String, ByVal sociedad As String,
                                     ByVal cargoContacto As String, ByVal nomNegociador As String, ByVal emailNegociador As String,
                                     ByVal telfNegociador As String, ByVal extranjero As String, ByVal grupoABC As String, ByVal okCompras As String) As DataTable
        Dim Cn As New SqlConnection(psconexion)
        Dim Cmd As New SqlCommand("PRC_GTP_INS_CLIENTES", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@CIF", cif)
        Cmd.Parameters.AddWithValue("@COD_GPS", gps)
        Cmd.Parameters.AddWithValue("@ADQUIRA", adquira)
        Cmd.Parameters.AddWithValue("@FEC_ADQUIRA", fecha)
        Cmd.Parameters.AddWithValue("@NOMBRE", nombre)
        Cmd.Parameters.AddWithValue("@GMI", gmi)
        Cmd.Parameters.AddWithValue("@TELF2", telf2)
        Cmd.Parameters.AddWithValue("@DIRECCION", direccion)
        Cmd.Parameters.AddWithValue("@TELF3", telf3)
        Cmd.Parameters.AddWithValue("@CIUDAD", ciudad)
        Cmd.Parameters.AddWithValue("@PROVINCIA", provincia)
        Cmd.Parameters.AddWithValue("@PAIS", pais)
        Cmd.Parameters.AddWithValue("@COD_POSTAL", codPostal)
        Cmd.Parameters.AddWithValue("@TELF_EFECTIVO", telfE)
        Cmd.Parameters.AddWithValue("@MOD_FACTURACION", modoFacturacion)
        Cmd.Parameters.AddWithValue("@GRUPO", grupo)
        Cmd.Parameters.AddWithValue("@OC", oc)
        Cmd.Parameters.AddWithValue("@MOD_ENTRADA", modoEntrada)
        Cmd.Parameters.AddWithValue("@SOCIEDAD", sociedad)
        Cmd.Parameters.AddWithValue("@CARGO_CONTACTO", cargoContacto)
        Cmd.Parameters.AddWithValue("@NOM_NEGOCIADOR", nomNegociador)
        Cmd.Parameters.AddWithValue("@EMAIL_N", emailNegociador)
        Cmd.Parameters.AddWithValue("@TELF_N", telfNegociador)
        Cmd.Parameters.AddWithValue("@EXTRANJERO", extranjero)
        Cmd.Parameters.AddWithValue("@GRUPO_ABC", grupoABC)
        Cmd.Parameters.AddWithValue("@OK_COMPRAS", okCompras)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTP_INS_CLIENTES")
        Da.Fill(Dt)
        Return Dt
    End Function


    Public Function Actualizar_Cliente(ByVal psconexion As String, ByVal codigo As String, ByVal cif As String, ByVal gps As String, ByVal adquira As String,
                                        ByVal fecha As String, ByVal nombre As String, ByVal gmi As String, ByVal telf2 As String,
                                        ByVal direccion As String, ByVal telf3 As String, ByVal ciudad As String, ByVal provincia As String,
                                        ByVal pais As String, ByVal codPostal As String, ByVal telfE As String, ByVal modoFacturacion As String,
                                        ByVal grupo As String, ByVal oc As String, ByVal modoEntrada As String, ByVal sociedad As String,
                                        ByVal cargoContacto As String, ByVal nomNegociador As String, ByVal emailNegociador As String,
                                        ByVal telfNegociador As String, ByVal extranjero As String, ByVal grupoABC As String, ByVal okCompras As String) As DataTable
        Dim Cn As New SqlConnection(psconexion)
        Dim Cmd As New SqlCommand("PRC_GTP_UPD_CLIENTES", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@CODIGO", codigo)
        Cmd.Parameters.AddWithValue("@CIF", cif)
        Cmd.Parameters.AddWithValue("@COD_GPS", gps)
        Cmd.Parameters.AddWithValue("@ADQUIRA", adquira)
        Cmd.Parameters.AddWithValue("@FEC_ADQUIRA", fecha)
        Cmd.Parameters.AddWithValue("@NOMBRE", nombre)
        Cmd.Parameters.AddWithValue("@GMI", gmi)
        Cmd.Parameters.AddWithValue("@TELF2", telf2)
        Cmd.Parameters.AddWithValue("@DIRECCION", direccion)
        Cmd.Parameters.AddWithValue("@TELF3", telf3)
        Cmd.Parameters.AddWithValue("@CIUDAD", ciudad)
        Cmd.Parameters.AddWithValue("@PROVINCIA", provincia)
        Cmd.Parameters.AddWithValue("@PAIS", pais)
        Cmd.Parameters.AddWithValue("@COD_POSTAL", codPostal)
        Cmd.Parameters.AddWithValue("@TELF_EFECTIVO", telfE)
        Cmd.Parameters.AddWithValue("@MOD_FACTURACION", modoFacturacion)
        Cmd.Parameters.AddWithValue("@GRUPO", grupo)
        Cmd.Parameters.AddWithValue("@OC", oc)
        Cmd.Parameters.AddWithValue("@MOD_ENTRADA", modoEntrada)
        Cmd.Parameters.AddWithValue("@SOCIEDAD", sociedad)
        Cmd.Parameters.AddWithValue("@CARGO_CONTACTO", cargoContacto)
        Cmd.Parameters.AddWithValue("@NOM_NEGOCIADOR", nomNegociador)
        Cmd.Parameters.AddWithValue("@EMAIL_N", emailNegociador)
        Cmd.Parameters.AddWithValue("@TELF_N", telfNegociador)
        Cmd.Parameters.AddWithValue("@EXTRANJERO", extranjero)
        Cmd.Parameters.AddWithValue("@GRUPO_ABC", grupoABC)
        Cmd.Parameters.AddWithValue("@OK_COMPRAS", okCompras)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTP_UPD_CLIENTES")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Agregar_Persona_Cliente(ByVal psConexion As String, ByVal codCliente As String, ByVal categoria As String,
                                            ByVal ruc As String, ByVal razSocial As String, ByVal perCodigo As String, ByVal apePat As String,
                                            ByVal apeMat As String, ByVal nombres As String, ByVal nomContacto As String, ByVal tipo As String,
                                            ByVal tipoCli As String, ByVal provee As String, ByVal direccion As String, ByVal pais As String,
                                            ByVal dpto As String, ByVal prov As String, ByVal dist As String, ByVal email1 As String,
                                            ByVal email2 As String, ByVal web1 As String, ByVal web2 As String, ByVal telf1 As String,
                                            ByVal telf2 As String, ByVal telfOf As String, ByVal anexoOf As String, ByVal telfCelular As String,
                                            ByVal fax1 As String, ByVal fax2 As String, ByVal sysMod As String, ByVal sysCre As String,
                                            ByVal certInscr As String, ByVal pago As String, ByVal cepro As String, ByVal accion As String,
                                            ByVal respSolucion As String, ByVal codSistema As String, ByVal diasCredito As String, ByVal extranjero As String,
                                            ByVal padron As String, ByVal referencia As String, ByVal fechaNac As String, ByVal urbanizacion As String,
                                            ByVal comercial As String, ByVal estSunat As String, ByVal rubro As String, ByVal provCod As String,
                                            ByVal proviene As String, ByVal cuii As String, ByVal user As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_CONT_INS_PERSONA", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@COD_CLIENTE", codCliente)
        Cmd.Parameters.AddWithValue("@CATEGORIA", categoria)
        Cmd.Parameters.AddWithValue("@RUC", ruc)
        Cmd.Parameters.AddWithValue("@RAZ_SOCIAL", razSocial)
        Cmd.Parameters.AddWithValue("@PERSON_CODIGO", perCodigo)
        Cmd.Parameters.AddWithValue("@APE_PAT", apePat)
        Cmd.Parameters.AddWithValue("@APE_MAT", apeMat)
        Cmd.Parameters.AddWithValue("@NOMBRES", nombres)
        Cmd.Parameters.AddWithValue("@NOM_CONTACTO", nomContacto)
        Cmd.Parameters.AddWithValue("@TIPO", tipo)
        Cmd.Parameters.AddWithValue("@TIPO_CLI", tipoCli)
        Cmd.Parameters.AddWithValue("@PROVEE", provee)
        Cmd.Parameters.AddWithValue("@DIRECCION", direccion)
        Cmd.Parameters.AddWithValue("@PAIS", pais)
        Cmd.Parameters.AddWithValue("@DPTO", dpto)
        Cmd.Parameters.AddWithValue("@PROV", prov)
        Cmd.Parameters.AddWithValue("@DIST", dist)
        Cmd.Parameters.AddWithValue("@EMAIL1", email1)
        Cmd.Parameters.AddWithValue("@EMAIL2", email2)
        Cmd.Parameters.AddWithValue("@WEB1", web1)
        Cmd.Parameters.AddWithValue("@WEB2", web2)
        Cmd.Parameters.AddWithValue("@TELF1", telf1)
        Cmd.Parameters.AddWithValue("@TELF2", telf2)
        Cmd.Parameters.AddWithValue("@TELF_OF", telfOf)
        Cmd.Parameters.AddWithValue("@ANEXO_OF", anexoOf)
        Cmd.Parameters.AddWithValue("@TELF_CELULAR", telfCelular)
        Cmd.Parameters.AddWithValue("@FAX1", fax1)
        Cmd.Parameters.AddWithValue("@FAX2", fax2)
        Cmd.Parameters.AddWithValue("@SYS_MOD", sysMod)
        Cmd.Parameters.AddWithValue("@SYS_CRE", sysCre)
        Cmd.Parameters.AddWithValue("@CERT_INSCR", certInscr)
        Cmd.Parameters.AddWithValue("@PAGO", pago)
        Cmd.Parameters.AddWithValue("@CEPRO", cepro)
        Cmd.Parameters.AddWithValue("@RESP_SOLUCION", respSolucion)
        Cmd.Parameters.AddWithValue("@COD_SISTEMA", codSistema)
        Cmd.Parameters.AddWithValue("@DIAS_CREDITO", diasCredito)
        Cmd.Parameters.AddWithValue("@EXTRANJERO", extranjero)
        Cmd.Parameters.AddWithValue("@PADRON", padron)
        Cmd.Parameters.AddWithValue("@REFERENCIA", referencia)
        Cmd.Parameters.AddWithValue("@FECHA_NAC", fechaNac)
        Cmd.Parameters.AddWithValue("@URBANIZACION", urbanizacion)
        Cmd.Parameters.AddWithValue("@COMERCIAL", comercial)
        Cmd.Parameters.AddWithValue("@EST_SUNAT", estSunat)
        Cmd.Parameters.AddWithValue("@RUBRO", rubro)
        Cmd.Parameters.AddWithValue("@PROV_COD", provCod)
        Cmd.Parameters.AddWithValue("@PROVIENE", proviene)
        Cmd.Parameters.AddWithValue("@CUII", cuii)
        Cmd.Parameters.AddWithValue("@USER", user)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_CONT_INS_PERSONA")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Actualizar_Persona_Cliente(ByVal psConexion As String, ByVal codCliente As String, ByVal categoria As String,
                                            ByVal ruc As String, ByVal razSocial As String, ByVal perCodigo As String, ByVal apePat As String,
                                            ByVal apeMat As String, ByVal nombres As String, ByVal nomContacto As String, ByVal tipo As String,
                                            ByVal tipoCli As String, ByVal provee As String, ByVal direccion As String, ByVal pais As String,
                                            ByVal dpto As String, ByVal prov As String, ByVal dist As String, ByVal email1 As String,
                                            ByVal email2 As String, ByVal web1 As String, ByVal web2 As String, ByVal telf1 As String,
                                            ByVal telf2 As String, ByVal telfOf As String, ByVal anexoOf As String, ByVal telfCelular As String,
                                            ByVal fax1 As String, ByVal fax2 As String, ByVal sysMod As String, ByVal sysCre As String,
                                            ByVal certInscr As String, ByVal pago As String, ByVal cepro As String, ByVal accion As String,
                                            ByVal respSolucion As String, ByVal codSistema As String, ByVal diasCredito As String, ByVal extranjero As String,
                                            ByVal padron As String, ByVal referencia As String, ByVal fechaNac As String, ByVal urbanizacion As String,
                                            ByVal comercial As String, ByVal estSunat As String, ByVal rubro As String, ByVal provCod As String,
                                            ByVal proviene As String, ByVal cuii As String, ByVal user As String, ByVal codPersona As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_CONT_UPD_PERSONA", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@COD_CLIENTE", codCliente)
        Cmd.Parameters.AddWithValue("@CATEGORIA", categoria)
        Cmd.Parameters.AddWithValue("@RUC", ruc)
        Cmd.Parameters.AddWithValue("@RAZ_SOCIAL", razSocial)
        Cmd.Parameters.AddWithValue("@PERSON_CODIGO", perCodigo)
        Cmd.Parameters.AddWithValue("@APE_PAT", apePat)
        Cmd.Parameters.AddWithValue("@APE_MAT", apeMat)
        Cmd.Parameters.AddWithValue("@NOMBRES", nombres)
        Cmd.Parameters.AddWithValue("@NOM_CONTACTO", nomContacto)
        Cmd.Parameters.AddWithValue("@TIPO", tipo)
        Cmd.Parameters.AddWithValue("@TIPO_CLI", tipoCli)
        Cmd.Parameters.AddWithValue("@PROVEE", provee)
        Cmd.Parameters.AddWithValue("@DIRECCION", direccion)
        Cmd.Parameters.AddWithValue("@PAIS", pais)
        Cmd.Parameters.AddWithValue("@DPTO", dpto)
        Cmd.Parameters.AddWithValue("@PROV", prov)
        Cmd.Parameters.AddWithValue("@DIST", dist)
        Cmd.Parameters.AddWithValue("@EMAIL1", email1)
        Cmd.Parameters.AddWithValue("@EMAIL2", email2)
        Cmd.Parameters.AddWithValue("@WEB1", web1)
        Cmd.Parameters.AddWithValue("@WEB2", web2)
        Cmd.Parameters.AddWithValue("@TELF1", telf1)
        Cmd.Parameters.AddWithValue("@TELF2", telf2)
        Cmd.Parameters.AddWithValue("@TELF_OF", telfOf)
        Cmd.Parameters.AddWithValue("@ANEXO_OF", anexoOf)
        Cmd.Parameters.AddWithValue("@TELF_CELULAR", telfCelular)
        Cmd.Parameters.AddWithValue("@FAX1", fax1)
        Cmd.Parameters.AddWithValue("@FAX2", fax2)
        Cmd.Parameters.AddWithValue("@SYS_MOD", sysMod)
        Cmd.Parameters.AddWithValue("@SYS_CRE", sysCre)
        Cmd.Parameters.AddWithValue("@CERT_INSCR", certInscr)
        Cmd.Parameters.AddWithValue("@PAGO", pago)
        Cmd.Parameters.AddWithValue("@CEPRO", cepro)
        Cmd.Parameters.AddWithValue("@RESP_SOLUCION", respSolucion)
        Cmd.Parameters.AddWithValue("@COD_SISTEMA", codSistema)
        Cmd.Parameters.AddWithValue("@DIAS_CREDITO", diasCredito)
        Cmd.Parameters.AddWithValue("@EXTRANJERO", extranjero)
        Cmd.Parameters.AddWithValue("@PADRON", padron)
        Cmd.Parameters.AddWithValue("@REFERENCIA", referencia)
        Cmd.Parameters.AddWithValue("@FECHA_NAC", fechaNac)
        Cmd.Parameters.AddWithValue("@URBANIZACION", urbanizacion)
        Cmd.Parameters.AddWithValue("@COMERCIAL", comercial)
        Cmd.Parameters.AddWithValue("@EST_SUNAT", estSunat)
        Cmd.Parameters.AddWithValue("@RUBRO", rubro)
        Cmd.Parameters.AddWithValue("@PROV_COD", provCod)
        Cmd.Parameters.AddWithValue("@PROVIENE", proviene)
        Cmd.Parameters.AddWithValue("@CUII", cuii)
        Cmd.Parameters.AddWithValue("@USER", user)
        Cmd.Parameters.AddWithValue("@CODIGO", codPersona)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_CONT_UPD_PERSONA")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Cambiar_Accion_Cliente(ByVal psConexion As String, ByVal codCliente As String, ByVal estado As String,
                                           ByVal fecha As String, ByVal hora As String, ByVal usuario As String,
                                           ByVal accion As String, ByVal referencia As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_GTP_UPD_TRAKING_ACCION_CLIENTE", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@CODIGO", codCliente)
        Cmd.Parameters.AddWithValue("@ESTADO", estado)
        Cmd.Parameters.AddWithValue("@FECHA", fecha)
        Cmd.Parameters.AddWithValue("@HORA", hora)
        Cmd.Parameters.AddWithValue("@USER", usuario)
        Cmd.Parameters.AddWithValue("@ACCION", accion)
        Cmd.Parameters.AddWithValue("@REFERENCIA", referencia)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTP_UPD_TRAKING_ACCION_CLIENTE")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Cambiar_Estado_Cliente(ByVal psConexion As String, ByVal codCliente As String,
                                           ByVal estado As String, ByVal fecha As String, ByVal hora As String,
                                           ByVal usuario As String, ByVal codAsignado As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_GTP_UPD_TRAKING_ESTADO_CLIENTE", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@COD_CLIENTE", codCliente)
        Cmd.Parameters.AddWithValue("@EST_CLIENTE", estado)
        Cmd.Parameters.AddWithValue("@FECHA", fecha)
        Cmd.Parameters.AddWithValue("@HORA", hora)
        Cmd.Parameters.AddWithValue("@USUARIO", usuario)
        Cmd.Parameters.AddWithValue("@COD_ASIGNADO", codAsignado)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTP_UPD_TRAKING_ESTADO_CLIENTE")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Elimina_Cliente(ByVal psConexion As String, ByVal codigo As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_GTP_DEL_CLIENTE", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@CODIGO", codigo)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTP_DEL_CLIENTE")
        Da.Fill(Dt)
        Return Dt
    End Function


    '----------------------------------------------------------'
    Public Function Registra_Estado_Accion_Cliente(ByVal psConexion As String, ByVal estado As String, ByVal accion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_GTP_INS_ESTADO_ACCION_CLIENTE", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@ESTADO", estado)
        Cmd.Parameters.AddWithValue("@ACCION", accion)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTP_INS_ESTADO_ACCION_CLIENTE")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Agregar_Tiempo_Estado_Cliente(ByVal psConexion As String, ByVal estado As String, ByVal dia As String,
                                                  ByVal hora As String, ByVal minuto As String, ByVal total As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_GTP_INS_TIEMPO_ESTADO_CLIENTE", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@ESTADO", estado)
        Cmd.Parameters.AddWithValue("@DIA", dia)
        Cmd.Parameters.AddWithValue("@HORA", hora)
        Cmd.Parameters.AddWithValue("@MINUTO", minuto)
        Cmd.Parameters.AddWithValue("@TOTAL", total)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTP_INS_TIEMPO_ESTADO_CLIENTE")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Editar_Tiempo_Estado_Cliente(ByVal psConexion As String, ByVal estado As String, ByVal dia As String,
                                                  ByVal hora As String, ByVal minuto As String, ByVal total As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_GTP_UPD_TIEMPO_ESTADO_CLIENTE", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@ESTADO", estado)
        Cmd.Parameters.AddWithValue("@DIA", dia)
        Cmd.Parameters.AddWithValue("@HORA", hora)
        Cmd.Parameters.AddWithValue("@MINUTO", minuto)
        Cmd.Parameters.AddWithValue("@TOTAL", total)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTP_UPD_TIEMPO_ESTADO_CLIENTE")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Elimina_Acciones_XEstado(ByVal psConexion As String, ByVal estado As String, ByVal accion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_GTP_DEL_ACCIONES_XESTADO", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@ESTADO", estado)
        Cmd.Parameters.AddWithValue("@ACCION", accion)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTP_DEL_ACCIONES_XESTADO")
        Da.Fill(Dt)
        Return Dt
    End Function


End Class
