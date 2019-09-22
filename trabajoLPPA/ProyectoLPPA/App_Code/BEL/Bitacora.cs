using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

/// <summary>
/// Descripción breve de Bitacora
/// </summary>

public class Bitacora
{
    private DateTime _fecha;

    private String _mensaje;

    private String _nombre;
    public Bitacora(DateTime fecha, String mensaje,String nombre)
    {
        this._fecha = fecha;
        this._mensaje = mensaje;
        this._nombre = nombre;
    }
    public Bitacora() { }



    public string fecha {
        get { return _fecha.ToShortDateString(); }
        set { _fecha = DateTime.Parse(value); }
    }

    private int myVar;

    


    public string mensaje {
        get { return _mensaje; }
        set { _mensaje = value; }
    }

    
    public string nombre {
        get { return _nombre; }
        set { _nombre = value;  }
    }
}