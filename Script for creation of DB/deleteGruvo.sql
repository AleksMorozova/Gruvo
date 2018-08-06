USE master;
GO

ALTER DATABASE Gruvo 
SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
DROP DATABASE Gruvo;
PRINT 'Database "Gruvo" has been deleted successfully'
GO