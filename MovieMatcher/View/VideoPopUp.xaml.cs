using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieMatcher.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MovieMatcher.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VideoPopUp : ContentPage
    {

        public VideoPopUp(MovieMatcher.Model.Movie movie)
        {
            Title = "Movie description";
            InitializeComponent();
            BindingContext = new VideoPopUpViewModel(movie);
        }
    }
}