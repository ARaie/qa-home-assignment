FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

COPY . .

RUN dotnet restore

RUN dotnet build --configuration Release

RUN dotnet test /app/CardValidation.Tests/ --logger "trx;LogFileName=test-results.trx"

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

WORKDIR /app

COPY --from=build /app /app

EXPOSE 5000

ENTRYPOINT ["dotnet", "CardValidation.Web.dll"]
