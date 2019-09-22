using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UI_WS_Webservice : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    [WebMethod(MessageName = "getBitacora")]

    public static String getBitacora(String desde, String hasta)
    {
        return new BitacoraService().executeXML(desde, hasta);
    }

}