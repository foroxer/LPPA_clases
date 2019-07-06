using System;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProyectoLPPA;
 

public partial class SiteMaster : MasterPage
{
    private Boolean mostrarCliente = false;
    private Boolean mostrarWebmaster = false;
    private Boolean mostrarOperador = false;

    protected void Page_Init(object sender, EventArgs e)
    {
        this.btnLogOut.ServerClick += BtnLogOut_ServerClick;       
        if(Request.Cookies["user"] == null)
        {
            this.btnLogOut.Visible = false;
            this.btnLogin.Visible = true;
        }
        else
        {
            this.btnLogin.Visible = false;
            this.btnLogOut.Visible = true;
        }
    }

    private void BtnLogOut_ServerClick(object sender, EventArgs e)
    {

        String user = this.Request.Cookies["user"].Value.ToString();
        String type = this.Request.Cookies["tipo"].Value.ToString();
        int userId = int.Parse(this.Request.Cookies["userId"].Value.ToString());

        this.Response.Cookies["tipo"].Expires =DateTime.Now.AddDays(-1);
        this.Request.Cookies["tipo"].Expires = DateTime.Now.AddDays(-1);
        this.Request.Cookies["tipo"].Value = "invalid";
        this.Response.Cookies["tipo"].Value = "invalid";
        this.Response.Cookies["user"].Expires = DateTime.Now.AddDays(-1);
        this.Request.Cookies["user"].Expires = DateTime.Now.AddDays(-1);
        this.Response.Cookies["userId"].Expires = DateTime.Now.AddDays(-1);
        this.Request.Cookies["userId"].Expires = DateTime.Now.AddDays(-1);
        this.ulCliente.Visible = false;
        this.ulWebmaster.Visible = false;
        this.ulOperador.Visible = false;

        Session.Abandon();
        this.btnLogin.Visible = true;
        this.btnLogOut.Visible = false;

        SeguridadUtiles.grabarBitacora(userId, "Se deslogueo " + user + " que tiene el tipo " + type);
        Response.Redirect("Default.aspx");

    }

    

    protected void Page_Load(object sender, EventArgs e)
    {
        var tipo = Request.Cookies["tipo"];
        var user = Request.Cookies["user"];
        this.errorBitacora.Visible = false;

        if (user != null && user.Values.Count > 0 && user.Value != null)
        {
            Response.Cookies.Set(new HttpCookie("user", user.Value.ToString()));
            Response.Cookies.Set(new HttpCookie("tipo", tipo.Value.ToString()));
            btnLogin.Visible = false;
            switch (tipo.Value.ToString())
            {
                case "S":
                    this.mostrarCliente = true;
                    break;
                case "T":
                    this.mostrarOperador = true;
                    break;
                case "A":
                    this.mostrarWebmaster = true;
                    this.hacerBackup.ServerClick += handleBackup;
                    this.restaurarBackup.ServerClick += handleRestore;
                    this.errorBitacora.Visible = !this.verificarDatos();
                    break;
            }
        }
        else
        {
            btnLogOut.Visible = false;
        }
        if (!this.mostrarCliente)
            this.ulCliente.Visible = false;
        if (!this.mostrarWebmaster)
            this.ulWebmaster.Visible = false;
        if (!this.mostrarOperador)
            this.ulOperador.Visible = false;
    }

    private void handleRestore(object sender, EventArgs e)
    {
        SeguridadUtiles.realizarRestore(AppDomain.CurrentDomain.BaseDirectory + "Backup//bkp1.bak");
        this.errorBitacora.Visible = !this.verificarDatos();
    }

    private void handleBackup(object sender, EventArgs e)
    {
        SeguridadUtiles.realizarBackup(1, AppDomain.CurrentDomain.BaseDirectory + "Backup//bkp");
    }

    protected void Unnamed_LoggingOut(object sender, LoginCancelEventArgs e)
    {
        Context.GetOwinContext().Authentication.SignOut();
    }


    protected Boolean verificarDatos()
    {
        Boolean datosOk = false;
        try
        {
            SeguridadUtiles.verificarDigitosVerificadores();
            datosOk = true;
        }
        catch (Exception ex)
        {
            datosOk = false;  
        }
        return datosOk;
    }

}