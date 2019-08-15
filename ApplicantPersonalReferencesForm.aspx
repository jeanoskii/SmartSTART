<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/SmartStart.Master" CodeBehind="ApplicantPersonalReferencesForm.aspx.cs" Inherits="CAPRES.ApplicantPersonalReferencesForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <h2 class="text-center">
        <asp:Label ID="lblStep" runat="server" Text=""></asp:Label>
    </h2>
    <div class="container-fluid">
        <p class="text-center"><em>Aside from your family members</em></p>
        <h4 class="text-center">
            <asp:Label ID="lblError" runat="server" CssClass="error-label"></asp:Label>
        </h4>
        <div class="table-responsive">
            <asp:GridView ID="gvPersonal" runat="server" CssClass="table table-bordered table-condensed"
                AutoGenerateColumns="false" DataKeyNames="personal_reference_id" 
                onrowcancelingedit="gvPersonal_RowCancelingEdit" onrowdatabound="gvPersonal_RowDataBound"
                onrowdeleting="gvPersonal_RowDeleting" onrowediting="gvPersonal_RowEditing"
                onrowupdating="gvPersonal_RowUpdating" onrowcreated="gvPersonal_RowCreated" ShowFooter="true">
                <Columns>
                    <asp:BoundField HeaderText="professional_reference_id"
                        DataField="professional_reference_id" ReadOnly="true" Visible="False"/>
                    <asp:TemplateField HeaderText="LAST NAME">
                        <ItemTemplate>
                            <%# Eval("last_name") %>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEditLastName" runat="server" CssClass="form-control" MaxLength="25"></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control" MaxLength="25"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="FIRST NAME">
                        <ItemTemplate>
                            <%# Eval("first_name") %>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEditFirstName" runat="server" CssClass="form-control" MaxLength="25"></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control" MaxLength="25"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="CONTACT NUMBER">
                        <ItemTemplate>
                            <%# Eval("contact_number") %>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEditContactNumber" runat="server" CssClass="form-control" type="number" MaxLength="14">
                            </asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtContactNumber" runat="server" CssClass="form-control" type="number" MaxLength="14">
                            </asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="RELATIONSHIP">
                        <ItemTemplate>
                            <%# Eval("relationship") %>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEditRelationship" runat="server" CssClass="form-control" MaxLength="15"></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtRelationship" runat="server" CssClass="form-control" MaxLength="15"></asp:TextBox>
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
                            <button type="button" class="btn btn-lg btn-block btn-danger btn-gridview" data-toggle="modal" data-target="#body_gvPersonal_pnlDeleteModal_<%# Container.DataItemIndex %>">
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
        <div class="text-center">
            <div class="btn-group btn-group-lg">
                <asp:Button ID="btnPrevious" runat="server" CssClass="btn btn-default" Text="BACK"
                    UseSubmitBehavior="False" onclick="btnPrevious_Click" />
                <asp:Button ID="btnNext" runat="server" CssClass="btn btn-primary" Text="NEXT"
                    UseSubmitBehavior="False" onclick="btnNext_Click" />
            </div>
        </div>
    </div>
</asp:Content>