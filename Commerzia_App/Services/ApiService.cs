using Commerzia_App.Models;
using Commerzia_App.Models.DTOs;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Commerzia_App.Services
{
    public class ApiService
    {
        // SINGLETON
        private static ApiService? _instancia;
        public static ApiService Instancia => _instancia ??= new ApiService();

        // CONFIGURACIÓN DEL CLIENTE
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://localhost:7166/api/";

        private ApiService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_baseUrl);
        }

        // MÉTODOS DE OPERACIÓN (CRUD)

        // Inyectar el Token JWT
        public void SetAuthToken(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        // Login
        public async Task<string?> LoginAsync(string user, string password)
        {
            var request = new LoginRequest { Username = user, Password = password };
            var response = await PostAsync<LoginRequest, LoginResponse>("Account/login", request);

            if (response != null && !string.IsNullOrEmpty(response.Token))
            {
                // Guardamos el token en la instancia Singleton para futuras peticiones
                SetAuthToken(response.Token);
                return response.Token;
            }
            return null;
        }

        // LEER (GET)
        public async Task<T?> GetAsync<T>(string endpoint)
        {
            var response = await _httpClient.GetAsync(endpoint);
            response.EnsureSuccessStatusCode();

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

        // OBTENER DATOS
        // Obtener Categorías
        public async Task<List<CategoriaDTO>?> GetCategoriesAsync()
        {
            return await GetAsync<List<CategoriaDTO>>("Categoria");
        }

        // Obtener Marcas (Vendors)
        public async Task<List<MarcaDTO>?> GetBrandsAsync()
        {
            return await GetAsync<List<MarcaDTO>>("Marca");
        }

        // Obtener Estados (Opcional, si tienes un endpoint. Si no, puedes quemarlos en el ViewModel por ahora)
        public async Task<List<EstadoDTO>?> GetStatusesAsync()
        {
            // Asumiendo que tu API permite filtrar estados por contexto (ej: "Producto")
            return await GetAsync<List<EstadoDTO>>("Estado/Contexto/Producto");
        }

        // Guardar Producto
        public async Task<bool> CreateProductAsync(ProductoRequest product)
        {
            // El método PostAsync devolverá un string o un objeto. Si no es null, fue exitoso.
            var response = await PostAsync<ProductoRequest, object>("Producto", product);
            return response != null;
        }
    }
}