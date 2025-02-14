# Etapa para construir o frontend (React)
FROM node:16-alpine AS frontend-build
WORKDIR /frontend
COPY front/package*.json ./
RUN npm install
COPY front/ ./
RUN npm install --save-dev @babel/plugin-proposal-private-property-in-object
ENV CI=false
RUN npm run build
RUN ls -la build/

# Etapa para compilar o backend (.NET)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Back/WebApia/Ellp.Api.Webapi.csproj", "WebApia/"]
COPY ["Back/Ellp.Api.Application/Ellp.Api.Application.csproj", "Ellp.Api.Application/"]
COPY ["Back/Ellp.Api.Domain/Ellp.Api.Domain.csproj", "Ellp.Api.Domain/"]
COPY ["Back/Ellp.Infra.SqlServer/Ellp.Api.Infra.SqlServer.csproj", "Ellp.Infra.SqlServer/"]
RUN dotnet restore "WebApia/Ellp.Api.Webapi.csproj"
COPY Back/ ./
WORKDIR "/src/WebApia"
RUN dotnet publish "Ellp.Api.Webapi.csproj" -c Release -o /app/publish

# Etapa para criar a imagem final do backend .NET com o frontend React
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS runtime
WORKDIR /app

# Instalar ICU para globalização
RUN apk add --no-cache icu-libs

# Definir variáveis de ambiente para desabilitar o modo invariável de globalização
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
ENV LC_ALL=en_US.UTF-8
ENV LANG=en_US.UTF-8

# Copiar arquivos compilados do backend e do frontend
COPY --from=build /app/publish .
COPY --from=frontend-build /frontend/build /app/wwwroot

# Definir a URL do ASP.NET e ambiente de produção
ENV ASPNETCORE_URLS=http://+:5000
ENV ASPNETCORE_ENVIRONMENT=Production

# Expor as portas para o frontend e o backend
EXPOSE 5000
EXPOSE 3000

# Definir o ponto de entrada da aplicação
ENTRYPOINT ["dotnet", "Ellp.Api.Webapi.dll"]
