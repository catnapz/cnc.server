FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Cnc.Dm/Cnc.Dm.csproj", "Cnc.Dm/"]
RUN dotnet restore "Cnc.Dm/Cnc.Dm.csproj"
COPY . .
WORKDIR "/src/Cnc.Dm"
RUN dotnet build "Cnc.Dm.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Cnc.Dm.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Cnc.Dm.dll"]
