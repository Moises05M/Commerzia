using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Commerzia_App.Services
{
    public class ApiService
    {
        // ==========================================
        // 1. PATRÓN SINGLETON
        // ==========================================
        private static ApiService? _instancia;

        // Esta es la propiedad global que usarás en toda tu app
        public static ApiService Instancia => _instancia ??= new ApiService();

        // ==========================================
        // 2. CONFIGURACIÓN DEL CLIENTE
        // ==========================================
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://localhost:7166/api/";

        private ApiService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_baseUrl);
        }

        // ==========================================
        // 3. MÉTODOS DE OPERACIÓN (CRUD)
        // ==========================================

        // Inyectar el Token JWT
        public void SetAuthToken(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        // LEER (GET)
        public async Task<T?> GetAsync<T>(string endpoint)
        {
            var response = await _httpClient.GetAsync(endpoint);
            response.EnsureSuccessStatusCode(); // Lanza excepción si hay error (ej. 404 o 500)

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        // CREAR (POST)
        public async Task<TResponse?> PostAsync<TRequest, TResponse>(string endpoint, TRequest data)
        {
            var json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(endpoint, content);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<TResponse>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        // ACTUALIZAR (PUT)
        public async Task PutAsync<TRequest>(string endpoint, TRequest data)
        {
            var json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync(endpoint, content);
            response.EnsureSuccessStatusCode();
        }

        // ELIMINAR (DELETE)
        public async Task DeleteAsync(string endpoint)
        {
            var response = await _httpClient.DeleteAsync(endpoint);
            response.EnsureSuccessStatusCode();
        }
    }
}