#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["TFTStats.Summoners.csproj", ""]
COPY [".editorconfig", ""]
COPY ["Directory.Packages.props", ""]
RUN dotnet restore "TFTStats.Summoners.csproj"

COPY . .
RUN dotnet build "TFTStats.Summoners.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
RUN dotnet publish "TFTStats.Summoners.csproj" --no-restore -c $BUILD_CONFIGURATION -o /app/publish

FROM base AS final
WORKDIR /app
ENV ASPNETCORE_HTTP_PORTS=5194
EXPOSE 5194
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TFTStats.Summoners.dll"]