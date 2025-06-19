FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["ProductMgtDashboardBe/ProductMgtDashboardBe.csproj", "ProductMgtDashboardBe/"]
RUN dotnet restore "ProductMgtDashboardBe/ProductMgtDashboardBe.csproj"
COPY . .
WORKDIR "/src/ProductMgtDashboardBe"
RUN dotnet build "ProductMgtDashboardBe.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ProductMgtDashboardBe.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProductMgtDashboardBe.dll"]
