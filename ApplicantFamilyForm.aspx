<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="SmartStart.Master" CodeBehind="ApplicantFamilyForm.aspx.cs" Inherits="CAPRES.ApplicantFamilyForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <h2 class="text-center">
        <asp:Label ID="lblStep" runat="server"></asp:Label>
    </h2>
    <div class="container-fluid">
        <p class="text-center"><em>If single, please indicate your parents' information only. If married
            please indicate your parents, spouse, and child/ren's information.</em></p>
        <h4 class="text-center">
            <asp:Label ID="lblError" runat="server" CssClass="error-label"></asp:Label>
        </h4>
        <div class="table-responsive">
            <asp:GridView ID="gvDependents" runat="server" CssClass="table table-bordered table-condensed" 
                AutoGenerateColumns="false" DataKeyNames="file5_id" OnRowEditing="gvDependents_RowEditing"
                OnRowCancelingEdit="gvDependents_RowCancelingEdit" OnRowUpdating="gvDependents_RowUpdating" 
                OnRowDeleting="gvDependents_RowDeleting" onrowdatabound="gvDependents_RowDataBound"
                onrowcreated="gvDependents_RowCreated" ShowFooter="true">
                <Columns>
                    <asp:BoundField HeaderText="file5_id" DataField="file5_id" ReadOnly="true" Visible="False"/>
                    <asp:TemplateField HeaderText="NAME">
                        <ItemTemplate>
                            <%# Eval("dependents_name") %>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEditDependentsName" CssClass="form-control" runat="server"
                                Text='<%# Eval("dependents_name") %>'/>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtDependentsName" runat="server" CssClass="form-control" required
                                MaxLength="80" placeholder="FN MI. LN"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="RELATIONSHIP">
                        <ItemTemplate>
                            <%# Eval("family_member") %>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlEditFamilyMember" CssClass="form-control" runat="server">
                            </asp:DropDownList>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:DropDownList ID="ddlFamilyMember" runat="server" CssClass="form-control" required
                                AppendDataBoundItems="true">
                                <asp:ListItem Selected="True" Text="Select..." Value="0" disabled></asp:ListItem>
                            </asp:DropDownList>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="GENDER">
                        <ItemTemplate>
                            <%# Eval("gender") %>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlEditGender" CssClass="form-control" runat="server">
                            </asp:DropDownList>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:DropDownList ID="ddlGender" runat="server" CssClass="form-control" required
                                AppendDataBoundItems="true">
                                <asp:ListItem Selected="True" Text="Select..." Value="0" disabled></asp:ListItem>
                            </asp:DropDownList>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="BIRTHDATE">
                        <ItemTemplate>
                            <%# Eval("birthdate") != DBNull.Value ? 
                                Convert.ToDateTime(Eval("birthdate")).ToString("d") : " "  %>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEditBirthdate" runat="server" CssClass="form-control" type="date"
                                Text='<%# Bind("birthdate", "{0:yyyy-MM-dd}") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtBirthdate" runat="server" CssClass="form-control" type="date" required
                                placeholder="mm/dd/yyyy"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="COMPANY">
                        <ItemTemplate>
                            <%# Eval("company_name") %>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEditCompanyName" runat="server" CssClass="form-control"
                                Text='<%# Eval("company_name") %>'/>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtCompanyName" runat="server" CssClass="form-control" required
                                MaxLength="50" placeholder="If applicable"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="POSITION">
                        <ItemTemplate>
                            <%# Eval("position_title") %>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEditPositionTitle" runat="server" CssClass="form-control"
                                Text='<%# Eval("position_title") %>'/>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtPositionTitle" runat="server" CssClass="form-control" required
                                MaxLength="50" placeholder="If applicable"></asp:TextBox>
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
                            <button type="button" class="btn btn-lg btn-block btn-danger btn-gridview" data-toggle="modal" data-target="#body_gvDependents_pnlDeleteModal_<%# Container.DataItemIndex %>">
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