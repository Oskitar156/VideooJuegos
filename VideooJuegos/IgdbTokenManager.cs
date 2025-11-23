using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace VideooJuegos
{
    public class IgdbAuthResponse
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }
    }

    public class IgdbTokenFile
    {
        public string AccessToken { get; set; }
        public DateTime Expiration { get; set; }
    }

    public static class IgdbTokenManager
    {
        private static readonly string tokenFile = "igdb_token.json";

        public static async Task<string> GetTokenAsync(string clientId, string clientSecret)
        {
            // 1) Intentar leer token válido del archivo
            if (File.Exists(tokenFile))
            {
                var json = File.ReadAllText(tokenFile);
                var data = JsonConvert.DeserializeObject<IgdbTokenFile>(json);

                if (data != null && DateTime.Now < data.Expiration)
                    return data.AccessToken;
            }

            // 2) Si no hay archivo o está vencido, pedir token nuevo
            using (var client = new HttpClient())
            {
                var url = $"https://id.twitch.tv/oauth2/token" +
                          $"?client_id={clientId}" +
                          $"&client_secret={clientSecret}" +
                          $"&grant_type=client_credentials";

                var response = await client.PostAsync(url, null);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                var auth = JsonConvert.DeserializeObject<IgdbAuthResponse>(json);

                var tokenData = new IgdbTokenFile
                {
                    AccessToken = auth.AccessToken,
                    // resto de segundos de vida del token (~60 días)
                    Expiration = DateTime.Now.AddSeconds(auth.ExpiresIn - 60)
                };

                // Guardar token + fecha de expiración en JSON
                File.WriteAllText(
                    tokenFile,
                    JsonConvert.SerializeObject(tokenData, Formatting.Indented)
                );

                return tokenData.AccessToken;
            }
        }
    }
}
