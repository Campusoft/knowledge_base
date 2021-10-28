# Xamarin.iOS


Launch Screens for Xamarin.iOS Apps
- Add an image to the Assets.xcassets Asset Catalog so that it is available for use on the Launch Screen. 
https://docs.microsoft.com/en-us/xamarin/ios/app-fundamentals/images-icons/launch-screens?tabs=windows

 App Store icons now must be included as part of your project bundle and added within an asset catalog. Apps that do not contain an App Store icon will be rejected by Apple.
https://docs.microsoft.com/en-us/xamarin/ios/app-fundamentals/images-icons/app-store-icon

# Laboratorios

***app icon***

iPhone Icon Sizes
https://docs.microsoft.com/en-us/xamarin/ios/app-fundamentals/images-icons/app-icons?tabs=windows#iphone-icon-sizes

Generator Icon
https://appicon.co/


***launch-screens***


```
/Users/marie/Documents/aplicacion lulapp/lulapp-main/src/xamarin/lulapp/lulapp/lulapp.iOS/MTOUCH: Warning MT0178: Debugging symbol file for '/Users/marie/.nuget/packages/xam.plugin.connectivity/3.2.0/lib/Xamarin.iOS10/Plugin.Connectivity.dll' is not valid and was ignored. (MT0178) (lulapp.iOS)
```

***Debug en iphone***

/Library/Frameworks/Mono.framework/External/xbuild/Xamarin/iOS/Xamarin.Shared.targets(1056,3): error : No se encontraron claves de firma de código de iOS válidas en la cadena de claves. Debe solicitar un certificado de firma de código de https://developer.apple.com.
/Library/Frameworks/Mono.framework/External/xbuild/Xamarin/iOS/Xamarin.Shared.targets(1056,3): error :         
    0 Advertencia(s)
    1 Errores

Tiempo transcurrido 00:00:00.83

========== Compilar: correctos 1; incorrectos 1; actualizados 0; omitidos 0 ==========

Compilación:1 errores, 0 advertencias

Solucion:

Firma con código
https://developer.apple.com/es/support/code-signing/

Why does my iOS build fail with: no valid iPhone code signing keys found in keychain?
https://docs.microsoft.com/en-us/xamarin/ios/troubleshooting/questions/no-codesigning-keys



https://docs.microsoft.com/en-us/xamarin/ios/get-started/installation/device-provisioning/free-provisioning?tabs=macos