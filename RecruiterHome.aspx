<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="SmartStart.Master" CodeBehind="RecruiterHome.aspx.cs" Inherits="CAPRES.RecruiterHome" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <h2 class="text-center">Candidates</h2>
    <p class="text-center"><em>To add a single candidate, use the textboxes in the table.
        To add multiple candidates, upload a text file with this format:</em> Candidate ID|Email</p>
    <div class="container-fluid">
        <h4 class="text-center">
            <asp:Label ID="lblMessage" runat="server" CssClass="error-label"></asp:Label>
        </h4>
        <div class="input-group input-group-lg">
            <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Please input your search terms (candidate ID, email, status)."></asp:TextBox>
            <span class="input-group-btn">
                <asp:LinkButton ID="btnSearch" runat="server" CssClass="btn btn-info" Text="SEARCH" ToolTip="SEARCH" OnClick="btnSearch_Click">
                    <span class="glyphicon glyphicon-search"></span>
                </asp:LinkButton>
                <asp:LinkButton ID="btnResetSearch" runat="server" CssClass="btn btn-info" Text="RESET" ToolTip="RESET" OnClick="btnResetSearch_Click">
                    <span class="glyphicon glyphicon-repeat"></span>
                </asp:LinkButton>
            </span>
        </div>
        <br />
        <div class="table-responsive">
            <asp:GridView ID="gvCandidates" runat="server" 
                CssClass="table table-bordered table-condensed" 
                PagerStyle-CssClass="pagination-ys" PagerStyle-HorizontalAlign="Center" AutoGenerateColumns="false"
                DataKeyNames="candidate_id" AllowPaging="True" AllowSorting="true"
                onpageindexchanging="gvCandidates_PageIndexChanging" ShowFooter="true" 
                onsorting="gvCandidates_Sorting" 
                onrowdatabound="gvCandidates_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="CANDIDATE ID <i class='glyphicon glyphicon-sort'></i>" SortExpression="candidate_id">
                        <ItemTemplate>
                            <asp:Label ID="lblCandidateId" runat="server" Text='<%# Eval("candidate_id") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtCandidateId" runat="server" CssClass="form-control" type="number" required placeholder="e.g. 1001" min="0"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="EMAIL <i class='glyphicon glyphicon-sort'></i>" SortExpression="email">
                        <ItemTemplate>
                          <asp:Label ID="lblCandidateEmail" runat="server" Text='<%# Eval("email") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtCandidateEmail" runat="server" CssClass="form-control" MaxLength="50" required placeholder="e.g. example@test.com"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="STATUS <i class='glyphicon glyphicon-sort'></i>" SortExpression="status">
                        <ItemTemplate>
                            <asp:Label ID="lblCandidateStatus" runat="server" Text='<%# Eval("status") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtCandidateStatus" runat="server" CssClass="form-control text-center" Text="pending" ReadOnly="true">
                            </asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ACTION">
                        <ItemTemplate>
                            <div class="btn-group btn-group-lg">
                                <asp:LinkButton ID="btnViewApplication" runat="server" CssClass="btn btn-success" Text="VIEW APPLICATION"
                                    UseSubmitBehavior="false" OnClick="btnViewApplication_Click" ToolTip="VIEW APPLICATION">
                                    <span class="glyphicon glyphicon-file"></span>
                                </asp:LinkButton>
                                <asp:LinkButton ID="btnResendEmail" runat="server" CssClass="btn btn-default" Text="RESEND EMAIL"
                                    UseSubmitBehavior="false" OnClick="btnResendEmail_Click" ToolTip="RESEND EMAIL">
                                    <span class="glyphicon glyphicon-envelope"></span>
                                </asp:LinkButton>
                            </div>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:LinkButton ID="btnRegister" runat="server" CssClass="btn btn-lg btn-block btn-primary btn-gridview"
                                onclick="btnRegister_Click" ToolTip="ADD">
                                <span class="glyphicon glyphicon-plus-sign"></span>
                            </asp:LinkButton>
                        </FooterTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        <div class="input-group input-group-lg">
            <span class="input-group-btn">
                <label class="btn btn-default btn-file" for="fuCandidates">
                    <asp:FileUpload ID="fuCandidates" runat="server" ToolTip="BROWSE" />
                    <span class="glyphicon glyphicon-folder-open"></span>
                </label>
            </span>
            <asp:Textbox ID="txtFileUpload" runat="server" CssClass="form-control" ReadOnly="true" placeholder="Please choose a text file to upload.">
            </asp:Textbox>
            <span class="input-group-btn">
                <asp:LinkButton ID="btnUpload" runat="server" CssClass="btn btn-info" Text="UPLOAD" ToolTip="UPLOAD"
                    onclick="btnUpload_Click" UseSubmitBehavior="false">
                    <span class="glyphicon glyphicon-cloud-upload"></span>
                </asp:LinkButton>
            </span>
        </div>
    </div>
</asp:Content>