# Demo WebApi notes and Continuous deployment

Appveyor [![Build status](https://ci.appveyor.com/api/projects/status/6r9dr4v54dcvl821?svg=true)](https://ci.appveyor.com/project/pzamgar/apidemo)

Demo practica de una webapi realizada en .net core.

Sencilla webapi que realiza la gestion de notas.
Ejecucion de la webapi en docker y continuous deployment con appveyor.

> IMPORTANTE
>
> La rama master corre sobre .net core 2.2.

## Running the sample using Docker
En directorio raiz del proyecto ejecutar los comandos siguientes:
> docker-compose build
>
> docker-compose up

Se muestra la documentacion de la api en swagger en localhost:
> http://localhost:3000/index.html

