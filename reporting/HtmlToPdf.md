## Name
Select.HtmlToPdf.NetCore (nuget.org)

## Description
SelectPdf Html To Pdf Converter for .NET Core - Community Edition is the free version of the powerful html to pdf converter available in SelectPdf Library for .NET Core.

The converter offers a lot of powerful options (convert any web page to pdf, convert any html string to pdf, html5/css3/javascript support, headers and footers support, etc) and the only limitation is that it can generate pdf documents up to 5 pages long.

Free Html to Pdf Converter For .NET – Community Edition Features: Generate pdf documents up to 5 pages, Convert any web page to pdf, Convert any raw html string to pdf, Set pdf page settings (page size, page orientation, page margins), Resize content during conversion to fit the pdf page, Set pdf document properties, Set pdf viewer preferences, Set pdf security (passwords, permissions), Set conversion delay and web page navigation timeout, Custom headers and footers, Support for html in headers and footers, Automatic and manual page breaks, Repeat html table headers on each page, Support for @media types screen and print, Support for internal and external links, Generate bookmarks automatically based on html elements, Support for HTTP headers, Support for HTTP cookies, Support for web pages that require authentication, Support for proxy servers, Enable/disable javascript, Modify color space, Multithreading support, HTML5/CSS3 support, Web fonts support and many more.

## Project url
https://selectpdf.com/community-edition/

## Errores con despliegue en azure
El problema se presenta por el plan de azure, selecthtml no funciona ni con el plan basic ni free, se debe pasar a un b1 para que funcione de forma correcta las conversiones html a pdf.

## Definir margenes para el documento
Lo hacemos fijando al objeto converter.Options los valores deseados

```
public static byte[] PdfSharpConvert(string html, int marginBottom, int marginTop, int marginLeft, int marginRight)
{
    HtmlToPdf converter = new HtmlToPdf();
    converter.Options.MarginBottom = marginBottom;
    converter.Options.MarginTop = marginTop;
    converter.Options.MarginLeft = marginLeft;
    converter.Options.MarginRight = marginRight;
    PdfDocument doc = converter.ConvertHtmlString(html);
    return doc.Save();
}
```

