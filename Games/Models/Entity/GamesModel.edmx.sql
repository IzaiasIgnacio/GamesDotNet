
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/28/2016 12:05:14
-- Generated from EDMX file: F:\new\Games\Models\Entity\GamesModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Games];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_game_developerPublisher_developerPublisher]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[game_developerPublisher] DROP CONSTRAINT [FK_game_developerPublisher_developerPublisher];
GO
IF OBJECT_ID(N'[dbo].[FK_game_developerPublisher_game]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[game_developerPublisher] DROP CONSTRAINT [FK_game_developerPublisher_game];
GO
IF OBJECT_ID(N'[dbo].[FK_game_genre_game]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[game_genre] DROP CONSTRAINT [FK_game_genre_game];
GO
IF OBJECT_ID(N'[dbo].[FK_game_genre_genre]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[game_genre] DROP CONSTRAINT [FK_game_genre_genre];
GO
IF OBJECT_ID(N'[dbo].[FK_game_platform_game]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[game_platform] DROP CONSTRAINT [FK_game_platform_game];
GO
IF OBJECT_ID(N'[dbo].[FK_game_platform_platform]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[game_platform] DROP CONSTRAINT [FK_game_platform_platform];
GO
IF OBJECT_ID(N'[dbo].[FK_game_platform_rating]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[game_platform] DROP CONSTRAINT [FK_game_platform_rating];
GO
IF OBJECT_ID(N'[dbo].[FK_game_platform_region]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[game_platform] DROP CONSTRAINT [FK_game_platform_region];
GO
IF OBJECT_ID(N'[dbo].[FK_game_platform_status]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[game_platform] DROP CONSTRAINT [FK_game_platform_status];
GO
IF OBJECT_ID(N'[dbo].[FK_game_platform_store]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[game_platform] DROP CONSTRAINT [FK_game_platform_store];
GO
IF OBJECT_ID(N'[dbo].[FK_rating_region]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[rating] DROP CONSTRAINT [FK_rating_region];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[developerPublisher]', 'U') IS NOT NULL
    DROP TABLE [dbo].[developerPublisher];
GO
IF OBJECT_ID(N'[dbo].[game]', 'U') IS NOT NULL
    DROP TABLE [dbo].[game];
GO
IF OBJECT_ID(N'[dbo].[game_developerPublisher]', 'U') IS NOT NULL
    DROP TABLE [dbo].[game_developerPublisher];
GO
IF OBJECT_ID(N'[dbo].[game_genre]', 'U') IS NOT NULL
    DROP TABLE [dbo].[game_genre];
GO
IF OBJECT_ID(N'[dbo].[game_platform]', 'U') IS NOT NULL
    DROP TABLE [dbo].[game_platform];
GO
IF OBJECT_ID(N'[dbo].[genre]', 'U') IS NOT NULL
    DROP TABLE [dbo].[genre];
GO
IF OBJECT_ID(N'[dbo].[platform]', 'U') IS NOT NULL
    DROP TABLE [dbo].[platform];
GO
IF OBJECT_ID(N'[dbo].[rating]', 'U') IS NOT NULL
    DROP TABLE [dbo].[rating];
GO
IF OBJECT_ID(N'[dbo].[region]', 'U') IS NOT NULL
    DROP TABLE [dbo].[region];
GO
IF OBJECT_ID(N'[dbo].[status]', 'U') IS NOT NULL
    DROP TABLE [dbo].[status];
GO
IF OBJECT_ID(N'[dbo].[store]', 'U') IS NOT NULL
    DROP TABLE [dbo].[store];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'developerPublisher'
CREATE TABLE [dbo].[developerPublisher] (
    [id] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(50)  NOT NULL,
    [id_igdb] int  NOT NULL
);
GO

-- Creating table 'game'
CREATE TABLE [dbo].[game] (
    [id] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(max)  NOT NULL,
    [summary] varchar(max)  NULL,
    [cloudnary_id] nvarchar(50)  NULL,
    [id_igdb] int  NULL,
    [completo] bit  NOT NULL,
    [nota] decimal(3,1)  NULL
);
GO

-- Creating table 'game_developerPublisher'
CREATE TABLE [dbo].[game_developerPublisher] (
    [id] int IDENTITY(1,1) NOT NULL,
    [id_game] int  NOT NULL,
    [id_developerPublisher] int  NOT NULL,
    [tipo] int  NOT NULL
);
GO

-- Creating table 'game_platform'
CREATE TABLE [dbo].[game_platform] (
    [id] int IDENTITY(1,1) NOT NULL,
    [id_game] int  NOT NULL,
    [id_platform] int  NOT NULL,
    [id_status] int  NOT NULL,
    [release_date] datetime  NULL,
    [id_region] int  NULL,
    [id_rating] int  NULL,
    [metacritic] int  NULL,
    [preco] decimal(5,2)  NULL,
    [formato] int  NULL,
    [tamanho] decimal(5,2)  NULL,
    [id_store] int  NULL
);
GO

-- Creating table 'platform'
CREATE TABLE [dbo].[platform] (
    [id] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(50)  NOT NULL,
    [sigla] nchar(6)  NOT NULL,
    [ordem] int  NOT NULL,
    [id_igdb] int  NULL
);
GO

-- Creating table 'rating'
CREATE TABLE [dbo].[rating] (
    [id] int IDENTITY(1,1) NOT NULL,
    [idade] int  NULL,
    [id_regiao] int  NOT NULL,
    [name] nvarchar(50)  NULL
);
GO

-- Creating table 'region'
CREATE TABLE [dbo].[region] (
    [id] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(50)  NOT NULL,
    [sigla] char(2)  NOT NULL
);
GO

-- Creating table 'store'
CREATE TABLE [dbo].[store] (
    [id] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'status'
CREATE TABLE [dbo].[status] (
    [id] int IDENTITY(1,1) NOT NULL,
    [name] nchar(10)  NOT NULL
);
GO

-- Creating table 'game_genre'
CREATE TABLE [dbo].[game_genre] (
    [id] int IDENTITY(1,1) NOT NULL,
    [id_game] int  NOT NULL,
    [id_genre] int  NOT NULL
);
GO

-- Creating table 'genre'
CREATE TABLE [dbo].[genre] (
    [id] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(50)  NOT NULL,
    [id_igdb] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [id] in table 'developerPublisher'
ALTER TABLE [dbo].[developerPublisher]
ADD CONSTRAINT [PK_developerPublisher]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'game'
ALTER TABLE [dbo].[game]
ADD CONSTRAINT [PK_game]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'game_developerPublisher'
ALTER TABLE [dbo].[game_developerPublisher]
ADD CONSTRAINT [PK_game_developerPublisher]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'game_platform'
ALTER TABLE [dbo].[game_platform]
ADD CONSTRAINT [PK_game_platform]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'platform'
ALTER TABLE [dbo].[platform]
ADD CONSTRAINT [PK_platform]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'rating'
ALTER TABLE [dbo].[rating]
ADD CONSTRAINT [PK_rating]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'region'
ALTER TABLE [dbo].[region]
ADD CONSTRAINT [PK_region]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'store'
ALTER TABLE [dbo].[store]
ADD CONSTRAINT [PK_store]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'status'
ALTER TABLE [dbo].[status]
ADD CONSTRAINT [PK_status]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'game_genre'
ALTER TABLE [dbo].[game_genre]
ADD CONSTRAINT [PK_game_genre]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'genre'
ALTER TABLE [dbo].[genre]
ADD CONSTRAINT [PK_genre]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [id_developerPublisher] in table 'game_developerPublisher'
ALTER TABLE [dbo].[game_developerPublisher]
ADD CONSTRAINT [FK_game_developerPublisher_developerPublisher]
    FOREIGN KEY ([id_developerPublisher])
    REFERENCES [dbo].[developerPublisher]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_game_developerPublisher_developerPublisher'
CREATE INDEX [IX_FK_game_developerPublisher_developerPublisher]
ON [dbo].[game_developerPublisher]
    ([id_developerPublisher]);
GO

-- Creating foreign key on [id_game] in table 'game_developerPublisher'
ALTER TABLE [dbo].[game_developerPublisher]
ADD CONSTRAINT [FK_game_developerPublisher_game]
    FOREIGN KEY ([id_game])
    REFERENCES [dbo].[game]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_game_developerPublisher_game'
CREATE INDEX [IX_FK_game_developerPublisher_game]
ON [dbo].[game_developerPublisher]
    ([id_game]);
GO

-- Creating foreign key on [id_game] in table 'game_platform'
ALTER TABLE [dbo].[game_platform]
ADD CONSTRAINT [FK_game_platform_game]
    FOREIGN KEY ([id_game])
    REFERENCES [dbo].[game]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_game_platform_game'
CREATE INDEX [IX_FK_game_platform_game]
ON [dbo].[game_platform]
    ([id_game]);
GO

-- Creating foreign key on [id_platform] in table 'game_platform'
ALTER TABLE [dbo].[game_platform]
ADD CONSTRAINT [FK_game_platform_platform]
    FOREIGN KEY ([id_platform])
    REFERENCES [dbo].[platform]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_game_platform_platform'
CREATE INDEX [IX_FK_game_platform_platform]
ON [dbo].[game_platform]
    ([id_platform]);
GO

-- Creating foreign key on [id_regiao] in table 'rating'
ALTER TABLE [dbo].[rating]
ADD CONSTRAINT [FK_rating_region]
    FOREIGN KEY ([id_regiao])
    REFERENCES [dbo].[region]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_rating_region'
CREATE INDEX [IX_FK_rating_region]
ON [dbo].[rating]
    ([id_regiao]);
GO

-- Creating foreign key on [id_status] in table 'game_platform'
ALTER TABLE [dbo].[game_platform]
ADD CONSTRAINT [FK_game_platform_status]
    FOREIGN KEY ([id_status])
    REFERENCES [dbo].[status]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_game_platform_status'
CREATE INDEX [IX_FK_game_platform_status]
ON [dbo].[game_platform]
    ([id_status]);
GO

-- Creating foreign key on [id_rating] in table 'game_platform'
ALTER TABLE [dbo].[game_platform]
ADD CONSTRAINT [FK_game_platform_rating]
    FOREIGN KEY ([id_rating])
    REFERENCES [dbo].[rating]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_game_platform_rating'
CREATE INDEX [IX_FK_game_platform_rating]
ON [dbo].[game_platform]
    ([id_rating]);
GO

-- Creating foreign key on [id_region] in table 'game_platform'
ALTER TABLE [dbo].[game_platform]
ADD CONSTRAINT [FK_game_platform_region]
    FOREIGN KEY ([id_region])
    REFERENCES [dbo].[region]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_game_platform_region'
CREATE INDEX [IX_FK_game_platform_region]
ON [dbo].[game_platform]
    ([id_region]);
GO

-- Creating foreign key on [id_game] in table 'game_genre'
ALTER TABLE [dbo].[game_genre]
ADD CONSTRAINT [FK_game_genre_game]
    FOREIGN KEY ([id_game])
    REFERENCES [dbo].[game]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_game_genre_game'
CREATE INDEX [IX_FK_game_genre_game]
ON [dbo].[game_genre]
    ([id_game]);
GO

-- Creating foreign key on [id_genre] in table 'game_genre'
ALTER TABLE [dbo].[game_genre]
ADD CONSTRAINT [FK_game_genre_genre]
    FOREIGN KEY ([id_genre])
    REFERENCES [dbo].[genre]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_game_genre_genre'
CREATE INDEX [IX_FK_game_genre_genre]
ON [dbo].[game_genre]
    ([id_genre]);
GO

-- Creating foreign key on [id_store] in table 'game_platform'
ALTER TABLE [dbo].[game_platform]
ADD CONSTRAINT [FK_game_platform_store]
    FOREIGN KEY ([id_store])
    REFERENCES [dbo].[store]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_game_platform_store'
CREATE INDEX [IX_FK_game_platform_store]
ON [dbo].[game_platform]
    ([id_store]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------