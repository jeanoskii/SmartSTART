<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile ="SmartStart.Master" CodeBehind="ApplicantHome.aspx.cs" Inherits="CAPRES.ApplicantHome" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="container text-center">
        <h3>Welcome! This tool will help you fill out forms for your application. 
            <br />Please click the button to get started.
        </h3>
        <asp:Button ID="btnStart" runat="server" CssClass="btn btn-lg btn-primary" Text="START"
            UseSubmitBehavior="False" onclick="btnStart_Click"/>
    </div>
</asp:Content>