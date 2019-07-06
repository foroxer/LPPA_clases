﻿<%@ Application Language="C#" %>
<%@ Import Namespace="ProyectoLPPA" %>
<%@ Import Namespace="System.Web.Optimization" %>
<%@ Import Namespace="System.Web.Routing" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e)
    {
        BundleConfig.RegisterBundles(BundleTable.Bundles);
        ScriptManager.ScriptResourceMapping.AddDefinition("jqueryUI", new ScriptResourceDefinition
        {
            Path = "~/Scripts/jquery-ui.min.js"
        }) ;
         ScriptManager.ScriptResourceMapping.AddDefinition("datatables", new ScriptResourceDefinition
        {
            Path = "~/Scripts/datatables.min.js"

        }) ;
         

    }

</script>


