FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY ["WebCoreDeploy/WebCoreDeploy.csproj", "WebCoreDeploy/"]
RUN dotnet restore "WebCoreDeploy/WebCoreDeploy.csproj"
COPY . .
WORKDIR "/src/WebCoreDeploy"
RUN dotnet build "WebCoreDeploy.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "WebCoreDeploy.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "WebCoreDeploy.dll"]