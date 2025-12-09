using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;

namespace MoneyTransfer
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Suscribirse al evento que disparamos desde App.xaml.cs
            App.CurrentThemeChanged += OnAppThemeChanged;

            // Aplicar el color inicial basado en el tema actual
            SetStatusBarColor(App.Current.RequestedTheme);
        }

        private void OnAppThemeChanged(object sender, AppTheme theme)
        {
            // El evento se dispara cuando el tema cambia, aplicamos el color
            SetStatusBarColor(theme);
        }

        private void SetStatusBarColor(AppTheme currentTheme)
        {
            // Utilizamos el Dispatcher para garantizar que la manipulación de la UI se haga en el hilo principal
            MainThread.BeginInvokeOnMainThread(() =>
            {
                // Acceso directo a la ventana de esta Activity
                var window = Window;

                if (currentTheme == AppTheme.Light)
                {
                    // Tema Claro: Barra blanca con iconos oscuros
                    
                    window.SetStatusBarColor(Android.Graphics.Color.White);

                    if (Build.VERSION.SdkInt >= BuildVersionCodes.M)
                    {
                        // Necesitamos iconos oscuros en un fondo claro
                        window.DecorView.SystemUiVisibility |= (StatusBarVisibility)SystemUiFlags.LightStatusBar;
                    }

                    window.SetNavigationBarColor(Android.Graphics.Color.White);

                    // Necesitamos iconos oscuros en la barra inferior (desde API 26+)
                    if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
                    {
                        window.DecorView.SystemUiVisibility |= (StatusBarVisibility)SystemUiFlags.LightNavigationBar;
                    }
                }
                else // AppTheme.Dark
                {
                    // Tema Oscuro: Barra oscura con iconos claros
                    window.SetStatusBarColor(Android.Graphics.Color.Argb(1,20,20,20));

                    if (Build.VERSION.SdkInt >= BuildVersionCodes.M)
                    {
                        // Necesitamos iconos claros en un fondo oscuro (quitamos la bandera LightStatusBar)
                        window.DecorView.SystemUiVisibility &= ~(StatusBarVisibility)SystemUiFlags.LightStatusBar;
                    }

                    // --- 2. BARRA INFERIOR (Navigation Bar) ---
                    window.SetNavigationBarColor(Android.Graphics.Color.Argb(1, 20, 20, 20));

                    // Quitamos la flag para asegurar iconos claros en la barra inferior
                    if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
                    {
                        window.DecorView.SystemUiVisibility &= ~(StatusBarVisibility)SystemUiFlags.LightNavigationBar;
                    }
                }
            });
        }
    }
}
