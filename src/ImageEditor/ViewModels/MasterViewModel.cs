﻿using System;
using System.IO;
using System.Linq;
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
                if (this.imageStream == null) return null;

                return Utilities.StreamHelpers.Copy(this.imageStream);
            }

            /* this setter will create a new stream and assign it to backing field. */
            set
            {
                if (value == null) return;

                var stream = Utilities.StreamHelpers.Copy(value);
                base.SetProperty(ref this.imageStream, stream, nameof(ImageStream));
            }
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
            this.EditorTemplate = this.templatesLookupService.GetTemplate(argument as Type);

        }

    }
}
