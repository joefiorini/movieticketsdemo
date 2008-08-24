<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Views/Shared/Site.Master" CodeBehind="Index.aspx.cs" Inherits="MovieTickets.MVC.Demo.Web.Views.Movies.Index" %>
<asp:Content ContentPlaceHolderID="MainContent" ID="Content" runat="server">
    <table>
    <% foreach (var movie in ViewData.Model.Movies) { %>
    <tr>
        <td><%= movie.Name %></td>
    </tr>    
    <% } %>
    </table>        
</asp:Content>