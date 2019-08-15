<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/SmartStart.Master" CodeBehind="ApplicantPersonalForm.aspx.cs" Inherits="CAPRES.ApplicantPersonalForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <h2 class="text-center">
        <asp:Label ID="lblStep" runat="server" Text=""></asp:Label>
    </h2>
    <div class="container">
        <asp:HiddenField ID="hidFile1ID" runat="server" />
        <div class="well well-lg">
            <h4 class="text-center">
                  <asp:Label ID="lblError" runat="server" class="error-label" Text=""></asp:Label>
            </h4>
            <div class="row">
                <div class="col-xs-12 col-sm-6 col-md-3">
                    <div class="form-group">
                        <label class="control-label" for="txtFirstName">First Name:</label>
                        <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control"
                            required MaxLength="25" placeholder="e.g. Jose"></asp:TextBox>
                    </div>
                </div>
                <div class="col-xs-12 col-sm-6 col-md-3">
                    <div class="form-group">
                        <label class="control-label" for="txtNickName">Nick Name:</label>
                        <asp:TextBox ID="txtNickName" runat="server" CssClass="form-control"
                            required MaxLength="25" placeholder="e.g. Pepe"></asp:TextBox>
                    </div>
                </div>
                <div class="col-xs-12 col-sm-6 col-md-3">
                    <div class="form-group">
                        <label class="control-label" for="txtMiddleName">Middle Name:</label>
                        <asp:TextBox ID="txtMiddleName" runat="server" CssClass="form-control"
                            required MaxLength="25" placeholder="e.g. Protacio"></asp:TextBox>
                    </div>
                </div>
                <div class="col-xs-12 col-sm-6 col-md-3">
                    <div class="form-group">
                        <label class="control-label" for="txtLastName">Last Name:</label>
                        <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control"
                            required MaxLength="25" placeholder="e.g. Rizal"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <label class="control-label" for="txtPresentMobileNumber">Mobile Number:</label>
                <asp:TextBox ID="txtPresentMobileNumber" runat="server" CssClass="form-control"
                    MaxLength="14" placeholder="e.g. 09071234567"></asp:TextBox>
            </div>
            <div class="row">
                <div class="col-xs-12 col-sm-6">
                    <div class="form-group">
                        <label class="control-label" for="txtDateOfBirth">Date of Birth:</label>
                        <asp:TextBox ID="txtDateOfBirth" runat="server" CssClass="form-control"
                            type="date" required placeholder="mm/dd/yyyy"></asp:TextBox>
                    </div>
                </div>
                <div class="col-xs-12 col-sm-6">
                    <div class="form-group">
                        <label class="control-label" for="txtPlaceOfBirth">Place of Birth:</label>
                        <asp:TextBox ID="txtPlaceOfBirth" runat="server" CssClass="form-control"
                            required MaxLength="25" placeholder="e.g. Calamba, Laguna"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-12 col-sm-6">
                    <div class="form-group">
                        <label for="ddlCivilStatus">Civil Status:</label>
                        <asp:DropDownList ID="ddlCivilStatus" runat="server" CssClass="form-control"
                            required AutoPostBack="true" AppendDataBoundItems="true"
                            onselectedindexchanged="ddlCivilStatus_SelectedIndexChanged">
                            <asp:ListItem Selected="True" Text="Select..." Value="-1" disabled></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-xs-12 col-sm-6">
                    <asp:Panel ID="pnlCivilStatusDate" runat="server" Visible="false">
                        <div class="form-group">
                            <label for="txtCivilStatusDate">Since when?</label>
                            <asp:TextBox ID="txtCivilStatusDate" runat="server" type="date" CssClass="form-control"
                                required placeholder="mm/dd/yyyy"></asp:TextBox>
                        </div>
                    </asp:Panel>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-12 col-sm-3">
                    <div class="form-group">
                        <label class="control-label" for="ddlGender">Gender:</label>
                        <asp:DropDownList ID="ddlGender" runat="server" CssClass="form-control"
                            required AppendDataBoundItems="true">
                            <asp:ListItem Selected="True" Text="Select..." Value="0" disabled></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-xs-12 col-sm-3">
                    <div class="form-group">
                        <label for="ddlBloodType">Blood Type:</label>
                        <asp:DropDownList ID="ddlBloodType" runat="server" CssClass="form-control" required
                            AppendDataBoundItems="true">
                            <asp:ListItem Selected="True" Text="Select..." Value="0" disabled></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-xs-12 col-sm-3">
                    <div class="form-group">
                        <label for="txtWeight">Weight (kg):</label>
                        <asp:TextBox ID="txtWeight" runat="server" CssClass="form-control"
                            type="number" max="200" min="20" placeholder="e.g. 50kg"></asp:TextBox>
                    </div>
                </div>
                <div class="col-xs-12 col-sm-3">
                    <div class="form-group">
                        <label for="txtHeightFeet">Height (ft, in):</label>
                        <div class="form-inline">
                            <asp:TextBox ID="txtHeightFeet" runat="server" CssClass="form-control"
                                type="number" max="9" min="4" placeholder="Feet"></asp:TextBox>
                            <asp:TextBox ID="txtHeightInches" runat="server" CssClass="form-control"
                                type="number" max="11" min="0" placeholder="Inches"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-12 col-sm-6 col-md-12">
                    <div class="form-group">
                        <label for="txtPresentAddress">Present Address:</label>
                        <asp:TextBox ID="txtPresentAddress" runat="server" required CssClass="form-control"
                            MaxLength="75" placeholder="e.g. Rizal Park, Roxas Boulevard"></asp:TextBox>
                    </div>
                </div>
                <div class="col-xs-12 col-sm-6 col-md-4">
                    <div class="form-group">
                        <label for="txtPresentCity">Present City:</label>
                        <asp:TextBox ID="txtPresentCity" runat="server" required CssClass="form-control"
                            MaxLength="35" placeholder="e.g. Metro Manila"></asp:TextBox>
                    </div>
                </div>
                <div class="col-xs-12 col-sm-6 col-md-4">
                    <div class="form-group">
                        <label for="txtPresentDistrict">Present District:</label>
                        <asp:TextBox ID="txtPresentDistrict" runat="server" CssClass="form-control"
                            MaxLength="30" placeholder="Leave blank if not applicable"></asp:TextBox>
                    </div>
                </div>
                <div class="col-xs-12 col-sm-6 col-md-4">
                    <div class="form-group">
                        <label for="ddlPresentCountry">Present Country:</label>
                        <asp:DropDownList ID="ddlPresentCountry" runat="server" required CssClass="form-control"
                            AppendDataBoundItems="true">
                            <asp:ListItem Selected="True" Text="Select..." Value="0" disabled></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-12 col-sm-6 col-md-12">
                    <div class="form-group">
                        <label for="txtProvincialAddress">Provincial Address:</label>
                        <asp:TextBox ID="txtProvincialAddress" runat="server" CssClass="form-control"
                            MaxLength="75" placeholder="e.g. Francisco Mercado St. cor. Jose P. Rizal St., Brgy. 5, Poblacion">
                        </asp:TextBox>
                    </div>
                </div>
                <div class="col-xs-12 col-sm-6 col-md-4">
                    <div class="form-group">
                        <label for="txtProvincialCity">Provincial City:</label>
                        <asp:TextBox ID="txtProvincialCity" runat="server" CssClass="form-control"
                            MaxLength="35" placeholder="e.g. Calamba"></asp:TextBox>
                    </div>
                </div>
                <div class="col-xs-12 col-sm-6 col-md-4">
                    <div class="form-group">
                        <label for="txtPermanentDistrict">Provincial District:</label>
                        <asp:TextBox ID="txtProvincialDistrict" runat="server" CssClass="form-control"
                            MaxLength="30" placeholder="e.g. Laguna"></asp:TextBox>
                    </div>
                </div>
                <div class="col-xs-12 col-sm-6 col-md-4">
                    <div class="form-group">
                        <label for="ddlProvincialCountry">Provincial Country:</label>
                        <asp:DropDownList ID="ddlProvincialCountry" runat="server" CssClass="form-control"
                            AppendDataBoundItems="true">
                            <asp:ListItem Selected="True" Text="Select..." Value="0" disabled></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <asp:Panel ID="pnlFile1" runat="server">
                <div class="row">
                    <div class="col-xs-12 col-sm-3">
                        <div class="form-group has-success">
                            <label for="ddlNationality">Nationality:</label>
                            <span class="label label-success">NEW</span>
                            <asp:DropDownList ID="ddlNationality" runat="server" CssClass="form-control"
                                AppendDataBoundItems="true">
                                <asp:ListItem Selected="True" Text="Select..." Value="0" disabled></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-xs-12 col-sm-3">
                        <div class="form-group has-success">
                            <label for="ddlReligion">Religion:</label>
                            <span class="label label-success">NEW</span>
                            <asp:DropDownList ID="ddlReligion" runat="server" CssClass="form-control"
                                AppendDataBoundItems="true">
                                <asp:ListItem Selected="True" Text="Select..." Value="0" disabled></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-xs-12 col-sm-3">
                        <div class="form-group has-success">
                            <label for="ddlAffix">Affix:</label>
                            <span class="label label-success">NEW</span>
                            <asp:DropDownList ID="ddlAffix" runat="server" CssClass="form-control"
                                AppendDataBoundItems="true">
                                <asp:ListItem Selected="True" Text="Select..." Value="0" disabled></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-12 col-sm-6">
                        <div class="form-group has-success">
                            <label for="txtSSS">SSS Number:</label>
                            <span class="label label-success">NEW</span>
                            <asp:TextBox ID="txtSSS" runat="server" CssClass="form-control" type="number" min="0000000000" max="9999999999" MaxLength="10"
                                placeholder="e.g. XY123456789"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-xs-12 col-sm-6">
                        <div class="form-group has-success">
                            <label for="txtTIN">TIN Number:</label>
                            <span class="label label-success">NEW</span>
                            <asp:TextBox ID="txtTIN" runat="server" CssClass="form-control" type="number" min="000000000000" max="999999999999" MaxLength="12"
                                placeholder="e.g. XYZ123456789"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-12 col-sm-6 col-md-3">
                        <div class="form-group has-success">
                            <label for="txtPresentTelephoneHome">Home Telephone:</label>
                            <span class="label label-success">NEW</span>
                            <asp:TextBox ID="txtPresentTelephoneHome" runat="server" CssClass="form-control"
                                MaxLength="14" placeholder="e.g. 0491234567"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-xs-12 col-sm-6 col-md-3">
                        <div class="form-group has-success">
                            <label for="txtProvincialTelephone">Provincial Telephone:</label>
                            <span class="label label-success">NEW</span>
                            <asp:TextBox ID="txtProvincialTelephone" runat="server" CssClass="form-control"
                                MaxLength="14" placeholder="e.g. 0490111110"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-xs-12 col-sm-6 col-md-3">
                        <div class="form-group has-success">
                            <label for="txtPresentTelephoneAdditional">Additional Telephone:</label>
                            <span class="label label-success">NEW</span>
                            <asp:TextBox ID="txtPresentTelephoneAdditional" runat="server" CssClass="form-control"
                                MaxLength="25" placeholder="e.g. 0497654321"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-xs-12 col-sm-6 col-md-3">
                        <div class="form-group has-success">
                            <label for="txtPresentFax">Fax:</label>
                            <span class="label label-success">NEW</span>
                            <asp:TextBox ID="txtPresentFax" runat="server" CssClass="form-control"
                                MaxLength="40" placeholder="e.g. 0491000001"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-12 col-sm-12">
                        <div class="form-group has-success">
                            <label for="txtPermanentAddress">Permanent Address:</label>
                            <span class="label label-success">NEW</span>
                            <asp:TextBox ID="txtPermanentAddress" runat="server" CssClass="form-control"
                                MaxLength="75" placeholder="e.g. Rizal Park, Roxas Boulevard"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-xs-12 col-sm-4">
                        <div class="form-group has-success">
                            <label for="txtPermanentCity">Permanent City:</label>
                            <span class="label label-success">NEW</span>
                            <asp:TextBox ID="txtPermanentCity" runat="server" CssClass="form-control"
                                MaxLength="35" placeholder="e.g. Metro Manila"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-xs-12 col-sm-4">
                        <div class="form-group has-success">
                            <label for="txtPermanentDistrict">Permanent District:</label>
                            <span class="label label-success">NEW</span>
                            <asp:TextBox ID="txtPermanentDistrict" runat="server" CssClass="form-control"
                                MaxLength="30" placeholder="Leave blank if not applicable"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-xs-12 col-sm-4">
                        <div class="form-group has-success">
                            <label for="ddlPermanentCountry">Permanent Country:</label>
                            <span class="label label-success">NEW</span>
                            <asp:DropDownList ID="ddlPermanentCountry" CssClass="form-control" runat="server"
                                AppendDataBoundItems="true">
                                <asp:ListItem Selected="True" Text="Select..." Value="0" disabled></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
        </asp:Panel>
        </div>
        <div class="text-center">
            <div class="btn-group btn-group-lg">
                <asp:Button ID="btnPrevious" runat="server" CssClass="btn btn-default" Text="BACK" 
                    UseSubmitBehavior="false" onclick="btnPrevious_Click" 
                    />&nbsp
                <asp:Button ID="btnHome" runat="server" CssClass="btn btn-default" Text="HOME" UseSubmitBehavior="false" 
                    onclick="btnHome_Click"/>&nbsp
                <asp:Button ID="btnNext" runat="server" CssClass="btn btn-lg btn-primary" Text="NEXT" 
                    onclick="btnNext_Click" />
            </div>
        </div>
    </div>
</asp:Content>
