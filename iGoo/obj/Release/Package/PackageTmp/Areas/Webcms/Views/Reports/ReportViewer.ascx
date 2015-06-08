<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<head id="Head1" runat="server">
    <title>Viện Công nghệ thông tin & Truyền thông - CDiT</title>
    
</head>


<script type="text/javascript" src="~/Script/jquery-1.2.6.min.js"></script>

<script  type="text/javascript">
    function printdiv() {
        var headstr = "<html><head><title></title></head><body>";
        var footstr = "</body>";
        var newstr = $("#ReportViewer1_ctl10").html()
        var popupWin = window.open('', '_blank');
        popupWin.document.write(headstr + newstr + footstr);
        popupWin.print();
        return false;


        //var WinPrint = window.open('', '', 'left=100,top=10,width=600,height=600,toolbar=0,scrollbars=0,status=0');
        //WinPrint.document.write(pri.innerHTML);  // pri is div ID in which all the information is shown.
        //document.close();
        //WinPrint.document.close();
        //WinPrint.focus();
        //WinPrint.print();
        //WinPrint.close();

        //var myDropDown = document.getElementById('myDropDown');
        //myDropDown.style.display = "none";
        ////Whatever other elements to hide.
        //window.print();
        //myDropDown.style.display = "block";
        //return true;
    }

    function showPrintButton() {    
        var table = $("table[title='Refresh']");
        var parentTable = $(table).parents('table');
        

    }
</script>
<script runat="server">
  private void Page_Load(object sender, System.EventArgs e)
  {
      ReportViewer1.Visible = true;
      var sCodeOrder = "";
      sCodeOrder = Request.Params["CodeOrder"];
      ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/Phieu_XK.rdlc");
      ReportParameter rp = new ReportParameter("sOrderCode", sCodeOrder);
      
          this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp });

          //var sCodeOrder = "";     
          //var reportParameterCollection = new ReportParameter[1];
          //reportParameterCollection[0] = new ReportParameter { Name = "sOrderCode" };
          //reportParameterCollection[0].Values.Add(sCodeOrder);
          //reportParameterCollection[0].Visible = true;

          //ReportViewer1.LocalReport.SetParameters(reportParameterCollection);


          ReportViewer1.LocalReport.Refresh();          
          
                 
  }
  
  
</script>

<form id="Form2" runat="server">
     
  <%-- <asp:TextBox ID ="CodeOrder" runat="server" Visible="true" Text="RO-27052013-1361"></asp:TextBox>
    <a href ="ReportViewer" target="_blank" class="button" >In Phiếu</a>--%>
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<rsweb:ReportViewer ID="ReportViewer1" runat="server" AsyncRendering="False" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="971px" ShowToolBar="true" Height="600px" >
    <LocalReport ReportEmbeddedResource="iGoo.Reports.Phieu_XK.rdlc">
       
        <DataSources>
            <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="DataSet1" />
        </DataSources>
       
    </LocalReport>
    </rsweb:ReportViewer>
    
    <%--<asp:ObjectDataSource ID="ObjectDataSource2" runat="server" SelectMethod="GetData" TypeName="babycuatoi_dbDataSetTableAdapters.sp_CMS_Orders_SelectByOrderCodeTableAdapter"></asp:ObjectDataSource>--%>
    
    <asp:Panel ID="panReport" runat="server" CssClass="panelReport" Visible="true">
    <table style="border-collapse:collapse; width:400px;" cellspacing="0" cellpadding="0" border="0">
        
    </table>
</asp:Panel>

    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="iGoo.babycuatoi_dbDataSetTableAdapters.sp_CMS_Orders_SelectByOrderCodeTableAdapter" UpdateMethod="GetData">
        <SelectParameters>
            <asp:QueryStringParameter Name="sOrderCode" QueryStringField="CodeOrder" Type="String" />
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="sOrderCode" Type="String" />
        </UpdateParameters>
    </asp:ObjectDataSource>
    
</form>
