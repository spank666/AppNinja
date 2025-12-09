using Microsoft.Extensions.DependencyInjection;

namespace MoneyTransfer
{
    public partial class App : Application
    {
        // Evento estático para notificar a la capa nativa
        public static event EventHandler<AppTheme> CurrentThemeChanged;

        public App()
        {
            InitializeComponent();

            //LoadCurrentTheme();
            UserAppTheme = AppTheme.Unspecified;
            // 1. Notificar el tema inicial al cargar.
            CurrentThemeChanged?.Invoke(this, RequestedTheme);
            RequestedThemeChanged += (s, e) => 
            { 
                LoadCurrentTheme();
                CurrentThemeChanged?.Invoke(s, e.RequestedTheme);
            };

            
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }

        private void LoadCurrentTheme()
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                if (RequestedTheme == AppTheme.Dark)
                    UserAppTheme = AppTheme.Light;
                else
                    UserAppTheme = AppTheme.Dark;
            });
        }

        protected override void OnStart()
        {
            base.OnStart();
        }
    }
}