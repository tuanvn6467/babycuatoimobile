

<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<head id="Head1" runat="server">
    <title>Cty CPSXTM&DV BBT Việt Nam - Cty TNHH Intex Việt Nam</title>
    
</head>
<script type="text/javascript" src="~/Script/jquery-1.2.6.min.js"></script>
<script runat="server">
  private void Page_Load(object sender, System.EventArgs e)
  {
      
      
      ReportViewer1.Visible = true;
      var sShipperID = "";
      var sFromDate = "";
      var sToDate = "";
      
      sShipperID = Request.Params["ShipperID"];
      ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/Phieu_GH.rdlc");
      ReportParameter[] parameters = new ReportParameter[3];
      //ReportParameter rp = new ReportParameter("sShipperID", sShipperID);
        parameters[0] = new ReportParameter("sShipperID", sShipperID);
        parameters[1] = new ReportParameter("sFromDate", sFromDate);
        parameters[2] = new ReportParameter("sToDate", sToDate);

        this.ReportViewer1.LocalReport.SetParameters(parameters);
         // this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp });
      
          ReportViewer1.LocalReport.Refresh();          
                 
  }
  
  
</script>

<form id="Form2" runat="server">
     
  <%-- <asp:TextBox ID ="CodeOrder" runat="server" Visible="true" Text="RO-27052013-1361"></asp:TextBox>
    <a href ="ReportViewer" target="_blank" class="button" >In Phiếu</a>--%>
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<rsweb:ReportViewer ID="ReportViewer1" runat="server" AsyncRendering="False" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="10in" ShowToolBar="True" Height="600px">
    <LocalReport ReportEmbeddedResource="iGoo.Reports.Phieu_GH.rdlc">
       
        <DataSources>
            <rsweb:ReportDataSource DataSourceId="ObjectDataSource2" Name="DataSet2" />
        </DataSources>
       
    </LocalReport>
    </rsweb:ReportViewer>
    
    <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" SelectMethod="GetData" TypeName="iGoo.babycuatoi_dbDataSet2TableAdapters.sp_CMS_Orders_SelectByShipperIDTableAdapter" OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
            <asp:QueryStringParameter Name="sShipperID" QueryStringField="ShipperID" Type="String" />
            <asp:QueryStringParameter Name="sFromDate" QueryStringField="sFromDate" Type="String" />
            <asp:QueryStringParameter Name="sToDate" QueryStringField="sToDate" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
   
    
    <asp:Panel ID="panReport" runat="server" CssClass="panelReport" Visible="false">
    <table style="border-collapse:collapse; width:400px;" cellspacing="0" cellpadding="0" border="0">
        <tr>
            <td class="printtd"><asp:Button id="btPrintRep" CssClass="btprint" ToolTip="Print Report" Text="Print" runat="server" /></td>
            <td class="printtd" style="width:20px;"><asp:Button ID="btFirstPage" ToolTip="First Page" Text="<<" CssClass="btprint" runat="server" /></td>
            <td class="printtd" style="width:20px;"><asp:Button ID="btNextPage" ToolTip="Next Page" Text=">" CssClass="btprint" runat="server" /></td>
            <td class="printtd" style="width:20px;"><asp:Button ID="btPrevPage" ToolTip="Previous Page" Text="<" CssClass="btprint" runat="server" /></td>
            <td class="printtd" style="width:20px;"><asp:Button ID="btLastPage" ToolTip="Last Page" Text=">>" CssClass="btprint" runat="server" /></td>
            <td class="printtd"><asp:Button ID="btCloseRep" ToolTip="Close" Text="Close" CssClass="btprint" runat="server" /></td>
        </tr>
    </table>
</asp:Panel>

</form>
