FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["Melidya.Microservices.Product.Api/Melidya.MicroservicesProductt.Api.csproj", "Melidya.Microservices.Product.Api/"]
COPY ["MelidyaEntity/MelidyaEntity.csproj", "MelidyaEntity/"]
RUN dotnet restore "Melidya.Microservices.Product.Api/Melidya.MicroservicesProductt.Api.csproj"
COPY . .
WORKDIR "/src/Melidya.Microservices.Product.Api"
RUN dotnet build "Melidya.MicroservicesProductt.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Melidya.MicroservicesProductt.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Melidya.MicroservicesProductt.Api.dll"]