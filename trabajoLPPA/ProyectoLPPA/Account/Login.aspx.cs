using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Web;
using System.Web.UI;
using ProyectoLPPA;
using System.Data.SqlClient;
using System.Configuration;

public partial class Account_Login : Page
{
        protected void Page_Load(object sender, EventArgs e)
        {
            var returnUrl = HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
            
        }

        protected void LogIn(object sender, EventArgs e)
        {
            if (IsValid)
            {
            SqlConnection cn = ConexionSingleton.obtenerConexion();
            if (cn.State == System.Data.ConnectionState.Open)
                cn.Close();
            cn.Open();
            SqlTransaction tx = cn.BeginTransaction();
            SqlCommand cmd = new SqlCommand("SELECT * from Usuario where Alias = @id and Password = @password");
            cmd.Parameters.Add(new SqlParameter("id", UserName.Text));
            cmd.Parameters.Add(new SqlParameter("password", Password.Text));
            cmd.Connection = cn;
            cmd.Transaction = tx;
            SqlDataReader reader = cmd.ExecuteReader();
            Response.Cookies.Clear();
            String user = null;
            String tipo = null;


            if (reader.Read())
            {
                user = reader.GetString(5);
                tipo = reader.GetString(4).Trim();
                Response.Cookies.Add(new HttpCookie("user", user));
                Response.Cookies.Add(new HttpCookie("tipo", tipo));
                
            }
            reader.Close();
            tx.Commit();
            cn.Close();
            if(user != null)
                SeguridadUtiles.grabarBitacora(0, "Se logueo " + user + " que tiene el tipo " + tipo);

            if (Response.Cookies.Count > 0)
                {
                    IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
                }
                else
                {
                    FailureText.Text = "Invalid username or password.";
                    ErrorMessage.Visible = true;
                }
            }
        }
}