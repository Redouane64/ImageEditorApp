using System;
using System.Collections.Generic;
using System.Text;

namespace ImageEditor.ViewModels
{
    internal class ImageScaleEditorViewModel : ViewModelBase
    {
        private float scale;

        public ImageScaleEditorViewModel() { }

        public float Scale
        {
            get => this.scale;
            set => base.SetProperty(ref this.scale, value, nameof(Scale), new Action(ScaleChanges));
        }

        private void ScaleChanges()
        {
            
        }
    }
}
