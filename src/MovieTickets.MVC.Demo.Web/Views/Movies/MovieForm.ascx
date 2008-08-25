<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MovieForm.ascx.cs" Inherits="MovieTickets.MVC.Demo.Web.Views.Movies.MovieForm" %>
<% Movie movie = ViewData.Model.Movie; %>
    <div id="movie-form">
        <p>
            <label for="movie.name">Name:</label><%= Html.TextBox("movie.name") %>
        </p>
        <p>
            <label for="movie.releasedate">Release Date:</label><%= Html.TextBox("movie.releasedate") %>
        </p>
        <p>
            <label for="movie.runtime">Runtime:</label><%= Html.TextBox("movie.runtime") %>
        </p>
        <p>
            <label for="movie.rating">Rated:</label><%= Html.DropDownList("movie.rating", AppHelper.SelectListFor(Movie.Ratings))%>
        </p>
        <p>
            <label for="movie.ratingdescription">Rating Description:</label><%= Html.TextBox("movie.ratingdescription", new { size=100 }) %>
        </p>
        <p>
            <label for="movie.description">Description:</label><%= Html.TextArea("movie.description", movie.Description, 15, 91)%>
        </p>
        <p>
            <label for="movie.posterthumbnailurl">Poster Thumbnail URL:</label><%= Html.TextBox("movie.posterthumbnailurl", new {size=50}) %>
        </p>
        <p>
            <input type="submit" id="save" name="save" value="Save" />
        </p>
    </div>  
