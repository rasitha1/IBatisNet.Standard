@echo off
echo Setting up SqlLocalDB database instance...
sqllocaldb create "SqlBatis" -s
echo Executing MSSQL Database Init Script...
sqlcmd -S "(localdb)\SqlBatis" -E -i .\test\SqlBatis.DataMapper.Test\Scripts\MSSQL\DataBase.sql

echo Done