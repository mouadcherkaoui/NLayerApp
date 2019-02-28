using System.Dynamic;
using System.Reflection;
namespace DotNetCore.Views
{
    public class DynamicPageObject: DynamicObject
    {
        private object _originalObject;
        public DynamicPageObject(object originalObject)
        {
            _originalObject = originalObject;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result =_originalObject
                        .GetType()
                        .GetMethod(binder.Name
                            , BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.Public)
                        .Invoke(_originalObject, null);
            return true;
        }

        public override bool TryGetIndex(GetIndexBinder binder, object[] indexes, out object result)
        {
            string index = (string)indexes[0];
            result = _originalObject.GetType().GetProperty(index).GetValue(_originalObject);
            return true;
        }

        public object this[string index]
        {
            get
            {
                object result = _originalObject.GetType().GetProperty(index).GetValue(_originalObject);
                return result;
            }
        }
    }
}