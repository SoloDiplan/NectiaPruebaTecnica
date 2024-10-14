# Prueba Técnica (Gestión de Cuentas de Usuario y Consolas de Videojuegos)

Este proyecto es una aplicación web desarrollada en ASP.NET MVC 5, que consta de dos módulos principales: **Cuentas de Usuario** y **Consolas de Videojuegos**. 

## Descripción

1. **Módulo de Cuentas de Usuario**:
   - Registro de usuarios
   - Inicio de sesión
   - Consulta de detalles de usuario
   - Eliminación de usuario

2. **Módulo de Consolas de Videojuegos**:
   - Registro de consolas
   - Detalle de consola
   - Actualización de consola
   - Eliminación de consola

## Requisitos Previos

- Visual Studio
- .NET Framework 5 o superior
- SQL Server


## Instalación

1. **Clona el repositorio**: https://github.com/SoloDiplan/NectiaPruebaTecnica
   ```bash
   git clone

2. **Ejecuta el script de la base de datos**:

-Abre SQL Server Management Studio (SSMS) y conéctate a tu servidor de SQL Server.
-Crea una nueva base de datos llamada PruebaTecnicaBD.
-Abre el archivo SQL que contiene el script de creación de tablas y datos iniciales.
-Ejecuta el script en la base de datos PruebaTecnicaBD para crear las tablas y agregar los datos    iniciales.

3. **Configura la cadena de conexión**:

-Abre Web.config en el proyecto.
-Busca la sección <connectionStrings> y actualiza la cadena de conexión para apuntar a tu base de datos. Debería verse así:

<connectionStrings>
    <add name="PruebaTecnicaEntities" connectionString="metadata=res://*/Model1.csdl|res://*/Model1.ssdl|res://*/Model1.msl;provider=System.Data.SqlClient;provider connection string=&quot;data source=tu_servidor;initial catalog=PruebaTecnica;integrated security=True;trustservercertificate=True;MultipleActiveResultSets=True;App=EntityFramework&quot;" providerName="System.Data.EntityClient" />
</connectionStrings>


se debera cambiar la parte tu_servidor  

4.**Inicia la aplicación**:

Ejecuta el proyecto desde Visual Studio (F5 o Ctrl + F5).
