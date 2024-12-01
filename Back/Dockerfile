# Estágio de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar os arquivos de projeto e restaurar as dependências
COPY ["WebApia/Ellp.Api.Webapi.csproj", "WebApia/"]
COPY ["Ellp.Infra.SqlServer/Ellp.Api.Infra.SqlServer.csproj", "Ellp.Infra.SqlServer/"]
COPY ["ellp.api.application/Ellp.Api.Application.csproj", "Ellp.Api.Application/"]
COPY ["Ellp.api.Domain/Ellp.Api.Domain.csproj", "Ellp.Api.Domain/"]

RUN dotnet restore "WebApia/Ellp.Api.Webapi.csproj"
COPY . .
WORKDIR "/src/WebApia"
RUN dotnet build "Ellp.Api.Webapi.csproj" -c Release -o /app/build

# Estágio de publicação
FROM build AS publish
RUN dotnet publish "Ellp.Api.Webapi.csproj" -c Release -o /app/publish

# Estágio final
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Definir a porta que a aplicação irá escutar
EXPOSE 7777

# Comando para iniciar a aplicação
ENTRYPOINT ["dotnet", "Ellp.Api.Webapi.dll"]
