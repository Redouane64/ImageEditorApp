using System;
using Xamarin.Forms;

using SkiaSharp;
using System.IO;
using ImageEditor.Extensions;
using System.Threading.Tasks;

namespace ImageEditor.ViewModels
{
    internal class RotateImageEditorViewModel : ViewModelBase
    {
        private Services.ImageEditingService imageEditingService;
        private int angle = 0;

        public RotateImageEditorViewModel()
        {
            this.imageEditingService = new Services.ImageEditingService();
            this.RotateCommand = new Command(async () => await this.ApplyRotationAsync());
        }


        public int Angle
        {
            get => this.angle;
            set => base.SetProperty(ref this.angle, value, nameof(Angle), new Action(AngleChanges));
        }

        public Command RotateCommand { get; }

        private async Task ApplyRotationAsync()
        {

            MasterViewModel.Current.ImageStream = await MasterViewModel.Current.Bitmap.ApplyOperationAsync((source) => {

                int height = MasterViewModel.Current.Bitmap.Height, 
                    width = MasterViewModel.Current.Bitmap.Width;

                var a_copy = this.imageEditingService.RotateCW(source, height, width);

                return a_copy;
            });

        }

        private void AngleChanges()
        {
            // TODO:
        }
    }
}
