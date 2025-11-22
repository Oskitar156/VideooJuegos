using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using IGDB;


namespace VideooJuegos
{
    public class IgdbManager
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private const string BaseUrl = "https://api.igdb.com/v4/";

        // Constructor: Configura la conexión y los encabezados de autenticación
        public IgdbManager(string clientId, string accessToken)
        {
            _httpClient.BaseAddress = new Uri(BaseUrl);
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // ** Requerido por IGDB: Client-ID y Authorization (Bearer Token) **
            _httpClient.DefaultRequestHeaders.Add("Client-ID", clientId);
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
        }

        // Método de Consulta: Usa POST y toma el query APICalypse
        public async Task<List<IgdbGame>> GetGamesAsync(string query)
        {
            // El cuerpo de la petición POST contiene la consulta APICalypse (el "filtro" de IGDB)
            var content = new StringContent(query, Encoding.UTF8, "text/plain");

            // La URL es solo el endpoint, ya que el BaseAddress está en el constructor
            var response = await _httpClient.PostAsync("games", content);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();

                // Deserializa directamente a una lista de objetos IgdbGame
                return JsonConvert.DeserializeObject<List<IgdbGame>>(json);
            }
            else
            {
                // Si la petición falla (ej. 401 Unauthorized por token inválido), lanza un error
                var error = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Error al consultar IGDB ({response.StatusCode}): {error}");
            }
        }
    }
}
