# NetCoreDynamics

## Tools :
- NLayerdApp runtime and sdk.
- nodejs and npm on your local machine.
- local instance of a Sql Server "Express Edition"
- VS Code IDE

## Running the project
- on a command prompt go to the folder ./DoNetCore.MvcApp
- run : 
    dotnet restore
- run : dotnet ef database update --project ../DoNetCore.DataAccessLayer/DoNetCore.DataAccessLayer.csproj

the instance of Sql Server i use is the default one on the express edition : ./SQLExpress
if you have another name you must update the connection string on "DoNetCore.MvcApp/appSettings.json" and "DoNetCore.DataAccessLayer/AppDataContext.cs"
