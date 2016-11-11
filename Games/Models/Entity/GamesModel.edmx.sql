
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/11/2016 15:11:23
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
IF OBJECT_ID(N'[dbo].[FK_game_gender_game]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[game_gender] DROP CONSTRAINT [FK_game_gender_game];
GO
IF OBJECT_ID(N'[dbo].[FK_game_gender_gender]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[game_gender] DROP CONSTRAINT [FK_game_gender_gender];
GO
IF OBJECT_ID(N'[dbo].[FK_game_platform_game]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[game_platform] DROP CONSTRAINT [FK_game_platform_game];
GO
IF OBJECT_ID(N'[dbo].[FK_game_platform_platform]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[game_platform] DROP CONSTRAINT [FK_game_platform_platform];
GO
IF OBJECT_ID(N'[dbo].[FK_game_rating]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[game] DROP CONSTRAINT [FK_game_rating];
GO
IF OBJECT_ID(N'[dbo].[FK_game_store]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[game] DROP CONSTRAINT [FK_game_store];
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
IF OBJECT_ID(N'[dbo].[game_gender]', 'U') IS NOT NULL
    DROP TABLE [dbo].[game_gender];
GO
IF OBJECT_ID(N'[dbo].[game_platform]', 'U') IS NOT NULL
    DROP TABLE [dbo].[game_platform];
GO
IF OBJECT_ID(N'[dbo].[gender]', 'U') IS NOT NULL
    DROP TABLE [dbo].[gender];
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
IF OBJECT_ID(N'[dbo].[store]', 'U') IS NOT NULL
    DROP TABLE [dbo].[store];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'developerPublisher'
CREATE TABLE [dbo].[developerPublisher] (
    [id] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'game'
CREATE TABLE [dbo].[game] (
    [id] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(max)  NOT NULL,
    [nota] nchar(10)  NULL,
    [release_date] datetime  NULL,
    [preco] decimal(2,0)  NULL,
    [metacritic] int  NULL,
    [completo] int  NOT NULL,
    [summary] varchar(max)  NULL,
    [formato] varchar(50)  NULL,
    [tamanho] nchar(10)  NULL,
    [id_store] int  NULL,
    [id_rating] int  NULL
);
GO

-- Creating table 'game_developerPublisher'
CREATE TABLE [dbo].[game_developerPublisher] (
    [id] int IDENTITY(1,1) NOT NULL,
    [id_game] int  NOT NULL,
    [id_developerPublisher] int  NOT NULL
);
GO

-- Creating table 'game_gender'
CREATE TABLE [dbo].[game_gender] (
    [id] int IDENTITY(1,1) NOT NULL,
    [id_game] int  NOT NULL,
    [id_gender] int  NOT NULL
);
GO

-- Creating table 'game_platform'
CREATE TABLE [dbo].[game_platform] (
    [id] int IDENTITY(1,1) NOT NULL,
    [id_game] int  NOT NULL,
    [id_platform] int  NOT NULL
);
GO

-- Creating table 'gender'
CREATE TABLE [dbo].[gender] (
    [id] int IDENTITY(1,1) NOT NULL,
    [gender1] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'platform'
CREATE TABLE [dbo].[platform] (
    [id] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'rating'
CREATE TABLE [dbo].[rating] (
    [id] int IDENTITY(1,1) NOT NULL,
    [rating1] nvarchar(50)  NULL,
    [idade] int  NULL,
    [id_regiao] int  NOT NULL
);
GO

-- Creating table 'region'
CREATE TABLE [dbo].[region] (
    [id] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'store'
CREATE TABLE [dbo].[store] (
    [id] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(50)  NOT NULL
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

-- Creating primary key on [id] in table 'game_gender'
ALTER TABLE [dbo].[game_gender]
ADD CONSTRAINT [PK_game_gender]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'game_platform'
ALTER TABLE [dbo].[game_platform]
ADD CONSTRAINT [PK_game_platform]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'gender'
ALTER TABLE [dbo].[gender]
ADD CONSTRAINT [PK_gender]
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

-- Creating foreign key on [id_game] in table 'game_gender'
ALTER TABLE [dbo].[game_gender]
ADD CONSTRAINT [FK_game_gender_game]
    FOREIGN KEY ([id_game])
    REFERENCES [dbo].[game]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_game_gender_game'
CREATE INDEX [IX_FK_game_gender_game]
ON [dbo].[game_gender]
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

-- Creating foreign key on [id_store] in table 'game'
ALTER TABLE [dbo].[game]
ADD CONSTRAINT [FK_game_store]
    FOREIGN KEY ([id_store])
    REFERENCES [dbo].[store]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_game_store'
CREATE INDEX [IX_FK_game_store]
ON [dbo].[game]
    ([id_store]);
GO

-- Creating foreign key on [id_gender] in table 'game_gender'
ALTER TABLE [dbo].[game_gender]
ADD CONSTRAINT [FK_game_gender_gender]
    FOREIGN KEY ([id_gender])
    REFERENCES [dbo].[gender]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_game_gender_gender'
CREATE INDEX [IX_FK_game_gender_gender]
ON [dbo].[game_gender]
    ([id_gender]);
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

-- Creating foreign key on [id_rating] in table 'game'
ALTER TABLE [dbo].[game]
ADD CONSTRAINT [FK_game_rating]
    FOREIGN KEY ([id_rating])
    REFERENCES [dbo].[rating]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_game_rating'
CREATE INDEX [IX_FK_game_rating]
ON [dbo].[game]
    ([id_rating]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------