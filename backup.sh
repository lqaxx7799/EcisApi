POSITIONAL_ARGS=()

while [[ $# -gt 0 ]]; do
  case $1 in
    -n|--name)
      FILENAME="$2"
      shift # past argument
      shift # past value
      ;;
    -*|--*)
      echo "Unknown option $1"
      exit 1
      ;;
    *)
      POSITIONAL_ARGS+=("$1") # save positional arg
      shift # past argument
      ;;
  esac
done

set -- "${POSITIONAL_ARGS[@]}"

if [ -z ${FILENAME} ]
then
	echo "Filename is missing!"
	exit 1
fi

echo "Backup started!"

docker exec -it mssql mkdir -p /var/opt/mssql/backup
docker exec -it mssql /opt/mssql-tools/bin/sqlcmd \
   -S localhost -U SA -P 'mssql' \
   -Q "BACKUP DATABASE [ecis_db_v2] TO DISK = N'/var/opt/mssql/backup/ecis_db_v2_${FILENAME}.bak' WITH NOFORMAT, NOINIT, NAME = 'ecis_db_v2-full', SKIP, NOREWIND, NOUNLOAD, STATS = 10"
cd ../backup
docker cp mssql:"/var/opt/mssql/backup/ecis_db_v2_${FILENAME}.bak" "ecis_db_v2_${FILENAME}.bak"

echo "Backup created at ecis_db_v2_${FILENAME}.bak"
