# Build frontend
FROM node:16-alpine AS frontend-build
WORKDIR /frontend
COPY front/package*.json ./
RUN npm install
COPY front/ ./
RUN npm install --save-dev @babel/plugin-proposal-private-property-in-object
ENV CI=false
RUN npm run build
RUN ls -la build/

# Build backend
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy .csproj files
COPY ["Back/WebApia/Ellp.Api.Webapi.csproj", "WebApia/"]
COPY ["Back/Ellp.Api.Application/Ellp.Api.Application.csproj", "Ellp.Api.Application/"]
COPY ["Back/Ellp.Api.Domain/Ellp.Api.Domain.csproj", "Ellp.Api.Domain/"]
COPY ["Back/Ellp.Infra.SqlServer/Ellp.Api.Infra.SqlServer.csproj", "Ellp.Infra.SqlServer/"]
COPY ["Back/Ellp.Api.UnitTest/Ellp.Api.UnitTest.csproj", "Ellp.Api.UnitTest/"]
COPY ["Back/Ellp.Api.FunctionalTest/Ellp.Api.FunctionalTest.csproj", "Ellp.Api.FunctionalTest/"]
# Restore dependencies
RUN dotnet restore "WebApia/Ellp.Api.Webapi.csproj"

# Copy the rest of the backend code
COPY Back/ .

WORKDIR "/src/WebApia"
RUN dotnet publish "Ellp.Api.Webapi.csproj" -c Release -o /app/publish

# Final image
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS runtime
WORKDIR /app
COPY --from=build /app/publish .
# Copy frontend build to wwwroot
COPY --from=frontend-build /frontend/build /app/wwwroot
ENV ASPNETCORE_URLS=http://+:5000
ENV ASPNETCORE_ENVIRONMENT=Production
EXPOSE 5000
ENTRYPOINT ["dotnet", "Ellp.Api.Webapi.dll"]


