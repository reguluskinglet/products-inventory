FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

COPY . .
WORKDIR /app/ProductsFacadeApi
RUN dotnet restore
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build-env /app/ProductsFacadeApi/out ./

ENTRYPOINT ["dotnet", "ProductsFacadeApi.dll"]
