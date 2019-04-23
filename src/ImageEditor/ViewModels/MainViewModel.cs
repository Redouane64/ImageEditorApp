using Plugin.Media;
using Plugin.Media.Abstractions;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ImageEditor.ViewModels
{
    internal class MainViewModel : ViewModelBase
    {
        private ImageSource imageSource = null;
        private SKCanvasView canvas;

        public MainViewModel()
        {
            this.imageSource = ImageSource.FromFile("camera.png");
            this.PickImageCommand = new Command(async () => await this.PickImageAsync());
            this.TakeImageCommand = new Command(async () => await this.TakeImageAsync());
        }

        public ImageSource Image
        {
            get
            {
                return this.imageSource;
            }

            set
            {
                base.SetProperty(ref imageSource, value, nameof(Image));
            }
        }

        public Command PickImageCommand { get; private set; }

        private async Task PickImageAsync()
        {
            await CrossMedia.Current.Initialize();

            if(CrossMedia.Current.IsPickPhotoSupported)
            {
                var media = await CrossMedia.Current.PickPhotoAsync();

                if (media is null) return;

                Image = ImageSource.FromStream(media.GetStream);
            }
        }

        public Command TakeImageCommand { get; private set; }

        private async Task TakeImageAsync()
        {
            await CrossMedia.Current.Initialize();

            if(CrossMedia.Current.IsTakePhotoSupported)
            {
                var media = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions()
                {
                    RotateImage = true
                });

                if (media is null) return;

                Image = ImageSource.FromStream(media.GetStream);                
            }
        }
    }
}
