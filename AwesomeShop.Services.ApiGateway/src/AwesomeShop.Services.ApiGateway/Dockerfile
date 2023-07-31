FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["AwesomeShop.Services.ApiGateway/src/AwesomeShop.Services.ApiGateway/AwesomeShop.Services.ApiGateway.csproj", "src/AwesomeShop.Services.ApiGateway/"]
RUN dotnet restore "src/AwesomeShop.Services.ApiGateway/AwesomeShop.Services.ApiGateway.csproj"
COPY "AwesomeShop.Services.ApiGateway/src/AwesomeShop.Services.ApiGateway" "src/AwesomeShop.Services.ApiGateway/"
WORKDIR "src/AwesomeShop.Services.ApiGateway"
RUN dotnet build "AwesomeShop.Services.ApiGateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AwesomeShop.Services.ApiGateway.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AwesomeShop.Services.ApiGateway.dll"]
