using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for User
/// </summary>
public class User
{
    private string _username;

    public string username
    {
        get { return _username; }
        set { _username = value; }
    }

    private string _tipo;

    public string tipo
    {
        get { return _tipo; }
        set { _tipo = value; }
    }


    public User(String username,String tipo)
    {
        this.username = username;
        this.tipo = tipo;
    }
}