using System;
using Xamarin.Forms;

using SkiaSharp;
using System.IO;
using ImageEditor.Extensions;
using System.Threading.Tasks;
using ImageEditor.Utilities;
using ImageEditor.Models;

namespace ImageEditor.ViewModels
{
    internal class RotateImageEditorViewModel : ViewModelBase
    {
        public enum RotationDirection
        {
            CW, CCW
        }

        public static RotationDirection CW => RotationDirection.CW;
        public static RotationDirection CCW => RotationDirection.CCW;

        private int angle = 0;

        public RotateImageEditorViewModel()
        {
            this.RotateCommand = new Command<RotationDirection>(async (arg) => await this.ApplyRotationAsync(arg));
        }


        public int Angle
        {
            get => this.angle;
            set => base.SetProperty(ref this.angle, value, nameof(Angle), new Action(AngleChanges));
        }

        public Command RotateCommand { get; }

        private async Task ApplyRotationAsync(RotationDirection direction)
        {
            var data = new BitmapData
            {
                Height = MasterViewModel.Current.Bitmap.Height,
                Width = MasterViewModel.Current.Bitmap.Width,
                Pixels = MasterViewModel.Current.Bitmap.Pixels,
                Extras = direction
            };

            //MasterViewModel.Current.ImageStream = await MasterViewModel.Current.Bitmap.ApplyOperationAsync(this.Rotate, data);
            var bitmap = await MasterViewModel.Current.Bitmap.ApplyOperationAsync(Rotate, data);
            MasterViewModel.Current.ImageStream = await bitmap.ToPngStreamAsync();
        }

        private BitmapData Rotate(BitmapData data)
        {
            int height = data.Height,
                width = data.Width;

            var direction = (RotationDirection)data.Extras;

            if (direction == RotationDirection.CW)
            {
                data.Pixels = PixelsArrayHelpers.RotateCW(data.Pixels, height, width);
            }
            else if (direction == RotationDirection.CCW)
            {
                data.Pixels = PixelsArrayHelpers.RotateCCW(data.Pixels, height, width);
            }

            data.Height = width;
            data.Width = height;

            return data;
        }

        private void AngleChanges()
        {
            // TODO:
        }
    }
}
