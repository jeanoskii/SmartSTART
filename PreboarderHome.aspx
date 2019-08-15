<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="SmartStart.Master" CodeBehind="PreboarderHome.aspx.cs" Inherits="CAPRES.PreboarderHome" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <h2 class="text-center">Candidates</h2>
    <p class="text-center"><em>To add a single candidate, use the textboxes in the table.
        To add multiple candidates, upload a text file with this format:</em> Candidate ID|Email</p>
    <p class="text-center"><em>Only candidates with preboarding statuses can be hired or rejected.</em></p>
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
                onrowdatabound="gvCandidates_RowDataBound" 
                onrowdeleting="gvCandidates_RowDeleting">
                <Columns>
                    <asp:TemplateField HeaderText="CANDIDATE ID <i class='glyphicon glyphicon-sort'></i>" SortExpression="candidate_id">
                        <ItemTemplate>
                            <asp:Label ID="lblCandidateId" runat="server" Text='<%# Eval("candidate_id") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtCandidateId" runat="server" CssClass="form-control" type="number" placeholder="e.g. 1001" min="0"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="EMAIL <i class='glyphicon glyphicon-sort'></i>" SortExpression="email">
                        <ItemTemplate>
                          <asp:Label ID="lblCandidateEmail" runat="server" Text='<%# Eval("email") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtCandidateEmail" runat="server" CssClass="form-control" MaxLength="50" placeholder="e.g. example@test.com"></asp:TextBox>
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
                            <asp:Panel ID="pnlHireModal" runat="server" CssClass="modal fade" tabindex="-1" role="dialog">
                                <div class="modal-dialog" role="document">
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                            <h4 class="modal-title">Hire Candidate</h4>
                                        </div>
                                        <div class="modal-body">
                                            <p>Please input the hiring date then click Hire.</p>
                                            <br />
                                            <div class="form-inline">
                                                <label for="txtHiringDate">Hiring Date:</label>
                                                <asp:TextBox ID="txtHiringDate" runat="server" CssClass="form-control" type="date"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="modal-footer">
                                            <button type="button" class="btn btn-default" data-dismiss="modal">CLOSE</button>
                                            <asp:Button ID="btnHire" runat="server" CssClass="btn btn-success"
                                                Text="HIRE" OnClick="btnHire_Click" ToolTip="HIRE" UseSubmitBehavior="false"/>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                            <div class="btn-group btn-group-lg">
                                <button type="button" class="btn btn-success" data-toggle="modal" data-target="#body_gvCandidates_pnlHireModal_<%# Container.DataItemIndex %>">
                                    <span class="glyphicon glyphicon-thumbs-up"></span>
                                </button>
                                <asp:LinkButton ID="btnDelete" runat="server" CssClass="btn btn-danger" Text="DELETE" CommandName="Delete" ToolTip="REJECT CANDIDATE">
                                    <span class='glyphicon glyphicon-thumbs-down'></span>
                                </asp:LinkButton>
                                <asp:LinkButton ID="btnView" runat="server" CssClass="btn btn-default"
                                    Text="VIEW" UseSubmitBehavior="false" ToolTip="VIEW APPLICATION">
                                    <span class="glyphicon glyphicon-file"></span>
                                </asp:LinkButton>
                                <asp:LinkButton ID="btnResendEmail" runat="server" CssClass="btn btn-default" Text="RESEND EMAIL"
                                    UseSubmitBehavior="false" ToolTip="RESEND EMAIL">
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