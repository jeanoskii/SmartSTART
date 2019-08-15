<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/SmartStart.Master" CodeBehind="ApplicantOtherQualificationsForm.aspx.cs" Inherits="CAPRES.ApplicantOtherQualificationsForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <h2 class="text-center">
        <asp:Label ID="lblStep" runat="server" Text=""></asp:Label>
    </h2>
    <div class="container-fluid">
        <h4 class="text-center">
            <asp:Label ID="lblError" runat="server" CssClass="error-label"></asp:Label>
        </h4>
        <div class="table-responsive">
            <asp:GridView ID="gvLicenses" runat="server" CssClass="table table-bordered table-condensed"
                AutoGenerateColumns="false"
                DataKeyNames="other_qualification_id" 
                onrowcancelingedit="gvLicenses_RowCancelingEdit" 
                onrowdatabound="gvLicenses_RowDataBound" onrowdeleting="gvLicenses_RowDeleting" 
                onrowediting="gvLicenses_RowEditing" 
                onrowupdating="gvLicenses_RowUpdating" ShowFooter="true" 
                onrowcreated="gvLicenses_RowCreated">
                <Columns>
                    <asp:BoundField HeaderText="other_qualification_id"
                        DataField="other_qualification_id" ReadOnly="true" Visible="False"/>
                    <asp:TemplateField HeaderText="LICENSES">
                        <ItemTemplate>
                            <%# Eval("license") %>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlEditParticulars" runat="server" CssClass="form-control"
                                AutoPostBack="true" onselectedindexchanged="ddlEditParticulars_SelectedIndexChanged">
                                </asp:DropDownList>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:DropDownList ID="ddlParticulars" runat="server" CssClass="form-control" 
                                AutoPostBack="true" AppendDataBoundItems="true"
                                onselectedindexchanged="ddlParticulars_SelectedIndexChanged">
                                <asp:ListItem Selected="True" Text="Select..." Value="0" disabled></asp:ListItem>
                            </asp:DropDownList>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="OTHERS">
                        <ItemTemplate>
                            <%# Eval("others_name") %>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEditSpecifyValue" runat="server" CssClass="form-control"  MaxLength="25"
                                placeholder="Please specify other particular name.">
                            </asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtOthersName" runat="server" CssClass="form-control"  MaxLength="25"
                                placeholder="Please specify other particular name.">
                            </asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="LICENSE NUMBER">
                        <ItemTemplate>
                            <%# Eval("number") %>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEditLicenseNumber" runat="server" CssClass="form-control"  MaxLength="20">
                                </asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtLicenseNumber" runat="server" CssClass="form-control"  MaxLength="20"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="RATING">
                        <ItemTemplate>
                            <%# Eval("rating") %>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEditRating" runat="server" CssClass="form-control"  MaxLength="20"></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtRating" runat="server" CssClass="form-control"  MaxLength="20"></asp:TextBox>
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
                            <button type="button" class="btn btn-lg btn-block btn-danger btn-gridview" data-toggle="modal" data-target="#body_gvLicenses_pnlDeleteModal_<%# Container.DataItemIndex %>">
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
        <div class="well">
            <div class="container-fluid">
                <div class="row form-inline question-row">
                    <div class="col-xs-8 col-sm-5">
                        <div class="text-right col-height-equals-textbox">
                            <asp:Label ID="lblQuestion1" runat="server" CssClass="text-uppercase"></asp:Label>
                        </div>
                    </div>
                    <div class="col-xs-4 col-sm-2">
                        <div class="col-height-equals-textbox">
                            <asp:RadioButtonList ID="rblQ1" runat="server" RepeatDirection="Horizontal" 
                            onselectedindexchanged="rblQ1_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem Text="NO" Value="False"></asp:ListItem>
                                <asp:ListItem Text="YES" Value="True"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                    <div class="col-xs-12 col-sm-5">
                        <asp:Panel ID="pnlQ1Yes" runat="server" Visible="false">
                            <div class="row">
                                <div class="col-xs-12 col-sm-4 col-height-equals-textbox">
                                    <label for="txtQ1Specify">Specify location/s:</label>
                                </div>
                                <div class="form-group col-xs-12 col-sm-8">
                                    <asp:TextBox ID="txtQ1Specify" runat="server" CssClass="form-control"
                                        MaxLength="50" required placeholder="Locations"></asp:TextBox>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
                <div class="row form-inline question-row">
                    <div class="col-xs-8 col-sm-5">
                        <div class="text-right col-height-equals-textbox">
                            <asp:Label ID="lblQuestion2" runat="server" CssClass="text-uppercase"></asp:Label>
                        </div>
                    </div>
                    <div class="col-xs-4 col-sm-2">
                        <div class="col-height-equals-textbox">
                            <asp:RadioButtonList ID="rblQ2" runat="server" RepeatDirection="Horizontal" 
                                onselectedindexchanged="rblQ2_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem Text="NO" Value="False"></asp:ListItem>
                                <asp:ListItem Text="YES" Value="True"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                    <div class="col-xs-12 col-sm-5">
                        <asp:Panel ID="pnlQ2Yes" runat="server" Visible="false">
                            <div class="row">
                                <div class="col-xs-12 col-sm-4 col-height-equals-textbox">
                                    <label for="txtQ2Specify">Specify case/s:</label>
                                </div>
                                <div class="form-group col-xs-12 col-sm-8">
                                    <asp:TextBox ID="txtQ2Specify" runat="server" CssClass="form-control"
                                        MaxLength="50" required placeholder="Cases"></asp:TextBox>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
                <div class="row form-inline question-row">
                    <div class="col-xs-8 col-sm-5">
                        <div class="text-right col-height-equals-textbox">
                            <asp:Label ID="lblQuestion3" runat="server" CssClass="text-uppercase"></asp:Label>
                        </div>
                    </div>
                    <div class="col-xs-4 col-sm-2">
                        <div class="col-height-equals-textbox">
                            <asp:RadioButtonList ID="rblQ3" runat="server" RepeatDirection="Horizontal" 
                            onselectedindexchanged="rblQ3_SelectedIndexChanged" AutoPostBack="true">
                            <asp:ListItem Text="NO" Value="False"></asp:ListItem>
                            <asp:ListItem Text="YES" Value="True"></asp:ListItem>
                        </asp:RadioButtonList>
                        </div>
                    </div>
                    <div class="col-xs-12 col-sm-5">
                        <asp:Panel ID="pnlQ3Yes" runat="server" Visible="false">
                            <div class="row">
                                <div class="col-xs-12 col-sm-4 col-height-equals-textbox">
                                    <label for="txtQ3Specify">Specify crime/s:</label>
                                </div>
                                <div class="form-group col-xs-12 col-sm-8">
                                    <asp:TextBox ID="txtQ3Specify" runat="server" CssClass="form-control"
                                        MaxLength="50" required placeholder="Criminal Charges"></asp:TextBox>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
                <div class="row form-inline question-row">
                    <div class="col-xs-8 col-sm-5">
                        <div class="text-right col-height-equals-textbox">
                            <asp:Label ID="lblQuestion4" runat="server" CssClass="text-uppercase"></asp:Label>
                        </div>
                    </div>
                    <div class="col-xs-4 col-sm-2">
                        <div class="col-height-equals-textbox">
                        <asp:RadioButtonList ID="rblQ4" runat="server" RepeatDirection="Horizontal" 
                            onselectedindexchanged="rblQ4_SelectedIndexChanged" AutoPostBack="true">
                            <asp:ListItem Text="NO" Value="False"></asp:ListItem>
                            <asp:ListItem Text="YES" Value="True"></asp:ListItem>
                        </asp:RadioButtonList>
                        </div>
                    </div>
                    <div class="col-xs-12 col-sm-5">
                        <asp:Panel ID="pnlQ4Yes" runat="server" Visible="false">
                            <div class="form-inline">
                                <div class="row">
                                    <div class="col-xs-12 col-sm-4 col-height-equals-textbox">
                                        <label>Specify company & relative:</label>
                                    </div>
                                    <div class="col-xs-12 col-sm-8">
                                        <div class="form-group">
                                            <asp:TextBox ID="txtQ4SpecifyCompany" runat="server" CssClass="form-control"
                                                MaxLength="50" required placeholder="Company Name"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <asp:TextBox ID="txtQ4SpecifyRelative" runat="server" CssClass="form-control"
                                                MaxLength="50" required placeholder="Relative Name (FN MI. LN)"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
                <div class="row form-inline question-row">
                    <div class="col-xs-8 col-sm-5">
                        <div class="text-right col-height-equals-textbox">
                            <asp:Label ID="lblQuestion5" runat="server" CssClass="text-uppercase"></asp:Label>
                        </div>
                    </div>
                    <div class="col-xs-4 col-sm-2">
                        <div class="col-height-equals-textbox">
                            <asp:RadioButtonList ID="rblQ5" runat="server" RepeatDirection="Horizontal" 
                                onselectedindexchanged="rblQ5_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem Text="NO" Value="False"></asp:ListItem>
                                <asp:ListItem Text="YES" Value="True"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                    <div class="col-xs-12 col-sm-5">
                        <asp:Panel ID="pnlQ5Yes" runat="server" Visible="false">
                            <div class="form-inline">
                                <div class="row">
                                    <div class="col-xs-12 col-sm-4 col-height-equals-textbox">
                                        <label>Specify company & relative:</label>
                                    </div>
                                    <div class="col-xs-12 col-sm-8">
                                        <div class="form-group">
                                            <asp:TextBox ID="txtQ5SpecifyCompany" runat="server" CssClass="form-control"
                                                MaxLength="50" required placeholder="Company Name"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <asp:TextBox ID="txtQ5SpecifyRelative" runat="server" CssClass="form-control"
                                                MaxLength="50" required placeholder="Relative Name (FN MI. LN)"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
                <div class="row form-inline question-row">
                    <div class="col-xs-8 col-sm-5">
                        <div class="text-right col-height-equals-textbox">
                            <asp:Label ID="lblQuestion6" runat="server" CssClass="text-uppercase"></asp:Label>
                        </div>
                    </div>
                    <div class="col-xs-4 col-sm-2">
                        <div class="col-height-equals-textbox">
                            <asp:RadioButtonList ID="rblQ6" runat="server" RepeatDirection="Horizontal" 
                                onselectedindexchanged="rblQ6_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem Text="NO" Value="False"></asp:ListItem>
                                <asp:ListItem Text="YES" Value="True"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                    <div class="col-xs-12 col-sm-5">
                        <asp:Panel ID="pnlQ6Yes" runat="server" Visible="false">
                            <div class="row">
                                <div class="col-xs-12 col-sm-4 col-height-equals-textbox">
                                    <label for="txtQ6Specify">Specify reason/s:</label>
                                </div>
                                <div class="form-group col-xs-12 col-sm-8">
                                    <asp:TextBox ID="txtQ6Specify" runat="server" CssClass="form-control"
                                        MaxLength="50" required placeholder="Hospital Name"></asp:TextBox>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
                <div class="row form-inline question-row">
                    <div class="col-xs-8 col-sm-5">
                        <div class="text-right col-height-equals-textbox">
                            <asp:Label ID="lblQuestion7" runat="server" CssClass="text-uppercase"></asp:Label>
                        </div>
                    </div>
                    <div class="col-xs-4 col-sm-2">
                        <div class="col-height-equals-textbox">
                            <asp:RadioButtonList ID="rblQ7" runat="server" RepeatDirection="Horizontal" 
                                onselectedindexchanged="rblQ7_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem Text="NO" Value="False"></asp:ListItem>
                                <asp:ListItem Text="YES" Value="True"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                    <div class="col-xs-12 col-sm-5">
                        <asp:Panel ID="pnlQ7Yes" runat="server" Visible="false">
                            <div class="row">
                                <div class="col-xs-12 col-sm-4 col-height-equals-textbox">
                                    <label for="txtQ7Specify">Specify condition/s:</label>
                                </div>
                                <div class="form-group col-xs-12 col-sm-8">
                                    <asp:TextBox ID="txtQ7Specify" runat="server" CssClass="form-control"
                                        MaxLength="50" required placeholder="Medical Condition"></asp:TextBox>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
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