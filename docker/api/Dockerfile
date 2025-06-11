# Étape de build (SDK .NET 9)
FROM mcr.microsoft.com/dotnet/sdk:9.0-alpine AS build-env
WORKDIR /app
COPY *.sln ./
COPY src/App/*.csproj ./src/App/
RUN dotnet restore ./src/App/LodgerBackend.csproj
COPY . .
RUN dotnet publish ./src/App/LodgerBackend.csproj -c Release -o out

# Étape de runtime (Serveur Web ASP .NET 9)
FROM mcr.microsoft.com/dotnet/aspnet:9.0-alpine AS runtime-env
WORKDIR /app

# Configuration globalization & timezone
RUN apk update && apk add --no-cache icu-libs tzdata \
    && cp /usr/share/zoneinfo/Europe/Paris /etc/localtime \
    && echo "Europe/Paris" > /etc/timezone

ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
ENV LANG=fr_FR.UTF-8
ENV LC_ALL=fr_FR.UTF-8
ENV TZ=Europe/Paris

# Copie du build
COPY --from=build-env /app/out .
COPY --from=build-env /app/assets ./assets
COPY serilog.json ./
COPY appsettings*.json ./

EXPOSE 8080
ENTRYPOINT ["dotnet", "LodgerBackend.dll"]
