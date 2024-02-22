FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["RinhaBackend.ReverseProxy/RinhaBackend.ReverseProxy.csproj", "RinhaBackend.ReverseProxy/"]
RUN dotnet restore "RinhaBackend.ReverseProxy/RinhaBackend.ReverseProxy.csproj"
COPY . .
WORKDIR "/src/RinhaBackeNd.ReverseProxy"
RUN dotnet build "RinhaBackend.ReverseProxy.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RinhaBackend.ReverseProxy.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RinhaBackend.ReverseProxy.dll"]
