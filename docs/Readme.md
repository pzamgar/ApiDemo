# Dependencies Project

## Content Dependecies Package

- [Swagger OpenApi](#Swagger-OpenApi) 
- [Api versioning and Configuration swagger](#Api-versioning-and-Configuration-swagger)
- [Login Serilog](#Login-Serilog)
- [Monitorizar Login SEQ](#Monitorizar-Login-SEQ)
- [Health Check WebApi](#Health-Check-WebApi)
- [Database with EF](#Database-with-EF)

## Swagger OpenApi

Generacion documentacion de la api.

Instalacion de paquetes:

- Swashbuckle.AspNetCore (5.0.0-rc2) version prerelease.

Referencias:

- https://github.com/domaindrivendev/Swashbuckle.AspNetCore
- https://docs.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-2.1&tabs=visual-studio-code
- https://github.com/microsoft/OpenAPI.NET

## Api versioning and Configuration swagger

Versionado de la api.

Instalacion de paquetes:

- Microsoft.AspNetCore.Mvc.ApiExplorer (2.2.0)
- Microsoft.AspNetCore.Mvc.Versioning (3.1.4)
- Microsoft.AspNetCore.Mvc.Versioning.ApiExplorer (3.2.0)

Referencias:

- https://github.com/microsoft/aspnet-api-versioning/wiki/API-Documentation
- https://samanthaneilen.github.io/2019/01/19/Web-API-versioning-using-the-NuGet-packages-from-Microsoft.html

## Login Serilog

Loggin con la biblioteca Serilog

Instalacion de paquetes:

- Microsoft.Extensions.Logging
- Serilog.AspNetCore
- Serilog.Settings.Configuration
- Serilog.Sinks.File
- Serilog.Sinks.Async

Referencias:

- https://docs.microsoft.com/en-us/aspnet/core/fundamentals/logging/?view=aspnetcore-2.2
- https://github.com/dotnet-architecture/eShopOnContainers/wiki/Serilog-and-Seq
- https://serilog.net/
- https://github.com/serilog/serilog (<strong>wiki</strong>)
- https://github.com/serilog/serilog-settings-configuration
- https://github.com/serilog/serilog-sinks-file
- https://github.com/serilog/serilog-sinks-console
- https://github.com/serilog/serilog-enrichers-environment
- https://github.com/serilog/serilog-enrichers-thread
- https://medium.com/@matthew.bajorek/configuring-serilog-in-asp-net-core-2-2-web-api-5e0f4d89749c

## Monitorizar Login SEQ

Monitorizamos los logs de la aplicacion

Instalacion de paquetes:

- Serilog.Sinks.Seq
- Seq.Extensions.Logging

Referencias:

- https://docs.datalust.co/docs/getting-started-with-docker
- https://github.com/serilog/serilog/wiki/Provided-Sinks
- https://github.com/serilog/serilog-sinks-seq
- https://github.com/dotnet-architecture/eShopOnContainers/wiki/Serilog-and-Seq

### Ejecutar Seq en un contenedor Docker

Ejecutar la app de Seq en un contenedor de docker para no tener que instalarse Seq.

```
docker run \
  -d \
  --restart unless-stopped \
  --name seq \
  -e ACCEPT_EULA=Y \
  -v /tmp/seq/data:/data \
  -p 5341:80 \
  datalust/seq:latest
  ```

Ejecutar app Seq:
> http://localhost:5341/#/events


## Health Check WebApi

Configurar health check.

Instalacion de paquetes:

- Microsoft.AspNetCore.Diagnostics.HealthChecks
- AspNetCore.HealthChecks.UI

Referencias:

- https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/health-checks?view=aspnetcore-2.2
- https://www.telerik.com/blogs/health-checks-in-aspnet-core
- https://github.com/Xabaril/AspNetCore.Diagnostics.HealthChecks


## Database with EF

Utilizar Entity Framework para la gestion de la base de datos.

Instalacion de paquetes:

- Microsoft.EntityFrameworkCore.SqlServer

Referencias:
- https://docs.microsoft.com/en-us/aspnet/core/data/ef-mvc/intro?view=aspnetcore-2.2
- https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/
- https://stackoverflow.com/questions/48509470/ef-core-migrations-in-separate-project-issues


### Crear Migracions EF

Iniciar migracion en projecto separado del runtime
> dotnet ef --startup-project ../ApiBuildDemo.Api/ migrations add InitialModel

Crear migracion con el contexto concreto desde el projecto inicio
> dotnet ef migrations add InitialModel -c ValueContext -s ../ApiBuildDemo.Api/ -v

Update de la base de datos desde un punto concreto migrado.
> dotnet ef database update InitialModel -s ../ApiBuildDemo.Api/ -v

### Ejecutar SqlServer en un contenedor Docker

Ejecutar un servidor de SqlServer en docker para poder utilizar entity framework.

```
docker run \
  -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=P@ssw0rd1*.' \
  -p 1433:1433 --name sqlserver1 \
  -d mcr.microsoft.com/mssql/server:2017-latest
```

### Configurar Database SQL server en Docker

Configurar sql server en docker-compose y crear database con shell script

Referencias:
* https://cardano.github.io/blog/2017/11/15/mssql-docker-container
* https://www.cathrinewilhelmsen.net/2018/12/02/sql-server-2019-docker-container/
* https://dockertips.com/ms_sql_server
* http://www.andreavallotti.tech/en/2017/10/using-ef-cores-migration-with-docker-and-mysql/

### Autenticacion y Autorizacion

Se realizar la Autorizacion de la api via JWT, se genera un token para ser utilizado en las llamadas a las apis.

Instalacion de paquetes:
* Microsoft.AspNetCore.Authorization
* System.IdentityModel.Tokens.Jwt

Referencias:
* https://www.variablenotfound.com/2017/12/autenticacion-jwt-en-apis-con-aspnet.html
* https://jonhilton.net/security/apis/secure-your-asp.net-core-2.0-api-part-2---jwt-bearer-authentication/
* https://github.com/domaindrivendev/Swashbuckle.AspNetCore/blob/master/README-v5.md
* https://stackoverflow.com/questions/56234504/migrating-to-swashbuckle-aspnetcore-version-5
