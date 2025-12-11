using Microsoft.Maui.Controls.Platform;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using MoneyTransfer.Platforms.Windows;

[assembly: ResolutionGroupName("MoneyTransfer")]
[assembly: ExportEffect(typeof(MoneyTransfer.Platforms.Windows.RedBorderEffect), nameof(RedBorderEffect))]
namespace MoneyTransfer.Platforms.Windows
{
    public class RedBorderEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            // En MAUI Windows, el Entry nativo es TextBox de WinUI 3
            if (Control is TextBox textBox)
            {
                textBox.BorderBrush = new Microsoft.UI.Xaml.Media.SolidColorBrush(Microsoft.UI.Colors.Red);
                textBox.BorderThickness = new Microsoft.UI.Xaml.Thickness(2);  // Usa Windows.UI para Thickness si es necesario
                textBox.CornerRadius = new Microsoft.UI.Xaml.CornerRadius(12);  // Ajusta a tu radio
            }
        }

        protected override void OnDetached()
        {
            // Limpia al remover (opcional, para resetear)
            if (Control is TextBox textBox)
            {
                textBox.BorderBrush = null;
                textBox.BorderThickness = new Microsoft.UI.Xaml.Thickness(0);
            }
        }
    }
}
