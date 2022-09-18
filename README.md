# SMS-service

## _How to run application_

- Change directory into src folder and run:

```sh
docker-compose up
```

- Then open http://localhost:5000/swagger

In case of failure some commands for running manually:

```sh
docker run -p 15672:15672 -p 5672:5672 masstransit/rabbitmq

docker run --name fdx-test-13.3 -p 5432:5432 -e POSTGRES_USER=fdxuser -e POSTGRES_PASSWORD=Aa123456 -e POSTGRES_DB=fdxdb -d postgres:13.3
```