<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApplicantPrint.aspx.cs" Inherits="CAPRES.ApplicantPrint" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h2><asp:Label ID="lblUser" runat="server" Text=""></asp:Label></h2>
        <h3>Application Details</h3>
        <strong><label>Application Date: </label></strong>
        <asp:Label ID="lblApplicationDate" runat="server" Text=""></asp:Label>
        <br />
        <strong><label>Position Applied For: </label></strong>
        <asp:Label ID="lblPositionAppliedFor" runat="server" Text=""></asp:Label>
        <br />
        <strong><label>Interviewer Name: </label></strong>
        <asp:Label ID="lblInterviewerName" runat="server" Text=""></asp:Label>
        <h3>Personal Information</h3>
        <strong><label>Name: </label></strong>
        <asp:Label ID="lblName" runat="server" Text=""></asp:Label>
        <br />
        <asp:Panel ID="pnlApplicantPersonalInfo" runat="server">
            <strong><label>Mobile Number: </label></strong>
            <asp:Label ID="lblApplicantMobileNumber" runat="server" Text=""></asp:Label>
            <br />
            <strong><label>Email Address: </label></strong>
            <asp:Label ID="lblApplicantEmailAddress" runat="server" Text=""></asp:Label>
            <br />
            <strong><label>Date of Birth: </label></strong>
            <asp:Label ID="lblApplicantDateOfBirth" runat="server" Text=""></asp:Label>
            <br />
            <strong><label>Place of Birth: </label></strong>
            <asp:Label ID="lblApplicantPlaceOfBirth" runat="server" Text=""></asp:Label>
            <br />
            <strong><label>Gender: </label></strong>
            <asp:Label ID="lblApplicantGender" runat="server" Text=""></asp:Label>
            <br />
            <strong><label>Civil Status: </label></strong>
            <asp:Label ID="lblApplicantCivilStatus" runat="server" Text=""></asp:Label>
            <br />
            <strong><label>Present Address: </label></strong>
            <asp:Label ID="lblApplicantPresentAddress" runat="server" Text=""></asp:Label>
            <br />
            <strong><label>Provincial Address: </label></strong>
            <asp:Label ID="lblApplicantProvincialAddress" runat="server" Text=""></asp:Label>
            <br />
            <strong><label>Height: </label></strong>
            <asp:Label ID="lblApplicantHeight" runat="server" Text=""></asp:Label>
            <br />
            <strong><label>Weight: </label></strong>
            <asp:Label ID="lblApplicantWeight" runat="server" Text=""></asp:Label>
            <br />
            <strong><label>Blood Type: </label></strong>
            <asp:Label ID="lblApplicantBloodType" runat="server" Text=""></asp:Label>
        </asp:Panel>
        <asp:Panel ID="pnlPreboardingPersonalInfo" runat="server">
            <strong><label>Date of Birth: </label></strong>
            <asp:Label ID="lblDateOfBirth" runat="server" Text=""></asp:Label>
            <br />
            <strong><label>Place of Birth: </label></strong>
            <asp:Label ID="lblPlaceOfBirth" runat="server" Text=""></asp:Label>
            <br />
            <strong><label>Nationality: </label></strong>
            <asp:Label ID="lblNationality" runat="server" Text=""></asp:Label>
            <br />
            <strong><label>Gender: </label></strong>
            <asp:Label ID="lblGender" runat="server" Text=""></asp:Label>
            <br />
            <strong><label>Civil Status: </label></strong>
            <asp:Label ID="lblCivilStatus" runat="server" Text=""></asp:Label>
            <br />
            <strong><label>Religion: </label></strong>
            <asp:Label ID="lblReligion" runat="server" Text=""></asp:Label>
            <br />
            <strong><label>Height: </label></strong>
            <asp:Label ID="lblHeight" runat="server" Text=""></asp:Label>
            <br />
            <strong><label>Weight: </label></strong>
            <asp:Label ID="lblWeight" runat="server" Text=""></asp:Label>
            <br />
            <strong><label>Blood Type: </label></strong>
            <asp:Label ID="lblBloodType" runat="server" Text=""></asp:Label>
            <br />
            <strong><label>SSS Number: </label></strong>
            <asp:Label ID="lblSSS" runat="server" Text=""></asp:Label>
            <br />
            <strong><label>TIN Number: </label></strong>
            <asp:Label ID="lblTIN" runat="server" Text=""></asp:Label>
            <br />
            <strong><label>Addresses</label></strong>
            <br />
            <label>Present: </label>
            <asp:Label ID="lblPresentAddress" runat="server" Text=""></asp:Label>
            <br />
            <label>Permanent: </label>
            <asp:Label ID="lblPermanentAddress" runat="server" Text=""></asp:Label>
            <br />
            <label>Provincial: </label>
            <asp:Label ID="lblProvincialAddress" runat="server" Text=""></asp:Label>
            <br />
            <strong><label>Contact Numbers</label></strong>
            <br />
            <label>Home Telephone: </label>
            <asp:Label ID="lblHomeNumber" runat="server" Text=""></asp:Label>
            <br />
            <label>Provincial Telephone: </label>
            <asp:Label ID="lblProvincialNumber" runat="server" Text=""></asp:Label>
            <br />
            <label>Additional: </label>
            <asp:Label ID="lblAdditionalNumber" runat="server" Text=""></asp:Label>
            <br />
            <label>Mobile: </label>
            <asp:Label ID="lblMobileNumber" runat="server" Text=""></asp:Label>
            <br />
            <label>Fax: </label>
            <asp:Label ID="lblFaxNumber" runat="server" Text=""></asp:Label>
            <br />
            <label>Email Address: </label>
            <asp:Label ID="lblEmailAddress" runat="server" Text=""></asp:Label>
        </asp:Panel>
        <asp:Panel ID="pnlPreboardingEducationalBackground" runat="server">
            <h3>Educational Background</h3>
            <asp:GridView ID="gvEducations" runat="server" AutoGenerateColumns="false" 
                DataKeyNames="file2_id">
                <Columns>
                    <asp:BoundField HeaderText="file2_id" DataField="file2_id" ReadOnly="true"
                        Visible="False"/>
                    <asp:TemplateField HeaderText="Education Establishment">
                        <ItemTemplate>
                            <%# Eval("education_establishment") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="School">
                        <ItemTemplate>
                            <%# Eval("school") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="School Others">
                        <ItemTemplate>
                            <%# Eval("school_others") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Start Date">
                        <ItemTemplate>
                            <%# Convert.ToDateTime(Eval("start_date")).ToString("d")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="End Date">
                        <ItemTemplate>
                            <%# Convert.ToDateTime(Eval("end_date")).ToString("d")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Branch of Study 1">
                        <ItemTemplate>
                            <%# Eval("branch_of_study_1") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Certificate">
                        <ItemTemplate>
                            <%# Eval("certificate") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Course Appraisal">
                        <ItemTemplate>
                            <%# Eval("course_appraisal") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Branch of Study 2">
                        <ItemTemplate>
                            <%# Eval("branch_of_study_2") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </asp:Panel>
        <h3>Employment History</h3>
        <asp:GridView ID="gvEmployers" runat="server" AutoGenerateColumns="false" 
            DataKeyNames="file3_id">
            <Columns>
                <asp:BoundField HeaderText="file3_id" DataField="file3_id" ReadOnly="true"
                    Visible="False"/>
                <asp:TemplateField HeaderText="Previous Employer">
                    <ItemTemplate>
                        <%# Eval("previous_employer") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="City">
                    <ItemTemplate>
                        <%# Eval("city") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Position Held">
                    <ItemTemplate>
                        <%# Eval("position_held") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Specialization">
                    <ItemTemplate>
                        <%# Eval("specialization") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Immediate Superior">
                    <ItemTemplate>
                        <%# Eval("immediate_superior") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Previous Salary">
                    <ItemTemplate>
                        <%# Eval("previous_salary") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Industry">
                    <ItemTemplate>
                        <%# Eval("industry") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Reason">
                    <ItemTemplate>
                        <%# Eval("reason") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Start Date">
                    <ItemTemplate>
                        <%# Convert.ToDateTime(Eval("start_date")).ToString("d")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="End Date">
                    <ItemTemplate>
                        <%# Convert.ToDateTime(Eval("end_date")).ToString("d")%>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <h3>Other Qualifications</h3>
        <asp:GridView ID="gvOtherQualifications" runat="server" AutoGenerateColumns="false"
            DataKeyNames="other_qualification_id">
            <Columns>
                <asp:BoundField HeaderText="other_qualification_id"
                    DataField="other_qualification_id" ReadOnly="true" Visible="false" />
                <asp:TemplateField HeaderText="License">
                    <ItemTemplate>
                        <%# Eval("license") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="License Others">
                    <ItemTemplate>
                        <%# Eval("others_name") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Number">
                    <ItemTemplate>
                        <%# Eval("number") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Rating">
                    <ItemTemplate>
                        <%# Eval("rating") %>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
    </asp:GridView>
        <br />
        <asp:Label ID="lblQuestion1" runat="server" Text=""></asp:Label>
        <strong>
            <asp:Label ID="lblAnswer1" runat="server" Text=""></asp:Label>
        </strong>
        <br />
        <asp:Label ID="lblQuestion2" runat="server" Text=""></asp:Label>
        <strong>
            <asp:Label ID="lblAnswer2" runat="server" Text=""></asp:Label>
        </strong>
        <br />
        <asp:Label ID="lblQuestion3" runat="server" Text=""></asp:Label>
        <strong>
            <asp:Label ID="lblAnswer3" runat="server" Text=""></asp:Label>
        </strong>
        <br />
        <asp:Label ID="lblQuestion4" runat="server" Text=""></asp:Label>
        <strong>
            <asp:Label ID="lblAnswer4" runat="server" Text=""></asp:Label>
        </strong>
        <br />
        <asp:Label ID="lblQuestion5" runat="server" Text=""></asp:Label>
        <strong>
            <asp:Label ID="lblAnswer5" runat="server" Text=""></asp:Label>
        </strong>
        <br />
        <asp:Label ID="lblQuestion6" runat="server" Text=""></asp:Label>
        <strong>
            <asp:Label ID="lblAnswer6" runat="server" Text=""></asp:Label>
        </strong>
        <br />
        <asp:Label ID="lblQuestion7" runat="server" Text=""></asp:Label>
        <strong>
            <asp:Label ID="lblAnswer7" runat="server" Text=""></asp:Label>
        </strong>
        <br />
        <asp:Panel ID="pnlPreboardingOrganizationalMemberhip" runat="server">
            <h3>Organizational Membership</h3>
            <asp:GridView ID="gvOrganizations" runat="server" AutoGenerateColumns="false" 
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
        </asp:Panel>
        <h3>Family Members</h3>
        <asp:GridView ID="gvDependents" runat="server" AutoGenerateColumns="false"
            DataKeyNames="file5_id">
            <Columns>
                <asp:BoundField HeaderText="file5_id" DataField="file5_id" ReadOnly="true"
                    Visible="False"/>
                <asp:TemplateField HeaderText="Dependent's Name">
                    <ItemTemplate>
                        <%# Eval("dependents_name") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Relationship">
                    <ItemTemplate>
                        <%# Eval("family_member") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Gender">
                    <ItemTemplate>
                        <%# Eval("gender") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Birthdate">
                    <ItemTemplate>
                        <%# Convert.ToDateTime(Eval("birthdate")).ToString("d") %>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <h3>Professional References</h3>
        <asp:GridView ID="gvProfessionalReferences" runat="server" AutoGenerateColumns="false" 
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
        <h3>Personal References</h3>
        <asp:GridView ID="gvPersonalReferences" runat="server" AutoGenerateColumns="false"
            DataKeyNames="personal_reference_id">
            <Columns>
                <asp:BoundField HeaderText="personal_reference_id"
                    DataField="personal_reference_id" ReadOnly="true" Visible="false" />
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
                <asp:TemplateField HeaderText="Relationship">
                    <ItemTemplate>
                        <%# Eval("relationship") %>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    </form>
</body>
</html>
