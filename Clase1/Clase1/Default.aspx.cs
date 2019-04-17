using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication3
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // el metodo se ejecuta cada vez que se clickea el boton por lo que no funciona hacer esto
            //if (Session["user"] != null && Session["comment"] != null)
            //{
            //    user.Text = Session["user"].ToString();
            //    comment.InnerText = Session["comment"].ToString();
            //}
        }
        protected void click1(object sender, EventArgs e)
        {

            Session["user"] = user.Text.ToString();
            Session["comment"] = comment.InnerText.ToString();

            Response.Redirect("Response.aspx");
        }
    }
}