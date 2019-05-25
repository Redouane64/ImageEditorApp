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

        private PhotosService photoService = null;
        private ControlTemplatesLookupService templatesLookupService = null;

        private Stream imageStream = null;
        private bool isEditing;
        private ControlTemplate editorTemplate;
        private SkiaSharp.SKBitmap bitmap;

        public MasterViewModel()
        {
            MasterViewModel.Current = this;

            this.photoService = new PhotosService();
            this.templatesLookupService = new ControlTemplatesLookupService();

            this.BrowseImageCommand = new Command(async () => this.ImageStream = await this.photoService.PickImageAsync());
            this.TakeImageCommand = new Command(async () => this.ImageStream = await this.photoService.TakeImageAsync());
            this.ToggleEditingModeCommand = new Command(new Action<object>(ToggleEditingMode));
        }

        public bool IsEditing
        {
            get { return isEditing; }
            set { base.SetProperty(ref this.isEditing, value, nameof(IsEditing)); }
        }

        public Stream ImageStream
        {
            /* Return a copy of stream. */
            get
            {
                this.imageStream?.Seek(0, SeekOrigin.Begin);
                return this.imageStream ?? Stream.Null;
            }

            /* this setter will create a new stream and assign it to backing field. */
            set
            {
                // Always make sure that the stored image stream position is 
                // set to zero. this is important when re-using this stream
                // to edit image.
                value?.Seek(0, SeekOrigin.Begin);
                if(value != null)
                    base.SetProperty(ref this.imageStream, value, nameof(ImageStream), OnImageStreamChanges);
            }
        }

        private void OnImageStreamChanges()
        {
            var data = SkiaSharp.SKData.Create(this.imageStream);
            this.bitmap = SkiaSharp.SKBitmap.Decode(data);
        }

        public SkiaSharp.SKBitmap Bitmap
        {
            get => this.bitmap;
        }

        public ControlTemplate EditorTemplate
        {
            get => this.editorTemplate;
            set => base.SetProperty(ref this.editorTemplate, value, nameof(EditorTemplate));
        }

        public Command BrowseImageCommand { get; private set; }

        public Command TakeImageCommand { get; private set; }

        public Command ToggleEditingModeCommand { get; private set; }

        private void ToggleEditingMode(object argument)
        {
            // Toggle editing mode.
            this.IsEditing = !IsEditing;

            // Populate editor view template.
            this.EditorTemplate = this.templatesLookupService.GetTemplate(argument.ToString());

        }

    }
}
