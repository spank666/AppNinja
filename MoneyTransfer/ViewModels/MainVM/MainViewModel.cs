using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyTransfer.ViewModels.MainVM
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        private string email = "demo@ninja.com";

        [ObservableProperty]
        private string password = "123456";

        [ObservableProperty]
        private bool isLoggingIn;

        [ObservableProperty]
        private string? errorMessage;

        [ObservableProperty]
        private bool hasError;

        [RelayCommand]
        private async Task LoginAsync()
        {
            HasError = false;
            ErrorMessage = string.Empty;
            IsLoggingIn = true;

            try
            {
                // Simulación de login (reemplazá con tu API o Identity más adelante)
                await Task.Delay(2000);

                if (Email.Contains("ninja") && Password.Length >= 6)
                {
                    // ÉXITO → vamos a la página principal (TaskPage por ejemplo)
                    await Shell.Current.GoToAsync("//tasks");
                }
                else
                {
                    ErrorMessage = "Credenciales incorrectas, ninja";
                    HasError = true;
                }
            }
            finally
            {
                IsLoggingIn = false;
            }
        }

        [RelayCommand]
        private async Task Register()
        {
            await Shell.Current.DisplayAlert("Registro", "Funcionalidad en desarrollo", "OK");
        }

        public bool IsNotLoggingIn => !IsLoggingIn;
        partial void OnIsLoggingInChanged(bool value) => OnPropertyChanged(nameof(IsNotLoggingIn));
    }
}