FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["WannaBePrincipal.csproj", "."]
RUN dotnet restore "./././WannaBePrincipal.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "./WannaBePrincipal.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./WannaBePrincipal.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
# Switch to root to perform package updates
USER root
RUN apt-get update && apt-get upgrade -y
WORKDIR /app
COPY --from=publish /app/publish .
# Switch back to non-root user for security
USER app
ENTRYPOINT ["dotnet", "WannaBePrincipal.dll"]