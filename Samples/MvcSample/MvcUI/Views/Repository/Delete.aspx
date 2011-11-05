<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<GithubSharp.MvcSample.MvcApplication.Models.ViewModels.BaseViewModel<System.String>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Delete Repository
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Delete Repository -
        <%= Model.ModelParameter %></h2>
    <div class="notice">
        Are you sure you want to delete '<%= Model.ModelParameter %>'?</div>
    <% using (Html.BeginForm())
       { %>
    <%--= Html.AntiForgeryToken() Doesn't work on mono :( --%>
  <%= Html.Hidden("RepositoryName", Model.ModelParameter )%>
    <%= Html.CheckBox("Delete") %>
    <button type="submit">
        Continue</button>
    <%} %>
    <%= Html.ActionLink("Get me out of here!", "Index") %>
</asp:Content>
