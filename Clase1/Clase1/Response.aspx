<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Response.aspx.cs" Inherits="WebApplication3.WebForm2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        #TextArea1 {
            margin-bottom: 0px;
        }
    </style>
</head>
<body>
    <center>
    <form id="form1" runat="server">
        <div>
            Su usuario es :<asp:Label ID="user" runat="server" Text=""></asp:Label>
            <br />
            su comentario es:
            <textarea id="comment" cols="20" name="S1" rows="2"  runat="server"></textarea><br />
            <br />
            <asp:Button ID="volverBtn" runat="server" Text="volver" OnClick="volverBtn_Click" />
        </div>
    </form>
        </center>
</body>
</html>