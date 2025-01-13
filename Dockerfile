# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

# Copy all project files
COPY . .

# Restore dependencies
RUN dotnet restore

# Build the application
RUN dotnet build --configuration Release

# Run unit tests
RUN dotnet test CardValidation.Tests/ --logger:"trx;LogFileName=test-results.trx"

# Publish the application
RUN dotnet publish --configuration Release --output /app

# Use runtime image for serving the app
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

WORKDIR /app

# Copy published app from build stage
COPY --from=build /app .

ENTRYPOINT ["dotnet", "CardValidation.dll"]
