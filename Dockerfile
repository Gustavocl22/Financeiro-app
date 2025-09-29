# Imagem base do .NET SDK para build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copia os arquivos do projeto
COPY . ./

# Restaura dependências e publica em modo Release
RUN dotnet restore
RUN dotnet publish -c Release -o out

# Imagem base do .NET Runtime para execução
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copia os arquivos publicados do build
COPY --from=build /app/out ./

# Comando para iniciar a aplicação
ENTRYPOINT ["dotnet", "finance-app.dll"]