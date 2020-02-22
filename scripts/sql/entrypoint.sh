#!/bin/bash
database=MyDatabase
wait_time=30
password=Passw0rd!

echo importing data will start in $wait_time...
sleep $wait_time
echo importing data...
echo create database ...
/opt/mssql-tools/bin/sqlcmd -S 0.0.0.0 -U sa -P $password -i ./init.sql

echo create table
for file in "./table/*.sql"
do
  echo executing $file
  /opt/mssql-tools/bin/sqlcmd -S 0.0.0.0 -U sa -P $password -i $file
done

:'
echo importing data csv
for file in "./data/*.csv"
do
  shortname=$(echo $file | cut -f 3 -d '/' | cut -f 1 -d '.')
  tableName=$database.dbo.$shortname
  echo importing $tableName from $file
  /opt/mssql-tools/bin/bcp $tableName in $file -c -t "," -F 2 -S 0.0.0.0 -U sa -P $password
done
'
