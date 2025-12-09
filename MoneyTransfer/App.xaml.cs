using Microsoft.Extensions.DependencyInjection;

namespace MoneyTransfer
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //LoadCurrentTheme();
            UserAppTheme = AppTheme.Unspecified;
            RequestedThemeChanged += (s, e) => { LoadCurrentTheme(); };
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