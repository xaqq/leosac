FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["LeosacIntegration/LeosacIntegration.csproj", "LeosacIntegration/"]
RUN dotnet restore "LeosacIntegration/LeosacIntegration.csproj"
COPY . .
WORKDIR "/src/LeosacIntegration"
RUN dotnet build "LeosacIntegration.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LeosacIntegration.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LeosacIntegration.dll"]
