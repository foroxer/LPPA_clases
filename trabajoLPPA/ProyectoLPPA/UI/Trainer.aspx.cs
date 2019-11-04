using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using System.Xml.XPath;

public partial class Trainer : System.Web.UI.Page
{
    public List<XElement> clientes;
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            cargarClientes();
        } else
        {
            filtrar();
        }
            
    }

    private void cargarClientes()
    {
        XDocument document = XDocument.Load(Server.MapPath("clientes.xml"));
        clientes = document.Element("clientes").Elements("cliente").ToList();
    }

    public void filtrar()
    {
        String buscarPor = this.buscarPor.Text;
        if (String.IsNullOrEmpty(buscarPor))
        {
            cargarClientes();
        } else
        {
            XDocument document = XDocument.Load(Server.MapPath("clientes.xml"));
            clientes = document.Element("clientes").Elements("cliente")
                .Where(cliente => cliente.Element(this.DropDownList1.SelectedItem.Value).Value.ToUpper()
                                    .Contains(this.buscarPor.Text.ToUpper()))
                .ToList();
        }
       

    }
}