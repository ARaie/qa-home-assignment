FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

COPY . .

RUN dotnet restore

RUN dotnet build

RUN dotnet test /app/CardValidation.Tests/ --logger "trx;LogFileName=test-results.trx"

EXPOSE 80

CMD ["dotnet", "run", "--project", "/app/CardValidation.Web/CardValidation.Web.csproj"]
