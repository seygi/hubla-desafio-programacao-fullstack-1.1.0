FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
EXPOSE 7154
EXPOSE 5111

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copy the .csproj file and restore dependencies
COPY ["backend/src/Hubla.Sales.API/", "src/Hubla.Sales.API/"]
COPY ["backend/src/Hubla.Sales.Application/", "src/Hubla.Sales.Application/"]
# COPY ["**/*.csproj", "./"]
RUN dotnet restore "src/Hubla.Sales.API/Hubla.Sales.API.csproj"

# Copy the rest of the source code and build the project
COPY . .
WORKDIR "/src/src/Hubla.Sales.API"
RUN dotnet build "Hubla.Sales.API.csproj" -c Release -o /app/build

FROM build AS publish
WORKDIR "/src/src/Hubla.Sales.API"
RUN dotnet publish "Hubla.Sales.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Hubla.Sales.API.dll"]