# Instrucciones para Correr el Proyecto Localmente

Esta guía te ayudará a configurar y ejecutar el backend de la aplicación de chat en tu máquina local.

## 1. Prerrequisitos

Asegúrate de tener instalado lo siguiente:

*   **[.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)** o una versión superior.
*   **[SQL Server](https://www.microsoft.com/es-es/sql-server/sql-server-downloads)** (Express, Developer o cualquier otra edición).
*   **[Postman](https://www.postman.com/downloads/)** (Opcional, pero recomendado para probar la API).
*   **Herramientas de Entity Framework Core**: Si no las tienes, instálalas globalmente con el siguiente comando:
    ```bash
    dotnet tool install --global dotnet-ef
    ```

## 2. Configuración de la Base de Datos

1.  **Actualiza la Cadena de Conexión:**
    *   Abre el archivo `appsettings.Development.json`.
    *   Busca la sección `ConnectionStrings` y modifica el valor de `DefaultConnection` para que apunte a tu instancia local de SQL Server. Asegúrate de que el nombre de la base de datos (`Initial Catalog`) sea el que deseas.
    *   Ejemplo:
        ```json
        "ConnectionStrings": {
          "DefaultConnection": "Server=.\SQLEXPRESS;Database=ChatAppDb;Trusted_Connection=True;TrustServerCertificate=True;"
        }
        ```

2.  **Aplica las Migraciones:**
    *   Abre una terminal en la raíz del proyecto (`ApiEmpleados-Backend`).
    *   Ejecuta el siguiente comando para crear la base de datos y aplicar el esquema necesario:
        ```bash
        dotnet ef database update
        ```
    *   Esto creará las tablas `Empleados`, `Users` y `Rooms` en la base de datos que especificaste.

## 3. Ejecución del Proyecto

1.  **Desde la Terminal:**
    *   Navega a la raíz del proyecto en tu terminal.
    *   Ejecuta el siguiente comando:
        ```bash
        dotnet run
        ```

2.  **Desde Visual Studio / VS Code:**
    *   Simplemente presiona F5 o el botón de "Run" para iniciar el proyecto.

La API estará disponible en la URL especificada en `Properties/launchSettings.json` (generalmente algo como `https://localhost:7123`).

## 4. Probar la API con Postman

1.  **Importa la Colección:**
    *   Abre Postman e importa el archivo `Postman/ApiEmpleados-Backend.postman_collection.json`.

2.  **Configura el Entorno:**
    *   Crea un nuevo entorno en Postman.
    *   Añade una variable llamada `baseUrl` y establece su valor a la URL donde se está ejecutando tu API (ej. `https://localhost:7123`).
    *   Asegúrate de que este entorno esté seleccionado.

3.  **Ejecuta las Peticiones:**
    *   Sigue el orden de las carpetas en la colección:
        1.  **Authentication > Register User**: Para crear un nuevo usuario.
        2.  **Chat Rooms > Create Room**: Para crear una sala (esto usará automáticamente el ID del usuario registrado).
        3.  **Chat Rooms > Join Room**: Para validar el código de la sala creada.
