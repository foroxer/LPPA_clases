<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeFile="VerPerfil.aspx.cs" Inherits="UI_VerPerfil" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <div class="jumbotron col-sm-12" style="padding-right:0px;padding-left:5px;">
            <h2>Mis Datos</h2>
            <div class="col-sm-3">
                <img src="" alt="..." class="img-circle" style="height:250px;" id="foto">
            </div>
            <div class="col-sm-9">
                <div class="input-group col-sm-4 col-xs-4 " style="margin:10px;clear: both;">
                    <span class="input-group-addon" >Nombre</span>
                    <input type="text" class="form-control" id="nombre" aria-describedby="basic-addon1">
                </div>
                <div class="input-group col-sm-4 col-xs-4" style="margin:10px;clear: both;">
                    <span class="input-group-addon">Apellido</span>
                    <input type="text" class="form-control" id="apellido" aria-describedby="basic-addon1">
                </div>
                <div class="input-group col-sm-4 col-xs-4" style="margin:10px;clear: both;">
                    <span class="input-group-addon">DNI</span>
                    <input type="text" class="form-control" id="dni" aria-describedby="basic-addon1">
                </div>
                <div class="input-group col-sm-4 col-xs-4" style="margin:10px;clear: both;">
                    <span class="input-group-addon">Fecha de Nacimiento</span>
                    <input type="text" class="form-control" id="fecnac" aria-describedby="basic-addon1">
                </div>
                <div class="input-group col-sm-4 col-xs-4" style="">
                    <span class="input-group-addon" style="">
                        <label for="aptoFisico" style="float:left">Registro apto físico</label>
                        <input type="checkbox" id="aptoFisico" />
                    </span>
                </div>
                <!-- /input-group -->
            </div>
            
        </div>
    </div>
    <script type="text/javascript">
        buscar();
        function buscar(e) {
            $.ajax({
                type: "GET",
                url: '<%= ResolveUrl("VerPerfil.aspx/getPerfil") %>',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    console.log("Request: " + XMLHttpRequest.toString() + "\n\nStatus: " + textStatus + "\n\nError: " + errorThrown);
                },
                success: function (data) {
                    llenarCampos(JSON.parse(data.d));
                },
                complete: function (jqXHR, status) {
                    //alert("complete: " + status + "\n\nResponse: " + jqXHR.responseText);
                }
            });
        }

        function llenarCampos(data) {
            $("#nombre").val(data.nombre);
            $("#apellido").val(data.apellido);
            $("#fecnac").val(data.fecnac);
            $("#dni").val(data.dni);
            $("#aptoFisico").attr("checked", data.aptoFisico);
            $("#foto").attr("src", data.fotoURL);
        }
    </script>
</asp:Content>

