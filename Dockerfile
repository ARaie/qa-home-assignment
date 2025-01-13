# Kasutame SDK pilti, et ehitada ja testida rakendust
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

# Kopeerige kogu projekt konteinerisse
COPY . .

# Taastame sõltuvused
RUN dotnet restore

# Ehita rakendus
RUN dotnet build --configuration Release

# Testige rakendust
RUN dotnet test /app/CardValidation.Tests/ --logger "trx;LogFileName=test-results.trx"

# Kasutame ainult runtime pilti, et käivitada rakendus
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

WORKDIR /app

# Kopeerige ehitusest failid
COPY --from=build /app /app

EXPOSE 5000

ENTRYPOINT ["dotnet", "CardValidation.Web.dll"]
