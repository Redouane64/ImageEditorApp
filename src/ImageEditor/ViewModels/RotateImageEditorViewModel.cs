using System;
using Xamarin.Forms;

namespace ImageEditor.ViewModels
{
    internal class RotateImageEditorViewModel : ViewModelBase
    {

        private int angle = 0;

        public RotateImageEditorViewModel()
        {
            this.RotateCommand = new Command(ApplyRotation);
        }


        public int Angle
        {
            get => this.angle;
            set => base.SetProperty(ref this.angle, value, nameof(Angle), new Action(AngleChanges));
        }

        public Command RotateCommand { get; }

        private void ApplyRotation()
        {

        }

        private void AngleChanges()
        {
            
        }
    }
}
