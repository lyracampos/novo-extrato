use [master]
CREATE DATABASE [NovoExtrato.Consulta];
GO

use [NovoExtrato.Consulta]
GO
CREATE TABLE [__EFMigrationsHistory] (
    [MigrationId] nvarchar(150) NOT NULL,
    [ProductVersion] nvarchar(32) NOT NULL,
    CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
);
GO

CREATE TABLE [Extrato] (
    [Id] int NOT NULL IDENTITY,
    [ClienteId] int NOT NULL,
    [CriadoEm] datetime2 NOT NULL,
    [Tipo] int NOT NULL,
    [ValorTransacao] decimal(18,2) NOT NULL,
    [Descricao] varchar(255) NOT NULL,
    [ContaOrigem] nvarchar(max) NULL,
    [NomeClienteOrigem] varchar(255) NOT NULL,
    [ContaDestino] nvarchar(max) NULL,
    [NomeClienteDestino] varchar(255) NOT NULL,
    [Saldo] decimal(18,2) NOT NULL,
    [TransacaoTipo] int NOT NULL,
    CONSTRAINT [PK_Extrato] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20191124031402_initial', N'2.2.6-servicing-10079');
GO