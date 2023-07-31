FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["AwesomeShop.Services.Customers/src/AwesomeShop.Services.Customers.Api/AwesomeShop.Services.Customers.Api.csproj", "AwesomeShop.Services.Customers/src/AwesomeShop.Services.Customers.Api/"]
COPY ["AwesomeShop.Services.Customers/src/AwesomeShop.Services.Customers.Application/AwesomeShop.Services.Customers.Application.csproj", "AwesomeShop.Services.Customers/src/AwesomeShop.Services.Customers.Application/"]
COPY ["AwesomeShop.Services.Customers/src/AwesomeShop.Services.Customers.Core/AwesomeShop.Services.Customers.Core.csproj", "AwesomeShop.Services.Customers/src/AwesomeShop.Services.Customers.Core/"]
COPY ["AwesomeShop.Services.Customers/src/AwesomeShop.Services.Customers.Infrastructure/AwesomeShop.Services.Customers.Infrastructure.csproj", "AwesomeShop.Services.Customers/src/AwesomeShop.Services.Customers.Infrastructure/"]
RUN dotnet restore "AwesomeShop.Services.Customers/src/AwesomeShop.Services.Customers.Api/AwesomeShop.Services.Customers.Api.csproj"
COPY . .
WORKDIR "/src/AwesomeShop.Services.Customers/src/AwesomeShop.Services.Customers.Api"
RUN dotnet build "AwesomeShop.Services.Customers.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AwesomeShop.Services.Customers.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AwesomeShop.Services.Customers.Api.dll"]
