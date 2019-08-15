<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/SmartStart.Master" CodeBehind="ApplicantOrganizationalForm.aspx.cs" Inherits="CAPRES.ApplicantOrganizationalForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="container-fluid text-center">
        <h2>
            <asp:Label ID="lblStep" runat="server"></asp:Label>
        </h2>
        <h4>
            <asp:Label ID="lblError" runat="server" CssClass="error-label"></asp:Label>
        </h4>
        <div class="table-responsive">
            <asp:GridView ID="gvOrganizations" runat="server" CssClass="table table-bordered table-condensed" AutoGenerateColumns="false" 
                DataKeyNames="file4_id" onrowcancelingedit="gvOrganizations_RowCancelingEdit" 
                onrowdatabound="gvOrganizations_RowDataBound" 
                onrowdeleting="gvOrganizations_RowDeleting" 
                onrowediting="gvOrganizations_RowEditing" 
                onrowupdating="gvOrganizations_RowUpdating" ShowFooter="true" 
                onrowcreated="gvOrganizations_RowCreated">
                <Columns>
                    <asp:BoundField HeaderText="file4_id" DataField="file4_id" ReadOnly="true" Visible="False"/>
                    <asp:TemplateField HeaderText="ORG TYPE">
                        <ItemTemplate>
                            <%# Eval("organization_type") %>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlEditOrganizationType" runat="server" CssClass="form-control"></asp:DropDownList>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:DropDownList ID="ddlOrganizationType" runat="server" CssClass="form-control" required
                                AppendDataBoundItems="true">
                                <asp:ListItem Selected="True" Text="Select..." Value="0" disabled></asp:ListItem>
                            </asp:DropDownList>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ORG NAME">
                        <ItemTemplate>
                            <%# Eval("organization_name") %>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEditOrganizationName" runat="server" CssClass="form-control"
                                Text='<%# Eval("organization_name") %>' MaxLength="40"></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtOrganizationName" runat="server" CssClass="form-control" required></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="START DATE">
                        <ItemTemplate>
                            <%# Eval("start_date") != DBNull.Value ?
                                Convert.ToDateTime(Eval("start_date")).ToString("d") : "" %>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEditStartDate" runat="server" CssClass="form-control" type="date"
                                Text='<%# Bind("start_date", "{0:yyyy-MM-dd}") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtStartDate" runat="server" CssClass="form-control" type="date" required></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="END DATE">
                        <ItemTemplate>
                            <%#  Eval("end_date") != DBNull.Value ?
                                Convert.ToDateTime(Eval("end_date")).ToString("d") : "" %>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEditEndDate" runat="server" CssClass="form-control" type="date"
                                Text='<%# Bind("end_date", "{0:yyyy-MM-dd}") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtEndDate" runat="server" CssClass="form-control" type="date"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="POSITION">
                        <ItemTemplate>
                            <%# Eval("position_type") %>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlEditPositionType" CssClass="form-control" runat="server"></asp:DropDownList>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:DropDownList ID="ddlPositionType" CssClass="form-control" runat="server" required
                                AppendDataBoundItems="true">
                                 <asp:ListItem Selected="True" Text="Select..." Value="0" disabled></asp:ListItem>
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
                            <button type="button" class="btn btn-lg btn-block btn-danger btn-gridview" data-toggle="modal" data-target="#body_gvOrganizations_pnlDeleteModal_<%# Container.DataItemIndex %>">
                                <span class="glyphicon glyphicon-minus-sign"></span>
                            </button>
                            <asp:Panel ID="pnlDeleteModal" runat="server" CssClass="modal fade" tabindex="-1" role="dialog">
                                <div class="modal-dialog" role="document">
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                            <h4 class="modal-title">Confirm</h4>
                                        </div>
                                        <div class="modal-body">Are you sure you want to delete this record?</div>
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
        <br />
        <div class="btn-group btn-group-lg">
            <asp:Button ID="btnPrevious" runat="server" CssClass="btn btn-default" Text="BACK"
                UseSubmitBehavior="False" onclick="btnPrevious_Click" />
            <asp:Button ID="btnNext" runat="server" CssClass="btn btn-primary" Text="NEXT"
                UseSubmitBehavior="False" onclick="btnNext_Click" />
        </div>
    </div>
</asp:Content>