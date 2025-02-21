FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["MyAcademy.Course/MyAcademy.Course.csproj", "MyAcademy.Course/"]
RUN dotnet restore "MyAcademy.Course/MyAcademy.Course.csproj"
COPY . .
WORKDIR "/src/MyAcademy.Course"
RUN dotnet build "MyAcademy.Course.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "MyAcademy.Course.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MyAcademy.Course.dll"]
