using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Maui.Handlers;

namespace MoneyTransfer.Platforms.Handlers
{
    public partial class CustomEntryHandler : EntryHandler
    {
        public CustomEntryHandler()
        {
            // Mapeamos propiedades para que se apliquen en todas las plataformas
            Mapper.AppendToMapping("CustomEntryPadding", (handler, view) =>
            {
                if (view is AppNinja.Controls.CustomEntry customEntry)
                {
#if ANDROID
                // Android
                PlatformView.SetPadding(20, 0, 20, 0); // 20dp left/right
                PlatformView.SetMinimumHeight(55);
                PlatformView.BackgroundTintList = null;
                PlatformView.SetBackgroundColor(Colors.Transparent.ToPlatform());
#elif IOS || MACCATALYST
                // iOS
                PlatformView.BorderStyle = UIKit.UITextBorderStyle.RoundedRect;
                PlatformView.Layer.CornerRadius = 12;
                PlatformView.ClipsToBounds = true;
                PlatformView.TextEdgeInsets = new UIKit.UIEdgeInsets(0, 20, 0, 20);
#elif WINDOWS
                // Windows
                PlatformView.Padding = new Microsoft.UI.Xaml.Thickness(20, 0, 20, 0);
                PlatformView.CornerRadius = new Microsoft.UI.Xaml.CornerRadius(12);
#endif
                    // Color del cursor/selección
                    if (customEntry.AccentColor != null)
                    {
#if ANDROID
                    PlatformView.HighlightColor = customEntry.AccentColor.WithAlpha(0.3f).ToPlatform();
#elif IOS || MACCATALYST
                    PlatformView.TintColor = customEntry.AccentColor.ToPlatform();
#elif WINDOWS
                    PlatformView.Resources["TextControlSelectionHighlightColor"] = customEntry.AccentColor.ToPlatform();
#endif
                    }
                }
            });
        }
    }
}
