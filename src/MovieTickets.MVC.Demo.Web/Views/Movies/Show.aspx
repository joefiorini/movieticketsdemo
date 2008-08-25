<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="Show.aspx.cs" Inherits="MovieTickets.MVC.Demo.Web.Views.Movies.Show" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<% Movie movie = ViewData.Model.Movie; %>
    <h2 class="movie-name"><%= movie.Name %></h2>
    <%= Html.RouteLink("[edit]", "edit-movie", new {movie.Id}) %>
    <%= Html.RouteLink("[delete]", "delete-movie", new {movie.Id}) %>
    <div id="MovieBasicData">
        <p><%= Html.Image(movie.PosterThumbnailUrl, movie.Name, new { width = 150, height = 255 }) %></p>
        <p><strong>Rated:</strong> <%= movie.Rating %></p>
        <p>for <%= movie.RatingDescription %></p>
    </div>
    <div id="MovieDetailedData">
        <p><%= movie.Description %></p>
    </div>
    <div class="clear"></div>
</asp:Content>
