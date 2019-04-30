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
            RegisterHyperLink.NavigateUrl = "Register";
            OpenAuthLogin.ReturnUrl = Request.QueryString["ReturnUrl"];
            var returnUrl = HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
            if (!String.IsNullOrEmpty(returnUrl))
            {
                RegisterHyperLink.NavigateUrl += "?ReturnUrl=" + returnUrl;
            }
        }

        protected void LogIn(object sender, EventArgs e)
        {
            if (IsValid)
            {
            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings[1].ConnectionString);
            cn.Open();
            SqlTransaction tx = cn.BeginTransaction();
            SqlCommand cmd = new SqlCommand("SELECT * from Usuario where Alias = @id and Password = @password");
            cmd.Parameters.Add(new SqlParameter("id", UserName.Text));
            cmd.Parameters.Add(new SqlParameter("password", Password.Text));
            cmd.Connection = cn;
            cmd.Transaction = tx;
            SqlDataReader reader = cmd.ExecuteReader();
            Response.Cookies.Clear();
            if (reader.Read())
            {
                Response.Cookies.Add(new HttpCookie("user", reader.GetString(5)));
                Response.Cookies.Add(new HttpCookie("tipo", reader.GetString(4).Trim()));
                
            }
            reader.Close();
            tx.Commit();
            cn.Close();
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