# Usando a imagem oficial do SQL Server
FROM mcr.microsoft.com/mssql/server:2019-latest

# Definindo variáveis de ambiente
ENV ACCEPT_EULA=Y
ENV SA_PASSWORD=SuaSenhaForte123

# Expondo a porta padrão do SQL Server
EXPOSE 1433

# Copiando o arquivo de backup para o contêiner
COPY oficina.bak /backup/oficina.bak

# Iniciando o SQL Server
CMD /opt/mssql/bin/sqlservr
