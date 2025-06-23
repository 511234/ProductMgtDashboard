FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /ProductMgtDashboardBe

# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
RUN dotnet publish -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /ProductMgtDashboardBe
COPY --from=build /ProductMgtDashboardBe/out .
EXPOSE 80
EXPOSE 443
ENTRYPOINT ["dotnet", "ProductMgtDashboardBe.dll"]
