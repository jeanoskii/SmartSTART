<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/SmartStart.Master" CodeBehind="ApplicantEmploymentForm.aspx.cs" Inherits="CAPRES.ApplicantEmploymentForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <h2 class="text-center">
        <asp:Label ID="lblStep" runat="server" Text=""></asp:Label>
    </h2>
    <div class="container-fluid">
        <p class="text-center"><em>Please start with your most recent employer. Maximum of three (3) employers.</em></p>
        <h4 class="text-center">
            <asp:Label ID="lblError" runat="server" CssClass="error-label"></asp:Label>
        </h4>
        <div class="table-responsive">
            <asp:GridView ID="gvEmployers" runat="server" CssClass="table table-bordered table-condensed"
                AutoGenerateColumns="false" 
                DataKeyNames="file3_id" onrowcancelingedit="gvEmployers_RowCancelingEdit" 
                onrowdatabound="gvEmployers_RowDataBound" 
                onrowdeleting="gvEmployers_RowDeleting" onrowediting="gvEmployers_RowEditing" 
                onrowupdating="gvEmployers_RowUpdating" ShowFooter="true" 
                onrowcreated="gvEmployers_RowCreated">
                <Columns>
                    <asp:BoundField HeaderText="file3_id" DataField="file3_id" ReadOnly="true" Visible="False"/>
                    <asp:TemplateField HeaderText="PREVIOUS EMPLOYER">
                        <ItemTemplate>
                            <%# Eval("previous_employer") %>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEditPreviousEmployer" runat="server" CssClass="form-control" MaxLength="50"
                                Text='<%# Eval("previous_employer") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtPreviousEmployer" runat="server" CssClass="form-control" required MaxLength="50"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="CITY">
                        <ItemTemplate>
                            <%# Eval("city") %>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEditCity" runat="server" CssClass="form-control" MaxLength="40"
                                Text='<%# Eval("city") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtCity" runat="server" CssClass="form-control" required MaxLength="40"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="POSITION HELD">
                        <ItemTemplate>
                            <%# Eval("position_held") %>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEditPositionHeld" runat="server" CssClass="form-control" MaxLength="40"
                                Text='<%# Eval("position_held") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtPositionHeld" runat="server" CssClass="form-control" required MaxLength="40"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="SPECIALIZATION">
                        <ItemTemplate>
                            <%# Eval("specialization") %>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlEditSpecialization" runat="server" CssClass="form-control"></asp:DropDownList>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:DropDownList ID="ddlSpecialization" runat="server" CssClass="form-control" required
                                AppendDataBoundItems="true">
                                <asp:ListItem Selected="True" Text="Select..." Value="0" disabled></asp:ListItem>
                            </asp:DropDownList>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="IMMEDIATE SUPERIOR">
                        <ItemTemplate>
                            <%# Eval("immediate_superior") %>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEditImmediateSuperior" runat="server" CssClass="form-control" MaxLength="30"
                                Text='<%# Eval("immediate_superior") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtImmediateSuperior" runat="server" CssClass="form-control" required MaxLength="30"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PREVIOUS SALARY">
                        <ItemTemplate>
                            <%# Eval("previous_salary") %>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEditPreviousSalary" runat="server" CssClass="form-control" type="number"
                                MaxLength="13" Text='<%# Eval("previous_salary") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtPreviousSalary" runat="server" CssClass="form-control" type="number" required MaxLength="13"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="INDUSTRY">
                        <ItemTemplate>
                            <%# Eval("industry") %>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlEditIndustry" runat="server" CssClass="form-control"></asp:DropDownList>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:DropDownList ID="ddlIndustry" runat="server" CssClass="form-control" required
                                AppendDataBoundItems="true">
                                <asp:ListItem Selected="True" Text="Select..." Value="0" disabled></asp:ListItem>
                            </asp:DropDownList>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="REASON FOR LEAVING">
                        <ItemTemplate>
                            <%# Eval("reason") %>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlEditReason" runat="server" CssClass="form-control"></asp:DropDownList>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:DropDownList ID="ddlReason" runat="server" CssClass="form-control" required
                                AppendDataBoundItems="true">
                                <asp:ListItem Selected="True" Text="Select..." Value="0" disabled></asp:ListItem>
                            </asp:DropDownList>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="START DATE">
                        <ItemTemplate>
                            <%# Eval("start_date") != DBNull.Value ? 
                                Convert.ToDateTime(Eval("start_date")).ToString("d") : " " %>
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
                            <%# Eval("end_date") != DBNull.Value ? 
                                Convert.ToDateTime(Eval("end_date")).ToString("d") : " "%>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEditEndDate" runat="server" CssClass="form-control" type="date"
                                Text='<%# Bind("end_date", "{0:yyyy-MM-dd}") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtEndDate" runat="server" CssClass="form-control" type="date" required></asp:TextBox>
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
                            <button type="button" class="btn btn-lg btn-block btn-danger btn-gridview" data-toggle="modal" data-target="#body_gvEmployers_pnlDeleteModal_<%# Container.DataItemIndex %>">
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
                <asp:Button ID="btnPrevious" runat="server" CssClass="btn btn-default" Text="BACK" UseSubmitBehavior="False" 
                    onclick="btnPrevious_Click" />
                <asp:Button ID="btnNext" runat="server" CssClass="btn btn-primary" Text="NEXT" UseSubmitBehavior="False" 
                    onclick="btnNext_Click" />
            </div>
        </div>
    </div>
</asp:Content>