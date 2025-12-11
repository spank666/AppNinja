using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyTransfer.Behaviors
{
    public static class Validation
    {
        public static readonly BindableProperty IsValidProperty =
            BindableProperty.CreateAttached(
                "IsValid",
                typeof(bool),
                typeof(Validation),
                true,
                propertyChanged: OnIsValidChanged);

        public static bool GetIsValid(BindableObject view) => (bool)view.GetValue(IsValidProperty);
        public static void SetIsValid(BindableObject view, bool value) => view.SetValue(IsValidProperty, value);

        private static void OnIsValidChanged(BindableObject view, object oldValue, object newValue)
        {
            if (view is not Entry entry) return;

            bool isValid = (bool)newValue;

            // Quita cualquier efecto rojo previo
            var existingEffect = entry.Effects.FirstOrDefault(e => e is RedBorderEffect);
            if (existingEffect != null)
                entry.Effects.Remove(existingEffect);

            if (!isValid)
            {
                // Agrega el efecto rojo cuando es inválido
                entry.Effects.Add(new RedBorderEffect());

                // Opcional: fondo rojo suave para más feedback visual
                entry.BackgroundColor = Color.FromRgba(255, 68, 68, 20);  // Rojo semi-transparente (#44222222)
            }
            else
            {
                // Vuelve al color normal
                entry.BackgroundColor = Colors.White;  // O {DynamicResource CardBackground} si lo preferís
            }

            // Fuerza re-renderizado
            entry.InvalidateMeasure();
        }
    }

    // Clase del Effect (se usa internamente, no en XAML)
    public class RedBorderEffect : RoutingEffect
    {
        public RedBorderEffect() : base("MoneyTransfer.RedBorderEffect") { }
    }
    /*
    public static class Validation
    {
        public static readonly BindableProperty IsValidProperty =
            BindableProperty.CreateAttached(
                "IsValid",
                typeof(bool),
                typeof(Validation),
                true,
                propertyChanged: OnIsValidChanged);

        public static bool GetIsValid(BindableObject view) => (bool)view.GetValue(IsValidProperty);
        public static void SetIsValid(BindableObject view, bool value) => view.SetValue(IsValidProperty, value);

        private static void OnIsValidChanged(BindableObject view, object oldValue, object newValue)
        {
            // Solo para forzar que el Trigger se dispare
            if (view is VisualElement element)
                element.InvalidateMeasure();
        }
    }
    */
}
