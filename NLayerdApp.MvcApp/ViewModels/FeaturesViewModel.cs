using System.Collections.Generic;
using System.Reflection;

namespace NLayerdApp.MvcApp.ViewModels
{
    public class FeaturesViewModel
    {
        public FeaturesViewModel()
        {
            Controllers = new List<TypeInfo>();
            MetadataReferences = new List<Microsoft.CodeAnalysis.MetadataReference>();
            TagHelpers = new List<TypeInfo>();
            ViewComponents = new List<TypeInfo>();
        }

        public List<TypeInfo> Controllers { get; set; }
        public List<Microsoft.CodeAnalysis.MetadataReference> MetadataReferences { get; set; }
        public List<TypeInfo> TagHelpers { get; set; }
        public List<TypeInfo> ViewComponents { get; set; }
    }
}