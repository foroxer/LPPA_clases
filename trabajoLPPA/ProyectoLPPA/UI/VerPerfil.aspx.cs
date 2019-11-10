using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UI_VerPerfil : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public static String getPerfil() {
        Perfil perfil = new Perfil();
        perfil.nombre = "Roberto";
        perfil.apellido = "Gonzalez";
        perfil.dni = "32654815";
        perfil.fecnac = "20/11/1990";
        perfil.aptoFisico = true;
        perfil.fotoURL = "../Content/profile.png";
        return new JavaScriptSerializer().Serialize(perfil);
    }
}