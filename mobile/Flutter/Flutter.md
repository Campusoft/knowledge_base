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

Flutter state management for minimalists
- That’s where this article comes in. I’m going to introduce a way to manage your app state that doesn’t use any of the third-party state management solutions.
- the UI layer
- the state management layer. Some people call it a ViewModel. Others call it a Controller. Still others call it a Bloc or a Model or a Reducer or a ChangeNotifier. This state management layer is the place to perform logic based on UI events and then update the app state.
- and the service layer. which is sometimes called a data repository. That layer of protection allows you to swap out service implementations without breaking the rest of your app code.
https://suragch.medium.com/flutter-state-management-for-minimalists-4c71a2f2f0c1

## InheritedWidget 

Base class for widgets that efficiently propagate information down the tree.

In short and with simple words, the InheritedWidget allows to efficiently propagate (and share) information down a tree of widgets.

The InheritedWidget is a special Widget, that you put in the Widgets tree as a parent of another sub-tree. All widgets part of that sub-tree will have to ability to interact with the data which is exposed by that InheritedWidget.
 

Widget - State - Context - InheritedWidget
This article covers the important notions of Widget, State, Context and InheritedWidget in Flutter Applications. Special attention is paid on the InheritedWidget which is one of the most important and less documented widgets.
https://www.didierboelens.com/2018/06/widget-state-context-inheritedwidget/ 
 
 
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

Drawer. A material design panel that slides in horizontally from the edge of a Scaffold to show navigation links in an application.

BottomNavigationBar. 



**Container**

Container class in flutter is a convenience widget that combines common painting, positioning, and sizing of widgets. A Container class can be used to store one or more widgets and position them on the screen according to our convenience. Basically, a container is like a box to store contents. 

**MaterialApp**

MaterialApp Class: MaterialApp is a predefined class in a flutter. It is likely the main or core component of flutter. We can access all the other components and widgets provided by Flutter SDK. Text widget, Dropdownbutton widget, AppBar widget, Scaffold widget, ListView widget, StatelessWidget, StatefulWidget, IconButton widget, TextField widget, Padding widget, ThemeData widget, etc.

**Column**

A widget that displays its children in a vertical array.
https://api.flutter.dev/flutter/widgets/Column-class.html

**Rows**



Flutter Expanded and Flex (Expanded vs Flexible)
- Using an Expanded widget makes a child of a Row or Column (also for Flex) expand to fill the available space in the main axis ( horizontally for a Row or vertically for a Column)
https://medium.com/@apmntechdev/flutter-expanded-and-flex-cfd4e9f1e069


## NavigationRail and BottomNavigationBar

Using NavigationRail and BottomNavigationBar in Flutter 
- This article shows you how to create an adaptive layout in Flutter by using both NavigationRail and BottomNavigationBar 
https://www.kindacode.com/article/using-navigationrail-and-bottomnavigationbar-in-flutter/

## Forms

The Form widget acts as a container for grouping and validating multiple form fields.

Using a GlobalKey is the recommended way to access a form. However, if you have a more complex widget tree, you can use the Form.of() method to access the form within nested widgets.
 
 
Form Fields

- TextFormField. A FormField that contains a TextField.
  - keyboardType. The type of information for which to optimize the text input control. datetime, emailAddress, multiline, etc
- CheckboxListTile. A ListTile with a Checkbox. In other words, a checkbox with a label.

There are a couple ways to go about tracking text input form elements. You can use Form widgets, or you can track each text input separately.

**Form**

- _formKey.currentState.validate() method fires up the validators defined for every field inside the Form widget. 

- _formKey.currentState.save() that will run the save function inside all our inputs.


**TextEditingController**

A TextEditingController is basically a class that listens to its assigned TextField, and updates it's own internal state every time the text in the TextField changes.



Create and Handle Flutter Input Form like a Pro 
https://dev.to/erozonachi/create-and-handle-flutter-input-form-like-a-pro-2k9c

Handle changes to a text field
- Supply an onChanged() callback to a TextField or a TextFormField.
- Use a TextEditingController.
https://docs.flutter.dev/cookbook/forms/text-field-changes


Flutter TextField Validation: How to work with TextEditingController, Form, and TextFormField
https://codewithandrea.com/articles/flutter-text-field-form-validation/#1-flutter-textfield-validation-with-texteditingcontroller

## Other Widgets


**Image**

- Image.new, for obtaining an image from an ImageProvider.
- Image.asset, for obtaining an image from an AssetBundle using a key.
- Image.network, for obtaining an image from a URL.
- Image.file, for obtaining an image from a File.
- Image.memory, for obtaining an image from a Uint8List.


How to display a placeholder image when the image is loading.
```
FadeInImage.assetNetwork(
  placeholder: 'assets/images/placeholder.png', // Before image load
  image: 'https://picsum.photos/id/237/200/300', // After image load
  height: 200,
  width: 300,
)
``` 
 
How To Use Images In Flutter — To The Point
- A guide on showing how to work with images in Flutter.
https://medium.com/flutter-community/how-to-use-images-in-flutter-to-the-point-5542b4412a53 

Set Background Image in Flutter – The Right Way in 2022
- Steps to Set Background Image in Flutter
- Setting Background Image in Full Screen
- Preventing Image Resize
https://www.flutterbeads.com/set-background-image-in-flutter/

**WebView a tu app de Flutte**

Agrega WebView a tu app de Flutter
https://codelabs.developers.google.com/codelabs/flutter-webview#0

**Google Maps for Flutter**

Cómo agregar Google Maps a una app creada con Flutter
https://codelabs.developers.google.com/codelabs/google-maps-in-flutter#0

Package: Google Maps for Flutter 
https://pub.dev/packages/google_maps_flutter


**SafeArea**

SafeArea is an important and useful widget in Flutter which makes UI dynamic and adaptive to a wide variety of devices. While designing the layout of widgets, we consider different types of devices and their pre-occupied constraints of screen like status bar, notches, navigation bar, etc. 

It will also indent the child by the amount necessary to avoid The Notch on the iPhone X, or other similar creative physical features of the display.

**ClipRRect**

A widget that clips its child using a rounded rectangle.

**ClipPath**

Clippath is a widget, that has a property a clipper to define how it’s going to define the clipping path. Then it will create a shape that we can customize to the container or image.


Using ClipPath in Flutter
https://medium.com/@taufansyahrudin9/using-clippath-in-flutter-bfdbd06824f5


Custom Clipping using fluttershapemaker.com and ClipPath in Flutter
https://prasadsunny1.medium.com/custom-clipping-using-fluttershapemaker-com-and-clippath-in-flutter-953d3fdf54ce

ClipRRect & ClipPath In Flutter
https://medium.flutterdevs.com/cliprrect-clippath-in-flutter-4c41abe4e8
 



## Routes and Navigator in Flutter

The apps display their content in a full-screen container called pages or screens. In flutter, the pages or screens are called Routes. In android, these pages/screens are referred to as Activity and in iOS, it is referred to as ViewController. But, in a flutter, routes are referred to as Widgets. In Flutter, a Page / Screen is called a Route.


- Typically, small applications are served well by just using the Navigator API, via the MaterialApp constructor’s MaterialApp.routes property.
- More elaborate applications are usually better served by the Router API, via the MaterialApp.router constructor. 


To switch to a new route, use the Navigator.push() method. The push() method adds a Route to the stack of routes managed by the Navigator. Where does the Route come from? You can create your own, or use a MaterialPageRoute, which is useful because it transitions to the new route using a platform-specific animation.

The pop() method removes the current Route from the stack of routes managed by the Navigator.

**Navigate with named routes**

Flutter also supports named routes, which are defined in the routes parameter on MaterialApp or CupertinoApp:

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



# State management

Flutter is declarative. This means that Flutter builds its user interface to reflect the current state of your app:

List of state management approaches
- Provider
- Riverpod
- Redux
- BLoC / Rx. A family of stream/observable based patterns.
- GetIt
- MobX
https://docs.flutter.dev/development/data-and-backend/state-mgmt/options

## Provider package

- ChangeNotifier : the store of your state from which state is updated and widgets consuming the state are notified.
- ChangeNotifierProvider : this widget makes the ChangeNotifier accessible to underlying widgets in the tree.
- Consumer : a widget that listens to state changes and updates the UI accordingly.
	

Starting with Flutter: A simple guide for Provider
- we will build a simple app that will change its main color scheme using Provider.
https://medium.com/theotherdev-s/starting-with-flutter-a-simple-guide-for-provider-401b25957989

How To Manage State in Flutter With Provider
https://betterprogramming.pub/how-to-manage-state-in-flutter-with-provider-661ff322dd22

Using Provider for State Management in Flutter (2022) 
- Example: we will build a favorite movie list feature 
https://www.kindacode.com/article/using-provider-for-state-management-in-flutter/



Flutter Provider Http Get Request | Restful Api Example
https://www.dbestech.com/tutorials/flutter-provider-http-get-request-restful-api-example

## getx



# Gestures

All physical form of interaction with a flutter application is done through pre-defined gestures.

Gestures are used to interact with an application. It is generally used in touch-based devices to physically interact with the application. It can be as simple as a single tap on the screen to a more complex physical interaction like swiping in a specific direction to scrolling down an application

The GestureDetector widget doesn’t have a visual representation but instead detects gestures made by the user. When the user taps the Container, the GestureDetector calls its onTap() callback, in this case printing a message to the console. You can use GestureDetector to detect a variety of input gestures, including taps, drags, and scales.

Many widgets use a GestureDetector to provide optional callbacks for other widgets. For example, the IconButton, ElevatedButton, and FloatingActionButton widgets have onPressed() callbacks that are triggered when the user taps the widget.

Material design applications typically react to touches with ink splash effects. The InkWell class implements this effect and can be used in place of a GestureDetector for handling taps.

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


# Store Data Offline


5 Ways to Store Data Offline in Flutter 
- Using Text/CSV/JSON files
- SQLite
- Hive Database
- Shared Preferences Storage
- Objectbox
https://www.kindacode.com/article/ways-to-store-data-offline-in-flutter/

## Hive

hive_flutter. Extension for Hive. Make it easier to use Hive in Flutter apps.

## isar

Extremely fast, easy to use, and fully async NoSQL database for Flutter  
https://github.com/isar/isar


# Internationalizing 

Internationalizing Flutter apps
https://docs.flutter.dev/development/accessibility-and-localization/internationalization

Localization / Multi-Language In Flutter
https://medium.flutterdevs.com/localization-multi-language-in-flutter-5cedb6ff459b

# Theme 

Theming is the process of using a set of colors, fonts, shapes and design styles throughout your app. It’s a way to centralize all your stylistic decisions in one place.

**Use a custom font**
https://docs.flutter.dev/cookbook/design/fonts
 
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
----------------


Flutter: Building with plugins requires symlink support

Go to Windows settings. Select Update & Security. On that, select For Developers. In that window, there is an option called Install apps from any source, including loose files on the Developer Mode. Enable that option.

https://stackoverflow.com/questions/68089177/flutter-building-with-plugins-requires-symlink-support
# Command

Add a package as a direct dependency:
```
flutter pub add <package-name>
```

Add a package as a dev-dependency:

```
flutter pub add -d <package-name>
```

Remove a package:

```
flutter pub remove <package-name>
```




# Versions


# Authentication

**OAuth 2.0 and OpenID Connect**

Get Started with Flutter Authentication
-  flutter_appauth : A well-maintained wrapper package around AppAuth for Flutter 
- fluttersecurestorage: A library to securely persist data locally
https://auth0.com/blog/get-started-with-flutter-authentication/

# Labs

## List Pagination

***ScrollController***
***infinite_scroll_pagination***


# Referencias

The Flutter geolocator plugin is build following the federated plugin architecture. A detailed explanation of the federated plugin concept can be found in the Flutter documentation. 

https://github.com/Baseflow/flutter-geolocator


Getting Started With Flutter
- In this tutorial, you’ll build a Flutter app called GHFlutter that queries the GitHub API for team members in a GitHub organization and displays the information in a scrollable list

https://www.raywenderlich.com/24499516-getting-started-with-flutter




## Tools

DartPad is a free, open-source online editor to help developers learn about Dart and Flutter. 
https://dartpad.dev

Rive allows developers to create and ship beautiful, interactive animations to any platform. Their open-source runtimes make it possible for creators to animate once, then launch on any platform they want.
- Consolidating Flare and 2Dimensions Into One Brand
https://rive.app/


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

Flutter Travel UI
https://github.com/MarcusNg/flutter_travel_ui

## Plugin

Flutter plugin that allows you to showcase your features on flutter application.
https://github.com/SimformSolutionsPvtLtd/flutter_showcaseview


Flutter Community Plus Plugins
The official Flutter Community Plus plugins for Flutter
- Share Plus. A Flutter plugin to share content from your Flutter app via the platform's share dialog.
https://plus.fluttercommunity.dev/

## Flutter starter kit

Flutter Login Screen with Firebase Auth and Facebook Login 
https://github.com/instaflutter/flutter-login-screen-firebase-auth-facebook-login


## Application

E-Commerce Complate App - Flutter UI
https://github.com/abuanwar072/E-commerce-Complete-Flutter-UI

Flutter News App with newsapi.org. Developed using the Test Driven Development. 
- List daily news.
- Filter daily news by category.
- Refresh list daily news with pull to refresh style.
- Go to detail news website.
- Search news.
- Dark mode support. 
- Hive, Flutter BLoC, Dio, JSON Serializable
https://github.com/CoderJava/Flutter-News-App


Cross-platform wordpress news app built with Flutter and WP REST API. 
https://github.com/l3lackcurtains/Flutter-for-Wordpress-App


A privacy-friendly Twitter frontend for mobile devices 
- sqflite, sqflite_migration_plan, sqflite_common
- dio
- catcher
- infinite_scroll_pagination
https://github.com/jonjomckay/fritter


A Music Player App made with Flutter 
- hive, hive_flutter
- supabase. (Rev: Auth User)
- carousel_slider
- flip_card
https://github.com/Sangwan5688/BlackHole