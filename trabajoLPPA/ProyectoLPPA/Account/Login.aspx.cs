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
            Response.Cookies.Clear();
            LoginService.login(UserName.Text, Password.Text,Response);
          
            if (Response.Cookies.Count > 0)
                {
                    IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
                }
                else
                {
                    FailureText.Text = "Datos invalidos.";
                    ErrorMessage.Visible = true;
                }
            }
        }
}
