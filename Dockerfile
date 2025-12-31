FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia solution
COPY Loteria.sln .

# Copia os csproj
COPY Loteria.Api/Loteria.Api.csproj Loteria.Api/
COPY Loteria.Application/Loteria.Application.csproj Loteria.Application/
COPY Loteria.Domain/Loteria.Domain.csproj Loteria.Domain/
COPY Loteria.Infrastructure/Loteria.Infrastructure.csproj Loteria.Infrastructure/

# Restore pela solution (mais confiável)
RUN dotnet restore Loteria.sln

# Copia tudo
COPY . .

WORKDIR /src/Loteria.Api
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Loteria.Api.dll"]