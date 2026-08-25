# Flutter Installation & Getting Started

Guide to install Flutter and create your first project.

# Install Flutter

Official documentation:
https://docs.flutter.dev/get-started/install

## Requirements

- **Editor**: Android Studio or VS Code (with the Flutter & Dart plugins).
- **Platform**: Windows, macOS, Linux, or web.
- Git (optional but recommended).

## Steps

1. Download the Flutter SDK from the official page for your operating system.
   https://docs.flutter.dev/get-started/install
2. Extract the SDK to a location you control (e.g. `C:\src\flutter`).
3. Add the `bin` folder of the SDK to your `PATH` environment variable
   (e.g. `C:\src\flutter\bin`).
4. Verify the installation and check for missing dependencies:

   ```
   flutter doctor
   ```

5. Install Android Studio / VS Code and enable the platform toolchains
   (Android SDK, Android Emulator, Chrome for web, etc.).

## Add web support to an existing app

```
flutter create .
```

## Add desktop support to an existing Flutter app

```
flutter create --platforms=windows,macos,linux .
```

Warning: Hot reload is not supported in a web browser. Currently, Flutter
supports hot restart, but not hot reload in a web browser.

# Create and run your first project

```
flutter create my_app
cd my_app
flutter run
```

Useful commands:

- `flutter doctor`: check your environment.
- `flutter devices`: list connected devices.
- `flutter emulators`: list available emulators.
- `flutter pub get`: fetch project dependencies.

# Common errors

## Android SDK path

Flutter provides a command to update the Android SDK path:

```
flutter config --android-sdk <path-to-your-android-sdk-path>
```

## cmdline-tools component is missing

```
[!] Android toolchain - develop for Android devices (Android SDK version 29.0.3)
    X cmdline-tools component is missing
      Run `path/to/sdkmanager --install "cmdline-tools;latest"`
```

Fix from Android Studio:

- You must choose Android SDK in the left side of the Settings window.
  It's under Appearance & Behavior → System Settings.
- Search for "SDK tools" in the search box.
- SDK Tools tab
- Check Android SDK Command Line Tools
- Apply

## Flutter: Building with plugins requires symlink support

Go to Windows settings. Select Update & Security. On that, select For
Developers. In that window, there is an option called Install apps from any
source, including loose files on the Developer Mode. Enable that option.

https://stackoverflow.com/questions/68089177/flutter-building-with-plugins-requires-symlink-support

## Multidex support

```
App requires Multidex support
```

https://docs.flutter.dev/deployment/android#enabling-multidex-support

# Package management

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

# Editors

- Android Studio
- VS Code

# References

- Get started with Flutter: https://docs.flutter.dev/get-started/install
- Flutter documentation: https://docs.flutter.dev
