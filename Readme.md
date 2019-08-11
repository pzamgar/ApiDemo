# Demo WebApi and Continuous deployment

Master |
------ |
[![Build status](https://ci.appveyor.com/api/projects/status/6r9dr4v54dcvl821?svg=true)](https://ci.appveyor.com/project/pzamgar/apidemo) |


Demo practica de una webapi realizada en .net core.
Ejecucion webapi en docker y continuous deployment con appveyor.

> IMPORTANTE
>
> La rama master corre sobre .net core 2.2.


## Running the sample using Docker

Construir y levantar Demo en Docker:
> docker-compose build
>
> docker-compose up

Documentacion OpenApi con Swagger:
> http://localhost:3000/index.html

Gestion de los logs registrados con Seq:
> http://localhost:5341
