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

BACKUP_NAME="ecis_db_v2_${FILENAME}.bak"

docker exec -it mssql mkdir -p /var/opt/mssql/backup
docker exec -it mssql /opt/mssql-tools/bin/sqlcmd \
   -S localhost -U SA -P 'EcisDBSecret2021' \
   -Q "BACKUP DATABASE [ecis_db_v2] TO DISK = N'/var/opt/mssql/backup/${BACKUP_NAME}' WITH NOFORMAT, NOINIT, NAME = 'ecis_db_v2-full', SKIP, NOREWIND, NOUNLOAD, STATS = 10"

docker cp mssql:"/var/opt/mssql/backup/${BACKUP_NAME}" "../backup/${BACKUP_NAME}"

chown ubuntu "../backup/${BACKUP_NAME}"

echo "Backup created at ${BACKUP_NAME}"
