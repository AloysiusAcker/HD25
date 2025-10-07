<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CapturarImagen.aspx.vb" Inherits="EvaluacionProcesos_CapturarImagen" %>
<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">

    <%--<meta http-equiv="refresh" content="30;url=WebCamCapture.aspx">--%>

 

    <style type="text/css">

        #profile_pic_wrapper

        {

            position: relative;

            border: #ccc solid 1px;

            width: 120px;

            height: 120px;

            border: none;

        }

 

            #profile_pic_wrapper a

            {

                position: absolute;

                display: none;

                top: 30;

                right: 0;

                margin-top: -30px;

                line-height: 20px;

                padding: 5px;

                color: #fff;

                background-color: #333;

                width: 110px;

                text-decoration: underline;

                text-align: center;

                z-index: 100;

                text-decoration: none;

                font-family: Arial;

                font-size: 10px;

            }

 

            #profile_pic_wrapper:hover a

            {

                position: absolute;

                margin: 90px 0px 0px 0px;

                display: block;

                text-decoration: none;

                font-family: Arial;

                font-size: 10px;

            }

 

                #profile_pic_wrapper:hover a:hover

                {

                    text-decoration: none;

                    font-family: Arial;

                    font-size: 10px;

                }

        .profile_pic

        {

            width: 120px;

            height: 120px;

        }

    </style>

 

    <title>Web capture</title>

</head>

<body>

    <form id="form1" runat="server">

      <div style="float: left; padding-left: 35px;" id="profile_pic_wrapper">

      <asp:Image ID="img" Width="120" Height="120" runat="server" Style="float: left;" />

      <asp:LinkButton ID="Linkbutton1" runat="server" OnClick="Linkbutton1_Click">Change  Photo</asp:LinkButton>

        </div>

    </form> 

</body>

</html>