# ensitech-library-api

Notas:

### Clonar Repositorio
https://github.com/ingjoseortiz/ensitech-api.git

# utilizar ides VSCode o Visual Studio (en caso de utilizar visual estudio, 
# ejecutar en Debug "Http" y no "Https") 

1.- Al ejecutar comando dotner run: se instalaran las dependenccias. Y/o al ejecutar Debug en Visual Studio
2.- Esta WebApi cuenta con CORS la cual, tiene la ip y puerto de el cliente que podra conectarse
con la direccion: http://localhost:3000, y se encuentra en Program.cs

Favor de omitir Swagger, ya que este projecto cuenta con autenticacion y JWT, asi que
no se envia el Token Bearer por este medio.

en la clase Users de la carpeta Models: se encuentran los siguientes usuarios:

Id = "1",
UserName = "Jose",
Password = "6532r435a4t654t",
Rol = "client",
             
Id = "2",
UserName = "Luis",
Password = "56a4d46a4ds4",
Rol = "client",
              
Id = "3",
UserName = "Gabriel",
Password = "5ea4d46a4ds!",
Rol = "administrator",

Por fines practicos, solo existen 2 roles, Administrador y cliente.

El administrador puede hacer lo siguiente: 
Agregar, Eliminar, Rentar, y devolver libros

El cliente sol puede hacer lo siguiente: 
Rentar, y devolver libros

////////////////////////////////////////////////////////////////////

Para el tema de 