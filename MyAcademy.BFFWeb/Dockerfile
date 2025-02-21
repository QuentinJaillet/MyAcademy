FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["MyAcademy.BFFWeb/MyAcademy.BFFWeb.csproj", "MyAcademy.BFFWeb/"]
RUN dotnet restore "MyAcademy.BFFWeb/MyAcademy.BFFWeb.csproj"
COPY . .
WORKDIR "/src/MyAcademy.BFFWeb"
RUN dotnet build "MyAcademy.BFFWeb.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "MyAcademy.BFFWeb.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MyAcademy.BFFWeb.dll"]
