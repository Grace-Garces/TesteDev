# Desafio Desenvolvedor C\# - Senior | Globaltec

Este repositório contém a solução para o desafio técnico para a vaga de Analista/Desenvolvedor, proposto pela Senior | Globaltec. O projeto está dividido em duas partes, conforme a especificação:

1.  **API REST em C#**: Uma aplicação web que expõe endpoints para um CRUD de Pessoas, com autenticação baseada em Token JWT. Os dados são armazenados em memória para simplificar a execução.
2.  **Consulta SQL**: Um script SQL para SQL Server que cria e popula um banco de dados de teste e executa uma consulta para unificar informações de contas a pagar e contas pagas.

## Pré-requisitos

Para executar este projeto, você precisará ter o seguinte software instalado:

  * **.NET 8 SDK** (ou superior)
  * **SQL Server** 
  * Um editor de código como **Visual Studio 2022** ou **Visual Studio Code**.

## Parte 1: API REST em C\#

A API foi construída com ASP.NET Core e segue os padrões REST.

### Configuração e Execução

1.  **Clonar o Repositório:**

    ```bash
    git clone <url-do-seu-repositorio>
    cd <pasta-do-projeto>
    ```

2.  **Restaurar Dependências:**
    Abra um terminal na pasta do projeto e execute o comando:

    ```bash
    dotnet restore
    dotnet build
    ```

3.  **Executar a Aplicação:**
    Você pode executar a aplicação de duas maneiras:

      * **Pelo Visual Studio:**

          * Abra o arquivo da solução (`.sln`).
          * Pressione `F5` ou clique no botão de "Play" para iniciar o projeto.

      * **Pela Linha de Comando:**

        ```bash
        dotnet run
        ```

    A API estará em execução e acessível em `https://localhost:7065`, e a interface do Swagger será aberta automaticamente no seu navegador.

### Como Testar a API (usando o Swagger)

Todos os endpoints de `Pessoas`, com exceção da rota de autenticação, exigem um token de autenticação para serem acessados.

**1. Obter o Token de Acesso:**

  * Na interface do Swagger, localize o endpoint `POST /api/Auth/login`.
  * Clique em "Try it out" e utilize as seguintes credenciais no corpo da requisição:
    ```json
    {
      "username": "admin",
      "password": "12345"
    }
    ```
  * Execute a chamada. A resposta conterá um token JWT. Copie a string do token.

**2. Autorizar as Requisições no Swagger:**

  * Clique no botão **"Authorize"** no topo direito da página.
  * Na janela que abrir, cole o token no campo "Value" no seguinte formato: `Bearer <seu_token_copiado>`.
  * Clique em "Authorize" para fechar a janela.

**3. Testar os Endpoints de Pessoas:**

  * Agora você pode executar qualquer um dos endpoints do `PessoasController` (GET, POST, PUT, DELETE). O Swagger irá enviar automaticamente o token de autorização em cada requisição.

## Parte 2: Consulta SQL

O script SQL resolve o segundo desafio, que consiste em unificar dados financeiros.

### Execução do Script

1.  **Abra o SQL Server Management Studio (SSMS)** e conecte-se à sua instância do SQL Server.
2.  Abra o arquivo sql que está neste repositório.
3.  Execute o script inteiro pressionando `F5` ou clicando em "Execute". O resultado final da consulta será exibido na aba de "Results".
4.  Se necessário também está no repositorio a consuolta de create e insert da tabela

### A Consulta Resolvida

```sql
-- Consulta para unificar informações de contas a pagar e contas pagas
SELECT
    cap.Numero AS NumeroProcesso,
    p.Nome AS NomeFornecedor,
    cap.DataVencimento,
    NULL AS DataPagamento,
    (cap.Valor + cap.Acrescimo - cap.Desconto) AS ValorLiquido,
    'A Pagar' AS Status
FROM
    ContasAPagar AS cap
INNER JOIN
    Pessoas AS p ON cap.CodigoFornecedor = p.Codigo

UNION ALL

SELECT
    cpg.Numero AS NumeroProcesso,
    p.Nome AS NomeFornecedor,
    cpg.DataVencimento,
    cpg.DataPagamento,
    (cpg.Valor + cpg.Acrescimo - cpg.Desconto) AS ValorLiquido,
    'Paga' AS Status
FROM
    ContasPagas AS cpg
INNER JOIN
    Pessoas AS p ON cpg.CodigoFornecedor = p.Codigo

ORDER BY
    NumeroProcesso;
```
