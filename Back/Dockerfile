# Utiliza a imagem base do .NET 8 SDK para construir a aplicação
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia os arquivos .csproj
COPY ["Back/WebApia/Ellp.Api.Webapi.csproj", "WebApia/"]
COPY ["Back/Ellp.Api.Application/Ellp.Api.Application.csproj", "Ellp.Api.Application/"]
COPY ["Back/Ellp.Infra.SqlServer/Ellp.Api.Infra.SqlServer.csproj", "Ellp.Infra.SqlServer/"]
COPY ["Back/Ellp.Api.Domain/Ellp.Api.Domain.csproj", "Ellp.Api.Domain/"]

# Executa o dotnet restore
RUN dotnet restore "WebApia/Ellp.Api.Webapi.csproj"

# Copia todo o restante do código e publica a aplicação
COPY . . 
WORKDIR "/src/Back/WebApia"
RUN dotnet publish "Ellp.Api.Webapi.csproj" -c Release -o /app/publish

# Utiliza a imagem base do .NET 8 Runtime para executar a aplicação
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:5000
EXPOSE 5000
ENTRYPOINT ["dotnet", "Ellp.Api.Webapi.dll"]
