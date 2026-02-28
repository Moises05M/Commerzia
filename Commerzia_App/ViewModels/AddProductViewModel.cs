using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using Commerzia_App.Core;
using Commerzia_App.Services;
using Commerzia_App.Models.DTOs;
using Commerzia_App.Models;

namespace Commerzia_App.ViewModels
{
    public class AddProductViewModel : ViewModelBase
    {
        // PROPIEDADES PARA EL FORMULARIO
        private string _codigoSku;
        public string CodigoSku { get => _codigoSku; set { _codigoSku = value; OnPropertyChanged(); } }

        private string _nombre;
        public string Nombre { get => _nombre; set { _nombre = value; OnPropertyChanged(); } }

        private string _descripcion;
        public string Descripcion { get => _descripcion; set { _descripcion = value; OnPropertyChanged(); } }

        private decimal _precioVenta;
        public decimal PrecioVenta { get => _precioVenta; set { _precioVenta = value; OnPropertyChanged(); } }

        // CATÁLOGOS PARA LOS COMBOBOX
        public ObservableCollection<CategoriaDTO> Categorias { get; set; } = new ObservableCollection<CategoriaDTO>();
        public ObservableCollection<MarcaDTO> Marcas { get; set; } = new ObservableCollection<MarcaDTO>();

        private CategoriaDTO _selectedCategoria;
        public CategoriaDTO SelectedCategoria { get => _selectedCategoria; set { _selectedCategoria = value; OnPropertyChanged(); } }

        private MarcaDTO _selectedMarca;
        public MarcaDTO SelectedMarca { get => _selectedMarca; set { _selectedMarca = value; OnPropertyChanged(); } }

        // COMANDOS
        public ICommand SaveProductCommand { get; }

        public AddProductViewModel()
        {
            SaveProductCommand = new RelayCommand(async (o) => await ExecuteSaveProduct(), CanSaveProduct);

            _ = LoadCatalogos();
        }

        private async Task LoadCatalogos()
        {
            try
            {
                var categoriasApi = await ApiService.Instancia.GetCategoriesAsync();
                if (categoriasApi != null)
                {
                    foreach (var cat in categoriasApi) Categorias.Add(cat);
                }

                var marcasApi = await ApiService.Instancia.GetBrandsAsync();
                if (marcasApi != null)
                {
                    foreach (var marca in marcasApi) Marcas.Add(marca);
                }
            }
            catch
            {
                // Si falla (ej: token expirado o API apagada), no crasheamos la app
                MessageBox.Show("No se pudieron cargar los catálogos. Verifica tu conexión.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private bool CanSaveProduct(object obj)
        {
            // Validamos que los campos requeridos estén llenos
            return !string.IsNullOrWhiteSpace(Nombre) &&
                   !string.IsNullOrWhiteSpace(CodigoSku) &&
                   PrecioVenta > 0 &&
                   SelectedCategoria != null;
        }

        private async Task ExecuteSaveProduct()
        {
            try
            {
                var nuevoProducto = new ProductoRequest
                {
                    CodigoSKU = this.CodigoSku,
                    Nombre = this.Nombre,
                    Descripcion = this.Descripcion,
                    PrecioVenta = this.PrecioVenta,
                    IdCategoria = SelectedCategoria.IdCategoria,
                    IdMarca = SelectedMarca?.IdMarca,
                    IdEstado = 1 // 1 = Activo
                };

                var success = await ApiService.Instancia.CreateProductAsync(nuevoProducto);

                if (success)
                {
                    MessageBox.Show("¡Producto guardado exitosamente!", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                    LimpiarFormulario();
                }
                else
                {
                    MessageBox.Show("Error al guardar el producto en la base de datos.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Ocurrió un error de conexión: {ex.Message}", "Error Crítico", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LimpiarFormulario()
        {
            CodigoSku = string.Empty;
            Nombre = string.Empty;
            Descripcion = string.Empty;
            PrecioVenta = 0;
            SelectedCategoria = null;
            SelectedMarca = null;
        }
    }
}