<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
     <div class="jumbotron">
       <h1>MEGATLIN</h1>
        
        <p class="lead">
            Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.
        </p>
        <h3>Ventajas para la Empresa</h3>
        <p>
            Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.
        </p>
        <h3>Ventajas para el cliente</h3>
        <p>
            Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.
        </p>
    </div>
    <script type="text/javascript">
        var json = { desde: "", hasta: "" };
        function llamar() {
            
            $.ajax({
                type: "POST",
                url: '<%= ResolveUrl("~/UI/WS/Webservice.aspx/getBitacora") %>',
                 contentType: "application/json; charset=utf-8",
                 data: JSON.stringify(json),
                 dataType: "json",
                 error: function (XMLHttpRequest, textStatus, errorThrown) {
                     console.log("Request: " + XMLHttpRequest.toString() + "\n\nStatus: " + textStatus + "\n\nError: " + errorThrown);
                     console.log(XMLHttpRequest.responseText);
                 },
                 success: function (data) {
                     console.log(data.d);
                     
                 },
                 complete: function (jqXHR, status) {
                     //alert("complete: " + status + "\n\nResponse: " + jqXHR.responseText);
                }
            });
        }
       

</script>
    
</asp:Content>


