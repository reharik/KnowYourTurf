<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EquipmentTaskReport.aspx.cs" Inherits="KnowYourTurf.Web.Areas.Reporting.ReportViewer.EquipmentTaskReport" %>

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
            Width="960"
            Height="875">
            <LocalReport ReportPath="Areas\Reporting\RDLC\EquipmentTaskReport.rdlc">
                <DataSources>
                    <rsweb:ReportDataSource DataSourceId="SqlDataSource1" Name="EquipmentTaskReport" />
                </DataSources>
            </LocalReport>
        </rsweb:ReportViewer>

      <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString='<%$ appSettings:KnowYourTurf.sql_server_connection_string %>'
        SelectCommand="EquipmentTaskReport" SelectCommandType="StoredProcedure"> 
        <SelectParameters>
            <asp:QueryStringParameter Name="EquipmentId" QueryStringField="EquipmentId" Type="Int32" DefaultValue="0" />
            <asp:QueryStringParameter Name="ClientId" QueryStringField="ClientId" Type="Int32" />
            <asp:QueryStringParameter Name="StartDate" QueryStringField="StartDate" DefaultValue="dbnull" Type="DateTime" />
            <asp:QueryStringParameter Name="EndDate" QueryStringField="EndDate" DefaultValue="dbnull" Type="DateTime" />
            <asp:QueryStringParameter Name="Complete" QueryStringField="Complete" Type="Boolean" />
            <asp:QueryStringParameter Name="EmployeeId" QueryStringField="EmployeeId" Type="Int32" DefaultValue="0" />
            <asp:QueryStringParameter Name="TaskTypeId" QueryStringField="TaskTypeId" Type="Int32" DefaultValue="0" />
        </SelectParameters>
    </asp:SqlDataSource>
    
    </div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    </form>
</body>
</html>
