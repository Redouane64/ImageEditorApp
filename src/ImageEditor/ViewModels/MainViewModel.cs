using Plugin.Media;
using Plugin.Media.Abstractions;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ImageEditor.ViewModels
{
    internal class MainViewModel : ViewModelBase
    {
        private object currentEditView = null;
        private bool isEditMode = false;

        public MainViewModel()
        {
            this.PickImageCommand = new Command(async () => await this.PickImageAsync());
            this.TakeImageCommand = new Command(async () => await this.TakeImageAsync());
            this.RotateImageCommand = new Command(RotateImage);
        }

        public bool IsEditMode
        {
            get { return this.isEditMode;  }
            set { SetProperty(ref this.isEditMode, value, nameof(IsEditMode)); }
        }

        public object CurrentEditView
        {
            get { return this.currentEditView; }
            set { SetProperty(ref this.currentEditView, value, nameof(CurrentEditView)); }
        }

        public Command RotateImageCommand { get; }

        private void RotateImage(object argument)
        {
            IsEditMode = !IsEditMode;

            if(IsEditMode)
            {
                var rotateImageEditView = new Views.RotateImageView();
                rotateImageEditView.BindingContext = new ViewModels.RotateImageNinetyDegreeViewModel();

                this.CurrentEditView = rotateImageEditView;
            }
            else
            {

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

                MasterViewModel.Current.ImageStream = media.GetStream();
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

                MasterViewModel.Current.ImageStream = media.GetStream();
            }
        }

    }
}
