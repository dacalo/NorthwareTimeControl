using Newtonsoft.Json;
using NorthwareTimeControl.Business;
using NorthwareTimeControl.Helpers;
using NorthwareTimeControl.Http;
using NorthwareTimeControl.Models.Dtos;
using NorthwareTimeControl.Models.Dtos.Account;
using NorthwareTimeControl.Models.Response;
using NorthwareTimeControl.Resx;
using NorthwareTimeControl.Services;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace NorthwareTimeControl.ViewModels.Account
{
    public class LoginViewModel : ViewModelBase
    {
        #region [ Attributes ]
        private string _iconShowPassword;
        private bool _isPassword;
        private string _password;
        private RegexHelper _regexHelper;
        private ICommand _loginCommand;
        private ICommand _showPasswordCommand;
        #endregion [ Attributes ]

        #region [ Constructor ]
        public LoginViewModel()
        {
            Init();
        }
        #endregion [ Constructor ]

        #region [ Properties ]
        public string Email { get; set; }

        public string IconShowPassword { get => _iconShowPassword; set => SetProperty(ref _iconShowPassword, value); }

        public bool IsPassword { get => _isPassword; set => SetProperty(ref _isPassword, value); }

        public string Password { get => _password; set => SetProperty(ref _password, value); }
        #endregion [ Properties ]

        #region [ Commands ]
        public ICommand LoginCommand => _loginCommand ?? (_loginCommand = new Command(async () => await LoginAsync()));
        public ICommand ShowPasswordCommand => _showPasswordCommand ?? (_showPasswordCommand = new Command(ShowPassword));
        #endregion [ Commands ]

        #region [ Methods ]
        private void Init()
        {
            _regexHelper = new RegexHelper();
            IsEnabled = true;
            IsPassword = true;
            IconShowPassword = "\uf06e";
        }

        private async Task LoginAsync()
        {
            if (IsEnabled)
            {
                IsEnabled = false;
                try
                {
                    if (!await ValidateData())
                    {
                        IsEnabled = true;
                        return;
                    }

                    if (!ConnectionService.CheckConnection())
                    {
                        IsEnabled = true;
                        return;
                    }

                    LoginDTO loginDTO = new LoginDTO
                    {
                        Email = Email.Trim(),
                        Password = Password,
                    };

                    //ApiServiceHttpClient apiservice = new ApiServiceHttpClient();
                    //BaseResponse<UserLoginDTO> response = await apiservice.PostAsync<LoginDTO, UserLoginDTO>(
                    //    ConstantsApp.URL_BASE,
                    //    ConstantsApp.SERVICE_PREFIX,
                    //    EndPoints.POSTLOGIN,
                    //    loginDTO);

                    //if (!response.Successful)
                    //{
                    //    await App.Current.MainPage.DisplayAlert(
                    //        AppResources.Error,
                    //        string.Join(Environment.NewLine, response.Errors.ToArray()),
                    //        AppResources.Accept);
                    //    Password = string.Empty;
                    //    IsEnabled = true;
                    //    return;
                    //}

                    //App.UserLoginDTO = response.DataResponse;
                    //Preferences.Set(TextStrings.KeyIsLogin, true);
                    //Preferences.Set(TextStrings.KeyUser, JsonConvert.SerializeObject(response.DataResponse));

                    //if (!Preferences.ContainsKey(TextStrings.KeyPassword))
                    //    Preferences.Set(TextStrings.KeyPassword, App.UserLoginDTO.Password);
                    
                }
                catch (Exception ex)
                {
                    await App.Current.MainPage.DisplayAlert(AppResources.Error, ex.Message, AppResources.Accept);
                    IsEnabled = true;
                }
            }
        }

        private void ShowPassword()
        {
            IsPassword = IsPassword ? false : true;
            IconShowPassword = IsPassword ? "\uf06e" : "\uf070";
        }

        private async Task<bool> ValidateData()
        {
            if (string.IsNullOrEmpty(Email) || !_regexHelper.IsValidEmail(Email))
            {
                await App.Current.MainPage.DisplayAlert(AppResources.Error, AppResources.EmailValidationMessage, AppResources.Accept);
                return false;
            }

            if (string.IsNullOrEmpty(Password))
            {
                await App.Current.MainPage.DisplayAlert(AppResources.Error, AppResources.PasswordValidationMessage, AppResources.Accept);
                return false;
            }
            return true;
        }
        #endregion [ Methods ]
    }
}
