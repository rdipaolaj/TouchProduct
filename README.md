# ProductService README

## Descripción General
ProductService es un microservicio diseñado para gestionar información y operaciones relacionadas con productos. Este servicio permite la creación, actualización, eliminación y consulta de productos. La arquitectura está estructurada de manera modular para facilitar la escalabilidad y el mantenimiento.

---

## Estructura del Directorio

### Directorios Principales
- **product.api**: Contiene la capa API, incluyendo controladores y rutas.
- **product.application**: Lógica de negocio y casos de uso.
- **product.common**: Utilidades compartidas y constantes.
- **product.data**: Interacciones con la base de datos.
- **product.dto**: Objetos de Transferencia de Datos (DTO).
- **product.entities**: Modelos y definiciones de entidades.
- **product.handler**: Manejo de errores y respuestas.
- **product.infraestructure**: Código relacionado con infraestructura y servicios externos.
- **product.internalservices**: Servicios internos utilizados por el microservicio.
- **product.redis**: Gestión de caché y configuración de Redis.
- **product.request**: Modelos y validaciones de solicitudes.
- **product.requestvalidator**: Validaciones adicionales de las solicitudes.
- **product.secretsmanager**: Manejo seguro de secretos.
- **product.test**: Pruebas unitarias e integradas.

### Archivos de Soporte
- **.gitignore**: Archivos y directorios ignorados por Git.
- **ProductService.sln**: Archivo de solución para Visual Studio.
- **Dockerfile**: Archivo para la configuración y despliegue en Docker.
- **README.md**: Documentación del proyecto.

---

## Configuración Inicial de ProductService

### Requisitos Previos
1. **Docker Instalado**: Asegúrate de que Docker esté instalado y en funcionamiento.
2. **SDK de .NET Instalado**: Necesario para la ejecución y construcción local.
3. **Redis**: Configura una instancia de Redis.
4. **Base de Datos**: Configura la conexión para la base de datos en las variables de entorno.

### Pasos para Ejecutar

#### Usando Docker
1. Navega al directorio del proyecto:
   ```
   cd "ubicacion de la descarga"
   ```
2. Construye la imagen de Docker:
   ```
   docker build -t productservice:latest .
   ```
3. Ejecuta el contenedor Docker:
   ```
   docker run -p 5212:5212 -e "ENV_VARIABLES=values" productservice:latest
   ```
   Reemplaza `ENV_VARIABLES` con valores reales para la configuración del servicio.

#### Ejecución Local
1. Navega al directorio de la solución:
   ```
   cd C:\Users\Usuario\Desktop\Proyectos\TouchConsulting\ProductService\ProductService
   ```
2. Restaura las dependencias del proyecto:
   ```
   dotnet restore
   ```
3. Actualiza el archivo `appsettings.json` con las configuraciones necesarias:
   ```json
   {
     "Logging": {
       "LogLevel": {
         "Default": "Information",
         "Microsoft.AspNetCore": "Warning"
       }
     },
     "CorsDomains": [
       "http://localhost:4200/"
     ],
     "RedisSettings": {
       "Endpoint": "localhost:6379",
       "Local": true,
       "Active": false
     },
     "JwtSettings": {
       "Secret": "",
       "Issuer": "http://localhost:5063",
       "Audience": "http://localhost:4200/auth/login"
     },
     "SecretManagerSettings": {
       "Local": true,
       "Region": "us-east-1",
       "UseLocalstack": true,
       "ServiceURL": "http://localhost:4566",
       "AWSAccessKey": "test",
       "AWSSecretKey": "test",
       "ArnPostgresSecrets": "arn:aws:secretsmanager:us-east-1:000000000000:secret:postgres-products-kSNCEK",
       "ArnRedisSecrets": "arn:aws:secretsmanager:us-east-1:000000000000:secret:redis-qrKUBr",
       "ArnEmailSecrets": "arn:aws:secretsmanager:us-east-1:000000000000:secret:email-cLqaJO",
       "ArnJwtSecrets": "arn:aws:secretsmanager:us-east-1:000000000000:secret:jwtkey-kbmfXb"
     },
     "ApiSettings": {
       "UrlMsUser": "http://localhost:5106/"
     }
   }
   ```
4. Ejecuta el proyecto:
   ```
   dotnet run --project product.api
   ```
5. Accede al servicio en:
   ```
   http://localhost:5212
   ```

---

## Variables de Entorno

| Nombre de Variable      | Descripción                                     |
|-------------------------|-------------------------------------------------|
| `ArnPostgresSecrets`    | Secreto para la conexión a la base de datos    |
| `ArnRedisSecrets`       | Secreto para la configuración de Redis         |
| `ArnEmailSecrets`       | Secreto para configuraciones de correo         |
| `ArnJwtSecrets`         | Secreto para la generación y validación de JWT |

---

## Pruebas
1. Navega al directorio de pruebas:
   ```
   cd product.test
   ```
2. Ejecuta las pruebas:
   ```
   dotnet test
   ```

---

## Soporte
Para cualquier problema, consulta este README o abre un issue en el repositorio de GitHub.

