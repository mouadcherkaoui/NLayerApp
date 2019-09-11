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

 ![](/FSharpDeveloper/NLayerApp/blob/master/images/NLayerApp-Structure.png?raw=true)