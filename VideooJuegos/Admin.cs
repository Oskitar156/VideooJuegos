using System;
using System.Collections.Generic;
using System.Linq;

namespace VideooJuegos
{
    public class Administrador
    {
        // Maneja lectura y escritura del JSON
        private Usuarios gestorUsuarios = new Usuarios();

        // Lista fija de administradores por defecto (no se puede reemplazar la lista)
        private readonly List<Usuarios> adminsDefault = new List<Usuarios>
        {
            new Usuarios
            {
                Nombres = "Oscar",
                Email = "oscar123@gmail.com",
                Password = "123456",
                Rol = "Admin"
            },
            new Usuarios
            {
                Nombres = "Brayan",
                Email = "brayan123@gmail.com",
                Password = "123456",
                Rol = "Admin"
            }
        };

        // Crea los admins por defecto si no existen en el JSON
        public void CrearAdministradoresPorDefecto()
        {
            var usuarios = gestorUsuarios.ReadDataFromJson();
            bool seGuardo = false;

            // Agrega los admins que falten
            foreach (var admin in adminsDefault)
            {
                if (!usuarios.Any(u => u.Email == admin.Email))
                {
                    usuarios.Add(admin);
                    seGuardo = true;
                }
            }

            // Guarda solo si se agregaron admins
            if (seGuardo)
                gestorUsuarios.SaveToJson(usuarios);
        }

        // Cambia el rol de un usuario por su email
        public bool CambiarRol(string emailUsuario, string nuevoRol)
        {
            var usuarios = gestorUsuarios.ReadDataFromJson();
            var user = usuarios.FirstOrDefault(u => u.Email == emailUsuario);

            if (user == null)
                return false;

            user.Rol = nuevoRol;
            gestorUsuarios.SaveToJson(usuarios);
            return true;
        }

        // Verifica si el usuario autenticado es admin
        public bool EsAdmin(string email, string password)
        {
            var user = gestorUsuarios.FindByEmail(email);

            return user != null &&
                   user.Password == password &&
                   user.Rol == "Admin";
        }
    }
}
