#Windows Azure ACS Management

[Download on NuGet] (http://nuget.org/List/Packages/WindowsAzure.ACS.Management "Windows Azure ACS Management on NuGet")

##Introduction

This Project provides some management functionality around the Windows Azure AppFabric Access Control Service.

Currently, the only feature is a Wrapper of Home Realm Discovery ([Documented] (http://msdn.microsoft.com/en-us/library/gg185963.aspx "Login Pages and Home Realm Discovery"))

[Find out more about Access Control] (http://www.microsoft.com/windowsazure/features/accesscontrol/ "Windows Azure Access Control")

##Requirements

*.Net 4.0

##How to Use

###Install via NuGet

PM> Install-Package WindowsAzure.ACS.Management

###Retrieve Home Realms in Code

```csharp
public ActionResult LogOn()
{
var manager = new ACSServiceManager("<ACS Namespace Name>", "<Realm Uri>");
IIdentityProvider[] providers = manager.GetIdentityProviders<IdentityProvider>();
return View(providers);
}

```
###Create a Logon Control

```csharp
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<SyntaxC4.WindowsAzure.ACSManagement.IIdentityProvider>>" %>
<ul class="login">
<% foreach (var ip in Model)
{ %>
<li class="login-item">
<object data="<%: ip.ImageUrl %>" onclick="javascript:location.href='<%: ip.LoginUrl %>'">
<a href="<%: ip.LoginUrl %>" class="login-item-link"><%: ip.Name %></a>
</object>
</li>
<% } %>
</ul>

```

###Setup Claims Aware ASP.NET Site

Review this [blog post] (http://blogs.objectsharp.com/cs/blogs/steve/archive/2010/08/03/making-an-asp-net-website-claims-aware-with-the-windows-identity-foundation.aspx "Making ASP.NET Website Claims Aware with Windows Identity Foundation.")

###License

Copyright (c) 2011 Cory Fowler
Published under the MIT License, see LICENSE.