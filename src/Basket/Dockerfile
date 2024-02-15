# Use the Microsoft .NET Core SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copy the CSPROJ file and restore any NuGet packages
COPY src/Basket/Basket.csproj ./Basket/
RUN dotnet restore Basket/Basket.csproj

# Copy the rest of the code and build the app
COPY src/Basket/ ./Basket/
RUN dotnet publish Basket/Basket.csproj -c Release -o out

# Generate the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "Basket.dll"]
