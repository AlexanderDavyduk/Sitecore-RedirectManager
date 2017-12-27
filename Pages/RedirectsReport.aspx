<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RedirectsReport.aspx.cs" Inherits="Sitecore.SharedSource.RedirectManager.Pages.RedirectsReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Redirects Report</title>
    <link rel="stylesheet" href="/css/redirect_manager.css" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="buttons-right">
        <asp:Button CssClass="button" runat="server" ID="ShowOld" OnClick="ShowOldRedirects_Click" Text="Show old redirects"/>
        <asp:Button CssClass="button" runat="server" ID="ShowAll" OnClick="ShowAllRedirects_Click" Text="Show all redirects"/>
    </div>
    <div class="main">
        <asp:TreeView ID="Redirects" runat="server" EnableViewState="True"/>
    </div>
    </form>
</body>
</html>

