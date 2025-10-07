<%@ Page Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="Personal_Mostrar.aspx.vb" Inherits="Personal_Mostrar" title="Personal" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"	Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <style type="text/css">
        input[type="file"] {
            width: 0.1px;
            height: 0.1px;
            opacity: 0;
        }
        .estiloImagen {
            width: 200px;
            height: 150px;
        }  
        .imagen {
            flex-shrink: 0;  /* Evita que la imagen se reduzca demasiado */
        }

        .imagen-perfil {
            border-radius: 10px;  /* Opcional: Bordes redondeados */
            box-shadow: 2px 2px 10px rgba(0, 0, 0, 0.2);  /* Sombra elegante */
        }     
    </style>

     <script type="text/javascript">
        function showimagepreview(input) {
            var fileInput = document.getElementById('FileUpload2');
            var filePath = fileInput.value;
            var allowedExtensions = /(.jpg|.jpeg|.png|.gif|.jp|.jfif)$/i;
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                if (!allowedExtensions.exec(filePath)) {
                    alert('Seleccione una imagen');
                    fileInput.value = '';
                    document.getElementById("imagenCarga2").setAttribute("src", "");
                    return false;
                } else {
                    reader.onload = function (e) {
                        document.getElementById("imagenCarga2").setAttribute("src", e.target.result);
                    }
                    reader.readAsDataURL(input.files[0]);
                    return false;
                }
            } else {
                document.getElementById("imagenCarga2").setAttribute("src", "");
            }
        }
         
    </script>


    <div class="container">
        <div class="row">
            <div class="col-md-12">
                <asp:Label ID="LblEtiq1" runat="server" Text="Define Personal de Empresa" CssClass="Titulos" />
            </div> 
        </div>
        <div class="row espacio">
            <div class="col-md-12">   
                <asp:Label ID="LblError" runat="server"  CssClass="control-label-2"  Text="" forecolor="Red"></asp:Label>
            </div> 
        </div>  
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <div class="row espacio">
                    <div class="col-lg-6">
                        <asp:Label ID="LblEtiq2" runat="server"  CssClass="control-label-2"  Text="Personal de Grupo"></asp:Label>
                        <asp:DropDownList ID="cboGE" runat="server" CssClass="form-control"  AutoPostBack="True">
                        </asp:DropDownList>
                    </div>
                    <div class="col-lg-6">
                        <asp:Label ID="LblEtiq3" runat="server"  CssClass="control-label-2"  Text="Empresa"></asp:Label>
                        <asp:DropDownList ID="cboGEE" runat="server" CssClass="form-control"  AutoPostBack="True">
                        </asp:DropDownList>
                    </div>
                </div>  
                <div class="row espacio">
                    <div class="col-lg-4">
                        </div>
                    <div class="col-lg-2">
                        <asp:Button ID="btnActivo" runat="server" ControlStyle-CssClass="form-control btn btn-default" Text="Activo" />
                    </div>
                    <div class="col-lg-2">
                        <asp:Button ID="btnRetirado" runat="server" ControlStyle-CssClass="form-control btn btn-default" Text="Retirado" />
                    </div>
                    <div class="col-lg-2">
                        <asp:Button ID="btnEliminado" runat="server" ControlStyle-CssClass="form-control btn btn-default" Text="Eliminado" />
                    </div>
                    <div class="col-lg-2">
                        <asp:Button ID="btnNuevo" runat="server" ControlStyle-CssClass="form-control btn btn-default" Text="Nuevo" />
                    </div>
                </div>
                
                <div id="fraDatosPersonal" runat="server" visible ="false" class="espacio">
                    <div class="row espacio">
                        <div class="col-lg-12">
                            <asp:Label ID="lblEtiqueta" runat="server"  CssClass="control-label-2" Font-Bold="true" Text="Nuevo Personal" forecolor="Maroon" ></asp:Label>
                        </div>
                    </div>
                              
                    <div id="dvFoto" runat="server" visible ="false">
                        <div class="row espacio">
                            <div class="col-md-3">
                                <div class="imagen">
                                    <asp:Image ID="imgUsuario" runat="server" Width="150px" Height="150px"  CssClass="imagen-perfil" />
                                </div> 
                            </div>
                        </div>
                    
                        <div class="row espacio">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <div class="col-lg-2">
                                        <asp:FileUpload ID="FileUpload2" Font-Names="file" runat="server" ClientIDMode="Static"
                                            onchange="showimagepreview(this)" onclick="Ayuda" />
                                        <label id="FileNombre2" runat="server" class="btn btn-default" for="FileUpload2" >Seleccionar Foto</label>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="BtnGuardarImg" />
                                </Triggers>
                            </asp:UpdatePanel>                          
                            <div class="col-lg-2">
                                <asp:Button ID="BtnGuardarImg" ControlStyle-CssClass="btn btn-success" runat="server" Text="Guardar Foto"/>
                            </div> 
                        </div>   
                    
                        <div class="row espacio">
                            <div class="col-md-3">
                                <img id="imagenCarga2" src=""  alt=""/> 
                            </div>
                        </div> 
                    </div>                    
                    
                    <div class="row espacio">
                        <div class="col-lg-3">
                            <asp:Label ID="lbl13" runat="server" CssClass="control-label-2"  Text="Código"></asp:Label>
                            <asp:TextBox ID="txtUsuario" runat="server" Font-Bold="True" CssClass="form-control" ReadOnly="True"></asp:TextBox>
                        </div>
                        <div class="col-lg-3">
                            <asp:Label ID="lbl15" runat="server" CssClass="control-label-2"  Text="Cód. Interno"></asp:Label>
                            <asp:TextBox ID="txtCodInterno" runat="server"  CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row espacio">
                        <div class="col-lg-3">
                            <asp:Label ID="lbl14" runat="server" CssClass="control-label-2" Text="Estado"></asp:Label>
                            <asp:DropDownList ID="cboEstado" runat="server" Enabled="False" CssClass="form-control" >
                            </asp:DropDownList>
                        </div>
                        <div class="col-lg-3">
                            <asp:Label ID="lbl6" runat="server" CssClass="control-label-2" Text="Sexo"></asp:Label>
                            <asp:DropDownList ID="cboSexo" runat="server" CssClass="form-control" >
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row espacio">
                        <div class="col-lg-3">
                            <asp:Label ID="lbl3" runat="server" CssClass="control-label-2"  Text="Ape. Paterno"></asp:Label>
                            <asp:TextBox ID="txtApepat" runat="server"  CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-lg-3">
                            <asp:Label ID="lbl4" runat="server" CssClass="control-label-2"  Text="Ape. Materno"></asp:Label>
                            <asp:TextBox ID="txtApemat" runat="server"  CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-lg-6">
                            <asp:Label ID="lbl5" runat="server" CssClass="control-label-2"  Text="Nombres"></asp:Label>
                            <asp:TextBox ID="txtNombres" runat="server"  CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row espacio">
                        <div class="col-lg-3">
                            <asp:Label ID="lbl7" runat="server" CssClass="control-label-2" Text="País"></asp:Label>
                            <asp:DropDownList ID="cboPais" runat="server"  CssClass="form-control" AutoPostBack="true" >
                            </asp:DropDownList>
                        </div>
                        <div class="col-lg-3">
                            <asp:Label ID="lbl8" runat="server" CssClass="control-label-2" Text="Dpto."></asp:Label>
                            <asp:DropDownList ID="cboDpto" runat="server"  CssClass="form-control"  AutoPostBack="true" >
                            </asp:DropDownList>
                        </div>
                        <div class="col-lg-3">
                            <asp:Label ID="lbl9" runat="server" CssClass="control-label-2" Text="Provincia"></asp:Label>
                            <asp:DropDownList ID="cboProv" runat="server"  CssClass="form-control"  AutoPostBack="true" >
                            </asp:DropDownList>
                        </div>
                        <div class="col-lg-3">
                            <asp:Label ID="lbl10" runat="server" CssClass="control-label-2" Text="Distrito"></asp:Label>
                            <asp:DropDownList ID="cboDist" runat="server"  CssClass="form-control" >
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row espacio">
                        <div class="col-lg-6">
                            <asp:Label ID="lbl11" runat="server" CssClass="control-label-2"  Text="Dirección"></asp:Label>
                            <asp:TextBox ID="txtDireccion" runat="server"  CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-lg-6">
                            <asp:Label ID="lbl12" runat="server" CssClass="control-label-2"  Text="Correo electrónico"></asp:Label>
                            <asp:TextBox ID="txtEmail" runat="server"  CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    
                    <div class="row espacio">
                        <asp:TextBox ID="txtCodPersonal" runat="server"  CssClass="form-control" Visible ="false" ></asp:TextBox>
                    </div>
                    <div class="row espacio">
                        <div class="col-lg-2">
                            <asp:Button ID="BtnGuardar" runat="server" ControlStyle-CssClass="form-control btn btn-default" Text="Guardar" />
                        </div>
                        <div class="col-lg-2">
                            <asp:Button ID="BtnCancelar" runat="server" ControlStyle-CssClass="form-control btn btn-default" Text="Cancelar" />
                        </div>
                    </div>
                </div>

                <div class="row espacio">
                    <div class="col-lg-12">
                        <asp:GridView ID="FlexP" runat="server"  AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                            <Columns>
                                <asp:ButtonField CommandName="Editar" Text="Editar" ButtonType="Button">
                                    <ControlStyle CssClass="Form-control btn btn-default"></ControlStyle>
                                    <ItemStyle Height="10px" Width="10px" />
                                </asp:ButtonField>
                                <asp:ButtonField CommandName="Foto" Text="Foto" ButtonType="Button">
                                    <ControlStyle CssClass="Form-control btn btn-default"></ControlStyle>
                                    <ItemStyle Height="10px" Width="10px" />
                                </asp:ButtonField>
                                <asp:BoundField DataField="PERSON_CODIGO" HeaderText="C&#243;digo">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="PERSON_COD_INTERNO" HeaderText="Cod. Interno">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="PERSON_APEPAT" HeaderText="Ape. Paterno">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="PERSON_APEMAT" HeaderText="Ape. Materno">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="PERSON_NOMBRES" HeaderText="Nombres">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="GEE_NOMBRE" HeaderText="Grupo Empresa -&gt; Empresa que pertenece">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="PERSON_CODEST">
                                    <ItemStyle ForeColor="White" Width="0px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="PERSON_SYS_EST">
                                    <ItemStyle ForeColor="White" Width="0px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="GRPOEMPRESA_CODIGO">
                                    <ItemStyle ForeColor="White" Width="0px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="EMPRESA_CODIGO">
                                    <ItemStyle ForeColor="White" Width="0px" />
                                </asp:BoundField>
                            </Columns>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:GridView>
                    </div>
                </div>

            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="cboGE" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="cboDpto" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="cboProv" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="cboPais" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="btnActivo" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnEliminado" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnRetirado" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnNuevo" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnCancelar" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnGuardar" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="FlexP" EventName="RowCommand" />
            </Triggers>
        </asp:UpdatePanel>
    </div> 
</asp:Content>

