using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

/// <summary>
/// Descripción breve de MostrarBitacora
/// </summary>
public class BitacoraService
{
    public BitacoraService() { }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public String execute(String desde, String hasta)
    {
        List<Bitacora> registros = BitacoraDAO.execute(desde, hasta);
        return new JavaScriptSerializer().Serialize(registros);
    }

    public String executeXML(String desde, String hasta)
    {
        List<Bitacora> registros = BitacoraDAO.execute(desde, hasta);
        MemoryStream stream = new MemoryStream();
        XmlSerializer serializer = new XmlSerializer(typeof(Bitacora[]));
        serializer.Serialize(stream, registros.ToArray());
        stream.Position = 0;
        StreamReader reader = new StreamReader(stream);
        return reader.ReadToEnd();
    }
}
