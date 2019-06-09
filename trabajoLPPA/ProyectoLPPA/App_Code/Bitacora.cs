﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de Bitacora
/// </summary>
public class Bitacora
{
    private DateTime fecha;

    private String mensaje;

    private String nombre;
    public Bitacora(DateTime fecha, String mensaje,String nombre)
    {
        this.fecha = fecha;
        this.mensaje = mensaje;
        this.nombre = nombre;
    }

    public String _fecha { get { return fecha.ToShortDateString(); } }

    public String _mensaje { get { return mensaje; } }

    public String _nombre { get { return nombre; } }
}