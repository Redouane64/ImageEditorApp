using System;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace ImageEditor.Services
{
    internal class ControlTemplatesLookupService
    {
        
        public ControlTemplate GetTemplate(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (App.Current.Resources.TryGetValue(ViewModelTypeNameToControlTemplateResourceKey(type.Name), out var controlTemplate))
                return controlTemplate as ControlTemplate;
            else
                throw new Exception("Control Template resource with provided key does not exist."); // Bad things could happen. :(
        }

        // It may be not a good idea to use Regex, but yeah. deal with it!
        private string ViewModelTypeNameToControlTemplateResourceKey(string token) => Regex.Replace(token, "ViewModel", "ControlTemplate", RegexOptions.ECMAScript & RegexOptions.IgnoreCase & RegexOptions.Compiled);
    }
}
