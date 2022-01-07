docker exec -it mssql mkdir /var/opt/mssql/backup
docker exec -it mssql /opt/mssql-tools/bin/sqlcmd \
   -S localhost -U SA -P 'mssql' \
   -Q "BACKUP DATABASE [ecis_db_v2] TO DISK = N'/var/opt/mssql/backup/ecis_db_v2.bak' WITH NOFORMAT, NOINIT, NAME = 'ecis_db_v2-full', SKIP, NOREWIND, NOUNLOAD, STATS = 10"
cd ../backup
docker cp mssql:/var/opt/mssql/backup/ecis_db_v2.bak ecis_db_v2.bak