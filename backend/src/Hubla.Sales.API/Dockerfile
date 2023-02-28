#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Hubla.Sales.API/Hubla.Sales.API.csproj", "Hubla.Sales.API/"]
RUN dotnet restore "Hubla.Sales.API/Hubla.Sales.API.csproj"
COPY . .
WORKDIR "/src/Hubla.Sales.API"
RUN dotnet build "Hubla.Sales.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Hubla.Sales.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Hubla.Sales.API.dll"]