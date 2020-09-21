using Plugin.GoogleClient;
using Plugin.GoogleClient.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using MovieMatcher.Model;
using MovieMatcher.View;
using Xamarin.Forms;

namespace MovieMatcher.ViewModel
{
    public class LoginPageViewModel : BasePageViewModel
    {
        public INavigation Navigation;

        public bool IsLoggedIn { get; set; }

        public string Token { get; set; }

        public ICommand LoginCommand { get; set; }
        public ICommand LogoutCommand { get; set; }
        private readonly IGoogleClientManager _googleClientManager;

        public static Person User = null;

        public LoginPageViewModel()
        {
            LoginCommand = new Command(LoginAsync);
            LogoutCommand = new Command(Logout);

            _googleClientManager = CrossGoogleClient.Current;

            IsLoggedIn = false;
        }

        public async void LoginAsync()
        {
            _googleClientManager.OnLogin += OnLoginCompleted;
            try
            {
                await _googleClientManager.LoginAsync();
            }
            catch (GoogleClientSignInNetworkErrorException e)
            {
                await App.Current.MainPage.DisplayAlert("Error", e.Message, "OK");
            }
            catch (GoogleClientSignInCanceledErrorException e)
            {
                await App.Current.MainPage.DisplayAlert("Error", e.Message, "OK");
            }
            catch (GoogleClientSignInInvalidAccountErrorException e)
            {
                await App.Current.MainPage.DisplayAlert("Error", e.Message, "OK");
            }
            catch (GoogleClientSignInInternalErrorException e)
            {
                await App.Current.MainPage.DisplayAlert("Error", e.Message, "OK");
            }
            catch (GoogleClientNotInitializedErrorException e)
            {
                await App.Current.MainPage.DisplayAlert("Error", e.Message, "OK");
            }
            catch (GoogleClientBaseException e)
            {
                await App.Current.MainPage.DisplayAlert("Error", e.Message, "OK");
            }

        }


        private async void OnLoginCompleted(object sender, GoogleClientResultEventArgs<GoogleUser> loginEventArgs)
        {
            if (loginEventArgs.Data != null)
            {
                await Navigation.PushAsync(new TinderPage());
                GoogleUser googleUser = loginEventArgs.Data;
                User = new Person() { Nickname = googleUser.Name, Emailaddress = googleUser.Email};
                //googleUser.GivenName; googleUser.FamilyName;
                IsLoggedIn = true;
                Token = CrossGoogleClient.Current.AccessToken;

                List<Person> users = await Database.getUser(User);
                if (users.Count != 0)
                {
                    // User already exists in database.
                    User = users[0];
                    _googleClientManager.OnLogin -= OnLoginCompleted;
                }
                else
                {
                    // Save user to database.
                    if (await Database.Save<Person>(User))
                    {
                        _googleClientManager.OnLogin -= OnLoginCompleted;
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Could not save user.", loginEventArgs.Message, "OK");
                        await Navigation.PopAsync();
                    }
                }

            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Error", loginEventArgs.Message, "OK");
                await Navigation.PopAsync();
            }
        }

        
        public void Logout()
        {
            _googleClientManager.OnLogout += OnLogoutCompleted;
            _googleClientManager.Logout();
        }


        private void OnLogoutCompleted(object sender, EventArgs loginEventArgs)
        {
            IsLoggedIn = false;
            _googleClientManager.OnLogout -= OnLogoutCompleted;
        }
    }
}
