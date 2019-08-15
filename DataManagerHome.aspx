<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="SmartStart.Master" CodeBehind="DataManagerHome.aspx.cs" Inherits="CAPRES.DataManagerHome" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="container">
        <h2 class="text-center">Export Candidates</h2>
        <p class="text-center"><em>Select an Application Date range then click the download button below.
            This will download a zip file with information about Applicants that applied within the selected range.
            The zip file contains CSV text (.txt) files that can be uploaded to SAP.</em></p>
        <h4 class="text-center">
            <asp:Label ID="lblMessage" runat="server" CssClass="error-label"></asp:Label>
        </h4>
        <div class="row">
            <div class="form-group col-xs-12 col-sm-6">
                <label for="txtApplicationDateStart">Hiring Date Start:</label>
                <asp:Textbox ID="txtApplicationDateStart" runat="server" CssClass="form-control" required type="date" placeholder="mm/dd/yyyy">
                </asp:Textbox>
            </div>
            <div class="form-group col-xs-12 col-sm-6">
                <label for="txtApplicationDateEnd">Hiring Date End:</label>
                <asp:TextBox ID="txtApplicationDateEnd" runat="server" CssClass="form-control" required type="date" placeholder="mm/dd/yyyy">
                </asp:TextBox>
            </div>
        </div>
        <asp:LinkButton ID="btnExport" runat="server" Text="DOWNLOAD" CssClass="btn btn-lg btn-primary btn-block col-height-equals-textbox"
            onclick="btnExport_Click" ToolTip="DOWNLOAD">
            <span class="glyphicon glyphicon-download-alt"></span>
        </asp:LinkButton>
    </div>
</asp:Content>
