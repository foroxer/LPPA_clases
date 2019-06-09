﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

/// <summary>
/// Descripción breve de MostrarBitacora
/// </summary>
public class MostrarBitacora
{
    public MostrarBitacora(){ }

    public String execute(String desde, String hasta)
    {
        List<Bitacora> registros = MostrarBitacoraRepository.execute(desde, hasta);
        return new JavaScriptSerializer().Serialize(registros);
    }
}