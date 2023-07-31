FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["AwesomeShop.Services.Orders.Api/AwesomeShop.Services.Orders.Api.csproj", "AwesomeShop.Services.Orders.Api/"]
RUN dotnet restore "AwesomeShop.Services.Orders.Api/AwesomeShop.Services.Orders.Api.csproj"
COPY . .
WORKDIR "/src/AwesomeShop.Services.Orders.Api"
RUN dotnet build "AwesomeShop.Services.Orders.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AwesomeShop.Services.Orders.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AwesomeShop.Services.Orders.Api.dll"]
