#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["UnleashDemo.API/UnleashDemo.API.csproj", "UnleashDemo.API/"]
RUN dotnet restore "UnleashDemo.API/UnleashDemo.API.csproj"
COPY . .
WORKDIR "/src/UnleashDemo.API"
RUN dotnet build "UnleashDemo.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UnleashDemo.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UnleashDemo.API.dll"]