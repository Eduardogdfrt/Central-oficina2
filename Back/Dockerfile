# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiar os arquivos de projeto e restaurar dependências
COPY ./Back/Entrypoint/WebApi/WebApi.csproj ./Back/Entrypoint/WebApi/
COPY ./Back/infra ./Back/infra
COPY ./Back/Entrypoint/WebApi.ServiceDefaults ./Back/Entrypoint/WebApi.ServiceDefaults
RUN dotnet restore ./Back/Entrypoint/WebApi/WebApi.csproj

# Criar diretório para saída
RUN mkdir -p /app/out

# Copiar todo o código e publicar o projeto
COPY ./ .
RUN dotnet publish ./Back/Entrypoint/WebApi/WebApi.csproj -c Release -o /app/out

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "WebApi.dll"]
