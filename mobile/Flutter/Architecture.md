# Architecture

# BLoC

Bloc is a design pattern created by Google to help separate business logic from the presentation layer and enable a developer to reuse code more efficiently.

Stream is the foundation of Bloc, and you need to become familiar with the concept.

To summarize, BLoCs are objects that process and store business logic, use sinks to accept input and provide output via streams.



**Revision**
- BLoC stands for Business Logic Components. 

Flutter App Architecture: The Repository Pattern
- Flutter Project Structure: Feature-first or Layer-first?
- Flutter App Architecture: The Domain Model
- Flutter App Architecture: The Application Layer
- Flutter App Architecture: The Presentation Layer
https://codewithandrea.com/articles/flutter-repository-pattern/


# Referencias

The Flutter Architecture Samples project demonstrates strategies to help solve or avoid these common problems. This project implements the same app using different architectural concepts and tools.
- Uses the tools Flutter provides out of the box to manage app state.
- Uses an InheritedWidget
- with provider 
- Uses the Redux library to manage app state and update Widgets and adds Cloud_Firestore as the Todos database. (ReactiveTodosRepository)
https://github.com/brianegan/flutter_architecture_samples