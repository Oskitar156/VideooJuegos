using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace VideooJuegos
{
    public class Usuarios
    {
        private string archivo = "usuarios.json";

        public string Nombres { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public string Rol { get; set; } = "Usuario";

        public void SaveToJson(List<Usuarios> usuarios)
        {
            string json = JsonConvert.SerializeObject(usuarios, Formatting.Indented);
            File.WriteAllText(archivo, json);
        }

        public List<Usuarios> ReadDataFromJson()
        {
            if (File.Exists(archivo))
            {
                string datosJson = File.ReadAllText(archivo);
                return JsonConvert.DeserializeObject<List<Usuarios>>(datosJson);
            }

            return new List<Usuarios>();
        }

        public Usuarios FindByEmail(string email)
        {
            List<Usuarios> datos = ReadDataFromJson();
            return datos.FirstOrDefault(u => u.Email == email);
        }
    }
}
