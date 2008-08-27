<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="ZipCode.aspx.cs" Inherits="MovieTickets.MVC.Demo.Web.Views.Theaters.ZipCode" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<% List<Theater> theaters = ViewData.Model.Theaters; %>
<h2>Theaters in the <%= ViewData["zip_code"] %> zip code.</h2>
<ol id="theaters">
    <% foreach (var theater in theaters) { %>       
    <li>
        <img src="<%= theater.ImageUrl %>" alt="<%= theater.Name %>" />
        <p><%= theater.Name %></p>
        <p>
            <%= theater.Address %>
            <%= theater.City %>, <%= theater.State %> <%= theater.Zip %>
        </p>
    </li>
   <%}%>
</ol>
<div class="clear"></div>
</asp:Content>
