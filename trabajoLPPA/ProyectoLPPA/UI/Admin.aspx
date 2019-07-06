<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeFile="Admin.aspx.cs" Inherits="Admin" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron">
        <h1>Bienvenido Admin</h1>
        <div>
            <h2>Valide un mail</h2>
        </div>
        <div>
            <button onclick="verificar()" class="btn btn-secondary">
                Validar Mail
            </button>
        </div>
        <div class="accordion" id="accordionExample">
            <div class="card">
                <div class="card-header" id="headingTwo">
                    <h2 class="mb-0">
                        <button class="btn btn-link collapsed" type="button" data-toggle="collapse" data-target="#collapseTwo" aria-expanded="false" aria-controls="collapseTwo">
                            Ver Bitácora
                        </button>
                    </h2>
                </div>
                <div id="collapseTwo" class="collapse" aria-labelledby="headingTwo" data-parent="#accordionExample">
                    <div class="card card-body">
                        <div>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <div id="DESDE" class="col-sm-6">
                                    </div>
                                    <div id="HASTA" class="col-sm-6">
                                    </div>

                                </ContentTemplate>
                            </asp:UpdatePanel>

                            <input type="hidden" id="desdeFecha" runat="server" />
                            <input type="hidden" id="hastaFecha" runat="server" />
                        </div>
                        <div>
                        </div>
                        <div>
                            <div >
                                <button onclick="buscar()" class="btn btn-secondary" type="button" style="margin: 15px">
                                    Buscar
                                </button>
                            </div >
                            <div>
                                
                            <table id="table" class="table table-striped table-bordered" style="width: 100%">
                            </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        var dataTable = $('#table').DataTable({
            columns: [
                { data: '_fecha', title: "FECHA" },
                { data: '_mensaje', title: "MENSAJE" },
                { data: '_nombre', title: "NOMBRE" },
            ]
        });
        $(function () {
            $("#DESDE").datepicker({
                "showAnim": "drop",
                "dateFormat": "dd-mm-yyyy"
            }).val('');
            $("#HASTA").datepicker({
                "showAnim": "drop",
                "dateFormat": "dd-mm-yyyy",
                defaultDate: ""
            }).val('');
        });

        function buscar(e) {
            var desdeVal = $("#DESDE").datepicker('getDate');
            var hastaVal = $("#HASTA").datepicker('getDate');
            var json = { desde: desdeVal, hasta: hastaVal };
            $.ajax({
                type: "POST",
                url: '<%= ResolveUrl("Admin.aspx/getBitacoraJSON") %>',
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(json),
                dataType: "json",
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    console.log("Request: " + XMLHttpRequest.toString() + "\n\nStatus: " + textStatus + "\n\nError: " + errorThrown);
                },
                success: function (data) {
                    //console.log(data.d);
                    createDT(data.d);
                },
                complete: function (jqXHR, status) {
                    //alert("complete: " + status + "\n\nResponse: " + jqXHR.responseText);
                }
            });
        }

        function createDT(data) {
            dataTable.rows().remove().draw();
            var datos = JSON.parse(data);
            datos.forEach(function (element) {
                //var row = '<tr><td>' + element._fecha + '</td>'+ '<td>' + element._mensaje + '</td>' + '<td>' + element._nombre + '</td></tr>';
                dataTable.row.add(element);
            });
            dataTable.draw();
        }
    </script>
</asp:Content>
