#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["BigBlueBalancer.Api/BigBlueBalancer.Api.csproj", "BigBlueBalancer.Api/"]
COPY ["BigBlueButton.Client/BigBlueButton.Client.csproj", "BigBlueButton.Client/"]
RUN dotnet restore "BigBlueBalancer.Api/BigBlueBalancer.Api.csproj"
COPY . .
WORKDIR "/src/BigBlueBalancer.Api"
RUN dotnet build "BigBlueBalancer.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BigBlueBalancer.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BigBlueBalancer.Api.dll"]