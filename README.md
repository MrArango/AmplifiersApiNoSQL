# AmplifiersApiNoSQL

Es un proyecto API WEB que se conecta una base de datos Cosmos DB (Azure) usando MongoDB y JWT para gestionar la autentificación

Este proyecto te puede servir de ejemplo para construir tu propia API

Esta Api tiene dos controladores, a continuación, enlistare cada uno de sus metodos con sus rutas de acceso

                            -- -->            User            <-- --
                               URL: {aqui va tu dominio}/api/user
  Esquema:
  {
         "_id"  :   string,
    "username"  :   string,
    "password"  :   string
  }

- [Http - Get]    GetAll: Devuelve a todos los documentos de los usuarios             - Necesita Autentificación
- [Http - Get]   GetById: Devuelve el documento del usuario correspondiente al ID     - Necesita Autentificación - URL + /{id}
- [Http - Post]   Create: Es la función "Registrarse"                                 - Recibe un parametro tipo Users que es enviado en el body de la petición
- [Http - Post]    Login: Es la función "Iniciar Sesión" (y regresa un JWT)           - Recibe un parametro tipo Users que es enviado en el body de la petición - URL + /login
- [Http - Delete] Delete: Elimina el documento del usuario correspondeiente al ID     - Necesita Autentificación - URL + /{id}
- [Http - Put]    Update: Reescribe la información del usuario                        - Necesita Autentificación - URL + /{id} - Recibe un parametro tipo Users que es enviado en el body de la petición

                            -- -->            Amplifier            <-- --
                               URL: {aqui va tu dominio}/api/amplifier
  Esquema:
  {
            "_id" : string,
        "ampname" : string,
          "brand" : string,
           "user" : string,
    "Description" : string
  }

- [Http - Get]    GetAll: Devuelve a todos los Amplificadores correspondientes a un usuario   - Necesita Autentificación - Recibe un parametro tipo User que es enviado en el body de la petición
- [Http - Get]   GetById: Devuelve el documento del amplificador correspondiente al ID        - Necesita Autentificación - URL + /{id}
- [Http - Post]   Create: Registra un nuevo amplificador                                      - Necesita Autentificación - Recibe un parametro tipo Amplifiers que es enviado en el body de la petición
- [Http - Delete] Delete: Elimina el documento del amplificador correspondeiente al ID        - Necesita Autentificación - URL + /{id}
- [Http - Put]    Update: Reescribe el documento que corresponda con el ID solicitado         - Necesita Autentificación - URL + /{id} - Recibe un parametro tipo Amplifiers que es enviado en el body de la petición
