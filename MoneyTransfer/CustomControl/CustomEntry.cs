using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Shapes;
using Microsoft.Maui.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyTransfer.CustomControl
{
    public class CustomEntry : ContentView
    {
        private readonly Entry _entry;
        private readonly Label _placeholderLabel;
        private readonly Frame _frame;  // Frame para borde redondeado (estable en Windows)

        public CustomEntry()
        {
            //Border myBorder = new Border
            //{
            //    Stroke = Colors.Gray,
            //    StrokeThickness = 4, // Establece el grosor del borde
            //    StrokeShape = new RoundRectangleGeometry { CornerRadius = 10 },
            //    Padding = new Thickness(10),
            //    BackgroundColor = Color.FromArgb("#1e1e1e"),
            //    Content = new Label
            //    {
            //        Text = "Contenido dentro del borde",
            //        HorizontalOptions = LayoutOptions.Center,
            //        VerticalOptions = LayoutOptions.Center
            //    }
            //};
            // Frame como container (clipa redondeado perfecto en todas las plataformas)
            _frame = new Frame
            {
                BackgroundColor = Color.FromArgb("#1e1e1e"),  // Tu fondo oscuro
                BorderColor = Colors.LightGray,               // Borde gris normal
                CornerRadius = 12,                            // ¡Redondeado perfecto!
                HasShadow = false,                            // Opcional: shadow para profundidad
                Padding = 0,
                HeightRequest = 55,
                MinimumHeightRequest = 55,
                Margin = new Thickness(0, 5)
            };

            // Placeholder como Label superpuesto
            _placeholderLabel = new Label
            {
                TextColor = Colors.Gray,
                FontSize = 16,
                VerticalOptions = LayoutOptions.Center,
                Margin = new Thickness(15, 0),
                IsVisible = true
            };

            // Entry real (transparente, sin bordes)
            _entry = new Entry
            {
                BackgroundColor = Colors.Transparent,
                TextColor = Colors.White,
                FontSize = 16,
                Margin = new Thickness(5, 0),
                VerticalOptions = LayoutOptions.Center,
                Placeholder = null,  // Controlamos manual
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            // Grid para superponer placeholder y entry
            var contentGrid = new Grid
            {
                Padding = new Thickness(0),  // Sin padding extra
                Children = { _placeholderLabel, _entry }
            };

            _frame.Content = contentGrid;
            Content = _frame;

            // Eventos para lógica
            _entry.TextChanged += OnTextChanged;
            _entry.Focused += OnFocused;
            _entry.Unfocused += OnUnfocused;
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            _placeholderLabel.IsVisible = string.IsNullOrEmpty(e.NewTextValue);
        }

        private void OnFocused(object sender, FocusEventArgs e)
        {
            _frame.BorderColor = Color.FromArgb("#ff0066");  // Tu color en focus
            //_frame.StrokeThickness = 2;  // Borde más grueso
        }

        private void OnUnfocused(object sender, FocusEventArgs e)
        {
            _frame.BorderColor = IsValid ? Colors.LightGray : Colors.Red;  // Normal o error
            //_frame.StrokeThickness = IsValid ? 1 : 3;
        }

        // PROPIEDADES BINDABLES (iguales que antes)
        public static readonly BindableProperty TextProperty =
            BindableProperty.Create(nameof(Text), typeof(string), typeof(CustomEntry), string.Empty, BindingMode.TwoWay,
                propertyChanged: (b, o, n) => ((CustomEntry)b)._entry.Text = (string)n);

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public static readonly BindableProperty PlaceholderProperty =
            BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(CustomEntry), string.Empty,
                propertyChanged: (b, o, n) => ((CustomEntry)b)._placeholderLabel.Text = (string)n);

        public string Placeholder
        {
            get => (string)GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }

        public static readonly BindableProperty IsPasswordProperty =
            BindableProperty.Create(nameof(IsPassword), typeof(bool), typeof(CustomEntry), false,
                propertyChanged: (b, o, n) => ((CustomEntry)b)._entry.IsPassword = (bool)n);

        public bool IsPassword
        {
            get => (bool)GetValue(IsPasswordProperty);
            set => SetValue(IsPasswordProperty, value);
        }

        public static readonly BindableProperty IsValidProperty =
            BindableProperty.Create(nameof(IsValid), typeof(bool), typeof(CustomEntry), true,
                propertyChanged: (b, o, n) =>
                {
                    var control = (CustomEntry)b;
                    control._frame.BorderColor = (bool)n ? Colors.LightGray : Colors.Red;
                    //control._frame.StrokeThickness = (bool)n ? 1 : 3;
                    if (!control._entry.IsFocused)  // Solo si no está enfocado
                        control.OnUnfocused(null, null);
                });

        public bool IsValid
        {
            get => (bool)GetValue(IsValidProperty);
            set => SetValue(IsValidProperty, value);
        }
    }
}
