﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Script.Serialization;

/// <summary>
/// Descripción breve de MostrarBitacora
/// </summary>
public class BitacoraService
{
    public BitacoraService(){ }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public String execute(String desde, String hasta)
    {
        List<Bitacora> registros = BitacoraDao.execute(desde, hasta);
        return new JavaScriptSerializer().Serialize(registros);
    }
}