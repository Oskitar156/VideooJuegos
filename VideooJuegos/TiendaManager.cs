using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace VideooJuegos
{
    public static class TiendaManager
    {
        private static string rutaJson = "tienda.json";

        public static List<JuegoTienda> Cargar()
        {
            if (!File.Exists(rutaJson))
                return new List<JuegoTienda>();

            string json = File.ReadAllText(rutaJson);
            return JsonConvert.DeserializeObject<List<JuegoTienda>>(json) ?? new List<JuegoTienda>();
        }

        public static void Guardar(List<JuegoTienda> juegos)
        {
            string json = JsonConvert.SerializeObject(juegos, Formatting.Indented);
            File.WriteAllText(rutaJson, json);
        }

        // Método auxiliar para agregar un juego a la tienda
        public static void AgregarJuego(long id, decimal precio, int stock)
        {
            List<JuegoTienda> juegos = Cargar();

            // Verificar si el juego ya existe
            var juegoExistente = juegos.Find(j => j.Id == id);
            if (juegoExistente != null)
            {
                // Actualizar stock si ya existe
                juegoExistente.Stock += stock;
            }
            else
            {
                // Agregar nuevo juego
                juegos.Add(new JuegoTienda(id, precio, stock));
            }

            Guardar(juegos);
        }

        // Método para eliminar un juego de la tienda
        public static void EliminarJuego(long id)
        {
            List<JuegoTienda> juegos = Cargar();
            juegos.RemoveAll(j => j.Id == id);
            Guardar(juegos);
        }

        // Método para actualizar precio y stock de un juego
        public static void ActualizarJuego(long id, decimal? nuevoPrecio = null, int? nuevoStock = null)
        {
            List<JuegoTienda> juegos = Cargar();
            var juego = juegos.Find(j => j.Id == id);

            if (juego != null)
            {
                if (nuevoPrecio.HasValue)
                    juego.Precio = nuevoPrecio.Value;

                if (nuevoStock.HasValue)
                    juego.Stock = nuevoStock.Value;

                Guardar(juegos);
            }
        }

        // Método para obtener información de un juego específico
        public static JuegoTienda ObtenerJuego(long id)
        {
            List<JuegoTienda> juegos = Cargar();
            return juegos.Find(j => j.Id == id);
        }
    }
}