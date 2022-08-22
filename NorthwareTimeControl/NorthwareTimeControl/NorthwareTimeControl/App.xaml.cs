using Newtonsoft.Json;
using NorthwareTimeControl.Helpers;
using NorthwareTimeControl.Models.Dtos;
using NorthwareTimeControl.Resx;
using System.Globalization;
using Xamarin.CommunityToolkit.Helpers;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace NorthwareTimeControl
{
    public partial class App : Application
    {
        public App()
        {
            InitializeResourceManager();
            InitializeComponent();
            ChangeFontSize();

            bool isLogin = Preferences.Get(TextStrings.KeyIsLogin, false);
            if (isLogin)
            {
                UserLoginDTO = JsonConvert.DeserializeObject<UserLoginDTO>(Preferences.Get(TextStrings.KeyUser, string.Empty));
               // MainPage = new NavigationPage(new MainMasterDetailPage());
            }
            else
            {
                MainPage = new NavigationPage(new View.Account.LoginPage());
            }
        }

        #region [ Properties ]
        public static UserLoginDTO UserLoginDTO { get; set; }
        #endregion [ Properties ]

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        private void ChangeFontSize()
        {
            if (Xamarin.Forms.Device.Idiom == TargetIdiom.Tablet)
            {
                App.Current.Resources["FontSizeXXS"] = (double)19;
                App.Current.Resources["FontSizeXS"] = (double)20;
                App.Current.Resources["FontSizeSM"] = (double)22;
                App.Current.Resources["FontSizeMD"] = (double)26;
                App.Current.Resources["FontSizeMMD"] = (double)27;
                App.Current.Resources["FontSizeLG"] = (double)30;
                App.Current.Resources["FontSizeXL"] = (double)38;
                App.Current.Resources["FontSizeXXL"] = (double)46;
                App.Current.Resources["FontSizeXXXL"] = (double)54;
                App.Current.Resources["FontSizeButton"] = (double)22;
                App.Current.Resources["FontSizeMenu"] = (double)20;

                App.Current.Resources["FontSizeIconXS"] = (double)20;
                App.Current.Resources["FontSizeIconSM"] = (double)22;
                App.Current.Resources["FontSizeIconMD"] = (double)26;
                App.Current.Resources["FontSizeIconLG"] = (double)30;
                App.Current.Resources["FontSizeIconXL"] = (double)38;
                App.Current.Resources["FontSizeIconXXL"] = (double)46;
                App.Current.Resources["FontSizeIconXXXL"] = (double)54;

                App.Current.Resources["WidthRequestIconMenu"] = (double)60;
            }
        }

        private void InitializeResourceManager()
        {
            string language = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
            LocalizationResourceManager.Current.PropertyChanged += (sender, e) => AppResources.Culture = LocalizationResourceManager.Current.CurrentCulture;
            LocalizationResourceManager.Current.Init(AppResources.ResourceManager);
        }
    }
}
