﻿using ProyectoLPPA;
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
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static void login(String user, String password,HttpResponse response)
    {
        User usuario = LoginDAO.login(user, password);
        if (usuario != null)
        {

            SeguridadUtiles.grabarBitacora(0, "Se logueo " + usuario.username + " que tiene el tipo " + usuario.tipo);
            response.Cookies.Add(new HttpCookie("user", usuario.username));
            response.Cookies.Add(new HttpCookie("tipo", usuario.tipo));
            response.Cookies.Add(new HttpCookie("userId", usuario.id.ToString()));

        }

    }
}