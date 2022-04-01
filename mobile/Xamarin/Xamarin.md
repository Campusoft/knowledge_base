# Xamarin

Xamarin es una herramienta utilizada para el desarrollo de aplicaciones multiplataforma que permite a los desarrolladores compartir alrededor del 90 por ciento del código entre las principales plataformas. Fue creada por los desarrolladores de Mono, otra plataforma de desarrollo de código abierto basada en .NET Framework. 

# API
* https://docs.microsoft.com/es-es/xamarin/
* https://docs.microsoft.com/en-us/dotnet/api/xamarin.forms?view=xamarin-forms

# Habilitar consumo de servicios rest locales
https://docs.microsoft.com/en-us/xamarin/cross-platform/deploy-test/connect-to-local-web-services
* Android consume desde la direccion local desde http://10.0.0.2:<port>, ej. http://10.0.2.2:5000
* Windows consume desde la direccion local desde http://localhost:<port>, ej. http://localhost:5000

# Generacion de objectos c# a partir de json
https://json2csharp.com/

# Almacenamiento local
* https://docs.microsoft.com/en-us/learn/modules/store-local-data-with-sqlite/
* https://docs.microsoft.com/es-es/xamarin/xamarin-forms/data-cloud/data/databases

# Laboratorios

**How to convert your Website to an android app using Xamarin**

- WebView

WebView is a view for displaying web and HTML content in your app:

Xamarin.Forms WebView
https://docs.microsoft.com/en-us/xamarin/xamarin-forms/user-interface/webview?tabs=windows

- Binding

- Open url external
Open Xamarin Forms WebView links in external browser
https://adenearnshaw.com/open-xamarin-forms-webview-links-in-external-browser/

- Enlaces Telefonos. 
DeepLinking (Enlace profundo)?



**Splash screen -  Android**

Xamarin.Forms splash screen
https://docs.microsoft.com/en-us/xamarin/xamarin-forms/user-interface/splashscreen

Guidelines for Splash Screen (Dimensiones de las pantatllas para diferentes plataformas)
- Guidelines for Configuring Splash Screen for Android Applications
- Guidelines for Configuring Splash Screen for iOS Applications
- Guidelines for Configuring Splash Screen for Windows Applications
https://docs.kony.com/konylibrary/visualizer/app_design_dev/Content/Guidelines_for_Splash_Screen.htm




# Behaviors 

Behaviors for Xamarin.Forms 
https://github.com/davidbritch/behaviors


# Errores

ERR_CLEARTEXT_NOT_PERMITTED
http://xybernetics.com/techtalk/how-to-resolve-err_cleartext_not_permitted-error-in-xamarin/

# Referencias


Set Up Device for Development
https://docs.microsoft.com/en-us/xamarin/android/get-started/installation/set-up-device-for-development

Error en Xiaomi

[INSTALL_FAILED_USER_RESTRICTED: Install canceled by user]

In some Xiaomi devices “Install via USB” option in “developer options” cannot be turned on with a message popping:”this device is temporarily restricted”. By default, this option will be turned off to ensure the security of the device.

Updating Icon and Name (Android)

-    Replace the png's in mipmap folders with your new icon
-    If name was changed, update Icon value in MainActivity.cs
-    If name was changed, update name of (or create a new copy of) icon.xml and icon_round.xml
-    If name of launcher-foreground.png was changed then update value in icon.xml. Eg:
```
    <adaptive-icon xmlns:android="http://schemas.android.com/apk/res/android">
      <background android:drawable="@color/my_launcher_background"/>
      <foreground android:drawable="@mipmap/my_launcher_foreground"/>
    </adaptive-icon>
```
    If name wasn't changed and you've cleaned and re-built project but still your new icons are not deploying: Delete obj folder from Android project directory

https://stackoverflow.com/questions/37945767/how-to-change-application-icon-in-xamarin-forms

How to change Android application name in Xamarin.Forms?
- Opcion 1: Go to your MainActivity and change the Label property:
- Opcion 2: Or remove the Label of the MainActivity and add the name in the Manifest.xml via UI or code:
https://stackoverflow.com/questions/44613974/how-to-change-android-application-name-in-xamarin-forms
https://stackoverflow.com/questions/48337869/how-can-i-change-the-application-name-in-xamarin-android

## Herramientas

AirDroid 

Transfiere archivos a través de dispositivos, controla dispositivos Android en remoto, duplica la pantalla y gestiona los SMS y las notificaciones en el ordenador.   hace que el trabajo y la vida sean más eficientes. 

Iconos
Generar iconos
https://appicon.co/





## Aplicaciones / Codigo / Librerias


Librerias de App/UI. Listado de aplicaciones realizadas en Xamarin. 
https://github.com/jsuarezruiz/xamarin-forms-goodlooking-UI

## Publicar a Google Play

Publish smaller apps with the Android App Bundle
https://devblogs.microsoft.com/xamarin/android-app-bundle/


Errores

- No se firmó el Android App Bundle.


You have to upload AAB (Android app bundle).

Starting in August 2021, developers wanting to publish new Android apps on the Google Play Store will have to change how those apps are packaged together: Rather than the traditional APK (Android application package) format that has been in place for years, software makers are going to be required to use the AAB (Android app bundle) framework instead.


## Varios

How to Release Your Xamarin App for iOS and Android
https://instabug.com/blog/how-to-release-xamarin-app-ios-and-android/


