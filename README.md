# 💸 Finance App API

API REST para gestão financeira de empresas, desenvolvida em ASP.NET Core.

## 📝 Sobre o Back-end

Este projeto é uma API REST desenvolvida em ASP.NET Core para gerenciar empresas, entradas e resultados financeiros. Ele oferece rotas para cadastro, consulta, atualização, exclusão e geração de relatórios, facilitando a integração com sistemas front-end.

> ⚠️ Atenção: Os dados não são salvos permanentemente, pois o projeto não utiliza banco de dados. Todas as informações são mantidas apenas em memória e serão perdidas ao reiniciar, atualizar ou escalar o serviço.

## 🌐 Demonstração do Projeto

Acesse o front-end do projeto em: [financeiro-front-plum.vercel.app](https://financeiro-front-plum.vercel.app)


## ⚙️ Funcionalidades
- 🏢 Cadastro, consulta, atualização e exclusão de empresas
- 💰 Cadastro, consulta, atualização e exclusão de entradas financeiras
- 📊 Cadastro, consulta, atualização e exclusão de resultados financeiros
- 📄 Geração de relatórios financeiros
- 📈 Comparativo de indicadores financeiros

**Atenção:** Este projeto não utiliza banco de dados. Todos os dados são mantidos apenas em memória e serão perdidos ao reiniciar, atualizar ou escalar o serviço.

## 🖥️ Como rodar localmente

1. **Pré-requisitos:**
   - .NET 8 SDK
   - Docker (opcional)

2. **Restaurar dependências:**
   ```powershell
   dotnet restore
   ```

3. **Executar a aplicação:**
   ```powershell
   dotnet run
   ```

4. **Acessar a documentação Swagger:**
   - Normalmente disponível em `http://localhost:5000/swagger` ou `http://localhost:8080/swagger`



## 🐳 Deploy no Render

O arquivo `Dockerfile` está incluso apenas para facilitar o deploy no Render. Não é necessário rodar Docker localmente para usar a API.

**Como fazer o deploy no Render:**

1. Faça login ou crie uma conta em [Render](https://render.com).
2. Crie um novo serviço do tipo "Web Service".
3. Selecione seu repositório com o projeto.
4. Render detecta automaticamente o Dockerfile e faz o build.
5. Não é necessário configurar porta, pois Render usa a porta 80 do container.
6. Após o deploy, acesse a URL gerada para testar sua API.


## 📚 Principais Rotas da API

| Método e Rota                              | Descrição |
|---------------------------------------------|-----------|
| `GET /api/finance/companies`                | Retorna uma lista de todas as empresas cadastradas |
| `POST /api/finance/company`                 | Cadastra uma nova empresa (enviar JSON no corpo) |
| `PUT /api/finance/company/{id}`             | Atualiza os dados de uma empresa existente pelo ID |
| `DELETE /api/finance/company/{id}`          | Remove uma empresa pelo ID |
| `GET /api/finance/entries`                  | Retorna todas as entradas financeiras cadastradas |
| `POST /api/finance/entry`                   | Adiciona uma nova entrada financeira (enviar JSON no corpo) |
| `PUT /api/finance/entry/{id}`               | Atualiza uma entrada financeira existente pelo ID |
| `DELETE /api/finance/entry/{id}`            | Remove uma entrada financeira pelo ID |
| `GET /api/finance/company/{companyId}/results` | Lista os resultados financeiros de uma empresa específica |
| `POST /api/finance/company/{companyId}/result` | Adiciona um resultado financeiro para uma empresa específica |
| `PUT /api/finance/result/{id}`              | Atualiza um resultado financeiro existente pelo ID |
| `DELETE /api/finance/result/{id}`           | Remove um resultado financeiro pelo ID |
| `GET /api/finance/report`                   | Gera um relatório financeiro consolidado do período informado |

## 🛠️ Tecnologias Utilizadas

- ASP.NET Core 8
- C#
- Docker
- Swagger/OpenAPI
- Visual Studio Code
- Git & GitHub

---

Desenvolvido por: [Gustavo Catucci](https://github.com/Gustavocl22)


