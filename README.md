# Backend Taller de Introducción al Desarrollo Web/Móvil

### Felipe Rojas 19.264.425-9
### Pamela Vera 21.564.004-3

## INSTALACIÓN:
Debes instalar [Visual Studio Code](https://code.visualstudio.com/download) y el [SDK .NET8](https://dotnet.microsoft.com/es-es/download).

Para comenzar la instalación, debes abrir Visual Studio Code, ir a `File -> Open Folder`, y seleccionar la carpeta en donde quieres clonar el proyecto.

Ir a `Terminal -> New Terminal` para abrir una nueva terminal.

Ejecutar los siguientes comandos en orden: 

```bash
git clone https://github.com/itspalmera/Taller1-WebMovil.git
cd Taller1-WebMovil
dotnet restore
```
****
## CONFIGURACIÓN DEL ENTORNO

Antes de iniciar el sistema, asegúrate de crear un archivo `.env` en la raíz del proyecto con las siguientes variables:

```makefile
DATABASE_URL=Data Source=NombreDeLaBaseDeDatos.db  
JWT__Issuer=http://localhost:IngresaElPuerto
JWT__Audience=http://localhost:IngresaElPuerto
JWT__SigningKey=ColocaUnaClaveSegura ejemplo:llavesecreta123456789llave123456
```
****
## INCIAR SISTEMA:
En Visual Studio Code, ir a File -> Open Folder, y seleccionar la carpeta Taller1-WebMovil.

Ir a Terminal -> New Terminal para abrir una nueva terminal.

Ejecutar el siguiente comando:
```bash
dotnet run
```
****
## Postman

Para probar el backend usando "postman-file", necesitas instalar [Postman](https://www.postman.com/downloads/).
Al abrir Postman y elegir un espacio de trabajo, debes hacer click en "Import" y seleccionar "".
Asegurate que el puerto de las solicitudes coincida con el puerto de la ejecución.