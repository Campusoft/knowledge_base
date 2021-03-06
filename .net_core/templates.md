
# Scriban

Scriban is a fast, powerful, safe and lightweight scripting language and engine for .NET, which was primarily developed for text templating with a compatibility mode for parsing liquid templates.


Liquid Support
Scriban supports all the core liquid syntax types, operators, tags and filters.
https://github.com/scriban/scriban/blob/master/doc/liquid-support.md


A template loader is responsible for providing the text template from an include directive
https://github.com/scriban/scriban/blob/master/doc/runtime.md#include-and-itemplateloader


Rendering a template
In order to render a template, you need pass a context for the variables, objects, functions that will be accessed by the template.

A stack of ScriptObject that provides the actual variables/functions accessible to the template, accessible through Template.PushGlobal(scriptObj) and Template.PopGlobal()

The ScriptObject is a special implementation of a Dictionary<string, object> that runtime properties and functions accessible to a template:



# RazorLight

Use Razor to build templates from Files / EmbeddedResources / Strings / Database or your custom source outside of ASP.NET MVC. No redundant dependencies and workarounds in pair with excellent performance and .NET Standard 2.0 and .NET Core 3.0 support.
https://github.com/toddams/RazorLight


Razor View Engine to create templates


# DotLiquid

DotLiquid is a .Net port of the popular Ruby Liquid templating language. It is a separate project that aims to retain the same template syntax as the original, while using .NET coding conventions where possible.

http://dotliquidmarkup.org/

# RazorEngine

 Open source templating engine based on Microsoft's Razor parsing engine 
https://github.com/Antaris/RazorEngine


# Referencias


Document templates in .NET
- RazorEngine
- RazorLight
- DotLiquid
	
https://jonjam.medium.com/document-templates-in-net-3dd2d48520e5