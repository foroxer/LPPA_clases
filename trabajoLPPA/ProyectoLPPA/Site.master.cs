﻿using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SiteMaster : MasterPage
{
    private const string AntiXsrfTokenKey = "__AntiXsrfToken";
    private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
    private string _antiXsrfTokenValue;
    private Boolean mostrarCliente = false;
    private Boolean mostrarWebmaster = false;
    private Boolean mostrarOperador = false;

    protected void Page_Init(object sender, EventArgs e)
    {
        this.btnLogOut.ServerClick += BtnLogOut_ServerClick;
        // El código siguiente ayuda a proteger frente a ataques XSRF
        var requestCookie = Request.Cookies[AntiXsrfTokenKey];
       
        Guid requestCookieGuidValue;
        if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
        {
            // Utilizar el token Anti-XSRF de la cookie
            _antiXsrfTokenValue = requestCookie.Value;
            Page.ViewStateUserKey = _antiXsrfTokenValue;
        }
        else
        {
            // Generar un nuevo token Anti-XSRF y guardarlo en la cookie
            _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
            Page.ViewStateUserKey = _antiXsrfTokenValue;

            var responseCookie = new HttpCookie(AntiXsrfTokenKey)
            {
                HttpOnly = true,
                Value = _antiXsrfTokenValue
            };
            if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
            {
                responseCookie.Secure = true;
            }
            Response.Cookies.Set(responseCookie);
            
        }
       
        Page.PreLoad += master_Page_PreLoad;
    }

    private void BtnLogOut_ServerClick(object sender, EventArgs e)
    {
        this.Response.Cookies.Remove("tipo");
        this.Request.Cookies.Remove("tipo");
        this.Response.Cookies.Remove("user");
        this.Request.Cookies.Remove("user");

        this.btnLogin.Visible = true;
        this.btnLogOut.Visible = false;

    }

    protected void master_Page_PreLoad(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // Establecer token Anti-XSRF
            ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
            ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
        }
        else
        {
            // Validar el token Anti-XSRF
            if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
            {
                throw new InvalidOperationException("Error de validación del token Anti-XSRF.");
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        var tipo = Request.Cookies["tipo"];
        var user = Request.Cookies["user"];
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

    protected void Unnamed_LoggingOut(object sender, LoginCancelEventArgs e)
    {
        Context.GetOwinContext().Authentication.SignOut();
    }
}