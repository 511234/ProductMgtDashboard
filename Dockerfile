# # Build
# FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
# WORKDIR /app

# COPY ["ProductMgtDashboardBe/ProductMgtDashboardBe.csproj", "ProductMgtDashboardBe"]
# RUN dotnet restore 'ProductMgtDashboardBe/ProductMgtDashboardBe.csproj'

# COPY ["ProductMgtDashboardBe", "ProductMgtDashboardBe/"]
# WORKDIR /ProductMgtDashboardBe
# RUN dotnet build "ProductMgtDashboardBe.csproj" -c Release -o /app/build

# # Publish
# FROM build as publish
# RUN dotnet publish "ProductMgtDashboardBe.csproj" -c Release -o /app/publish

# FROM mcr.microsoft.com/dotnet/aspnet:8.0
# ENV ASPNETCORE_HTTP_PORTS=5001
# EXPOSE 5001
# WORKDIR /app
# COPY --from=publish /app/publish .
# ENTRYPOINT [ "dotnet", "ProductMgtDashboardBe.dll" ]