FROM microsoft/aspnetcore-build

ADD . /src
WORKDIR /src/NLayerAppp.MvcApp
RUN dotnet restore
RUN dotnet publish -o /app
WORKDIR /app
ENTRYPOINT ["dotnet", "NLayerAppp.MvcApp.dll"]
