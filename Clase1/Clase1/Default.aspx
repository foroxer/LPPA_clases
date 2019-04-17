<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplication3.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <center>
        <form id="form1" runat="server">
            <div>
                <asp:TextBox ID="user" runat="server" Width="167px"></asp:TextBox>
            &nbsp;Usuario<br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <textarea id="comment" cols="20" name="S1" rows="2"  runat="server"></textarea>&nbsp;Comentario<br /></div>
            <asp:Button ID="click" runat="server" Text="click" OnClick="click1" />
        
        </form>
    </center>
</body>
</html>