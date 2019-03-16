using System;
using System.Reflection;

namespace NLayerdApp.DynamicModelsDefnition
{
    public enum DynamicTypeCode
    {
        String = TypeCode.String,
        Int16 = TypeCode.Int16,
        Int32 = TypeCode.Int32,
        Int64 = TypeCode.Int64,
        UInt16 = TypeCode.UInt16,
        UInt32 = TypeCode.UInt32,
        UInt64 = TypeCode.UInt64,
        Single = TypeCode.Single,
        Char = TypeCode.Char,
        Byte = TypeCode.Byte,
        SByte = TypeCode.SByte,
        //DbNull = TypeCode.DBNull,
        DateTime = TypeCode.DateTime,
        Bool = TypeCode.Boolean,
        Decimal = TypeCode.Decimal,
        Double = TypeCode.Double, 
        Object = TypeCode.Object,
        Empty = TypeCode.Empty,
        Guid = 19,    
        State,
        RelatedCollection,
        RelatedType
    }

    public static class DynamicTypeCodeExtensions
    {
        public static TypeInfo ToType(this DynamicTypeCode dynamicTypeCode)
        {
            return ToType(dynamicTypeCode, typeof(object).Name);
        }

        public static TypeInfo ToType(this DynamicTypeCode dynamicTypeCode, string relatedTypeName)
        {
            var dynamicTypeCodeName = Enum.GetName(typeof(DynamicTypeCode), dynamicTypeCode);
            if (dynamicTypeCodeName != "State"
                && dynamicTypeCodeName != "RelatedType"
                && dynamicTypeCodeName != "RelatedCollection")
            {
                throw new ArgumentException("");
            }
            else
            {
                switch (dynamicTypeCode)
                {
                    case DynamicTypeCode.State:
                        //return typeof(State);
                        break;
                    case DynamicTypeCode.RelatedCollection:
                        return Type.GetType(relatedTypeName).GetTypeInfo();
                    case DynamicTypeCode.RelatedType:
                        return Type.GetType(relatedTypeName).GetTypeInfo();
                }
            }
            throw new ArgumentException("");
        }

        public static DynamicTypeCode ToTypeCode(this Type type)
        {
            var extractedType  = Enum.Parse(typeof(DynamicTypeCode), type.Name);
            return extractedType != null ? (DynamicTypeCode)extractedType : DynamicTypeCode.Empty;
        }
    }
}