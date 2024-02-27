# Use the official image as a parent image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Use SDK image to build the project file
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/Catalog/Catalog.csproj", "src/Catalog/"]
COPY ["src/Shared/Shared.csproj", "src/Shared/"]
RUN dotnet restore "src/Catalog/Catalog.csproj"
COPY . .
WORKDIR "/src/src/Catalog"
RUN dotnet build "Catalog.csproj" -c Release -o /app/build

# Also build the Shared project
WORKDIR "/src/src/Shared"
RUN dotnet build "Shared.csproj" -c Release -o /app/build/Shared

FROM build AS publish
WORKDIR "/src/src/Catalog"
RUN dotnet publish "Catalog.csproj" -c Release -o /app/publish

# Final stage/image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Catalog.dll"]
