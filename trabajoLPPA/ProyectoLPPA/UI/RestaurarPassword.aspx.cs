using System;
using System.Web;
using ProyectoLPPA;
public partial class UI_RestaurarPassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        String data = Request.Params.Get("data");
        if (data != null)
        {
            data = CryptoUtils.desencriptarAES(data);
            String user = data.Split(',')[0];
            String pass = data.Split(',')[1];
            if (user != null && pass != null)
            {
                SeguridadUtiles.cambiarPassword(user, pass);
            }
            Response.Redirect("Login.aspx");
        }
    }
    public void restaurar(object sender, EventArgs e)
    {
        MailService.restaurarPasswordMail(User.Text);
        Response.Redirect("Login.aspx");
    }
}