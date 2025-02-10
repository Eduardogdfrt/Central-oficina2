FROM node:22.13.1 AS frontend-build
WORKDIR /frontend
COPY front/package*.json ./
RUN npm install
COPY front/ ./
RUN npm install --save-dev @babel/plugin-proposal-private-property-in-object
ENV CI=false
RUN npm run build
RUN ls -la build/

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

FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS runtime
WORKDIR /app
COPY --from=build /app/publish .


COPY --from=frontend-build /frontend/build /app/wwwroot
RUN mkdir -p /app/wwwroot/models && cp -r /app/wwwroot/models /app/wwwroot


ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
ENV ASPNETCORE_URLS=http://+:${PORT:-5000}  
ENV ASPNETCORE_ENVIRONMENT=Production

EXPOSE 5000


ENTRYPOINT ["dotnet", "Ellp.Api.Webapi.dll"]
