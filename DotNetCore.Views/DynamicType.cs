using System;

namespace DotNetCore.Views
{
    public class DynamicType<T> //: Type
    {
        public string AssemblyQualifiedName => typeof(T).AssemblyQualifiedName;

        public string FullName => typeof(T).FullName;

        public int GenericParameterPosition => typeof(T).GenericParameterPosition;

        public Type[] GenericTypeArguments => typeof(T).GenericTypeArguments;

        public bool IsConstructedGenericType => typeof(T).IsConstructedGenericType;

        public bool IsGenericParameter => typeof(T).IsGenericParameter;

        public string Namespace => typeof(T).Namespace;

        public Type DeclaringType => typeof(T).DeclaringType;

        public string Name => typeof(T).Name;

        public int GetArrayRank()
        {
            return typeof(T).GetArrayRank();
        }

        public Type GetElementType()
        {
            return typeof(T).GetElementType();
        }

        public Type GetGenericTypeDefinition()
        {
            return typeof(T).GetGenericTypeDefinition();
        }

        public Type MakeArrayType()
        {
            return typeof(T).MakeArrayType();
        }

        public Type MakeArrayType(int rank)
        {
            return typeof(T).MakeArrayType(rank);
        }

        public Type MakeByRefType()
        {
            return typeof(T).MakeByRefType();
        }

        public Type MakeGenericType(params Type[] typeArguments)
        {
            return typeof(T).MakeGenericType(typeArguments);
        }

        public Type MakePointerType()
        {
            return typeof(T).MakePointerType();
        }
    }
}