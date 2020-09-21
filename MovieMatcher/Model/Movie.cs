using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.TizenSpecific;

namespace MovieMatcher.Model
{
    public class Movie
    {

        public string Id { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }

        public string Genre { get; set; }

        public string trailerUrl { get; set; }

        public ImageSource Source { get; set; }

        public Xamarin.Forms.Image publicImage { get; set; }

        /*
        public void saveImageToFile(Stream stream)
        {
            FileStream f = new FileStream(@"D:\samp\rt.jpg", FileMode.Create);
            byte[] b = new byte[10000];
            stream.Read(b, 0, b.Length);
            stream.Close();
            f.Write(b, 0, b.Length);
            f.Flush();
            f.Close();
        }*/

        public void setImageSource(byte[] imageAsBytes)
        {
            Source = ImageSource.FromStream(() => new MemoryStream(imageAsBytes));
        }

        /*
        public static byte[] ConvertStreamtoByte(Stream input)
        {
            using (var ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }

        public static byte[] GetBitmapFromCache(string fileName)
        {
            return File.ReadAllBytes(fileName);
        }*/
    }
}
