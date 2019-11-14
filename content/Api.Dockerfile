FROM mcr.microsoft.com/dotnet/core/sdk:3.0 as builder

COPY *.sln .
COPY /src/ /src/

WORKDIR /src/PlusUltra.Microservice.Api
RUN dotnet publish -c Release -o /app/

FROM mcr.microsoft.com/dotnet/core/aspnet:3.0
WORKDIR /app
COPY --from=builder /app .

ENTRYPOINT ["dotnet", "PlusUltra.Microservice.Api.dll", "--urls", "http://+:80"]