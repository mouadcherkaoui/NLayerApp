FROM microsoft/aspnetcore-build

ADD . /src
WORKDIR /src/DotNetCore.MvcApp
RUN dotnet restore
RUN dotnet publish -o /app
WORKDIR /app
ENTRYPOINT ["dotnet", "DotNetCore.MvcApp.dll"]
