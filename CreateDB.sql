CREATE DATABASE JoeSoft
GO

USE JoeSoft
CREATE TABLE Items (Id int NOT NULL IDENTITY(1,1) PRIMARY KEY, Col1 nvarchar(100) NULL)
GO

INSERT INTO Items (Col1) VALUES ('This is the Col1 from local')
GO


