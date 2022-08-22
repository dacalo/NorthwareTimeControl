using NorthwareTimeControl.Resx;
using Xamarin.Essentials;

namespace NorthwareTimeControl.Services
{
    public class ConnectionService
    {
        public static bool CheckConnection()
        {
            if (!(Connectivity.NetworkAccess == NetworkAccess.Internet))
            {
                App.Current.MainPage.DisplayAlert(AppResources.Error, AppResources.ConnectionError, AppResources.Accept);
                return false;
            }
            return true;
        }
    }
}
