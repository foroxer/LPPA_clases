using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : Page
{

    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Cookies["tipo"] != null)
        {
            String aspxPage = returnRoute(Request.Cookies["tipo"].Value);
            if (!String.IsNullOrEmpty(aspxPage))
            {
                Response.Redirect(aspxPage);
            }
        }
    }

    private String returnRoute(String userType)
    {
        switch (userType)
        {
            case "A":
                return "Admin.aspx";
            case "S":
                return "Cliente.aspx";
            case "T":
                return "Trainer.aspx";
            default:
                return null;
        }
    }
}