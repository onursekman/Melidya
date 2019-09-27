FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["Melidya.Admin.Panel/Melidya.Admin.Panel.csproj", "Melidya.Admin.Panel/"]
RUN dotnet restore "Melidya.Admin.Panel/Melidya.Admin.Panel.csproj"
COPY . .
WORKDIR "/src/Melidya.Admin.Panel"
RUN dotnet build "Melidya.Admin.Panel.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Melidya.Admin.Panel.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Melidya.Admin.Panel.dll"]