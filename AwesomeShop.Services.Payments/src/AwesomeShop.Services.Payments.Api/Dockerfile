FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/AwesomeShop.Services.Payments.Api/AwesomeShop.Services.Payments.Api.csproj", "src/AwesomeShop.Services.Payments.Api/"]
RUN dotnet restore "src/AwesomeShop.Services.Payments.Api/AwesomeShop.Services.Payments.Api.csproj"
COPY . .
WORKDIR "/src/src/AwesomeShop.Services.Payments.Api"
RUN dotnet build "AwesomeShop.Services.Payments.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AwesomeShop.Services.Payments.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AwesomeShop.Services.Payments.Api.dll"]
