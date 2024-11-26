#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["TFTStats.Matches.csproj", "TFTStats.Matches/"]
COPY [".editorconfig", ""]
COPY ["Directory.Packages.props", ""]
RUN dotnet restore "TFTStats.Matches/TFTStats.Matches.csproj"

WORKDIR "/src/TFTStats.Matches"
COPY . .
RUN dotnet build "TFTStats.Matches.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
RUN dotnet publish "TFTStats.Matches.csproj" --no-restore -c $BUILD_CONFIGURATION -o /app/publish

FROM base AS final
WORKDIR /app
ENV ASPNETCORE_HTTP_PORTS=5033
EXPOSE 5033
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TFTStats.Matches.dll"]