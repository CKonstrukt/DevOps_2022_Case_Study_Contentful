FROM mcr.microsoft.com/dotnet/core/sdk:6.0.402 AS build
WORKDIR /src
COPY . .
RUN dotnet publish -c release -o /app

FROM mcr.microsoft.com/dotnet/core/aspnet:4.8.1
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "Reload.dll"]
