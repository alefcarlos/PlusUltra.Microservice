FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
COPY . /
WORKDIR /src/PlusUltra.MicroserviceApi
RUN dotnet publish --output /app/ -c Release --no-restore

FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim
WORKDIR /app
COPY --from=builder /app .

# passar para compose ENV ASPNETCORE_ENVIRONMENT Development
ENV DOTNET_RUNNING_IN_CONTAINER true
ENV ASPNETCORE_URLS=http://*:80

EXPOSE 80
ENTRYPOINT ["dotnet", "PlusUltra.Microservice.Api.dll"]