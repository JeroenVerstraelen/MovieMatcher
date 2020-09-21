using MLToolkit.Forms.SwipeCardView.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MovieMatcher.Model;
using MovieMatcher.View;
using Xamarin.Forms;

namespace MovieMatcher.ViewModel
{
    public class TinderPageViewModel : BasePageViewModel
    {

        public INavigation Navigation;
     
        private ObservableCollection<Movie> _movies = new ObservableCollection<Movie>();
        private uint _threshold;
        public TinderPageViewModel()
        {
            InitializeMovies();

            Threshold = (uint)(Application.Current.MainPage.Width / 3);

            SwipedCommand = new Command<SwipedCardEventArgs>(OnSwipedCommand);
            DraggingCommand = new Command<DraggingCardEventArgs>(OnDraggingCommand);
        }

        public async void InitializeMovies()
        {
            Busy = true;
            List<Movie> loaded_movies = await Database.LoadAll<Movie>();

            foreach (var movie in loaded_movies)
            {
                Console.WriteLine(movie.Id);
                Console.WriteLine(movie.Name);
                Console.WriteLine(movie.Genre);
                Console.WriteLine(movie.trailerUrl);
                movie.setImageSource(Convert.FromBase64String(movie.Image));
                
                //ImageSource.
                Movies.Add(movie);
            }
            Busy = false;

            //byte[] buffer;
            //Stream stream = assembly.GetManifestResourceStream(imagePath)


            /*
            Movies.Add(new Movie() { Name = "Black Panther", Genre = "SciFi", Image=Movie.GetBitmapFromCache(imagePath), 
                    Source=Movie.getImageSource(Movie.GetBitmapFromCache(imagePath))});


            var image2 = new Xamarin.Forms.Image { Source = "pic_3.jpg" };
            Movies.Add(new Movie() { Name = "Do Little", Genre = "Fantasy", Image = Movie.GetBitmapFromCache(imagePath),
                Source = Movie.getImageSource(Movie.GetBitmapFromCache(imagePath))
            });*/
            /*
            Profiles.Add(new Profile() { Name = "Anil", Age = 18, Photo = "pic_1.jpg" });
            Profiles.Add(new Profile() { Name = "Sunil", Age = 23, Photo = "pic_2.jpg" });
            Profiles.Add(new Profile() { Name = "Pragati", Age = 28, Photo = "pic_3.jpg" });
            Profiles.Add(new Profile() { Name = "Nitesh", Age = 33, Photo = "pic_4.jpg" });
            Profiles.Add(new Profile() { Name = "Simeth", Age = 32, Photo = "pic_5.jpg" });
            Profiles.Add(new Profile() { Name = "John", Age = 42, Photo = "pic_6.jpg" });
            Profiles.Add(new Profile() { Name = "Amit", Age = 18, Photo = "pic_7.jpg" });
            Profiles.Add(new Profile() { Name = "Kedar", Age = 45, Photo = "pic_8.jpg" });*/

        }

        public ObservableCollection<Movie> Movies
        {
            get => _movies;
            set
            {
                _movies = value;
                RaisePropertyChanged();
            }
        }

        private bool _busy = true;
        public bool Busy {
            get => _busy;
            set
            {
                _busy = value;
                RaisePropertyChanged();
            }
        }
        public uint Threshold
        {
            get => _threshold;
            set
            {
                _threshold = value;
                RaisePropertyChanged();
            }
        }


        public ICommand SwipedCommand { get; }

        public ICommand DraggingCommand { get; }

        private void OnSwipedCommand(SwipedCardEventArgs eventArgs)
        {
        }

        private void OnDraggingCommand(DraggingCardEventArgs eventArgs)
        {
            switch (eventArgs.Position)
            {
                case DraggingCardPosition.Start:
                    return;

                case DraggingCardPosition.UnderThreshold:
                    break;

                case DraggingCardPosition.OverThreshold:
                    break;

                case DraggingCardPosition.FinishedUnderThreshold:
                 
                    return;

                case DraggingCardPosition.FinishedOverThreshold:
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

    }
}
