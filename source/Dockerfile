FROM microsoft/aspnetcore-build

ADD . /src
WORKDIR /src/NLayerApp.MvcApp
RUN dotnet restore
RUN dotnet publish -o /app
WORKDIR /app
ENTRYPOINT ["dotnet", "NLayerApp.MvcApp.dll"]
