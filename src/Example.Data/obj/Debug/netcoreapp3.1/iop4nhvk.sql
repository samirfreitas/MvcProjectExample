IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Vendors] (
    [Id] uniqueidentifier NOT NULL,
    [Name] varchar(200) NOT NULL,
    [Document] varchar(14) NOT NULL,
    [VendorType] int NOT NULL,
    [Active] bit NOT NULL,
    CONSTRAINT [PK_Vendors] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Adresses] (
    [Id] uniqueidentifier NOT NULL,
    [VendorId] uniqueidentifier NOT NULL,
    [PublicPlace] varchar(200) NOT NULL,
    [Number] varchar(50) NOT NULL,
    [Complement] varchar(255) NOT NULL,
    [ZipCode] varchar(8) NOT NULL,
    [District] varchar(100) NOT NULL,
    [City] varchar(100) NOT NULL,
    [State] varchar(50) NOT NULL,
    CONSTRAINT [PK_Adresses] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Adresses_Vendors_VendorId] FOREIGN KEY ([VendorId]) REFERENCES [Vendors] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Products] (
    [Id] uniqueidentifier NOT NULL,
    [VendorId] uniqueidentifier NOT NULL,
    [Name] varchar(200) NOT NULL,
    [Description] varchar(1000) NOT NULL,
    [Image] varchar(1000) NOT NULL,
    [Value] decimal(18,2) NOT NULL,
    [DateRegister] datetime2 NOT NULL,
    [Active] bit NOT NULL,
    CONSTRAINT [PK_Products] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Products_Vendors_VendorId] FOREIGN KEY ([VendorId]) REFERENCES [Vendors] ([Id]) ON DELETE NO ACTION
);
GO

CREATE UNIQUE INDEX [IX_Adresses_VendorId] ON [Adresses] ([VendorId]);
GO

CREATE INDEX [IX_Products_VendorId] ON [Products] ([VendorId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210306004825_Initial', N'5.0.3');
GO

COMMIT;
GO

