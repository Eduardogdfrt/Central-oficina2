# Usar a imagem oficial do SQL Server como base
FROM mcr.microsoft.com/mssql/server:2022-latest

# Definir variáveis de ambiente para aceitar o EULA e definir a senha do SA
ENV ACCEPT_EULA=Y
ENV SA_PASSWORD=StrongPassword123

# Expor a porta padrão do SQL Server
EXPOSE 1433

# Comando para rodar o SQL Server
CMD /opt/mssql/bin/sqlservr