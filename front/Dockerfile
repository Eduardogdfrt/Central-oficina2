# Etapa de build
FROM node:18 AS build

WORKDIR /app

# Copiar o arquivo de configuração e as dependências
COPY ./front/package*.json ./
RUN npm install

# Copiar o código da aplicação e gerar o build
COPY ./front .
RUN npm run build

# Etapa de deploy com Nginx
FROM nginx:alpine

# Copiar os arquivos gerados do build para o diretório onde o Nginx serve
COPY --from=build /app/build /usr/share/nginx/html

# Expor a porta 80
EXPOSE 80

# Comando para rodar o Nginx em primeiro plano
CMD ["nginx", "-g", "daemon off;"]
