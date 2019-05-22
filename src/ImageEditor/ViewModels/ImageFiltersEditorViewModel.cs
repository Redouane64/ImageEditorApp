using ImageEditor.Extensions;
using ImageEditor.Filters;
using ImageEditor.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ImageEditor.ViewModels
{
    internal class ImageFiltersEditorViewModel : ViewModelBase
    {
        public Command ApplyFilterCommand { get; }

        public IImageFilter InvertColorsFilter { get; }
        public IImageFilter BlackAndWhiteFilter { get; }

        public ImageFiltersEditorViewModel()
        {
            ApplyFilterCommand = new Command<IImageFilter>(async (arg) => await ApplyFilter(arg));
            InvertColorsFilter = new InvertColorsFilter();
            BlackAndWhiteFilter = new BlackAndWhiteFilter();
        }

        private async Task ApplyFilter(IImageFilter filter)
        {
            var data = new BitmapData
            {
                Height = MasterViewModel.Current.Bitmap.Height,
                Width = MasterViewModel.Current.Bitmap.Width,
                Pixels = MasterViewModel.Current.Bitmap.Pixels
            };

            var result = await MasterViewModel.Current.Bitmap.ApplyOperationAsync((arg) =>
            {
                arg.Pixels = filter.Apply(arg.Pixels);
                return arg;

            }, data);

            MasterViewModel.Current.ImageStream = await result.ToPngStreamAsync();
        }
    }
}
