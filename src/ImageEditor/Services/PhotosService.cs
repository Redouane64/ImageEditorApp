using System;
using System.IO;
using System.Threading.Tasks;
using Plugin.Media;
using Plugin.Media.Abstractions;

namespace ImageEditor.Services
{
    internal class PhotosService
    {
        public PhotosService() { }

        public async Task<Stream> PickImageAsync()
        {
            await CrossMedia.Current.Initialize();

            Stream result = null;

            if (CrossMedia.Current.IsPickPhotoSupported)
            {
                var media = await CrossMedia.Current.PickPhotoAsync();

                if (!(media == null)) result = media.GetStream();
            }

            return result;
        }

        public async Task<Stream> TakeImageAsync()
        {
            await CrossMedia.Current.Initialize();

            Stream result = null;

            if (CrossMedia.Current.IsTakePhotoSupported)
            {
                var media = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions()
                {
                    RotateImage = true
                });

                if (!(media == null)) result = media.GetStream();
            }

            return result;
        }
    }
}
