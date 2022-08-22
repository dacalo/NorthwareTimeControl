using NorthwareTimeControl.ViewModels.Account;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NorthwareTimeControl.View.Account
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            BindingContext = new LoginViewModel();
        }
    }
}