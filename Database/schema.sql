-- Verifica se o banco de dados já existe e cria um se não existir
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'SistemaEstoqueDB')
BEGIN
    CREATE DATABASE SistemaEstoqueDB;
END
GO -- Comando para separar lotes de instruções SQL

-- muda o contexto para o nosso novo banco de dados
USE SistemaEstoqueDB;
GO

-- cria a tabela de Usuários
CREATE TABLE Usuarios (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    NomeCompleto NVARCHAR(200) NOT NULL,
    NomeDeUsuario NVARCHAR(50) NOT NULL UNIQUE,
    SenhaHash NVARCHAR(MAX) NOT NULL,
    Cargo INT NOT NULL -- 0: Operador e 1: Admin
);
GO

-- Cria a tabela de Produtos
CREATE TABLE Produtos (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    Nome NVARCHAR(150) NOT NULL,
    Descricao NVARCHAR(500) NULL,
    Preco DECIMAL(18, 2) NOT NULL,
    Sku VARCHAR(50) NOT NULL UNIQUE -- Sku: Stock Keeping Unit
);
GO

-- Cria a tabela de Movimentações de Estoque
CREATE TABLE MovimentacoesEstoque (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    ProdutoId UNIQUEIDENTIFIER NOT NULL,
    UsuarioId UNIQUEIDENTIFIER NOT NULL,
    DataHora DATETIME2 NOT NULL,
    Quantidade INT NOT NULL,
    Tipo INT NOT NULL, -- 0: Entrada, 1: Saida, 2: Ajuste

    -- Define as chaves estrangeiras (FKs) e as relações
    CONSTRAINT FK_Movimentacao_Produto FOREIGN KEY (ProdutoId) REFERENCES Produtos(Id),
    CONSTRAINT FK_Movimentacao_Usuario FOREIGN KEY (UsuarioId) REFERENCES Usuarios(Id)
);