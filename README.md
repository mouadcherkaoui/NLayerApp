# netcore NLayerApp example 

## Tools :

- dotnet core runtime and sdk.
- nodejs and npm on your local machine.
- local instance of a Sql Server "Express Edition"
- VS Code IDE

## Running the project

- on a command prompt go to the folder ./NLayerApp.MvcApp
- run : 
    dotnet restore
- run : dotnet ef database update --project ../NLayerApp.DataAccessLayer/NLayerApp.DataAccessLayer.csproj

the instance of Sql Server i use is the default one on the express edition : ./SQLExpress
if you have another name you must update the connection string on "NLayerApp.MvcApp/appSettings.json" and "NLayerApp.DataAccessLayer/AppDataContext.cs"

## Project Structure:

![NLayerApp-Structure](https://github.com/FSharpDeveloper/NLayerApp/blob/master/images/NLayerApp-Structure.png?raw=true)

 ## NLayerApp.Infrastructure Project:

 this project contains different abstractions that will play the role of an abstact infrastructure for the different projects layers and components,
 it contains the folders below: 
![NLayerApp-Structure](https://github.com/FSharpDeveloper/NLayerApp/blob/master/images/NLayerApp.Infrastructure-Structure.png?raw=true)

1. Controllers folder, containing abstractions of the Mvc and Api Controllers that will be generated dynamically.
2. CQRS, contains the new interaction model which will be based on CQRS "Commands/Queries Responsiblity Separation"
3. DataAccessLayer, contains the IContext interface which is an abstraction of the DataAccess mechanism.
4. Models folder, containing interfaces used as abstraction and "signature for reflection usage" of entities objects.
5. Repositories, containing the IRepository interface.
