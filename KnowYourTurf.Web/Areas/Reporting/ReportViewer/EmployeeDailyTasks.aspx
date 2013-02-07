<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmployeeDailyTasks.aspx.cs" Inherits="KnowYourTurf.Web.Areas.Reporting.ReportViewer.EmployeeDailyTasks" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
        <form id="form2" runat="server">
    <div>
    
<rsweb:ReportViewer ID="ReportViewer1" 
            runat="server" 
            Font-Names="Verdana" 
            Font-Size="8pt" 
            WaitMessageFont-Names="Verdana" 
            WaitMessageFont-Size="14pt"
            Width="875"
            Height="875">
            <LocalReport ReportPath="Areas\Reporting\RDLC\EmployeeDailyTasks.rdlc">
                <DataSources>
                    <rsweb:ReportDataSource DataSourceId="SqlDataSource1" Name="EmployeeDailyTasks" />
                </DataSources>
            </LocalReport>
        </rsweb:ReportViewer>

      <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString='<%$ appSettings:KnowYourTurf.sql_server_connection_string %>'
        SelectCommand="EmployeeDailyTasks" SelectCommandType="StoredProcedure"> 
        <SelectParameters>
            <asp:QueryStringParameter Name="Date" QueryStringField="Date" Type="DateTime" />
            <asp:QueryStringParameter Name="ClientId" QueryStringField="ClientId" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    
    </div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    </form>
</body>
</html>
