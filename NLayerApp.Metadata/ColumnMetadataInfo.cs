using System.Reflection;

namespace NLayerApp.Metadata
{
    public class ColumnMetaDataInfo
    {
        public string FieldName { get; set; }
    
        public int Width { get; set; }
    
        public string Caption { get; set; }
    
        public string Format { get; set; }
    
        public string Align { get; set; }
    
        public bool Display { get; set; }
    
        public TypeInfo DataType { get; set; }
    }
}