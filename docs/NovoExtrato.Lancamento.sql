use [master]
CREATE DATABASE [NovoExtrato.Lancamento];
GO

use [NovoExtrato.Lancamento]
GO

CREATE TABLE [__EFMigrationsHistory] (
    [MigrationId] nvarchar(150) NOT NULL,
    [ProductVersion] nvarchar(32) NOT NULL,
    CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
);
GO

CREATE TABLE [Cliente] (
    [Id] int NOT NULL IDENTITY,
    [Nome] nvarchar(255) NOT NULL,
    CONSTRAINT [PK_Cliente] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [ContaCorrente] (
    [Id] int NOT NULL IDENTITY,
    [ClienteId] int NOT NULL,
    [Numero] nvarchar(100) NOT NULL,
    [Saldo] decimal(18,2) NOT NULL,
    CONSTRAINT [PK_ContaCorrente] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ContaCorrente_Cliente_ClienteId] FOREIGN KEY ([ClienteId]) REFERENCES [Cliente] ([Id]) ON DELETE CASCADE
);
GO

CREATE UNIQUE INDEX [IX_ContaCorrente_ClienteId] ON [ContaCorrente] ([ClienteId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20191124031711_initial', N'2.2.6-servicing-10079');
GO
