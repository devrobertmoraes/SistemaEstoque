# 📦 Sistema de Gestão de Estoque

Projeto de um sistema de gestão de estoque construído do zero, com foco em boas práticas de arquitetura de software, SOLID e acesso a dados com Dapper. Este projeto foi desenvolvido como parte de um estudo aprofundado sobre desenvolvimento backend com .NET.

## 🛠️ Tecnologias Utilizadas

* **.NET 9**
* **C#**
* **Dapper**: Micro-ORM para acesso a dados.
* **SQL Server**: Banco de dados relacional.
* **Arquitetura em Camadas**: Para separação de responsabilidades.
* **Injeção de Dependência**: Para desacoplamento de código.

## 🏛️ Arquitetura

O projeto segue um padrão de Arquitetura em Camadas para garantir um código limpo, testável e de fácil manutenção.

#### Diagrama de Classes (UML)

+------------------+           +----------------------+        +------------------+
|      Usuario     |<----------|  MovimentacaoEstoque |------->|     Produto      |
+------------------+ 1       * +----------------------+ * 1 +------------------+
| - Id: Guid       |           | - Id: Guid           |          | - Id: Guid       |
| - NomeCompleto   |           | - DataHora: DateTime |          | - Nome           |
| - NomeDeUsuario  |           | - Quantidade: int    |          | - Descricao      |
| - SenhaHash      |           | - Tipo               |          | - Preco          |
| - Cargo          |           | - ProdutoId: Guid    |          | - Sku            |
+------------------+           | - UsuarioId: Guid    |          +------------------+
                               +----------------------+

## 🚀 Como Executar o Projeto

Siga os passos abaixo para executar o projeto em sua máquina local.

### Pré-requisitos

* [.NET SDK](https://dotnet.microsoft.com/download) (versão 9 ou superior).
* [SQL Server Express](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads) ou outra edição.

### Passo a Passo

1.  **Clone o repositório:**
    ```bash
    git clone [https://github.com/devrobertmoraes/SistemaEstoque.git](https://github.com/seu-usuario/SistemaEstoque.git)
    cd SistemaEstoque
    ```

2.  **Crie o Banco de Dados:**
    * Abra o SQL Server Management Studio ou o Azure Data Studio.
    * Abra o arquivo `/Database/schema.sql`.
    * Execute o script para criar o banco `SistemaEstoqueDB` e suas tabelas.

3.  **Configure a Conexão:**
    * Na pasta `src/ConsoleApp/`, encontre o arquivo `appsettings.json`.
    * Altere a `DefaultConnection` para apontar para a sua instância do SQL Server, se necessário.

4.  **Execute a Aplicação:**
    * Abra um terminal na raiz do projeto.
    * Execute o seguinte comando:
        ```bash
        dotnet run --project src/ConsoleApp/SistemaEstoque.ConsoleApp.csproj
        ```

O menu interativo da aplicação deverá aparecer no seu terminal.