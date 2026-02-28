using Commerzia_App.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Commerzia_App.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        // 1. La propiedad que guarda la vista actual
        private ViewModelBase _currentChildView;
        public ViewModelBase CurrentChildView
        {
            get => _currentChildView;
            set
            {
                _currentChildView = value;
                OnPropertyChanged();
            }
        }

        // 2. Los Comandos para los botones del menú
        public ICommand ShowDashboardCommand { get; }
        public ICommand ShowInventoryCommand { get; }
        public ICommand ShowSalesCommand { get; }
        public ICommand ShowPurchasesCommand { get; }

        public MainViewModel()
        {
            // Inicializar comandos
            ShowDashboardCommand = new RelayCommand(ExecuteShowDashboard);
            ShowInventoryCommand = new RelayCommand(ExecuteShowInventory);
            ShowSalesCommand = new RelayCommand(ExecuteShowSales);
            ShowPurchasesCommand = new RelayCommand(ExecuteShowPurchases);

            // Vista por defecto al abrir la aplicación
            ExecuteShowDashboard(null);
        }

        // 3. Métodos que cambian la vista
        private void ExecuteShowDashboard(object? obj)
        {
            CurrentChildView = new DashboardViewModel();
        }

        private void ExecuteShowInventory(object? obj)
        {
            // Ojo: Aquí mandamos a AddProductViewModel por ahora. 
            // En el futuro crearás un InventoryListViewModel y desde ahí llamarás a AddProduct
            CurrentChildView = new AddProductViewModel();
        }

        private void ExecuteShowSales(object? obj)
        {
            CurrentChildView = new SalesOverviewViewModel();
        }

        private void ExecuteShowPurchases(object? obj)
        {
            CurrentChildView = new PurchasesViewModel();
        }
    }
}
