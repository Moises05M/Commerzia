using Commerzia_App.Core;
using Commerzia_App.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Commerzia_App.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private string _email = "";
        private bool _isBusy;
        private string _errorMessage = "";

        public string Email
        {
            get => _email;
            set { _email = value; OnPropertyChanged(); }
        }

        public bool IsBusy
        {
            get => _isBusy;
            set { _isBusy = value; OnPropertyChanged(); }
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set { _errorMessage = value; OnPropertyChanged(); }
        }

        public ICommand LoginCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(ExecuteLogin, CanExecuteLogin);
        }

        private bool CanExecuteLogin(object? parameter)
        {
            // El botón solo se habilita si no está cargando y si hay un email escrito
            return !IsBusy && !string.IsNullOrWhiteSpace(Email);
        }

        private async void ExecuteLogin(object? parameter)
        {
            // Hack seguro para MVVM en WPF: Pasamos el PasswordBox por parámetro
            var passwordBox = parameter as PasswordBox;
            if (passwordBox == null || string.IsNullOrWhiteSpace(passwordBox.Password))
            {
                ErrorMessage = "Por favor, ingrese su contraseña.";
                return;
            }

            ErrorMessage = "";
            IsBusy = true; // Mostramos indicador de carga (opcional en UI)

            try
            {
                var token = await ApiService.Instancia.LoginAsync(Email, passwordBox.Password);

                if (!string.IsNullOrEmpty(token))
                {
                    MessageBox.Show("¡Inicio de sesión exitoso!", "Bienvenido", MessageBoxButton.OK, MessageBoxImage.Information);

                    // Aquí es donde harías la navegación a tu Dashboard Principal
                    // Ej: Cambiar la ventana actual o navegar a DashboardPage.xaml
                }
                else
                {
                    ErrorMessage = "Credenciales incorrectas.";
                }
            }
            catch (System.Exception)
            {
                ErrorMessage = "Error al conectar con el servidor.";
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
