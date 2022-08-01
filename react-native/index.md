# React - Native
React Native is an open-source UI software framework created by Meta Platforms, Inc. It is used to develop applications for Android, Android TV, iOS, macOS, tvOS, Web, Windows and UWP by enabling developers to use the React framework along with native platform capabilities. It is also being used to develop virtual reality applications at Oculus.

# Setup
https://reactnative.dev/docs/environment-setup

# Crear proyecto TS
npx react-native init <project name> --template react-native-template-typescript

# Errores
Error de metro
Unable to load script. Make sure you're either running metro

## Solución 1
Ejecutar agregando parametro port: npx react-native run-android --port=1234

## Solución 2
Ejecutar agregando parametro variant: npx react-native run-android --variant=release

https://stackoverflow.com/questions/61827240/unable-to-load-script-make-sure-youre-either-running-a-metro-server-or-that-yo

# Navegación
https://reactnavigation.org/docs/getting-started

## SOLUCIÓN Para react-native-reanimated
### Crear proyecto
npx react-native init NavegacionApp --template react-native-template-typescript

### Instalar dependencias de navigation
npm install @react-navigation/native
npm install react-native-screens react-native-safe-area-context
npm install @react-navigation/stack
npm install react-native-gesture-handler react-native-reanimated
npm install @react-native-masked-view/masked-view
npm install @react-navigation/drawer

### Configurar reanimated y otros
#### Para reanimated:
Add Reanimated's babel plugin to your babel.config.js:
```
module.exports = {
presets: ['module:metro-react-native-babel-preset'],
plugins: [
'react-native-reanimated/plugin',
    ],
};
```

#### Turn on Hermes engine by editing android/app/build.gradle:
```
project.ext.react = [
enableHermes: true,
]
```

Edit MainApplication.java file which is located in android/app/src/main/java/<your package name>/ MainApplication.java.

Plug Reanimated in MainApplication.java:

```
import com.facebook.react.bridge.JSIModulePackage; // <- add
import com.swmansion.reanimated.ReanimatedJSIModulePackage; // <- add
```

pegar debajo del

```
@Override
protected String getJSMainModuleName() {
return "index";
}
```
Lo siguiente:

```
@Override
protected JSIModulePackage getJSIModulePackage() {
return new ReanimatedJSIModulePackage(); // <- add
}

```
Correr esto para limpiar cache:

```
npx react-native start --reset-cache
```
luego control c y arrancar de nuevo con:

```
npx react-native run-android
```
Más info: https://docs.swmansion.com/react-native-reanimated/docs/fundamentals/installation

Para native-screens (Un plus por si acaso que vi en la documentación oficial):

react-native-screens package requires one additional configuration step to properly work on Android devices. Edit MainActivity.java file which is located in android/app/src/main/java/<your package name>/MainActivity.java.

Add the following code to the body of MainActivity class:

```
@Override
protected void onCreate(Bundle savedInstanceState) {
super.onCreate(null);
}
```
and make sure to add an import statement at the top of this file:

```
import android.os.Bundle;
```

Si reanimated sigue sin funcionar entonces:

funciona pero con warnings:
```
npm remove react-native-reanimated
npm install react-native-reanimated@2.2.4 
```

# Tools 

Expo is a set of tools built around React Native to help you quickly start an app and, while it has many features.