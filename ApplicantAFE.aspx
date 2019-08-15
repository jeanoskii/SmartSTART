<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/SmartStart.Master" CodeBehind="ApplicantAFE.aspx.cs" Inherits="CAPRES.ApplicantAFE" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <h2 class="text-center">
        <asp:Label ID="lblStep" runat="server" Text=""></asp:Label>
    </h2>
    <div class="container">
        <asp:HiddenField ID="hidAFEId" runat="server" />
        <div class="well">
            <div class="form-group">
                <label for="txtApplicationDate">Application Date:</label>
                <asp:TextBox ID="txtApplicationDate" runat="server" CssClass="form-control"
                    required type="date" placeholder="mm/dd/yyyy"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="txtPositionAppliedFor">Position Applied For:</label>
                <asp:TextBox ID="txtPositionAppliedFor" runat="server" CssClass="form-control"
                    required MaxLength="50" placeholder="e.g. Developer"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="txtInterviewerName">Interviewer Name:</label>
                <asp:TextBox ID="txtInterviewerName" runat="server" CssClass="form-control"
                    required MaxLength="50" placeholder="FN MI. LN"></asp:TextBox>
            </div>
        </div>
        <div class="text-center">
            <div class="btn-group btn-group-lg">
                <asp:Button ID="btnHome" runat="server" CssClass="btn btn-default" Text="HOME" UseSubmitBehavior="false" 
                    onclick="btnHome_Click"/>&nbsp
                <asp:Button ID="btnNext" runat="server" CssClass="btn btn-primary" Text="NEXT" onclick="btnNext_Click" />
            </div>
        </div>
    </div>
</asp:Content>