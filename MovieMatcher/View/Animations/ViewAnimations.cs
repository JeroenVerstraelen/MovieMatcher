using System.Threading.Tasks;
using Xamarin.Forms;

namespace MovieMatcher.View.Animations
{
    public static class ViewAnimations
    {
        public static async Task FadeAnimY(Xamarin.Forms.View view)
        {

            await Task.WhenAll
               (
                    view.FadeTo(1, 200),
                    view.TranslateTo(0, 0, 200)
               );
        }
    }
}
