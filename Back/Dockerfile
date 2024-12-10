FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia os arquivos .csproj (sem o prefixo "Back/" já que estamos dentro da pasta)
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

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:5000
EXPOSE 5000
ENTRYPOINT ["dotnet", "Ellp.Api.Webapi.dll"]