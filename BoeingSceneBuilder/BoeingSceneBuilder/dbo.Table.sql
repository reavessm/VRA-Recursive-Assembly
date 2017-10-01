CREATE TABLE [dbo].[Table]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Name] VARCHAR(MAX) NULL, 
    [Date Added] DATETIME NULL, 
    [Fastest Time] TIME NULL, 
    [Trash] BIT NULL
)
