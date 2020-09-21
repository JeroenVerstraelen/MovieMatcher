using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using MovieMatcher.View.Animations;

namespace MovieMatcher.View
{
    public partial class LoginPage2 : ContentPage
    {
        public LoginPage2()
        {
            InitializeComponent();
            var ViewModel = new MovieMatcher.ViewModel.LoginPageViewModel();
            ViewModel.Navigation = Navigation;
            BindingContext = ViewModel;

        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            Task.Run(async () =>
            {
                await ViewAnimations.FadeAnimY(Logo);
                await ViewAnimations.FadeAnimY(FaceButton);
                await ViewAnimations.FadeAnimY(LoginButton);
                await ViewAnimations.FadeAnimY(SignupButton);
            });
        }
    }
}