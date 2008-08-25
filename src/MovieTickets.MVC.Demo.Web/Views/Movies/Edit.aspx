<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="MovieTickets.MVC.Demo.Web.Views.Movies.Show" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<% Movie movie = ViewData.Model.Movie; %>
<h2>Editing Movie: <%= movie.Name %></h2>
<% using(Html.Form<MoviesController>(x => x.Update(movie.Id))) {%>
    <%= Html.RenderUserControl("~/Views/Movies/MovieForm.ascx", ViewData.Model) %>
<%}%>
</asp:Content>
