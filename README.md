# Series Backend

This project uses:
- [postgresql](https://www.postgresql.org/)
- [docker compose](https://docs.docker.com/compose/)
- [.net 6](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
- [asp.net core](https://docs.microsoft.com/en-us/aspnet/core/introduction-to-aspnet-core?view=aspnetcore-6.0)
- [entity framework core](https://docs.microsoft.com/en-us/ef/core/)

### Running
Docker file and Docker compose are already provided.
You only need to run ```docker compose up -d``` inside the root of the repository.

### Development Ports
Api:
- ```http://localhost:5256```
- ```https://localhost:7258```

### Production Ports
Docker ports that are going to be exposed are:

Api:
- ```80``` that exports itself to ```https://localhost:7257```

PostgreSQL:
- ```5432``` that exports itself to ```http://localhost:5432```

### Swagger
The entire project is documented via swagger,
if you are running a **dev environment**,
you can access the swagger ui (endpoints, controllers, etc) at:
[https://localhost:7258/swagger/index.html](https://localhost:7258/swagger/index.html)

### PgAdmin
Is a web Open Source administration and development platform for PostgreSQL
and it is included with the docker compose file,
if you want to see the database with it, just 
build it and access [http://localhost:8080](http://localhost:8080)
