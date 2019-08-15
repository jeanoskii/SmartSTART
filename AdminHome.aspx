<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile ="SmartStartAdmin.Master" CodeBehind="AdminHome.aspx.cs" Inherits="CAPRES.AdminHome" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <h2 class="text-center">HR Accounts</h2>
    <div class="container-fluid">
        <h4 class="text-center">
            <asp:Label ID="lblMessage" runat="server" CssClass="error-label"></asp:Label>
        </h4>
        <div class="input-group input-group-lg">
            <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Please input your search terms (email, account type)."></asp:TextBox>
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
            <asp:GridView ID="gvHR" runat="server" 
                CssClass="table table-bordered table-condensed" PagerStyle-CssClass="pagination-ys"
                PagerStyle-HorizontalAlign="Center" AutoGenerateColumns="false" 
                AllowSorting="true" AllowPaging="true" ShowFooter="true" DataKeyNames="email"
                onrowcancelingedit="gvHR_RowCancelingEdit" onrowdatabound="gvHR_RowDataBound"
                onrowdeleting="gvHR_RowDeleting" onrowediting="gvHR_RowEditing" onrowupdating="gvHR_RowUpdating" 
                onrowcreated="gvHR_RowCreated" onsorting="gvHR_Sorting">
                <Columns>
                    <asp:TemplateField HeaderText="FIRST NAME <i class='glyphicon glyphicon-sort'></i>" SortExpression="first_name">
                        <ItemTemplate>
                            <asp:Label ID="lblFirstName" runat="server" Text='<%# Eval("first_name") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEditFirstName" runat="server" CssClass="form-control" MaxLength="25"></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control" MaxLength="25"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="LAST NAME <i class='glyphicon glyphicon-sort'></i>" SortExpression="last_name">
                        <ItemTemplate>
                            <asp:Label ID="lblLastName" runat="server" Text='<%# Eval("last_name") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEditLastName" runat="server" CssClass="form-control" MaxLength="25"></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control" MaxLength="25"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="EMAIL ADDRESS <i class='glyphicon glyphicon-sort'></i>" SortExpression="email">
                        <ItemTemplate>
                            <asp:Label ID="lblEmail" runat="server" Text='<%# Eval("email") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" type="email" MaxLength="50"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ACCOUNT TYPE <i class='glyphicon glyphicon-sort'></i>" SortExpression="type">
                        <ItemTemplate>
                            <asp:Label ID="lblType" runat="server" Text='<%# Eval("type") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlEditType" runat="server" CssClass="form-control">
                                <asp:ListItem Text="Recruiter" Value="recruiter"></asp:ListItem>
                                <asp:ListItem Text="Preboarder" Value="preboarder"></asp:ListItem>
                                <asp:ListItem Text="Data Manager" Value="datamanager"></asp:ListItem>
                                <asp:ListItem Text="Administrator" Value="admin"></asp:ListItem>
                            </asp:DropDownList>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:DropDownList ID="ddlType" runat="server" CssClass="form-control">
                                <asp:ListItem Text="Select.." Value="0" Selected="True" disabled></asp:ListItem>
                                <asp:ListItem Text="Recruiter" Value="recruiter"></asp:ListItem>
                                <asp:ListItem Text="Preboarder" Value="preboarder"></asp:ListItem>
                                <asp:ListItem Text="Data Manager" Value="datamanager"></asp:ListItem>
                                <asp:ListItem Text="Administrator" Value="admin"></asp:ListItem>
                            </asp:DropDownList>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ACTION">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnEdit" runat="server" CssClass="btn btn-lg btn-block btn-default
                                btn-gridview" Text="EDIT" CommandName="Edit" UseSubmitBehavior="false" ToolTip="EDIT">
                                <span class="glyphicon glyphicon-edit"></span>
                            </asp:LinkButton>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <div class="btn-group">
                                <asp:LinkButton ID="btnSave" runat="server" CssClass="btn btn-xs btn-success btn-gridview"
                                    CommandName="Update" Text="SAVE" UseSubmitBehavior="false" ToolTip="SAVE">
                                    <span class="glyphicon glyphicon-ok"></span>
                                </asp:LinkButton>
                                <asp:LinkButton ID="btnCancel" runat="server" CssClass="btn btn-xs btn-danger
                                    btn-gridview" Text="CANCEL" CommandName="Cancel" UseSubmitBehavior="false" ToolTip="CANCEL">
                                    <span class="glyphicon glyphicon-remove"></span>
                                </asp:LinkButton>
                            </div>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:LinkButton ID="btnAdd" runat="server" CssClass="btn btn-lg btn-block btn-primary
                                btn-gridview" Text="ADD" onclick="btnAdd_Click" ToolTip="ADD">
                                <span class="glyphicon glyphicon-plus-sign"></span>
                            </asp:LinkButton>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="btnResendEmail" runat="server" CssClass="btn btn-lg btn-block btn-default btn-gridview" Text="RESEND EMAIL"
                                UseSubmitBehavior="false" OnClick="btnResendEmail_Click" ToolTip="RESEND EMAIL">
                                <span class="glyphicon glyphicon-envelope"></span>
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <button type="button" class="btn btn-lg btn-block btn-danger btn-gridview" data-toggle="modal" data-target="#body_body_gvHR_pnlDeleteModal_<%# Container.DataItemIndex %>">
                                <span class="glyphicon glyphicon-minus-sign"></span>
                            </button>
                            <asp:Panel ID="pnlDeleteModal" runat="server" CssClass="modal fade" tabindex="-1" role="dialog">
                                <div class="modal-dialog" role="document">
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                            <h4 class="modal-title">Confirm</h4>
                                        </div>
                                        <div class="modal-body">Are you sure you want to revoke privileges from this account?</div>
                                        <div class="modal-footer">
                                            <button type="button" class="btn btn-default" data-dismiss="modal">CLOSE</button>
                                            <asp:LinkButton ID="btnDelete" runat="server" CssClass="btn btn-danger" Text="DELETE" CommandName="Delete" UseSubmitBehavior="false" ToolTip="DELETE">YES</asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        <h2 class="text-center">Manage Data</h2>
        <h4 class="text-center">
            <asp:Label ID="lblUploadError" runat="server" CssClass="error-label"></asp:Label>
        </h4>
        <div class="input-group input-group-lg">
            <span class="input-group-btn">
                <label class="btn btn-default btn-file" for="fuCandidates">
                    <asp:FileUpload ID="fuData" runat="server" ToolTip="BROWSE" />
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