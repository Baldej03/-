CREATE TABLE [dbo].[Applicants]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(50) NULL, 
    [Surname] NVARCHAR(50) NULL, 
    [Birthday] DATE NULL, 
    [PhysicsScores] NVARCHAR(50) NULL, 
    [MathScores] NVARCHAR(50) NULL, 
    [RusScores] NVARCHAR(50) NULL, 
    [CompScienceScores] NVARCHAR(50) NULL, 
    [AverageScore] NVARCHAR(50) NULL, 
    [Allowance] NVARCHAR(50) NULL
)
