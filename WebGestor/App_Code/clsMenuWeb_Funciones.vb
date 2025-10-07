Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports WebGestor
Public Class clsMenuWeb_Funciones
    Dim obj As New clsMenuWeb_Consultas
    Public Sub Llena_ItemsMenu(ByVal cbo As DropDownList, ByVal pdGrupo As Double, ByVal psCodEmpresa As String)
        cbo.Items.Clear()
        cbo.DataSource = obj.Lista_MenuItems(pdGrupo, psCodEmpresa)
        cbo.DataTextField = "ITEM_NOMBRE"
        cbo.DataValueField = "ITEM_CODIGO"
        cbo.DataBind()
        cbo.Items.Add("< Seleccionar >") : cbo.SelectedValue = "< Seleccionar >"
    End Sub
    Public Sub Llena_Categoria_xItem(ByVal cbo As DropDownList, ByVal pdGrupo As Double, _
                                     ByVal psCodEmpresa As String, ByVal pdCodItem As Double)
        cbo.Items.Clear()
        cbo.DataSource = obj.Lista_Categoris_xItems(pdGrupo, psCodEmpresa, pdCodItem)
        cbo.DataTextField = "CATEG_NOMBRE"
        cbo.DataValueField = "CATEG_CODIGO"
        cbo.DataBind()
        cbo.Items.Add("< Seleccionar >") : cbo.SelectedValue = "< Seleccionar >"
    End Sub
End Class
