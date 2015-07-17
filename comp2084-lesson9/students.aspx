﻿<%@ Page Title="" Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" CodeBehind="students.aspx.cs" Inherits="comp2084_lesson9.students" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h2>Students</h2>

    <a href="studetDetails.aspx">Add Student</a>
    <asp:GridView runat="server" ID="grdStudents" CssClass="table table-striped table-hover" AutoGenerateColumns="false" OnRowDeleting="grdStudents_RowDeleting" DataKeyNames="StudentID">
        <Columns>
            <asp:BoundField DataField="StudentID" HeaderText="ID" />
            <asp:BoundField DataField="LastName" HeaderText="Last Name" />
            <asp:BoundField DataField="FirstMidName" HeaderText="First Name" />
            <asp:BoundField DataField="EnrollmentDate" HeaderText="Enrollment Date" DataFormatString="{0:MM-dd-yyyy}" />
            <asp:HyperLinkField HeaderText="Edit" DataNavigateUrlFields="StudentID" Text="Edit" DataNavigateUrlFormatString="studetDetails.aspx?studentId={0}" />
            <asp:CommandField ShowDeleteButton="true" DeleteText="Delete" HeaderText="Delete" />
        </Columns>
    </asp:GridView>

</asp:Content>
