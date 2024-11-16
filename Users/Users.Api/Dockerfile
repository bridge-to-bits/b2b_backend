FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 7001

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Users/Users.Api/Users.Api.csproj", "Users/Users.Api/"]
COPY ["Users/Users.Core/Users.Core.csproj", "Users/Users.Core/"]
COPY ["Users/Users.Data/Users.Data.csproj", "Users/Users.Data/"]
COPY ["Common/Common.csproj", "Common/"]

RUN dotnet restore "./Users/Users.Api/Users.Api.csproj"

COPY . .

WORKDIR "/src/Users/Users.Api"
RUN dotnet build "./Users.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Users.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Users.Api.dll"]
