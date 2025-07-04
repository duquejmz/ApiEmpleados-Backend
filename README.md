# Backend de Aplicación de Chat en Tiempo Real

Este proyecto es el backend para una aplicación de chat en tiempo real construida con .NET, ASP.NET Core, SignalR para la comunicación por WebSockets y Entity Framework Core para la interacción con una base de datos SQL.

La aplicación permite a los usuarios autenticarse, crear salas de chat protegidas por un código único de 6 dígitos y comunicarse en tiempo real.

## Arquitectura Aplicada: Hexagonal (Puertos y Adaptadores)

Este proyecto sigue los principios de la **Arquitectura Hexagonal** (también conocida como Puertos y Adaptadores) para asegurar un bajo acoplamiento y una alta mantenibilidad. El objetivo es aislar la lógica de negocio principal de las dependencias externas como la base de datos, la API web o los servicios de terceros.

### El Hexágono (El Núcleo de la Aplicación)

El núcleo está compuesto por dos capas que no dependen de ningún detalle de infraestructura.

1.  **Capa de Dominio (`/Domain`)**: Es el centro de la aplicación. Contiene:
    *   **Entidades**: (`Room`, `User`, `Message`) que representan los objetos de negocio fundamentales.
    *   **Puertos (Interfaces)**: (`IRoomRepository`, `IMessageRepository`) que definen los contratos que la capa de aplicación necesita para comunicarse con el exterior (por ejemplo, para guardar o recuperar datos), sin saber cómo se implementará esa comunicación.

2.  **Capa de Aplicación (`/Application`)**: Contiene la lógica de los casos de uso de la aplicación.
    *   **Servicios (`ChatService`)**: Orquestan el flujo de datos, utilizando las entidades del dominio y llamando a los métodos definidos en los puertos (interfaces) para ejecutar las acciones. Esta capa no sabe si los datos vienen de una base de datos SQL, de un archivo o de otro servicio.

### Los Adaptadores (La Infraestructura)

Los adaptadores son los componentes que conectan el núcleo de la aplicación con el mundo exterior. Se dividen en dos tipos:

1.  **Adaptadores de Entrada (Driving Adapters)**: Son los que inician una acción en la aplicación.
    *   **`/Controllers` (`ChatController`, `AuthController`)**: Exponen la lógica de la aplicación como una API REST para que los clientes HTTP puedan interactuar con ella.
    *   **`/Hubs` (`ChatHub`)**: Expone la funcionalidad de tiempo real a través de WebSockets (SignalR), permitiendo una comunicación bidireccional.

2.  **Adaptadores de Salida (Driven Adapters)**: Son las implementaciones concretas de los puertos del dominio. La aplicación los "conduce" para realizar acciones.
    *   **`/Infrastructure/Repositories`**:
        *   `RoomRepository`: Implementa `IRoomRepository` utilizando Entity Framework Core para comunicarse con una base de datos SQL.
        *   `FileMessageRepository`: Implementa `IMessageRepository` para guardar y leer el historial de mensajes en archivos `.json` en el servidor.

## Estructura del Proyecto

```
/
├── Application/            # Lógica de los casos de uso (Capa de Aplicación)
│   └── Services/
│       └── ChatService.cs
├── Controllers/            # Adaptadores de Entrada (API REST)
│   ├── AuthController.cs
│   └── ChatController.cs
├── Data/                   # Configuración de la base de datos (DbContext)
│   └── AppDbContext.cs
├── Domain/                 # El núcleo del negocio (Capa de Dominio)
│   ├── Entities/           # Modelos de negocio (Room, User, Message)
│   └── Ports/              # Interfaces para los repositorios (IRoomRepository)
├── Hubs/                   # Adaptadores de Entrada (WebSockets)
│   └── ChatHub.cs
├── Infrastructure/         # Implementaciones de los puertos (Adaptadores de Salida)
│   └── Repositories/
│       ├── RoomRepository.cs
│       └── FileMessageRepository.cs
├── Migrations/             # Migraciones de Entity Framework Core
├── Postman/                # Colección de Postman para pruebas
│   └── ApiEmpleados-Backend.postman_collection.json
├── Properties/             # Configuraciones de lanzamiento del proyecto
│   └── launchSettings.json
├── appsettings.json        # Configuraciones de la aplicación
├── Program.cs              # Punto de entrada y registro de servicios
├── EmpleadosApi.csproj     # Archivo de proyecto .NET
└── README.md               # Este archivo
```

## Tecnologías Utilizadas

*   **.NET 9**
*   **ASP.NET Core**
*   **SignalR** para comunicación en tiempo real (WebSockets).
*   **Entity Framework Core** como ORM para la base de datos.
*   **SQL Server** como sistema de gestión de base de datos.
*   **Almacenamiento en archivos `.json`** para el historial de mensajes.
```# Backend de Aplicación de Chat en Tiempo Real

Este proyecto es el backend para una aplicación de chat en tiempo real construida con .NET, ASP.NET Core, SignalR para la comunicación por WebSockets y Entity Framework Core para la interacción con una base de datos SQL.

La aplicación permite a los usuarios autenticarse, crear salas de chat protegidas por un código único de 6 dígitos y comunicarse en tiempo real.

## Arquitectura Aplicada: Hexagonal (Puertos y Adaptadores)

Este proyecto sigue los principios de la **Arquitectura Hexagonal** (también conocida como Puertos y Adaptadores) para asegurar un bajo acoplamiento y una alta mantenibilidad. El objetivo es aislar la lógica de negocio principal de las dependencias externas como la base de datos, la API web o los servicios de terceros.

### El Hexágono (El Núcleo de la Aplicación)

El núcleo está compuesto por dos capas que no dependen de ningún detalle de infraestructura.

1.  **Capa de Dominio (`/Domain`)**: Es el centro de la aplicación. Contiene:
    *   **Entidades**: (`Room`, `User`, `Message`) que representan los objetos de negocio fundamentales.
    *   **Puertos (Interfaces)**: (`IRoomRepository`, `IMessageRepository`) que definen los contratos que la capa de aplicación necesita para comunicarse con el exterior (por ejemplo, para guardar o recuperar datos), sin saber cómo se implementará esa comunicación.

2.  **Capa de Aplicación (`/Application`)**: Contiene la lógica de los casos de uso de la aplicación.
    *   **Servicios (`ChatService`)**: Orquestan el flujo de datos, utilizando las entidades del dominio y llamando a los métodos definidos en los puertos (interfaces) para ejecutar las acciones. Esta capa no sabe si los datos vienen de una base de datos SQL, de un archivo o de otro servicio.

### Los Adaptadores (La Infraestructura)

Los adaptadores son los componentes que conectan el núcleo de la aplicación con el mundo exterior. Se dividen en dos tipos:

1.  **Adaptadores de Entrada (Driving Adapters)**: Son los que inician una acción en la aplicación.
    *   **`/Controllers` (`ChatController`, `AuthController`)**: Exponen la lógica de la aplicación como una API REST para que los clientes HTTP puedan interactuar con ella.
    *   **`/Hubs` (`ChatHub`)**: Expone la funcionalidad de tiempo real a través de WebSockets (SignalR), permitiendo una comunicación bidireccional.

2.  **Adaptadores de Salida (Driven Adapters)**: Son las implementaciones concretas de los puertos del dominio. La aplicación los "conduce" para realizar acciones.
    *   **`/Infrastructure/Repositories`**:
        *   `RoomRepository`: Implementa `IRoomRepository` utilizando Entity Framework Core para comunicarse con una base de datos SQL.
        *   `FileMessageRepository`: Implementa `IMessageRepository` para guardar y leer el historial de mensajes en archivos `.json` en el servidor.

## Estructura del Proyecto

```
/
├── Application/            # Lógica de los casos de uso (Capa de Aplicación)
│   └── Services/
│       └── ChatService.cs
├── Controllers/            # Adaptadores de Entrada (API REST)
│   ├── AuthController.cs
│   └── ChatController.cs
├── Data/                   # Configuración de la base de datos (DbContext)
│   └── AppDbContext.cs
├── Domain/                 # El núcleo del negocio (Capa de Dominio)
│   ├── Entities/           # Modelos de negocio (Room, User, Message)
│   └── Ports/              # Interfaces para los repositorios (IRoomRepository)
├── Hubs/                   # Adaptadores de Entrada (WebSockets)
│   └── ChatHub.cs
├── Infrastructure/         # Implementaciones de los puertos (Adaptadores de Salida)
│   └── Repositories/
│       ├── RoomRepository.cs
│       └── FileMessageRepository.cs
├── Migrations/             # Migraciones de Entity Framework Core
├── Postman/                # Colección de Postman para pruebas
│   └── ApiEmpleados-Backend.postman_collection.json
├── Properties/             # Configuraciones de lanzamiento del proyecto
│   └── launchSettings.json
├── appsettings.json        # Configuraciones de la aplicación
├── Program.cs              # Punto de entrada y registro de servicios
├── EmpleadosApi.csproj     # Archivo de proyecto .NET
└── README.md               # Este archivo
```

## Tecnologías Utilizadas

*   **.NET 9**
*   **ASP.NET Core**
*   **SignalR** para comunicación en tiempo real (WebSockets).
*   **Entity Framework Core** como ORM para la base de datos.
*   **SQL Server** como sistema de gestión de base de datos.
*   **Almacenamiento en archivos `.json`** para el historial de