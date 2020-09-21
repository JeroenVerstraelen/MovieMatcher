using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using MovieMatcher.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MovieMatcher.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FriendPage : ContentPage
    {
        public ObservableCollection<string> Items { get; set; }

        public FriendPage()
        {
            Title = "Add friends";
            InitializeComponent();
            FriendPageViewModel viewModel = new FriendPageViewModel();
            viewModel.Navigation = Navigation;
            BindingContext = viewModel;
        }
    }
}
