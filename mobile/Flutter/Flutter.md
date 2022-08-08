# Flutter


Flutter is an open source framework by Google for building beautiful, natively compiled, multi-platform applications from a single codebase.


Open source flutter apps
https://github.com/tortuvshin/open-source-flutter-apps

Flutter architecture application mainly consists of:

- Widgets
- Gestures
- Concept of State
- Layers
	


# Features

Flutter framework offers the following features to developers −

- Modern and reactive framework.
- Uses Dart programming language and it is very easy to learn.
- Fast development.
- Beautiful and fluid user interfaces.
- Huge widget catalog.
- Runs same UI for multiple platforms.
- High performance application.


# Widgets 

Flutter widgets are built using a modern framework that takes inspiration from React. The central idea is that you build your UI out of widgets. Widgets describe what their view should look like given their current configuration and state. When a widget’s state changes, the widget rebuilds its description, which the framework diffs against the previous description in order to determine the minimal changes needed in the underlying render tree to transition from one state to the next.

When writing an app, you’ll commonly author new widgets that are subclasses of either StatelessWidget or StatefulWidget, depending on whether your widget manages any state. 


The runApp() function takes the given Widget and makes it the root of the widget tree. 

```
import 'package:flutter/material.dart';

void main() {
  runApp(const HelloWord());
}

class HelloWord extends StatelessWidget {
  const HelloWord({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return const MaterialApp(
      home: Center(child: Text('Hello World')),
    );
  }
}
```

Build: It is a method that is responsible for drawing components on the Screen it takes a BuildContext as an argument that has information about which widget has to be displayed and in which order it has to be painted on the screen. 


## StatelessWidget

## StatefulWidget
 
## Layout widgets

Arrange other widgets columns, rows, grids, and many other layouts. 
https://docs.flutter.dev/development/ui/widgets/layout

## Basic widgets

**Scaffold**

Scaffold is a class in flutter which provides many widgets or we can say APIs like Drawer, SnackBar, BottomNavigationBar, FloatingActionButton, AppBar, etc. Scaffold will expand or occupy the whole device screen.

**Container**

Container class in flutter is a convenience widget that combines common painting, positioning, and sizing of widgets. A Container class can be used to store one or more widgets and position them on the screen according to our convenience. Basically, a container is like a box to store contents. 

**MaterialApp**

MaterialApp Class: MaterialApp is a predefined class in a flutter. It is likely the main or core component of flutter. We can access all the other components and widgets provided by Flutter SDK. Text widget, Dropdownbutton widget, AppBar widget, Scaffold widget, ListView widget, StatelessWidget, StatefulWidget, IconButton widget, TextField widget, Padding widget, ThemeData widget, etc.

## Routes and Navigator in Flutter

The apps display their content in a full-screen container called pages or screens. In flutter, the pages or screens are called Routes. In android, these pages/screens are referred to as Activity and in iOS, it is referred to as ViewController. But, in a flutter, routes are referred to as Widgets. In Flutter, a Page / Screen is called a Route.



# Gestures

All physical form of interaction with a flutter application is done through pre-defined gestures.

Gestures are used to interact with an application. It is generally used in touch-based devices to physically interact with the application. It can be as simple as a single tap on the screen to a more complex physical interaction like swiping in a specific direction to scrolling down an application


 
# Install

Install Flutter
https://docs.flutter.dev/get-started/install

Editor
- Android Studio
- VS Code
 


Add web support to an existing app
```
 flutter create .
```


Add desktop support to an existing Flutter app
```
flutter create --platforms=windows,macos,linux .
```


Warning: Hot reload is not supported in a web browser Currently, Flutter supports hot restart, but not hot reload in a web browser.
 
Errores
----------------
Flutter provides a command to update the Android SDK path:
flutter config --android-sdk <path-to-your-android-sdk-path>

----------------
```
[!] Android toolchain - develop for Android devices (Android SDK version 29.0.3)
    X cmdline-tools component is missing
      Run `path/to/sdkmanager --install "cmdline-tools;latest"`
```

Android Studio

- You must choose Android SDK in the left side of the Settings window. It’s under Appearance & Behavior → System Settings.
- Search for "SDK tools" in the search box.
- SDK Tools tab
- Check Android SDK Command Line Tools 
- Apply


# Training




100+ Flutter Examples 
- Code
- Video
https://afgprogrammer.com/flutter/


# Referencias

The Flutter geolocator plugin is build following the federated plugin architecture. A detailed explanation of the federated plugin concept can be found in the Flutter documentation. 

https://github.com/Baseflow/flutter-geolocator


Getting Started With Flutter
- In this tutorial, you’ll build a Flutter app called GHFlutter that queries the GitHub API for team members in a GitHub organization and displays the information in a scrollable list

https://www.raywenderlich.com/24499516-getting-started-with-flutter

## Tools

DartPad is a free, open-source online editor to help developers learn about Dart and Flutter. 
https://dartpad.dev


## Themes

Flutter Themes, Templates, Material Kit, UI/UX and App
Great Custom Design Themes/Templates for both Android and IOS, all at one place
https://www.flutter-themes.com/