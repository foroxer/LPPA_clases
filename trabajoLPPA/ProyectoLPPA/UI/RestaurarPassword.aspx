<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RestaurarPassword.aspx.cs" Inherits="UI_RestaurarPassword" MasterPageFile="~/Site.Master" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h2>Restaurar Password.</h2>
    <div class="row">
        <div class="col-md-8">
            <section id="loginForm">
                <div class="form-horizontal">
                    <h4>Ingrese su usuario para restaurar su contraseña.</h4>
                    <hr />
                    <div>
                        <div class="form-group">
                            <asp:Label runat="server" AssociatedControlID="User" CssClass="col-md-2 control-label">Usuario</asp:Label>
                            <div class="col-md-10">
                                <asp:TextBox runat="server" ID="User" CssClass="form-control" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="User" CssClass="text-danger" ErrorMessage="El campo de usuario es obligatorio." />
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-md-offset-2 col-md-10">
                                <asp:Button runat="server" OnClick="restaurar" Text="Restaurar" CssClass="btn btn-default" />
                            </div>
                        </div>
                    </div>
                </div>
            </section>
        </div>
    </div>


</asp:Content>
