FROM microsoft/dotnet:2.2-sdk as builder
COPY . /
WORKDIR /src/PlusUltra.MicroserviceApi
RUN dotnet publish --output /app/ -c Release --no-restore

FROM microsoft/dotnet:2.2-aspnetcore-runtime-alpine
WORKDIR /app
COPY --from=builder /app .

# passar para compose ENV ASPNETCORE_ENVIRONMENT Development
ENV DOTNET_RUNNING_IN_CONTAINER true
ENV ASPNETCORE_URLS=http://*:80

EXPOSE 80
ENTRYPOINT ["dotnet", "PlusUltra.Microservice.Api.dll"]