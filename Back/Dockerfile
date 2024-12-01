# Estágio de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar os arquivos de projeto e restaurar as dependências
COPY ["WebApia/WebApia.csproj", "WebApia/"]
COPY ["Ellp.Infra.SqlServer/Ellp.Infra.SqlServer.csproj", "Ellp.Infra.SqlServer/"]
COPY ["Ellp.Api.Application/Ellp.Api.Application.csproj", "Ellp.Api.Application/"]
COPY ["Ellp.Api.Domain/Ellp.Api.Domain.csproj", "Ellp.Api.Domain/"]
COPY ["Ellp.Api.Application.Utilities/Ellp.Api.Application.Utilities.csproj", "Ellp.Api.Application.Utilities/"]

RUN dotnet restore "WebApia/WebApia.csproj"

# Copiar todo o código e construir o projeto
COPY . .
WORKDIR "/src/WebApia"
RUN dotnet build "WebApia.csproj" -c Release -o /app/build

# Publicar a aplicação
FROM build AS publish
RUN dotnet publish "WebApia.csproj" -c Release -o /app/publish

# Estágio de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Definir a porta que a aplicação irá escutar
EXPOSE 80
EXPOSE 443

# Comando para iniciar a aplicação
ENTRYPOINT ["dotnet", "WebApia.dll"]
