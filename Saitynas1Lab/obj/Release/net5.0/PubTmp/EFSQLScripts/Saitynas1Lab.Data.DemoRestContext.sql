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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210923112916_Inil')
BEGIN
    CREATE TABLE [Posts] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        [Body] nvarchar(max) NULL,
        [CreationDateUtc] datetime2 NOT NULL,
        [TopicId] int NOT NULL,
        CONSTRAINT [PK_Posts] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210923112916_Inil')
BEGIN
    CREATE TABLE [Topics] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        [Description] nvarchar(max) NULL,
        [CreationTimeUtc] datetime2 NOT NULL,
        CONSTRAINT [PK_Topics] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210923112916_Inil')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210923112916_Inil', N'5.0.10');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210923113138_Added topic property')
BEGIN
    CREATE INDEX [IX_Posts_TopicId] ON [Posts] ([TopicId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210923113138_Added topic property')
BEGIN
    ALTER TABLE [Posts] ADD CONSTRAINT [FK_Posts_Topics_TopicId] FOREIGN KEY ([TopicId]) REFERENCES [Topics] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210923113138_Added topic property')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210923113138_Added topic property', N'5.0.10');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211116122609_DataBase')
BEGIN
    ALTER TABLE [Posts] DROP CONSTRAINT [FK_Posts_Topics_TopicId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211116122609_DataBase')
BEGIN
    DROP TABLE [Topics];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211116122609_DataBase')
BEGIN
    DROP INDEX [IX_Posts_TopicId] ON [Posts];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211116122609_DataBase')
BEGIN
    EXEC sp_rename N'[Posts].[TopicId]', N'Price', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211116122609_DataBase')
BEGIN
    EXEC sp_rename N'[Posts].[Name]', N'GameName', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211116122609_DataBase')
BEGIN
    CREATE TABLE [Orders] (
        [Id] int NOT NULL IDENTITY,
        [GameName] nvarchar(max) NULL,
        [Price] int NOT NULL,
        [Body] nvarchar(max) NULL,
        [CreationDateUtc] datetime2 NOT NULL,
        CONSTRAINT [PK_Orders] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211116122609_DataBase')
BEGIN
    CREATE TABLE [Reviews] (
        [Id] int NOT NULL IDENTITY,
        [Initiator] nvarchar(max) NULL,
        [Title] nvarchar(max) NULL,
        [Body] nvarchar(max) NULL,
        [CreationDateUtc] datetime2 NOT NULL,
        [PostId] int NOT NULL,
        CONSTRAINT [PK_Reviews] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Reviews_Posts_PostId] FOREIGN KEY ([PostId]) REFERENCES [Posts] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211116122609_DataBase')
BEGIN
    CREATE INDEX [IX_Reviews_PostId] ON [Reviews] ([PostId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211116122609_DataBase')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20211116122609_DataBase', N'5.0.10');
END;
GO

COMMIT;
GO

