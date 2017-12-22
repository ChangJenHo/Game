CREATE TABLE [dbo].[Account]
(
    [Id]             UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [Mail]           NVARCHAR (128)   NOT NULL,
    [Password]       NVARCHAR (MAX)   NULL,
    [SecurityStamp]  NVARCHAR (MAX)   NULL,
    [Name]           NVARCHAR (128)   NOT NULL,
    [FirstName]      NVARCHAR (32)    NULL,
    [LastName]       NVARCHAR (32)    NULL,
    [NickName]       NVARCHAR (32)    NULL,
    [BirthYear]      SMALLINT         DEFAULT ((1900)) NOT NULL,
    [BirthMonth]     TINYINT          DEFAULT ((1)) NOT NULL,
    [BirthDay]       TINYINT          DEFAULT ((1)) NOT NULL,
    [Nationality]    SMALLINT         DEFAULT ((0)) NOT NULL,
    [Gender]         TINYINT          DEFAULT ((0)) NOT NULL,
    [Photo]          VARBINARY (MAX)  NULL,
    [Background]     NVARCHAR (128)   NULL,
    [Animation]      NVARCHAR (128)   NULL,
    [RecordsSorting] TINYINT          DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_dbo.Account] PRIMARY KEY CLUSTERED ([Id] ASC)
)
