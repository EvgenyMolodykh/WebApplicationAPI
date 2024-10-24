FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

USER app

WORKDIR /app

EXPOSE 8080
EXPOSE 8081  

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

ARG BUILD_CONFIGURATION=Release

WORKDIR /src

COPY ["ServerApp.csproj", "."]

RUN dotnet restore "./ServerApp.csproj"

COPY . .

WORKDIR "/src/."

RUN dotnet build "./ServerApp.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish

ARG BUILD_CONFIGURATION=Release

RUN dotnet publish "./ServerApp.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final

WORKDIR /app

# Копируем сертификат в контейнер
COPY ["Certificates/localhost.pfx", "/https/localhost.pfx"]

COPY --from=publish /app/publish .


ENV ASPNETCORE_URLS=https://+:8081;http://+:8080

ENV ASPNETCORE_Kestrel__Certificates__Default__Path=/https/localhost.pfx

ENV ASPNETCORE_Kestrel__Certificates__Default__Password=123

ENTRYPOINT ["dotnet", "ServerApp.dll"]