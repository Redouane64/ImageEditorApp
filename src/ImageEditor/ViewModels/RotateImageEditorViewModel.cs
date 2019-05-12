using System;
using Xamarin.Forms;

using SkiaSharp;
using System.IO;
using ImageEditor.Extensions;
using System.Threading.Tasks;
using ImageEditor.Utilities;

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

        private Services.ImageEditingService imageEditingService;
        private int angle = 0;

        public RotateImageEditorViewModel()
        {
            this.imageEditingService = new Services.ImageEditingService();
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

            MasterViewModel.Current.ImageStream = await MasterViewModel.Current.Bitmap.ApplyOperationAsync((source) => {

                int height = MasterViewModel.Current.Bitmap.Height, 
                    width = MasterViewModel.Current.Bitmap.Width;

                var sourcePixels = source as SkiaSharp.SKColor[];
                SkiaSharp.SKColor[] pixels = null;

                if (direction == RotationDirection.CW)
                {
                    pixels = PixelsArrayHelpers.RotateCW(sourcePixels, height, width);
                }
                else if (direction == RotationDirection.CCW)
                {
                    pixels = PixelsArrayHelpers.RotateCCW(sourcePixels, height, width);
                }

                return pixels;
            });

        }

        private void AngleChanges()
        {
            // TODO:
        }
    }
}
