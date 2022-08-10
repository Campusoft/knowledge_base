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

It indicated once it is developed, we cannot modify or alter it unless they are initialized again.



## StatefulWidget

A Stateful Widget has a state, and it is dynamic. It indicates we can alter or change it simply across the lifecycle without reinitiating it again. 
 
## Layout widgets

Arrange other widgets columns, rows, grids, and many other layouts. 
https://docs.flutter.dev/development/ui/widgets/layout

All layout widgets have either of the following:
- A child property if they take a single child—for example, Center or Container
- A children property if they take a list of widgets—for example, Row, Column, ListView, or Stack.

Standard widgets

- Container: Adds padding, margins, borders, background color, or other decorations to a widget.
- GridView: Lays widgets out as a scrollable grid.
- ListView: Lays widgets out as a scrollable list.
- Stack: Overlaps a widget on top of another.

Material widgets

- Card: Organizes related info into a box with rounded corners and a drop shadow.
- ListTile: Organizes up to 3 lines of text, and optional leading and trailing icons, into a row


**ListView**

ListView, a column-like widget, automatically provides scrolling when its content is too long for its render box.

## Basic widgets

**Scaffold**

Scaffold is a class in flutter which provides many widgets or we can say APIs like Drawer, SnackBar, BottomNavigationBar, FloatingActionButton, AppBar, etc. Scaffold will expand or occupy the whole device screen.

**Container**

Container class in flutter is a convenience widget that combines common painting, positioning, and sizing of widgets. A Container class can be used to store one or more widgets and position them on the screen according to our convenience. Basically, a container is like a box to store contents. 

**MaterialApp**

MaterialApp Class: MaterialApp is a predefined class in a flutter. It is likely the main or core component of flutter. We can access all the other components and widgets provided by Flutter SDK. Text widget, Dropdownbutton widget, AppBar widget, Scaffold widget, ListView widget, StatelessWidget, StatefulWidget, IconButton widget, TextField widget, Padding widget, ThemeData widget, etc.

**Column**

A widget that displays its children in a vertical array.
https://api.flutter.dev/flutter/widgets/Column-class.html

## Other Widgets

**ClipRRect**

A widget that clips its child using a rounded rectangle.


## Routes and Navigator in Flutter

The apps display their content in a full-screen container called pages or screens. In flutter, the pages or screens are called Routes. In android, these pages/screens are referred to as Activity and in iOS, it is referred to as ViewController. But, in a flutter, routes are referred to as Widgets. In Flutter, a Page / Screen is called a Route.


- Typically, small applications are served well by just using the Navigator API, via the MaterialApp constructor’s MaterialApp.routes property.
- More elaborate applications are usually better served by the Router API, via the MaterialApp.router constructor. 


To switch to a new route, use the Navigator.push() method. The push() method adds a Route to the stack of routes managed by the Navigator. Where does the Route come from? You can create your own, or use a MaterialPageRoute, which is useful because it transitions to the new route using a platform-specific animation.

The pop() method removes the current Route from the stack of routes managed by the Navigator.

**Navigate with named routes**

if you need to navigate to the same screen in many parts of your app: The solution is to define a named route, and use the named route for navigation.

- To work with named routes, use the Navigator.pushNamed() function.
- The MaterialApp.initialRoute property defines which route the app should start with. 
- The MaterialApp.routes property defines the available named routes and the widgets to build when navigating to those routes

```
MaterialApp(
  title: 'Named Routes Demo',
  // Start the app with the "/" named route. In this case, the app starts
  // on the FirstScreen widget.
  initialRoute: '/',
  routes: {
    '/': (context) => const WidgetScreenFoo(),
    '/second': (context) => const WidgetScreenBar(),
  }
)
```

```
  // Navigate to the second screen using a named route.
  Navigator.pushNamed(context, '/second');
```


# Gestures

All physical form of interaction with a flutter application is done through pre-defined gestures.

Gestures are used to interact with an application. It is generally used in touch-based devices to physically interact with the application. It can be as simple as a single tap on the screen to a more complex physical interaction like swiping in a specific direction to scrolling down an application

The GestureDetector widget doesn’t have a visual representation but instead detects gestures made by the user. When the user taps the Container, the GestureDetector calls its onTap() callback, in this case printing a message to the console. You can use GestureDetector to detect a variety of input gestures, including taps, drags, and scales.

Many widgets use a GestureDetector to provide optional callbacks for other widgets. For example, the IconButton, ElevatedButton, and FloatingActionButton widgets have onPressed() callbacks that are triggered when the user taps the widget.

# Assets 

Flutter apps can include both code and assets (sometimes called resources). An asset is a file that is bundled and deployed with your app, and is accessible at runtime. Common types of assets include static data (for example, JSON files), configuration files, icons, and images (JPEG, WebP, GIF, animated WebP/GIF, PNG, BMP, and WBMP).

To add assets to your application, add an assets section in file pubspec.yaml

```
flutter:
  assets:
    - images/lake.jpg
```
# Networking - REST API

- FutureBuilder<T> class
- Future


Working with REST APIs — Flutter 
- Source Code
https://blog.codemagic.io/rest-api-in-flutter/

# Internationalizing 

Internationalizing Flutter apps
https://docs.flutter.dev/development/accessibility-and-localization/internationalization
 
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


The Ultimate Guide to App Development with Flutter 
- Learning Dart
- Learning Flutter UI
- Learning Firebase
- Connecting Firebase wth Flutter
- State Management
- Best Practices
- Challenge Project
https://github.com/antz22/ultimate-guide-to-flutter

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


**Flutter Gallery**
Flutter Gallery is a resource to help developers evaluate and use Flutter. It is a collection of Material Design & Cupertino widgets, behaviors, and vignettes implemented with Flutter. We often get asked how one can see Flutter in action, and this gallery demonstrates what Flutter provides and how it behaves in the wild.

https://github.com/flutter/gallery


**Best-Flutter-UI-Templates**
- hotel_booking
- fitness_app
- design_course 
- custom_drawer
- introduction_animation
completely free for everyone. Its build-in Flutter Dart.

https://github.com/mitesh77/Best-Flutter-UI-Templates


## Plugin

Flutter plugin that allows you to showcase your features on flutter application.
https://github.com/SimformSolutionsPvtLtd/flutter_showcaseview

## Application

E-Commerce Complate App - Flutter UI
https://github.com/abuanwar072/E-commerce-Complete-Flutter-UI