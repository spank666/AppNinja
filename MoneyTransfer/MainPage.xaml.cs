using MoneyTransfer.ViewModels.MainVM;

namespace MoneyTransfer
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage(MainViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;

            
        }
    }
}
