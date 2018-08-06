cd ..\Database
SET db_dir=%cd%
cd "..\Script for creation of DB"
sqlcmd -i createGruvo.sql
pause
