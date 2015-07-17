<%@ Page Title="" Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" CodeBehind="courseDetails.aspx.cs" Inherits="comp2084_lesson9.courseDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Course Details</h1>

    <div class="form-group">
        <label for="txtTitle" class="col-sm-2">Course Title: </label>
        <asp:TextBox ID="txtTitle" runat="server" required MaxLength="100" />
    </div>

    <div class="form-group">
        <label for="txtCredits" class="col-sm-2">Credits: </label>
        <asp:TextBox ID="txtCredits" runat="server" required MaxLength="100" type="number" />
        <asp:RangeValidator runat="server" ControlToValidate="txtCredits" MinimumValue="1" MaximumValue="10000" Type="Integer" ErrorMessage="Required" CssClass="label label-danger" />
    </div>

    <div class="form-group">
        <label for="ddlDepartment" class="col-sm-2">Departments</label>
        <asp:DropDownList runat="server" ID="ddlDepartments" DataTextField="Name" DataValueField="DepartmentID" >

        </asp:DropDownList>
    </div>
    <div class="form-group">
        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnSave_Click" />
        <asp:RangeValidator runat="server" ControlToValidate="ddlDepartments" Type="Integer" MinimumValue="1" MaximumValue="1000" ErrorMessage="Required" CssClass="lable label-danger" />
    </div>

    <asp:Panel ID="pnlEnrollments" runat="server">
        <h2>Student Enrollment</h2>
        <asp:GridView ID="grdEnrollments" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-hover">
            <Columns>
                <asp:BoundField DataField="Student.LastName" HeaderText="Last Name" />
                <asp:BoundField DataField="Student.FirstMidName" HeaderText="First Name" />
                <asp:BoundField DataField="Grade" HeaderText="grade" />

            </Columns>
        </asp:GridView>
    </asp:Panel>
</asp:Content>
