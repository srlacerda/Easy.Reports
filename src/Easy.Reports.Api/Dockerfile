#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/Easy.Reports.Api/Easy.Reports.Api.csproj", "src/Easy.Reports.Api/"]
RUN dotnet restore "src/Easy.Reports.Api/Easy.Reports.Api.csproj"
COPY . .
WORKDIR "/src/src/Easy.Reports.Api"
RUN dotnet build "Easy.Reports.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Easy.Reports.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Easy.Reports.Api.dll"]