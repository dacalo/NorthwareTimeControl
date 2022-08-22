using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace NorthwareTimeControl.ViewModels
{
    public class ViewModelBase : ObservableObject
    {
        #region [ Attributes ]
        private bool _isEnabled;
        private string _title;

        private ICommand _backCommand;
        #endregion [ Attributes ]

        #region [ Constructor ]
        public ViewModelBase()
        {
            IsEnabled = true;
        }
        #endregion [ Constructor ]

        #region [ Properties ]
        public bool IsEnabled { get => _isEnabled; set => SetProperty(ref _isEnabled, value); }

        public string Title { get => _title; set => SetProperty(ref _title, value); }
        #endregion [ Properties ]

        #region [ Commands ]
        public ICommand BackCommand => _backCommand ?? (_backCommand = new Command(async () => await Back()));

        #endregion [ Commands ]

        #region [ Methods ]
        private async Task Back()
        {
            if (IsEnabled)
            {
                IsEnabled = false;
                await Task.Delay(100);
                await App.Current.MainPage.Navigation.PopAsync();
                IsEnabled = true;
            }
        }

        #endregion [ Methods ]
    }
}
