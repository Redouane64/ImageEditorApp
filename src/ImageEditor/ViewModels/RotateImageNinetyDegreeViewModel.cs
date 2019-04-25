using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ImageEditor.ViewModels
{
    internal class RotateImageNinetyDegreeViewModel : ViewModelBase
    {

        public RotateImageNinetyDegreeViewModel()
        {
            this.RotateImageCommand = new Command(this.ApplyRotation);
        }

        public Command RotateImageCommand { get; }

        private void ApplyRotation()
        {

        }

    }
}
