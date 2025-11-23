using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideooJuegos
{
    public class Favorito
    {
        private static string archivoJson = "favoritos.json";

        public List<IgdbGame> ListaFavoritos { get; set; }

        public Favorito()
        {
            ListaFavoritos = new List<IgdbGame>();
        }

        public static Favorito CargarFavoritos()
        {
            if (File.Exists(archivoJson))
            {
                string json = File.ReadAllText(archivoJson);
                var favoritos = JsonConvert.DeserializeObject<Favorito>(json);
                return favoritos ?? new Favorito();
            }
            return new Favorito();
        }

        public void GuardarFavoritos()
        {
            string json = JsonConvert.SerializeObject(this, Formatting.Indented);
            File.WriteAllText(archivoJson, json);
        }

        public bool AgregarFavorito(IgdbGame juego)
        {
            if (!EsFavorito(juego.Id))
            {
                ListaFavoritos.Add(juego);
                GuardarFavoritos();
                return true;
            }
            return false;
        }

        public bool EliminarFavorito(int id)
        {
            var juego = ListaFavoritos.Find(j => j.Id == id);
            if (juego != null)
            {
                ListaFavoritos.Remove(juego);
                GuardarFavoritos();
                return true;
            }
            return false;
        }

        public bool EsFavorito(long id)
        {
            return ListaFavoritos.Exists(j => j.Id == id);
        }
    }

}
