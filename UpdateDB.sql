USE JoeSoft
ALTER TABLE Items ADD Col2 nvarchar(100) 
GO

UPDATE Items SET Col2 = 'This is a Col2 value'
GO
