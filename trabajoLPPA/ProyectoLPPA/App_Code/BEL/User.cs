﻿using System;

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


    private int _id;

    public int id
    {
        get { return _id; }
        set { _id = value; }
    }

    public User(String username,String tipo,int id, int intentos)
    {
        this.username = username;
        this.tipo = tipo;
        this.id = id;
        this.intentos = intentos;
    }

    private int _intentos;

    public int intentos
    {
        get { return _intentos; }
        set { _intentos = value; }
    }

}