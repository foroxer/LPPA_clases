<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeFile="Trainer.aspx.cs" Inherits="Trainer" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="">
        <input type="hidden" id="caller" value="Trainer" />
        <div class="page-header">
            <h1>Tus clientes</h1>
        </div>

        <asp:DropDownList ID="DropDownList1"  runat="server">
            <asp:ListItem Text="DNI" Value="dni"></asp:ListItem>
            <asp:ListItem Text="Apellido" Value="apellido"></asp:ListItem>
            <asp:ListItem Text="Numero de socio" Value="numeroSocio"></asp:ListItem>
        </asp:DropDownList>

        <asp:TextBox ID="buscarPor" runat="server"></asp:TextBox>

        <asp:Button CssClass="btn btn-default" runat="server" ID="botonFiltrar" Text="Filtrar"></asp:Button>
        
        <% foreach (XElement cliente in clientes)
            { %>
        <div id="container" class="panel panel-primary">
            <div class="panel-heading ">
                <%= cliente.Element("apellido").Value %>  <%= cliente.Element("nombre").Value %>
            </div>
            <div class="panel-body">
                <h5 class="card-title">DNI: <%= cliente.Element("dni").Value %> </h5>
                <h5 class="card-title">Numero de Socio: <%= cliente.Element("numeroSocio").Value %> </h5>
            </div>
        </div>
        <% } %>
    </div>

</asp:Content>
