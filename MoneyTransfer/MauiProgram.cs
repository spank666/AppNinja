using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.LifecycleEvents;
using MoneyTransfer.ViewModels.MainVM;

#if WINDOWS
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using WinRT.Interop;
using Windows.Graphics;
#endif 

namespace MoneyTransfer
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            
            #if WINDOWS
                        builder.ConfigureLifecycleEvents(events =>
                    {
                        events.AddWindows(windowsBuilder =>
                        {
                            windowsBuilder.OnWindowCreated(window =>
                            {
                                // ← AQUÍ LA MAGIA: Configura title bar transparente
                                var handle = WindowNative.GetWindowHandle(window);
                                var id = Win32Interop.GetWindowIdFromWindow(handle);
                                var appWindow = AppWindow.GetFromWindowId(id);
                                var titleBar = appWindow.TitleBar;

                                // Activa la ventana ANTES de cambiar (fix común para bugs)
                                window.Activate();


                                // Extiende contenido bajo la barra (para que se vea transparente)
                                titleBar.ExtendsContentIntoTitleBar = true;

                                // Colores transparentes para botones y fondo
                                titleBar.BackgroundColor = Microsoft.UI.Colors.Transparent;
                                titleBar.ButtonBackgroundColor = Microsoft.UI.Colors.Transparent;
                                titleBar.ButtonInactiveBackgroundColor = Microsoft.UI.Colors.Transparent;
                                titleBar.ButtonHoverBackgroundColor = Microsoft.UI.Colors.Transparent;
                                titleBar.ButtonPressedBackgroundColor = Microsoft.UI.Colors.Transparent;
                                titleBar.InactiveBackgroundColor = Microsoft.UI.Colors.Transparent;

                                if (AppWindowTitleBar.IsCustomizationSupported())
                                {
                                    var backdrop = new Microsoft.UI.Xaml.Media.MicaBackdrop();
                                    backdrop.Kind = Microsoft.UI.Composition.SystemBackdrops.MicaKind.Base;

                                    //var backdrop = new Microsoft.UI.Xaml.Media.MicaBackdrop();
                                    window.SystemBackdrop = backdrop;
                                }


                            });
                        });
                    });
            #endif

#if DEBUG
            builder.Logging.AddDebug();
#endif

            builder.Services.AddSingleton<MainViewModel>();
            builder.Services.AddTransient<MainPage>();

            return builder.Build();
        }
    }
}
