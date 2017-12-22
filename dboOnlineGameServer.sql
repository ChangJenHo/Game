CREATE TABLE [dbo].[OnlineGameServer]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[Address]       NVARCHAR (MAX)   NULL, 
    [Port] INT NULL, 
    [Name] NVARCHAR(MAX) NULL, 
    [DefaultConnection] INT NULL, 
    [OnlineConnection] INT NULL, 
    [ServerStatus] INT NULL, 
    [Country] NVARCHAR(MAX) NULL

)
