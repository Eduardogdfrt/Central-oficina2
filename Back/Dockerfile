# Build frontend
FROM node:16-alpine AS frontend-build
WORKDIR /frontend
COPY ../front/package*.json ./
RUN npm install
COPY ../front/ .
RUN npm install --save-dev @babel/plugin-proposal-private-property-in-object
ENV CI=false
RUN npm run build

# Build backend
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia os arquivos .csproj
COPY ["WebApia/Ellp.Api.Webapi.csproj", "WebApia/"]
COPY ["Ellp.Api.Application/Ellp.Api.Application.csproj", "Ellp.Api.Application/"]
COPY ["Ellp.Infra.SqlServer/Ellp.Api.Infra.SqlServer.csproj", "Ellp.Infra.SqlServer/"]
COPY ["Ellp.Api.Domain/Ellp.Api.Domain.csproj", "Ellp.Api.Domain/"]

# Executa o dotnet restore
RUN dotnet restore "WebApia/Ellp.Api.Webapi.csproj"

# Copia todo o restante do código
COPY . .
WORKDIR "/src/WebApia"
RUN dotnet publish "Ellp.Api.Webapi.csproj" -c Release -o /app/publish

# Final image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
COPY --from=frontend-build /frontend/build ./wwwroot/
ENV ASPNETCORE_URLS=http://+:5000
EXPOSE 5000
ENTRYPOINT ["dotnet", "Ellp.Api.Webapi.dll"]