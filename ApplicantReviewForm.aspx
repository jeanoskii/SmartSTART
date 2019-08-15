<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/SmartStart.Master" CodeBehind="ApplicantReviewForm.aspx.cs" Inherits="CAPRES.ApplicantReviewForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <h2 class="text-center">
        <asp:Label ID="lblStep" runat="server" Text=""></asp:Label>
    </h2>
    <div class="container">
        <div class="well">
            <h3 class="text-center">Application Details
                <asp:LinkButton ID="btnEditAFE" runat="server" CssClass="btn btn-lg btn-default"
                    Text="EDIT" UseSubmitBehavior="false" ToolTip="EDIT" 
                    onclick="btnEditAFE_Click">
                    <span class="glyphicon glyphicon-edit"></span>
                </asp:LinkButton>
            </h3>
            <div class="row">
                <div class="col-xs-6 text-right">
                    <label>Application Date:</label>
                </div>
                <div class="col-xs-6 text-left">
                    <asp:Label ID="lblApplicationDate" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-6 text-right">
                    <label>Position Applied For:</label>
                </div>
                <div class="col-xs-6 text-left">
                    <asp:Label ID="lblPositionAppliedFor" runat="server" Text=""></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-6 text-right">
                    <label>Interviewer Name:</label>
                </div>
                <div class="col-xs-6 text-left">
                    <asp:Label ID="lblInterviewerName" runat="server" Text=""></asp:Label>
                </div>
            </div>
        </div>
        <div class="well">
            <h3 class="text-center">Personal Information
                <asp:LinkButton ID="btnEditPersonal" runat="server" CssClass="btn btn-lg btn-default"
                    Text="EDIT" UseSubmitBehavior="false" ToolTip="EDIT" 
                    onclick="btnEditPersonal_Click">
                    <span class="glyphicon glyphicon-edit"></span>
                </asp:LinkButton>
            </h3>
            <div class="row">
                <div class="col-xs-6 text-right">
                    <label>Full Name:</label>
                </div>
                <div class="col-xs-6 text-left">
                    <asp:Label ID="lblName" runat="server"></asp:Label>
                </div>
            </div>
            <asp:Panel ID="pnlApplicantPersonalInfo" runat="server">
                <div class="row">
                    <div class="col-xs-6 text-right">
                        <label>Mobile Number:</label>
                    </div>
                    <div class="col-xs-6 text-left">
                        <asp:Label ID="lblApplicantMobileNumber" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-6 text-right">
                        <label>Email Address:</label>
                    </div>
                    <div class="col-xs-6 text-left">
                        <asp:Label ID="lblApplicantEmailAddress" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-6 text-right">
                        <label>Date of Birth:</label>
                    </div>
                    <div class="col-xs-6 text-left">
                        <asp:Label ID="lblApplicantDateOfBirth" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-6 text-right">
                        <label>Place of Birth:</label>
                    </div>
                    <div class="col-xs-6 text-left">
                        <asp:Label ID="lblApplicantPlaceOfBirth" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-6 text-right">
                        <label>Gender:</label>
                    </div>
                    <div class="col-xs-6 text-left">
                        <asp:Label ID="lblApplicantGender" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-6 text-right">
                        <label>Civil Status:</label>
                    </div>
                    <div class="col-xs-6 text-left">
                        <asp:Label ID="lblApplicantCivilStatus" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-6 text-right">
                        <label>Present Address:</label>
                    </div>
                    <div class="col-xs-6 text-left">
                        <asp:Label ID="lblApplicantPresentAddress" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-6 text-right">
                        <label>Provincial Address:</label>
                    </div>
                    <div class="col-xs-6 text-left">
                        <asp:Label ID="lblApplicantProvincialAddress" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-6 text-right">
                        <label>Height:</label>
                    </div>
                    <div class="col-xs-6 text-left">
                        <asp:Label ID="lblApplicantHeight" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-6 text-right">
                        <label>Weight:</label>
                    </div>
                    <div class="col-xs-6 text-left">
                        <asp:Label ID="lblApplicantWeight" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-6 text-right">
                        <label>Blood Type:</label>
                    </div>
                    <div class="col-xs-6 text-left">
                        <asp:Label ID="lblApplicantBloodType" runat="server" Text=""></asp:Label>
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel ID="pnlPreboardingPersonalInfo" runat="server">
                <div class="row">
                    <div class="col-xs-6 text-right">
                        <label>Date of Birth:</label>
                    </div>
                    <div class="col-xs-6 text-left">
                        <asp:Label ID="lblDateOfBirth" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-6 text-right">
                        <label>Place of Birth:</label>
                    </div>
                    <div class="col-xs-6 text-left">
                        <asp:Label ID="lblPlaceOfBirth" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-6 text-right">
                        <label>Nationality:</label>
                    </div>
                    <div class="col-xs-6 text-left">
                        <asp:Label ID="lblNationality" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-6 text-right">
                        <label>Gender:</label>
                    </div>
                    <div class="col-xs-6 text-left">
                        <asp:Label ID="lblGender" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-6 text-right">
                        <label>Civil Status:</label>
                    </div>
                    <div class="col-xs-6 text-left">
                        <asp:Label ID="lblCivilStatus" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-6 text-right">
                        <label>Religion:</label>
                    </div>
                    <div class="col-xs-6 text-left">
                        <asp:Label ID="lblReligion" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-6 text-right">
                        <label>Height:</label>
                    </div>
                    <div class="col-xs-6 text-left">
                        <asp:Label ID="lblHeight" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-6 text-right">
                        <label>Weight:</label>
                    </div>
                    <div class="col-xs-6 text-left">
                        <asp:Label ID="lblWeight" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-6 text-right">
                        <label>Blood Type:</label>
                    </div>
                    <div class="col-xs-6 text-left">
                        <asp:Label ID="lblBloodType" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-6 text-right">
                        <label>SSS Number:</label>
                    </div>
                    <div class="col-xs-6 text-left">
                        <asp:Label ID="lblSSS" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-6 text-right">
                        <label>TIN Number:</label>
                    </div>
                    <div class="col-xs-6 text-left">
                        <asp:Label ID="lblTIN" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-12 text-center">
                        <p class="text-muted">Addresses</p>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-6 text-right">
                        <label>Present:</label>
                    </div>
                    <div class="col-xs-6 text-left">
                        <asp:Label ID="lblPresentAddress" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-6 text-right">
                        <label>Permanent:</label>
                    </div>
                    <div class="col-xs-6 text-left">
                        <asp:Label ID="lblPermanentAddress" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-6 text-right">
                        <label>Provincial:</label>
                    </div>
                    <div class="col-xs-6 text-left">
                        <asp:Label ID="lblProvincialAddress" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-12 text-center">
                        <p class="text-muted">Contact Numbers</p>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-6 text-right">
                        <label>Home Telephone:</label>
                    </div>
                    <div class="col-xs-6 text-left">
                        <asp:Label ID="lblHomeNumber" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-6 text-right">
                        <label>Provincial Telephone:</label>
                    </div>
                    <div class="col-xs-6 text-left">
                        <asp:Label ID="lblProvincialNumber" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-6 text-right">
                        <label>Additional:</label>
                    </div>
                    <div class="col-xs-6 text-left">
                        <asp:Label ID="lblAdditionalNumber" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-6 text-right">
                        <label>Mobile:</label>
                    </div>
                    <div class="col-xs-6 text-left">
                        <asp:Label ID="lblMobileNumber" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-6 text-right">
                        <label>Fax:</label>
                    </div>
                    <div class="col-xs-6 text-left">
                        <asp:Label ID="lblFaxNumber" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-6 text-right">
                        <label>Email Address:</label>
                    </div>
                    <div class="col-xs-6 text-left">
                        <asp:Label ID="lblEmailAddress" runat="server" Text=""></asp:Label>
                    </div>
                </div>
            </asp:Panel>
        </div>
        <asp:Panel ID="pnlPreboardingEducational" runat="server">
            <div class="well">
                <h3 class="text-center">Educational Background
                    <asp:LinkButton ID="btnEditEducationalBackground" runat="server"
                        CssClass="btn btn-lg btn-default" Text="EDIT" UseSubmitBehavior="false" 
                        ToolTip="EDIT" onclick="btnEditEducationalBackground_Click">
                        <span class="glyphicon glyphicon-edit"></span>
                    </asp:LinkButton>
                </h3>
                <div class="table-responsive">
                    <asp:GridView ID="gvEducations" runat="server" CssClass="table table-bordered table-condensed" AutoGenerateColumns="false" 
                        DataKeyNames="file2_id">
                        <Columns>
                            <asp:BoundField HeaderText="file2_id" DataField="file2_id" ReadOnly="true"
                                Visible="False"/>
                            <asp:TemplateField HeaderText="EDUCATION ESTABLISHMENT">
                                <ItemTemplate>
                                    <%# Eval("education_establishment") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="SCHOOL">
                                <ItemTemplate>
                                    <%# Eval("school") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="SCHOOL-OTHERS">
                                <ItemTemplate>
                                    <%# Eval("school_others") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="START DATE">
                                <ItemTemplate>
                                    <%# Convert.ToDateTime(Eval("start_date")).ToString("d")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="END DATE">
                                <ItemTemplate>
                                    <%# Convert.ToDateTime(Eval("end_date")).ToString("d")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="BRANCH OF STUDY 1">
                                <ItemTemplate>
                                    <%# Eval("branch_of_study_1") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CERTIFICATE">
                                <ItemTemplate>
                                    <%# Eval("certificate") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="COURSE APPRAISAL">
                                <ItemTemplate>
                                    <%# Eval("course_appraisal") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="BRANCH OF STUDY 2">
                                <ItemTemplate>
                                    <%# Eval("branch_of_study_2") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </asp:Panel>
        <div class="well">
            <h3 class="text-center">Employment History
                <asp:LinkButton ID="btnEditEmployment" runat="server"
                    CssClass="btn btn-lg btn-default" Text="EDIT" UseSubmitBehavior="false" 
                    ToolTip="EDIT" onclick="btnEditEmployment_Click">
                    <span class="glyphicon glyphicon-edit"></span>
                </asp:LinkButton>
            </h3>
            <div class="table-responsive">
                <asp:GridView ID="gvEmployers" runat="server" CssClass="table table-bordered table-condensed" AutoGenerateColumns="false" 
                    DataKeyNames="file3_id">
                    <Columns>
                        <asp:BoundField HeaderText="file3_id" DataField="file3_id" ReadOnly="true"
                            Visible="False"/>
                        <asp:TemplateField HeaderText="PREVIOUS EMPLOYER">
                            <ItemTemplate>
                                <%# Eval("previous_employer") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="CITY">
                            <ItemTemplate>
                                <%# Eval("city") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="POSITION HELD">
                            <ItemTemplate>
                                <%# Eval("position_held") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="SPECIALIZATION">
                            <ItemTemplate>
                                <%# Eval("specialization") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="IMMEDIATE SUPERIOR">
                            <ItemTemplate>
                                <%# Eval("immediate_superior") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PREVIOUS SALARY">
                            <ItemTemplate>
                                <%# Eval("previous_salary") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="INDUSTRY">
                            <ItemTemplate>
                                <%# Eval("industry") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="REASON">
                            <ItemTemplate>
                                <%# Eval("reason") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="START DATE">
                            <ItemTemplate>
                                <%# Convert.ToDateTime(Eval("start_date")).ToString("d")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="END DATE">
                            <ItemTemplate>
                                <%# Convert.ToDateTime(Eval("end_date")).ToString("d")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <div class="well">
            <h3 class="text-center">Other Qualifications
                <asp:LinkButton ID="btnEditQualifications" runat="server"
                    CssClass="btn btn-lg btn-default" Text="EDIT" UseSubmitBehavior="false" 
                    ToolTip="EDIT" onclick="btnEditQualifications_Click">
                    <span class="glyphicon glyphicon-edit"></span>
                </asp:LinkButton>
            </h3>
            <div class="table-responsive">
                <asp:GridView ID="gvOtherQualifications" runat="server" CssClass="table table-bordered table-condensed" AutoGenerateColumns="false"
                    DataKeyNames="other_qualification_id">
                    <Columns>
                        <asp:BoundField HeaderText="other_qualification_id"
                            DataField="other_qualification_id" ReadOnly="true" Visible="false" />
                        <asp:TemplateField HeaderText="LICENSE">
                            <ItemTemplate>
                                <%# Eval("license") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="LICENSE-OTHERS">
                            <ItemTemplate>
                                <%# Eval("others_name") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="NUMBER">
                            <ItemTemplate>
                                <%# Eval("number") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="RATING">
                            <ItemTemplate>
                                <%# Eval("rating") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            <div class="row">
                <div class="col-xs-6 text-right">
                    <label><asp:Label ID="lblQuestion1" runat="server"></asp:Label></label>
                </div>
                <div class="col-xs-6 text-left">
                    <asp:Label ID="lblAnswer1" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-6 text-right">
                    <label><asp:Label ID="lblQuestion2" runat="server"></asp:Label></label>
                </div>
                <div class="col-xs-6 text-left">
                    <asp:Label ID="lblAnswer2" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-6 text-right">
                    <label><asp:Label ID="lblQuestion3" runat="server"></asp:Label></label>
                </div>
                <div class="col-xs-6 text-left">
                    <asp:Label ID="lblAnswer3" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-6 text-right">
                    <label><asp:Label ID="lblQuestion4" runat="server"></asp:Label></label>
                </div>
                <div class="col-xs-6 text-left">
                    <asp:Label ID="lblAnswer4" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-6 text-right">
                    <label><asp:Label ID="lblQuestion5" runat="server"></asp:Label></label>
                </div>
                <div class="col-xs-6 text-left">
                    <asp:Label ID="lblAnswer5" runat="server" Text=""></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-6 text-right">
                    <label><asp:Label ID="lblQuestion6" runat="server"></asp:Label></label>
                </div>
                <div class="col-xs-6 text-left">
                    <asp:Label ID="lblAnswer6" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-6 text-right">
                    <label><asp:Label ID="lblQuestion7" runat="server"></asp:Label></label>
                </div>
                <div class="col-xs-6 text-left">
                    <asp:Label ID="lblAnswer7" runat="server"></asp:Label>
                </div>
            </div>
        </div>
        <asp:Panel ID="pnlPreboardingOrganizational" runat="server">
            <div class="well">
                <h3 class="text-center">Organizational Membership
                    <asp:LinkButton ID="btnEditOrganization" runat="server"
                        CssClass="btn btn-lg btn-default" Text="EDIT" UseSubmitBehavior="false" 
                        ToolTip="EDIT" onclick="btnEditOrganization_Click">
                        <span class="glyphicon glyphicon-edit"></span>
                    </asp:LinkButton>
                </h3>
                <div class="table-responsive">
                    <asp:GridView ID="gvOrganizations" runat="server" CssClass="table table-bordered table-condensed" AutoGenerateColumns="false" 
                            DataKeyNames="file4_id">
                        <Columns>
                            <asp:BoundField HeaderText="file4_id" DataField="file4_id" ReadOnly="true"
                                Visible="False"/>
                            <asp:TemplateField HeaderText="Organization Type">
                                <ItemTemplate>
                                    <%# Eval("organization_type") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Organization Name">
                                <ItemTemplate>
                                    <%# Eval("organization_name") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Start Date">
                                <ItemTemplate>
                                    <%# Convert.ToDateTime(Eval("start_date")).ToString("d")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="End Date">
                                <ItemTemplate>
                                    <%# Convert.ToDateTime(Eval("end_date")).ToString("d") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Position Type">
                                <ItemTemplate>
                                    <%# Eval("position_type") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </asp:Panel>
        <div class="well">
            <h3 class="text-center">Family Members
                <asp:LinkButton ID="btnEditFamily" runat="server"
                    CssClass="btn btn-lg btn-default" Text="EDIT" UseSubmitBehavior="false" 
                    ToolTip="EDIT" onclick="btnEditFamily_Click">
                    <span class="glyphicon glyphicon-edit"></span>
                </asp:LinkButton>
            </h3>
            <div class="table-responsive">
                <asp:GridView ID="gvDependents" CssClass="table table-bordered table-condensed" runat="server" AutoGenerateColumns="false"
                    DataKeyNames="file5_id">
                    <Columns>
                        <asp:BoundField HeaderText="file5_id" DataField="file5_id" ReadOnly="true"
                            Visible="False"/>
                        <asp:TemplateField HeaderText="NAME">
                            <ItemTemplate>
                                <%# Eval("dependents_name") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="RELATIONSHIP">
                            <ItemTemplate>
                                <%# Eval("family_member") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="GENDER">
                            <ItemTemplate>
                                <%# Eval("gender") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="BIRTHDATE">
                            <ItemTemplate>
                                <%# Convert.ToDateTime(Eval("birthdate")).ToString("d") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <div class="well">
            <h3 class="text-center">Professional References
                <asp:LinkButton ID="btnEditProfessionalRef" runat="server"
                    CssClass="btn btn-lg btn-default" Text="EDIT" UseSubmitBehavior="false" 
                    ToolTip="EDIT" onclick="btnEditProfessionalRef_Click">
                    <span class="glyphicon glyphicon-edit"></span>
                </asp:LinkButton>
            </h3>
            <div class="table-responsive">
                <asp:GridView ID="gvProfessionalReferences" runat="server" CssClass="table table-bordered table-condensed" AutoGenerateColumns="false" 
                    DataKeyNames="professional_reference_id">
                    <Columns>
                        <asp:BoundField HeaderText="professional_reference_id"
                            DataField="professional_reference_id" ReadOnly="true" Visible="false" />
                        <asp:TemplateField HeaderText="Last Name">
                            <ItemTemplate>
                                <%# Eval("last_name") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="First Name">
                            <ItemTemplate>
                                <%# Eval("first_name") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Contact Number">
                            <ItemTemplate>
                                <%# Eval("contact_number") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Company Name">
                            <ItemTemplate>
                                <%# Eval("company_name") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Position Title">
                            <ItemTemplate>
                                <%# Eval("position_title") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <div class="well">
            <h3 class="text-center">Personal References
                <asp:LinkButton ID="btnEditPersonalRef" runat="server"
                    CssClass="btn btn-lg btn-default" Text="EDIT" UseSubmitBehavior="false" 
                    ToolTip="EDIT" onclick="btnEditPersonalRef_Click">
                    <span class="glyphicon glyphicon-edit"></span>
                </asp:LinkButton>
            </h3>
            <div class="table-responsive">
                <asp:GridView ID="gvPersonalReferences" runat="server" CssClass="table table-bordered table-condensed" AutoGenerateColumns="false"
                    DataKeyNames="personal_reference_id">
                    <Columns>
                        <asp:BoundField HeaderText="personal_reference_id"
                            DataField="personal_reference_id" ReadOnly="true" Visible="false" />
                        <asp:TemplateField HeaderText="LAST NAME">
                            <ItemTemplate>
                                <%# Eval("last_name") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="FIRST NAME">
                            <ItemTemplate>
                                <%# Eval("first_name") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="CONTACT NUMBER">
                            <ItemTemplate>
                                <%# Eval("contact_number") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="RELATIONSHIP">
                            <ItemTemplate>
                                <%# Eval("relationship") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <h4 class="text-center"><strong>Before clicking submit, please agree to these conditions.</strong></h4>
        <h4 class="text-center">
            <asp:Label ID="lblError" runat="server" CssClass="error-label"></asp:Label>
        </h4>
        <div class="row">
            <div class="col-xs-12 col-sm-1 text-center">
                <asp:CheckBox ID="chkAgree" runat="server" required />
            </div>
            <div class="col-xs-12 col-sm-11 text-justify">
                <p>I hereby declare that all stated information are true and correct.
                    Any falsification made herein shall be taken as sufficient ground for my dismissal.
                    SMART COMMUNICATIONS, INC. and its subsidiaries and accredited partners are
                    likewise authorized to obtain such information as basis to further evaluate
                    my personal data through its standard recruitment and selection process
                    (i.e. interview, background investigation).
                </p>
            </div>
        </div>
        <div class="text-center">
            <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-lg btn-primary"
                Text="SUBMIT" onclick="btnSubmit_Click"/>
        </div>
    </div>
</asp:Content>