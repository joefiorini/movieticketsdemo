<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="New.aspx.cs" Inherits="MovieTickets.MVC.Demo.Web.Views.Movies.New" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <% Movie movie = ViewData.Model.Movie; %>
    <h2>Adding a New Movie</h2>
    <% using(Html.Form<MoviesController>(x => x.Create())) {%>
        <%= Html.RenderUserControl("~/Views/Movies/MovieForm.ascx", ViewData.Model) %>
    <%}%>
</asp:Content>
