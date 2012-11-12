Ext.Direct for ASP.NET MVC
==========================

Ext.Direct.Mvc is an implementation of Ext Direct server-side stack for ASP.NET MVC. Ext Direct is a platform and language agnostic technology to remote server-side methods to the client-side. Ext Direct allows for seamless communication between the client-side of an Ext JS application and all popular server platforms. For more information about Ext Direct visit http://www.sencha.com/products/extjs/extdirect.

Key features
------------

*  Easy setup
*  Support for different types of parameters - simple types, complex types and arrays
*  Form post values can be bound to multiple simple type parameters, a single complex type parameter (object) or a mix of both on the server
*  Support for method aliases
*  Exceptions with full stack trace and additional user-defined data for easy debugging
*  Support for custom server-side events
*  Support for named arguments (Ext JS 4.x)

Official thread on Sencha forums
--------------------------------

http://www.sencha.com/forum/showthread.php?72245-Ext.Direct-for-ASP.NET-MVC

Compiling the source
--------------------

You need to open the solution in Visual Studio 2012 and allow NuGet to download missing packages before attempting to build it.
To do that go to the Options > Package Manager > General and check "Allow NuGet to download missing packages during build".

Quick Start
-----------

Here's how to quickly start using Ext.Direct.Mvc in your project:

In your ASP.NET MVC project add a reference to Ext.Direct.Mvc dll and the Newtonsoft.Json dll that comes with it. It is important that you reference the included Newtonsoft.Json.dll file because Ext.Direct.Mvc is compiled against it.

Add a script tag to your main view, normally Views/Home/Index.cshtml, and set its scr attribute to "~/DirectApi" relative to the root of your application. It outputs the method configurations for Ext Direct to create client-side stubs.

```
<script type="text/javascript" src="@Url.Content("~/DirectApi")"></script>
```

Add an Ext.Direct Provider to creates the proxy, or stub methods, to execute server-side methods. Because most of the time you will need stub methods before defining custom components, this should be done early in your code:

```
// in Ext JS 4 or Sencha Touch 2
Ext.direct.Manager.addProvider(Ext.app.REMOTING_API);

// or in Ext JS 3
Ext.Direct.addProvider(Ext.app.REMOTING_API);
```

Make sure that controllers which are intended to be used by Ext Direct inherit **DirectController** as opposed to just Controller.

Return data from controller actions by calling one of the overriden **Json** methods. Actions that process form posts must be marked with `[FormHandler]` attribute.

That's it! Now you can call your controller actions directly from your
JavaScript code through the created stub methods.