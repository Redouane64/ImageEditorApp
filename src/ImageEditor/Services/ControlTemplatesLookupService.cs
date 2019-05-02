using System;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace ImageEditor.Services
{
    internal class ControlTemplatesLookupService
    {
        
        public ControlTemplate GetTemplate(string key)
        {
            if (String.IsNullOrEmpty(key))
            {
                throw new ArgumentException($"Argument {nameof(key)} is null or empty.");
            }

            if (App.Current.Resources.TryGetValue(key, out var controlTemplate))
                return controlTemplate as ControlTemplate;
            else
                throw new Exception("Control Template resource with provided key does not exist."); // Bad things could happen. :(
        }

    }
}
