FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 7002

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Tracks/Tracks.Api/Tracks.Api.csproj", "Tracks/Tracks.Api/"]
COPY ["Tracks/Tracks.Core/Tracks.Core.csproj", "Tracks/Tracks.Core/"]
COPY ["Tracks/Tracks.Data/Tracks.Data.csproj", "Tracks/Tracks.Data/"]
COPY ["Common/Common.csproj", "Common/"]

RUN dotnet restore "./Tracks/Tracks.Api/Tracks.Api.csproj"

COPY . .

WORKDIR "/src/Tracks/Tracks.Api"
RUN dotnet build "./Tracks.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Tracks.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tracks.Api.dll"]
