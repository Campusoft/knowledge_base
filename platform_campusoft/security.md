# Security


## CSRF

Abp - Cross-Site Request Forgery (CSRF)

jQuery

The abp.jquery.js script defines an AJAX interceptor which adds the anti-forgery token to the request header for every request. It gets the token from the abp.security.antiForgery.getToken() JavaScript function. Use .ajaxSend  https://api.jquery.com/ajaxSend/

```
	$(document).ajaxSend(function (event, request, settings) {
        return abp.ajax.ajaxSendHandler(event, request, settings);
    });
	
```

https://aspnetboilerplate.com/Pages/Documents/XSRF-CSRF-Protection?searchKey=XSRF