using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace VideooJuegos
{
    public static class TiendaManager
    {
        private static string rutaJson = "tienda.json";

        public static List<long> Cargar()
        {
            if (!File.Exists(rutaJson))
                return new List<long>();

            string json = File.ReadAllText(rutaJson);
            return JsonConvert.DeserializeObject<List<long>>(json) ?? new List<long>();
        }

        public static void Guardar(List<long> ids)
        {
            string json = JsonConvert.SerializeObject(ids, Formatting.Indented);
            File.WriteAllText(rutaJson, json);
        }
    }
}
