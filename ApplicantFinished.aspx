<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/SmartStart.Master" CodeBehind="ApplicantFinished.aspx.cs" Inherits="CAPRES.ApplicantFinished" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="container text-center">
        <h3>Congratulations! We have received your application. Please wait 2-3 days for
            notifications. Thank you!</h3>
        <div class="btn-group btn-group-lg">
            <asp:Button ID="btnLogout" runat="server" CssClass="btn btn-default" Text="LOG OUT" 
                UseSubmitBehavior="false" onclick="btnLogout_Click" />
            <asp:Button ID="btnPrint" runat="server" CssClass="btn btn-primary" Text="PRINT APPLICATION" 
                UseSubmitBehavior="false" onclick="btnPrint_Click"/>
        </div>
    </div>
</asp:Content>