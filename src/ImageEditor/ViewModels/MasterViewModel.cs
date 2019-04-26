using System;
using System.IO;

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

        private Stream imageStream = null;

        public MasterViewModel() { MasterViewModel.Current = this; }

        public MainViewModel ImageAcquirementViewModel { get; private set; } = new MainViewModel();

        public Stream ImageStream
        {
            get => this.imageStream;

            /* this setter will create a new stream and assign it to backing field. */
            set
            {
                try
                {
                    var stream = this.Copy(value);
                    base.SetProperty(ref this.imageStream, stream, nameof(ImageStream));
                }
                finally { }
            }
        }

        private Stream Copy(Stream source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            try
            {
                var buffer = new byte[source.Length];
                source.Read(buffer, 0, buffer.Length);

                // Reset read position of source stream to begining.
                source.Seek(0, SeekOrigin.Begin);

                return new MemoryStream(buffer);
            }
            finally
            {
            }
        }
    }
}
