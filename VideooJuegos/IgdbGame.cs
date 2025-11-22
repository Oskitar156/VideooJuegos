using Newtonsoft.Json;
using System.Collections.Generic;

namespace VideooJuegos
{
    /// <summary>
    /// Modelo principal para deserializar la respuesta de la API de IGDB.
    /// Contiene todas las propiedades solicitadas para la Card y el Manager.
    /// </summary>
    public class IgdbGame
    {
        public long Id { get; set; }
        public string Name { get; set; }

        // El Rating de los usuarios
        [JsonProperty("rating")]
        public double Rating { get; set; }

        // El Rating de la prensa (críticas)
        [JsonProperty("aggregated_rating")]
        public double AggregatedRating { get; set; }

        [JsonProperty("first_release_date")]
        public long FirstReleaseDate { get; set; }

        // Propiedades que son Listas o Clases anidadas
        public List<IgdbGenre> Genres { get; set; }
        public IgdbCover Cover { get; set; }
        public List<IgdbPlatform> Platforms { get; set; } // Necesario para lblPlataforma
    }

    /// <summary>
    /// Clase auxiliar para el campo 'Genres'.
    /// </summary>
    public class IgdbGenre
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }

    /// <summary>
    /// Clase auxiliar para el campo 'Cover'.
    /// </summary>
    public class IgdbCover
    {
        public long Id { get; set; }
        public string Url { get; set; } // La URL de la imagen
    }

    /// <summary>
    /// Clase auxiliar para el campo 'Platforms'.
    /// </summary>
    public class IgdbPlatform
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}