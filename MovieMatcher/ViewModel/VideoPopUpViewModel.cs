using System;
using System.Collections.Generic;
using System.Text;
using MovieMatcher.Model;

namespace MovieMatcher.ViewModel
{
    class VideoPopUpViewModel : BasePageViewModel
    {

        public VideoPopUpViewModel(Movie movie)
        {
            VideoSource = movie.trailerUrl;
        }

        private string _videoSource = "";
        public string VideoSource
        {
            get => _videoSource;
            set
            {
                _videoSource = value;
                RaisePropertyChanged();
            }
        }
    }
}
