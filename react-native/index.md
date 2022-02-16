# React - Native
React Native is an open-source UI software framework created by Meta Platforms, Inc. It is used to develop applications for Android, Android TV, iOS, macOS, tvOS, Web, Windows and UWP by enabling developers to use the React framework along with native platform capabilities. It is also being used to develop virtual reality applications at Oculus.

## Setup
https://reactnative.dev/docs/environment-setup

## Crear proyecto TS
npx react-native init <project name> --template react-native-template-typescript

## Errores
Error de metro
Unable to load script. Make sure you're either running metro

Solución
Ejecutar agregando parametro variant: npx react-native run-android --variant=release

https://stackoverflow.com/questions/61827240/unable-to-load-script-make-sure-youre-either-running-a-metro-server-or-that-yo