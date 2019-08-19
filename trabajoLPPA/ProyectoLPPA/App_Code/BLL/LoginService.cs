using ProyectoLPPA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for LoginService
/// </summary>
public class LoginService
{
    public LoginService()
    { }

    public static void login(String user, String password, HttpResponse response)
    {
        User usuario = LoginDAO.login(user, password);
        if (usuario != null)
        {
            if (usuario.intentos >= 3)
            {
                response.Redirect("Bloqueado.aspx");
            }
            else
            {
                BitacoraDAO.grabarBitacora(0, "Se logueo " + usuario.username + " que tiene el tipo " + usuario.tipo);
                response.Cookies.Add(new HttpCookie("user", usuario.username));
                response.Cookies.Add(new HttpCookie("tipo", usuario.tipo));
                response.Cookies.Add(new HttpCookie("userId", usuario.id.ToString()));
            }


        }
        else
        {
            LoginDAO.wrongLogin(user);
        }

    }

    public static void restaurarPassword(String user,String password, HttpResponse response)
    {
        SeguridadUtiles.cambiarPassword(user, password);
        response.Redirect("Login.aspx");
    }
}