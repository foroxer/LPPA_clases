<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master"  CodeFile="Cliente.aspx.cs" Inherits="Cliente" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
       <div class="embed-responsive embed-responsive-4by3" >
            <input type="hidden" id="caller" value="Cliente" />
            <iframe id="myIframe" src="PlaceHolder.aspx" style="width: 100%; height: 100%" class="embed-responsive-item" allowfullscreen></iframe> 
       </div>
        
 </asp:Content>