FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Loteria.Api/Loteria.Api.csproj", "Loteria.Api/"]
COPY ["Loteria.Application/Loteria.Application.csproj", "Loteria.Application/"]
COPY ["Loteria.Domain/Loteria.Domain.csproj", "Loteria.Domain/"]
COPY ["Loteria.Infrastructure/Loteria.Infrastructure.csproj", "Loteria.Infrastructure/"]
RUN dotnet restore "./Loteria.Api/Loteria.Api.csproj"
COPY . .
WORKDIR "/src/Loteria.Api"
RUN dotnet build "./Loteria.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Loteria.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final stage
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Loteria.Api.dll"]