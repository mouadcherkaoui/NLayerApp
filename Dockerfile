FROM microsoft/aspnetcore-build

ADD . /src
WORKDIR /src/NLayerdApp.MvcApp
RUN dotnet restore
RUN dotnet publish -o /app
WORKDIR /app
ENTRYPOINT ["dotnet", "NLayerdApp.MvcApp.dll"]
