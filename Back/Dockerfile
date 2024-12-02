# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia os arquivos de projeto de todas as bibliotecas e do aplicativo
COPY ["WebApia/Ellp.Api.Webapi.csproj", "WebApia/"]
COPY ["Ellp.api.Domain/Ellp.api.Domain.csproj", "Ellp.api.Domain/"]
COPY ["ellp.api.application/Ellp.Api.Application.csproj", "ellp.api.application/"]
COPY ["Ellp.Infra.SqlServer/Ellp.Api.Infra.SqlServer.csproj", "Ellp.Infra.SqlServer/"]

# Restaura as dependências de todos os projetos
RUN dotnet restore "WebApia/Ellp.Api.Webapi.csproj"

# Copia todo o código-fonte para o container
COPY . .


# Compila e publica o projeto WebApi
WORKDIR "/src/WebApia"
RUN dotnet publish "Ellp.Api.Webapi.csproj" -c Release -o /app/publish

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
EXPOSE 8080
EXPOSE 443

# Copia a aplicação publicada para o container
COPY --from=build /app/publish .

# Define o comando para iniciar a aplicação
ENTRYPOINT ["dotnet", "Ellp.Api.Webapi.dll"]
