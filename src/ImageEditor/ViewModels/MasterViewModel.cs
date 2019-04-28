using System;
using System.IO;
using ImageEditor.Services;
using Xamarin.Forms;

namespace ImageEditor.ViewModels
{
    /// <summary>
    /// Represents parent view-model.
    /// </summary>
    internal class MasterViewModel : ViewModelBase
    {
        /* Only one master view-model instance, this is needed so
         * childern view-models have a easy way to refernece this 
         * parent view-model. Especially in order to access ImageStream
         * property.
         */
        public static MasterViewModel Current { get; private set; }

        public PhotosService photoService = null;

        private Stream imageStream = null;

        public MasterViewModel()
        {
            MasterViewModel.Current = this;

            this.photoService = new PhotosService();

            this.BrowseImageCommand = new Command(async () => await this.photoService.PickImageAsync());
            this.TakeImageCommand = new Command(async () => await this.photoService.TakeImageAsync());
        }

        private bool isEditing;

        public bool IsEditing
        {
            get { return isEditing; }
            set { base.SetProperty(ref this.isEditing, value, nameof(IsEditing)); }
        }

        public Stream ImageStream
        {
            /* Return a copy of stream. */
            get => Utilities.StreamHelpers.Copy(this.imageStream);

            /* this setter will create a new stream and assign it to backing field. */
            set
            {
                var stream = Utilities.StreamHelpers.Copy(value);
                base.SetProperty(ref this.imageStream, stream, nameof(ImageStream));
            }
        }

        public Command BrowseImageCommand { get; private set; }

        public Command TakeImageCommand { get; private set; }

    }
}
