<%@ Page Title="" Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" CodeBehind="courses.aspx.cs" Inherits="comp2084_lesson9.courses" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <a href="courseDetails.aspx">Add Course</a>
    <asp:GridView runat="server" ID="grdCourses" CssClass="table table-striped table-hover" AutoGenerateColumns="false" OnRowDeleting="grdCourses_RowDeleting" >
        <Columns>
            <asp:BoundField DataField="CourseID" HeaderText="ID" />
            <asp:BoundField DataField="Title" HeaderText="Title" />
            <asp:BoundField DataField="Credits" HeaderText="Credits" />
            <asp:BoundField DataField="Department.Name" HeaderText="Department" />
            <asp:HyperLinkField runat="server" HeaderText="Edit" DataNavigateUrlFields="CourseID" Text="Edit" DataNavigateUrlFormatString="courseDetails.aspx?CourseID={0}" />
            <asp:CommandField ShowDeleteButton="true" DeleteText="Delete" HeaderText="Delete" />
        </Columns>
    </asp:GridView>
</asp:Content>
