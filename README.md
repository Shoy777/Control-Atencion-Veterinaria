# Veterinaria-ASPNET-MVC

El proyecto esta constituido por los módulos de Mantenimiento (Usuario, Mascota, Cliente, Medicamento), 
Consulta (Registrar, Modificar, Anular, Restaurar), Atención (Diagnostico, Receta) y Ventas (en veremos).

Las tablas raza, especie, tipo de medicamento ya contienen información detallada, correspondiente a la temática de 
una veterinaria. Así que no se hizo el manteniento de estas. Especies (perro, gato, conejo, pajaro, roedor). 
La raza y el tipo de medicamento depende de la especie. Para una mejor especificación de la información de la 
mascota y el medicamento.

## Módulo Mantenimiento

Este módulo permite gestionar la información de usuarios, clientes, mascotas y medicamentos.

## Usuario
Aquí se gestiona la información del usuario. Roles (Admin, Veterinario, Cajero). Admin y Veterinario tienen acceso
a todo, el Cajero solo a Consultas y Ventas.

### Crud - Registrar y Modificar
Si la vista recibe un valor que no existe. El contralodor se encarga de registrar, en caso contrario se modifica.

![Crud Usuario](https://github.com/Shoy777/Veterinaria-ASPNET-MVC/blob/master/Pantallas.min/Crud%20Usuario-min.png)

### Restaurar Usuario
Se cambia estado de registro a 1

![Restaurar Usuario](https://github.com/Shoy777/Veterinaria-ASPNET-MVC/blob/master/Pantallas.min/Restaurar%20Usuario-min.png)

### Eliminar Usuario
Se cambia el estado de registro a 0

![Eliminar Usuario](https://github.com/Shoy777/Veterinaria-ASPNET-MVC/blob/master/Pantallas.min/Eliminar%20Usuario-min.png)

### Listado de Usuarios

![Listado de usuarios activos](https://github.com/Shoy777/Veterinaria-ASPNET-MVC/blob/master/Pantallas.min/Listado%20de%20Usuarios-min.png)

### Listado de Usuario Eliminados
Un usuario con cuenta desactivada no puede loguearse, el admininistrador tiene opcion a restaurar usuario.

![Listado de usuario eliminados](https://github.com/Shoy777/Veterinaria-ASPNET-MVC/blob/master/Pantallas.min/Listado%20de%20Usuarios%20Eliminados-min.png)

### Cambiar Password

![Cambiar Password](https://github.com/Shoy777/Veterinaria-ASPNET-MVC/blob/master/Pantallas.min/Cambiar%20Password-min.png)

### Inciar Sesión

![Iniciar sesion](https://github.com/Shoy777/Veterinaria-ASPNET-MVC/blob/master/Pantallas.min/Login-min.png)

### Perfil de Usuario

![Perfil de usuario](https://github.com/Shoy777/Veterinaria-ASPNET-MVC/blob/master/Pantallas.min/Perfil-min.png)

## Mascota
Aquí se gestiona la información de la mascota.

### Crud - Registrar y Modificar
Para registrar una mascota se debe seleccionar al dueño. En caso no este registrado se puede registrar dentro de la
misma vista.

![Crud Mascota](https://github.com/Shoy777/Veterinaria-ASPNET-MVC/blob/master/Pantallas.min/Crud%20Mascota-min.png)

### Listado de Mascotas

![Listado de Mascotas](https://github.com/Shoy777/Veterinaria-ASPNET-MVC/blob/master/Pantallas.min/Listado%20de%20Mascotas-min.png)

## Cliente

Aquí se gestiona la información del cliente.

### Registrar Cliente

Solo se puede registrar un cliente que no esté registrado, al momento del registro de una mascota

![Registrar Cliente](https://github.com/Shoy777/Veterinaria-ASPNET-MVC/blob/master/Pantallas.min/Registrar%20Cliente-min.png)

### Modificar Cliente

![Modificar Cliente](https://github.com/Shoy777/Veterinaria-ASPNET-MVC/blob/master/Pantallas.min/Modificar%20Cliente-min.png)

### Listado de Clientes

![Listado de Clientes](https://github.com/Shoy777/Veterinaria-ASPNET-MVC/blob/master/Pantallas.min/Listado%20de%20Clientes-min.png)

### Detalle de Cliente

![Detalle Cliente](https://github.com/Shoy777/Veterinaria-ASPNET-MVC/blob/master/Pantallas.min/Detalle%20Cliente-min.png)

## Medicamento
Aquí se gestiona la información del medicamento.

### Crud - Registrar y Modificar

![Crud Medicamento](https://github.com/Shoy777/Veterinaria-ASPNET-MVC/blob/master/Pantallas.min/Crud%20Medicamento-min.png)

### Listado de Medicamentos

![Listado de Medicamentos](https://github.com/Shoy777/Veterinaria-ASPNET-MVC/blob/master/Pantallas.min/Listado%20de%20Medicinas-min.png)

### Detalle de Medicamento

![Detalle Medicamento](https://github.com/Shoy777/Veterinaria-ASPNET-MVC/blob/master/Pantallas.min/Detalle%20Medicamento-min.png)

## Módulo Consulta
Citas para atender a las mascotas

### Registrar, Editar Consulta

![Registrar Consulta](https://github.com/Shoy777/Veterinaria-ASPNET-MVC/blob/master/Pantallas.min/Registrar%20Consulta-min.png)

### Elegir Mascota

1. Ingrese criterio de busqueda.
2. Seleccione un registro de la tabla Cliente.
3. Seleccione un registro de la tabla Mascota.

![Elegir mascota](https://github.com/Shoy777/Veterinaria-ASPNET-MVC/blob/master/Pantallas.min/Seleccionar%20Mascota%20x%20Cliente-min.png)

### Listado de Consultas del dia

![Listado de consultas para hoy](https://github.com/Shoy777/Veterinaria-ASPNET-MVC/blob/master/Pantallas.min/Listado%20de%20Consultas%20Hoy-min.png)

### Listado de Consultas por atender

![Listado de consultas por atender](https://github.com/Shoy777/Veterinaria-ASPNET-MVC/blob/master/Pantallas.min/Listado%20de%20Consultas%20Por%20Atender-min.png)

### Listado de Consultas Anuladas

![Listado de consultas anuladas](https://github.com/Shoy777/Veterinaria-ASPNET-MVC/blob/master/Pantallas.min/Listado%20de%20Consultas%20Anuladas-min.png)

### Listado de Consultas Atendidas
![Listado de consultas atendidas](https://github.com/Shoy777/Veterinaria-ASPNET-MVC/blob/master/Pantallas.min/Listado%20de%20Consultas%20Atendidas-min.png)

## Módulo Atención
Proximamente

## Módulo Consulta
Posiblemente

Pequeña Aplicación para una Veterinaria desarrollada con MVC5, SQL, Entity Framework, Bootstrap y Javascript
