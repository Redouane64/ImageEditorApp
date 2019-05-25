using ImageEditor.Extensions;
using ImageEditor.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImageEditor.ViewModels
{
    internal class ImageScaleEditorViewModel : ViewModelBase
    {
        private float scale = 1;

        public ImageScaleEditorViewModel() { }

        public float Scale
        {
            get => this.scale;
            set => base.SetProperty(ref this.scale, value, nameof(Scale), new Action(ScaleChanges));
        }

        private async void ScaleChanges()
        {
            var data = new BitmapData
            {
                Height = MasterViewModel.Current.Bitmap.Height,
                Width = MasterViewModel.Current.Bitmap.Width,
                Pixels = MasterViewModel.Current.Bitmap.Pixels
            };

            var scaledImage = await MasterViewModel.Current.Bitmap.ApplyOperationAsync((arg) =>
            {
                arg.Pixels = Utilities.ImageFiltersHelpers.Scale(arg.Pixels, arg.Height, arg.Width, Scale);

                return arg;
            }, data);

            MasterViewModel.Current.ImageStream = await scaledImage.ToPngStreamAsync();
        }
    }
}
