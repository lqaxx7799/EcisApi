# Stop any left-over services
# stop
localServices=$1

developmentServices=($(echo ${services[@]} ${localServices[@]} | tr ' ' '\n' | sort | uniq -u))

echo "Starting service containers"
docker-compose -p EcisApi up -d

source ./logs.sh

