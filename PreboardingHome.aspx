<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/SmartStart.Master" CodeBehind="PreboardingHome.aspx.cs" Inherits="CAPRES.PreboardingHome" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="container text-center">
        <h3>Congratulations! Your application has been considered. Please
            comply with the following requirements:
        </h3>
        <ul class="list-unstyled">
            <li>SSS</li>
            <li>TIN</li>
            <li>PhilHealth</li>
            <li>Pag-Ibig</li>
        </ul>
        <h3>Kindly submit these documents to one of these persons:</h3>
        <asp:BulletedList ID="blPreboarders" runat="server" CssClass="list-unstyled">
        </asp:BulletedList>
        <h3>We would also like you to fill out additional forms. Please click the button
        below to begin.</h3>
        <asp:Button ID="btnStart" runat="server" CssClass="btn btn-lg btn-primary" 
            Text="START" onclick="btnStart_Click" />
    </div>
</asp:Content>