<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/SmartStart.Master" CodeBehind="ApplicantEducationalForm.aspx.cs" Inherits="CAPRES.ApplicantEducationalForm" %>
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
            <asp:GridView ID="gvEducations" runat="server" CssClass="table table-bordered table-condensed" AutoGenerateColumns="false" 
                DataKeyNames="file2_id" onrowcancelingedit="gvEducations_RowCancelingEdit" 
                onrowdatabound="gvEducations_RowDataBound" 
                onrowdeleting="gvEducations_RowDeleting" onrowediting="gvEducations_RowEditing" 
                onrowupdating="gvEducations_RowUpdating" ShowFooter="true" 
                onrowcreated="gvEducations_RowCreated">
                <Columns>
                    <asp:BoundField HeaderText="file2_id" DataField="file2_id" ReadOnly="true" Visible="False"/>
                    <asp:TemplateField HeaderText="ESTABLISHMENT">
                        <ItemTemplate>
                            <%# Eval("education_establishment") %>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlEditEducationEstablishment" runat="server" CssClass="form-control"
                                OnSelectedIndexChanged="ddlEditEducationEstablishment_SelectedIndexChanged">
                            </asp:DropDownList>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:DropDownList ID="ddlEducationEstablishment" runat="server" CssClass="form-control" required 
                                AutoPostBack="True"  AppendDataBoundItems="true"
                                onselectedindexchanged="ddlEducationEstablishment_SelectedIndexChanged">
                                <asp:ListItem Selected="True" Text="Select..." Value="0" disabled></asp:ListItem>
                            </asp:DropDownList>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="SCHOOL">
                        <ItemTemplate>
                            <%# Eval("school") %>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlEditSchool" runat="server" CssClass="form-control" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlEditSchool_SelectedIndexChanged">
                            </asp:DropDownList>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:DropDownList ID="ddlSchool" runat="server" CssClass="form-control" AutoPostBack="True" required
                                AppendDataBoundItems="true"
                                onselectedindexchanged="ddlSchool_SelectedIndexChanged">
                                <asp:ListItem Selected="True" Text="Select..." Value="0" disabled></asp:ListItem>
                            </asp:DropDownList>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="OTHERS">
                        <ItemTemplate>
                            <%# Eval("school_others") %>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEditSchoolOthers" runat="server" CssClass="form-control" MaxLength="81"
                                Text='<%# Eval("school_others") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtSchoolOthers" runat="server" CssClass="form-control" MaxLength="81"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="START DATE">
                        <ItemTemplate>
                            <%# Eval("start_date") != DBNull.Value ? Convert.ToDateTime(Eval("start_date")).ToString("d") : " " %>
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
                            <%# Eval("end_date") != DBNull.Value ? Convert.ToDateTime(Eval("end_date")).ToString("d") : " " %>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEditEndDate" runat="server" CssClass="form-control" type="date"
                                Text='<%# Bind("end_date", "{0:yyyy-MM-dd}") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtEndDate" runat="server" CssClass="form-control" type="date" required>
                            </asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="FIRST MAJOR">
                        <ItemTemplate>
                            <%# Eval("branch_of_study_1") %>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlEditBranchOfStudy1" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:DropDownList ID="ddlBranchOfStudy1" runat="server" CssClass="form-control"
                                AppendDataBoundItems="true">
                                <asp:ListItem Selected="True" Text="Select..." Value="0" disabled></asp:ListItem>
                            </asp:DropDownList>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="CERTIFICATE">
                        <ItemTemplate>
                            <%# Eval("certificate") %>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlEditCertificate" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:DropDownList ID="ddlCertificate" runat="server" CssClass="form-control" required
                                AppendDataBoundItems="true">
                                <asp:ListItem Selected="True" Text="Select..." Value="0" disabled></asp:ListItem>
                            </asp:DropDownList>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="COURSE APPRAISAL">
                        <ItemTemplate>
                            <%# Eval("course_appraisal") %>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEditCourseAppraisal" runat="server" CssClass="form-control" MaxLength="30"
                                Text='<%# Eval("course_appraisal") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtCourseAppraisal" runat="server" CssClass="form-control" MaxLength="40"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="SECOND MAJOR">
                        <ItemTemplate>
                            <%# Eval("branch_of_study_2") %>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlEditBranchOfStudy2" runat="server" CssClass="form-control"></asp:DropDownList>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:DropDownList ID="ddlBranchOfStudy2" runat="server" CssClass="form-control"
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
                            <button type="button" class="btn btn-lg btn-block btn-danger btn-gridview" data-toggle="modal" data-target="#body_gvEducations_pnlDeleteModal_<%# Container.DataItemIndex %>">
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