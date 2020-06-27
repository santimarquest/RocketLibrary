# RocketLibrary
Principal considerations of this exercise

- Although it was not strictly necessary in this case, it's always better (flexible, maintainable, testable) to program against interfaces for the domain entities (landingarea, landingplatform, rocket). Then, it would be necessary to inject the corresponding dependencies for these interfaces. Dependency injection in C# Library needs a bit of work, maybe I could improve on this issue later (it's suposed to be an 1 hour exercice) .
- I've used a singleton for the landing platform (with the implementation of Lazy<T>), as it's a shared resource.
- I've used a ConcurrentDictionary to model the landing results in the platform, as more than one rocket can land on the same platform at the same time.
- I've used a simplified version of the builder design pattern to create a landing platform 
- I've added some more unit tests than required, to test the configuration, exceptions, and rocket landings in the border of the platform.
- Regarding the configuration, I've added an appsettings.json file in \RocketLibraryTest\bin\Debug\netcoreapp3.1\appsettings.json, what I needed for unit testing. It would be necessary to add the same configuration file in \RocketLibrary\bin\Release\netcoreapp3.1\appsettings.json for the usage of this library in production 
  
 # Setup
 Just download to a local folder the zip version of this repository, and unzip at the same folder. You can open the solution in Visual Studio 2019 Community and run the tests, all of them should pass.
