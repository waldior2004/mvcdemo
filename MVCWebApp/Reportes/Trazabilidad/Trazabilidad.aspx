<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Trazabilidad.aspx.cs" Inherits="com.msc.frontend.mvc.Reportes.Trazabilidad.Trazabilidad1" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>

    <form id="form1" runat="server">
        <rsweb:ReportViewer ID="rptViewer" runat="server" Width="100%" AsyncRendering="false" Height="700px">
        </rsweb:ReportViewer>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    </form>

</body>
</html>
