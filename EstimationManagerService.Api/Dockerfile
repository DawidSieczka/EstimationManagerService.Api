#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["EstimationManagerService.Api/EstimationManagerService.Api.csproj", "EstimationManagerService.Api/"]
COPY ["EstimationManagerService.Application/EstimationManagerService.Application.csproj", "EstimationManagerService.Application/"]
COPY ["EstimationManagerService.Infrastructure/EstimationManagerService.Infrastructure.csproj", "EstimationManagerService.Infrastructure/"]
COPY ["EstimationManagerService.Domain/EstimationManagerService.Domain.csproj", "EstimationManagerService.Domain/"]
RUN dotnet restore "EstimationManagerService.Api/EstimationManagerService.Api.csproj"
COPY . .
WORKDIR "/src/EstimationManagerService.Api"
RUN dotnet build "EstimationManagerService.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EstimationManagerService.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EstimationManagerService.Api.dll"]