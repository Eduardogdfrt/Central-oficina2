# Utiliza a imagem base do .NET 8 SDK para construir a aplica  o
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["WebApia/Ellp.Api.Webapi.csproj", "WebApia/"]
COPY ["ellp.api.application/Ellp.Api.Application.csproj", "ellp.api.application/"]
COPY ["Ellp.Infra.SqlServer/Ellp.Api.Infra.SqlServer.csproj", "Ellp.Infra.SqlServer/"]

RUN dotnet restore "WebApia/Ellp.Api.Webapi.csproj"

# Copia todo o restante do c digo e publica a aplica  o
COPY . .
WORKDIR "/src/WebApia"
RUN dotnet publish "Ellp.Api.Webapi.csproj" -c Release -o /app/publish

# Utiliza a imagem base do .NET 8 Runtime para executar a aplica  o
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:5000
EXPOSE 5000
ENTRYPOINT ["dotnet", "Ellp.Api.Webapi.dll"]