using Commerzia_App.Core;
using Commerzia_App.Models;
using Commerzia_App.Models.DTOs;
using Commerzia_App.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace Commerzia_App.ViewModels
{
    public class AddProductViewModel : ViewModelBase
    {
        // PROPIEDADES ENLAZADAS (BINDINGS)
        private string _nombre = "";
        public string Nombre { get => _nombre; set { _nombre = value; OnPropertyChanged(); } }

        private string _descripcion = "";
        public string Descripcion { get => _descripcion; set { _descripcion = value; OnPropertyChanged(); } }

        private string _codigoSKU = "";
        public string CodigoSKU { get => _codigoSKU; set { _codigoSKU = value; OnPropertyChanged(); } }

        private decimal _precioVenta;
        public decimal PrecioVenta { get => _precioVenta; set { _precioVenta = value; OnPropertyChanged(); } }

        private int _initialStock;
        public int InitialStock { get => _initialStock; set { _initialStock = value; OnPropertyChanged(); } }

        // COLECCIONES PARA COMBOBOXES
        public ObservableCollection<CategoriaDTO> Categories { get; set; } = new ObservableCollection<CategoriaDTO>();
        public ObservableCollection<MarcaDTO> Brands { get; set; } = new ObservableCollection<MarcaDTO>();
        public ObservableCollection<EstadoDTO> Statuses { get; set; } = new ObservableCollection<EstadoDTO>();

        private int _selectedCategoryId;
        public int SelectedCategoryId { get => _selectedCategoryId; set { _selectedCategoryId = value; OnPropertyChanged(); } }

        private int _selectedBrandId;
        public int SelectedBrandId { get => _selectedBrandId; set { _selectedBrandId = value; OnPropertyChanged(); } }

        private int _selectedStatusId;
        public int SelectedStatusId { get => _selectedStatusId; set { _selectedStatusId = value; OnPropertyChanged(); } }

        // COMANDOS
        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand UploadImageCommand { get; }

        public AddProductViewModel()
        {
            SaveCommand = new RelayCommand(ExecuteSave, CanExecuteSave);
            CancelCommand = new RelayCommand(ExecuteCancel);
            UploadImageCommand = new RelayCommand(ExecuteUploadImage);

            // Cargar datos de la API al iniciar el ViewModel
            _ = LoadDataAsync();
        }

        // LÓGICA DE NEGOCIO
        private async Task LoadDataAsync()
        {
            try
            {
                var categorias = await ApiService.Instancia.GetCategoriesAsync();
                var marcas = await ApiService.Instancia.GetBrandsAsync();

                // NOTA: Si aún no tienes un endpoint de Estados, descomenta esto para pruebas locales
                /*
                Statuses.Add(new Estado { IdEstado = 1, Nombre = "Activo" });
                Statuses.Add(new Estado { IdEstado = 2, Nombre = "Inactivo" });
                SelectedStatusId = 1;
                */

                if (categorias != null)
                {
                    foreach (var c in categorias) Categories.Add(c);
                    if (Categories.Count > 0) SelectedCategoryId = Categories[0].IdCategoria;
                }

                if (marcas != null)
                {
                    foreach (var m in marcas) Brands.Add(m);
                    if (Brands.Count > 0) SelectedBrandId = Brands[0].IdMarca;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar listas: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool CanExecuteSave(object? parameter)
        {
            return !string.IsNullOrWhiteSpace(Nombre) &&
                   !string.IsNullOrWhiteSpace(CodigoSKU) &&
                   PrecioVenta > 0 &&
                   SelectedCategoryId > 0 &&
                   SelectedBrandId > 0;
        }

        private async void ExecuteSave(object? parameter)
        {
            var newProduct = new ProductoRequest
            {
                Nombre = this.Nombre,
                CodigoSKU = this.CodigoSKU,
                Descripcion = this.Descripcion,
                PrecioVenta = this.PrecioVenta,
                IdCategoria = this.SelectedCategoryId,
                IdMarca = this.SelectedBrandId,
                IdEstado = this.SelectedStatusId, // O 1 si lo estás forzando a "Activo"
                StockInicial = this.InitialStock
            };

            try
            {
                bool success = await ApiService.Instancia.CreateProductAsync(newProduct);
                if (success)
                {
                    MessageBox.Show("¡Producto guardado exitosamente!", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                    // Aquí puedes limpiar el formulario o navegar hacia la lista de productos
                    LimpiarFormulario();
                }
                else
                {
                    MessageBox.Show("No se pudo guardar el producto. Verifica los datos.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error de conexión: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ExecuteCancel(object? parameter)
        {
            // Lógica para regresar a la vista anterior (Lo configuraremos en el MainViewModel)
            MessageBox.Show("Operación cancelada.");
            LimpiarFormulario();
        }

        private void ExecuteUploadImage(object? parameter)
        {
            // Aquí en el futuro puedes abrir un OpenFileDialog
            MessageBox.Show("Funcionalidad de subir imagen en desarrollo.");
        }

        private void LimpiarFormulario()
        {
            Nombre = "";
            Descripcion = "";
            CodigoSKU = "";
            PrecioVenta = 0;
            InitialStock = 0;
        }
    }
}
