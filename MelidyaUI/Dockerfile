FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
 
FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["MelidyaUI/MelidyaUI.csproj", "MelidyaUI/"]
RUN dotnet restore "MelidyaUI/MelidyaUI.csproj"
COPY . .
WORKDIR "/src/MelidyaUI"
RUN dotnet build "MelidyaUI.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "MelidyaUI.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "MelidyaUI.dll"]