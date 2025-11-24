using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace VideooJuegos
{
    public class TiendaManager
    {
        private static string path = "tienda.json";

        public static List<JuegoTienda> Cargar()
        {
            if (!File.Exists(path))
                return new List<JuegoTienda>();

            return JsonConvert.DeserializeObject<List<JuegoTienda>>(File.ReadAllText(path));
        }

        public static void Guardar(List<JuegoTienda> juegos)
        {
            File.WriteAllText(path, JsonConvert.SerializeObject(juegos, Formatting.Indented));
        }
    }
}
