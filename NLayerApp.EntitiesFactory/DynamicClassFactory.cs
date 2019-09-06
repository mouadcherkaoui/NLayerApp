using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.Serialization;
using NLayerApp.DynamicModelDefnition;
using NLayerApp.Infrastructure.Models;
using NLayerApp.Models;

namespace NLayerApp.DataStructureGenerator
{
    public class DynamicClassFactory
    {
        //private AppDomain _appDomain;
        private AssemblyBuilder _assemblyBuilder;
        private ModuleBuilder _moduleBuilder;
        private TypeBuilder _typeBuilder;
        private string _assemblyName;
        private Dictionary<DynamicType, TypeBuilder> _typeBuilders;
        private const MethodAttributes defaultGetterAndSetterAttributes = MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.SpecialName;

        public Assembly CurrentAssembly { get; set; }

        #region Constructors

        public DynamicClassFactory() : this("Dynamic.Entities")
        { }

        public DynamicClassFactory(string assemblyName)
        {
            _assemblyName = assemblyName;
            //_appDomain = Thread.GetDomain();

            InitializeAssemblyAndModuleBuilders();
        }

        #endregion

        #region DynamicType Generation using Metadata

        public List<TypeInfo> CreateDynamicTypes<T>(List<DynamicType> types) //where T : DynamicEntity
        {
            return CreateDynamicTypes<T>(types, typeof(IEntity));
        }

        public List<TypeInfo> CreateDynamicTypes<T>(List<DynamicType> types, Type interfaceType) //where T : DynamicEntity
        {
            _typeBuilders = new Dictionary<DynamicType, TypeBuilder>();
            List<TypeInfo> typeList = new List<TypeInfo>();
            foreach (var type in types)
            {
                _typeBuilders.Add(type, 
                                _moduleBuilder
                                    .DefineType(
                                        string.Format("{0}.{1}", _assemblyName, type.Name), 
                                        TypeAttributes.AnsiClass | TypeAttributes.Public | TypeAttributes.BeforeFieldInit, 
                                        typeof(T)));
            }

            foreach (var current in _typeBuilders)
            {
                _typeBuilder = current.Value;

                //interfaceType = Type.GetType(current.Key.InterfaceType);
                var propertiesInfos = typeof(T).GetRuntimeProperties().Concat(interfaceType.GetRuntimeProperties()).ToList();

                var properties = current.Key.Fields.ToList();

                properties
                    .Where(p => p.IsRelatedCollection || p.IsRelatedType)
                    .ToList()
                    .ForEach(p => {
                        p.FieldType = _typeBuilders.FirstOrDefault(tb => tb.Value.FullName == p.RelatedTypeName).Value.CreateTypeInfo();
                        if (p.IsRelatedCollection)
                        {                           
                            p.FieldType = typeof(ICollection<>).MakeGenericType(new Type[] { p.FieldType.AsType() }).GetTypeInfo();
                        }
                    });

                foreach (var propertyInfo in propertiesInfos)
                {
                    properties.RemoveAll(p => p.Name == propertyInfo.Name);
                    //properties.
                }

                // if (interfaceType.GetGenericTypeDefinition() != null)
                // {
                //     var exportType = interfaceType.GetGenericTypeDefinition();

                //     var customAttributeBuilder = AddCustomAttribute<ExportAttribute>(new Type[] { typeof(string), typeof(Type) }, new object[] { "IEntity", exportType });
                //     _typeBuilder.SetCustomAttribute(customAttributeBuilder);
                // }

                CreateProperties(_typeBuilder, properties);

                AddInterfaceMembers(_typeBuilder, interfaceType);

                typeList.Add(_typeBuilder.CreateTypeInfo());
            }
            CurrentAssembly = typeList.FirstOrDefault().Assembly;

            return typeList;
        }

        public Type CreateDynamicType(string name, List<DynamicFieldType> properties)
        {
            return CreateDynamicType(typeof(BaseEntity), name, properties);
        }

        public Type CreateDynamicType<T>(string name, List<DynamicFieldType> properties) //where T : DynamicEntity
        {
            return CreateDynamicType(typeof(T), name, properties);
        }

        public Type CreateDynamicType(Type type, string name, List<DynamicFieldType> properties)
        {
            var typeBuilder = CreateDynamicTypeBuilder(type, name, properties);
            return typeBuilder.AsType();
        }
        #region TypeBuilder Creation
        public TypeBuilder CreateDynamicTypeBuilder(string name, List<DynamicFieldType> properties)
        {
            return CreateDynamicTypeBuilder(typeof(BaseEntity), name, properties);
        }

        public TypeBuilder CreateDynamicTypeBuilder(Type type, string name, List<DynamicFieldType> properties)
        {
            _typeBuilder = _moduleBuilder
                .DefineType(
                    string.Format("{0}.{1}", _assemblyName, name),
                    TypeAttributes.Public | TypeAttributes.Class | TypeAttributes.AnsiClass
                    | TypeAttributes.Serializable | TypeAttributes.BeforeFieldInit,
                    type);

            //AddCustomAttribute<ExportAttribute>(new Type[] { typeof(Type)}, new object[] { typeof(IEntity) });
            var propertiesInfos = type.GetRuntimeProperties();

            foreach (var propertyInfo in propertiesInfos)
            {
                properties.RemoveAll(p => p.Name == propertyInfo.Name);
            }

            CreateProperties(_typeBuilder, properties);

            AddInterfaceMembers<IEntity>(_typeBuilder);
            return _typeBuilder;            
        }
        public TypeBuilder CreateDynamicTypeBuilder<T>(string name, List<DynamicFieldType> properties) //where T : DynamicEntity
        {
            return CreateDynamicTypeBuilder(typeof(T), name, properties);
        }

        private void InitializeAssemblyAndModuleBuilders()
        {
            if (_assemblyBuilder == null)
                //_assemblyBuilder = _appDomain.DefineDynamicAssembly(new AssemblyName(_assemblyName), AssemblyBuilderAccess.RunAndSave);

            if (_moduleBuilder == null)
                _moduleBuilder = _assemblyBuilder.DefineDynamicModule(string.Format("{0}.dll", _assemblyName));
        }

        private CustomAttributeBuilder AddCustomAttribute<T>(Type[] constructorTypes, object[] constructorParameters)
        {
            var attributeType = typeof(T);
            return new CustomAttributeBuilder(attributeType.GetTypeInfo().GetConstructor(constructorTypes), constructorParameters);
            //_typeBuilder.SetCustomAttribute(new CustomAttributeBuilder(attributeType.GetConstructor(constructorTypes), constructorParameters));
        }

        private void AddCustomAttributes(Type attributeType)
        {
            _typeBuilder.SetCustomAttribute(new CustomAttributeBuilder(attributeType.GetTypeInfo().GetConstructor(new[] { typeof(Type) }), new object[] { typeof(IEntity<>) }));
        }

        #endregion

        #region Backing Fields creation 
        private void CreateProperties(TypeBuilder _typeBuilder, List<DynamicFieldType> properties)
        {
            properties.ToList().ForEach(p => CreateFieldForType(p, p.Name));
        }

        private void CreateFieldForType(DynamicFieldType fieldType, string name, string relatedTypeName)
        {
            if (fieldType.IsRelatedCollection)
            {
                fieldType.FieldType = typeof(List<>).MakeGenericType(new Type[] { fieldType.FieldTypeCode.ToType(relatedTypeName).AsType() }).GetTypeInfo();
            }
            else
            {
                fieldType.FieldType = fieldType.FieldTypeCode.ToType(relatedTypeName);
            }
            CreateFieldForType(fieldType, name);
        }

        private void CreateFieldForType(DynamicFieldType fieldType, string name, MethodAttributes getterAndSetterAttributes = defaultGetterAndSetterAttributes, MethodInfo methodInfo = null)
        {
            var type =
                (fieldType.FieldTypeCode == DynamicTypeCode.RelatedCollection || fieldType.FieldTypeCode == DynamicTypeCode.RelatedType)
                ? fieldType.FieldType
                : fieldType.FieldTypeCode.ToType();
            FieldBuilder fieldBuilder = _typeBuilder.DefineField(string.Format("<{0}>k__BackingField", name), type.AsType(), FieldAttributes.Private);

            PropertyBuilder propertyBuilder = _typeBuilder.DefineProperty(name, PropertyAttributes.HasDefault, type.AsType(), null);

            AddValidationAttributes(fieldType.ValidationRules, propertyBuilder);

            propertyBuilder.SetGetMethod(CreateGetMethod(getterAndSetterAttributes, name, type.AsType(), fieldBuilder));

            propertyBuilder.SetSetMethod(CreateSetMethod(getterAndSetterAttributes, name, type.AsType(), fieldBuilder, methodInfo));
        }

        #endregion

        #region ValidationAttribute application

        private void AddValidationAttributes(IEnumerable<DynamicValidationRule> validationRules, PropertyBuilder propertyBuilder)
        {
            if (validationRules != null)
                foreach (var rule in validationRules)
                {
                    CustomAttributeBuilder attributeBuilder = ConfigureValidationRuleAttributeBuilder(rule);
                    propertyBuilder.SetCustomAttribute(attributeBuilder);
                }
        }

        private CustomAttributeBuilder ConfigureValidationRuleAttributeBuilder(DynamicValidationRule rule)
        {
            Type attributeType;

            PropertyInfo[] properties;
            object[] values;

            switch (rule.ValidationRule)
            {
                case ValidationRule.Key:
                    //attributeType = typeof(KeyAttribute);
                    return AddCustomAttribute<KeyAttribute>(Type.EmptyTypes, new object[] { });

                case ValidationRule.Required:
                    //attributeType = typeof(RequiredAttribute);
                    return AddCustomAttribute<RequiredAttribute>(Type.EmptyTypes, new object[] { });

                case ValidationRule.ColumnName:
                    attributeType = typeof(ColumnAttribute);
                    properties = new[]
                    {
                        typeof(ColumnAttribute).GetProperty("Name"),
                    };

                    values = new object[]
                    {
                        rule.Parameters.FirstOrDefault(p=> p.Name == "ColumnName")?.ParameterValue ?? "",
                    };

                    return new CustomAttributeBuilder(
                        attributeType.GetConstructor(new Type[] { typeof(string) }),
                        new object[] { values.FirstOrDefault() });

                case ValidationRule.MinLength:
                    attributeType = typeof(MinLengthAttribute);
                    return new CustomAttributeBuilder(
                        attributeType.GetConstructor(new Type[] { typeof(int) }),
                        new object[] { Convert.ToInt32(rule.Parameters.FirstOrDefault(p => p.Name == "length").ParameterValue) });

                case ValidationRule.MaxLength:
                    attributeType = typeof(MaxLengthAttribute);
                    return new CustomAttributeBuilder(
                        attributeType.GetConstructor(new Type[] { typeof(int) }),
                        new object[] { Convert.ToInt32(rule.Parameters.FirstOrDefault(p => p.Name == "length").ParameterValue) });

                case ValidationRule.DataType:
                    attributeType = typeof(DataTypeAttribute);
                    break;
                case ValidationRule.EmailAddress:
                    attributeType = typeof(EmailAddressAttribute);
                    return new CustomAttributeBuilder(
                        attributeType.GetConstructor(Type.EmptyTypes), new object[] { });

                case ValidationRule.Url:
                    attributeType = typeof(UrlAttribute);
                    return new CustomAttributeBuilder(
                        attributeType.GetConstructor(Type.EmptyTypes), new object[] { });

                case ValidationRule.StringLength:
                    attributeType = typeof(StringLengthAttribute);
                    return new CustomAttributeBuilder(
                        attributeType.GetConstructor(new Type[] { typeof(int) }),
                        new object[] { Convert.ToInt32(rule.Parameters.FirstOrDefault(p => p.Name == "length").ParameterValue) });

                case ValidationRule.Display:
                    attributeType = typeof(DisplayAttribute);
                    return new CustomAttributeBuilder(
                        attributeType.GetConstructor(new Type[] { typeof(string) }),
                        new object[] { rule.Parameters.FirstOrDefault(p => p.Name == "name").ParameterValue });
                case ValidationRule.AutoGeneratedKey:
                    attributeType = typeof(DatabaseGeneratedAttribute);
                    return new CustomAttributeBuilder(
                        attributeType.GetConstructor(new Type[] { typeof(DatabaseGeneratedOption) }),
                        new object[] { DatabaseGeneratedOption.Identity });
                case ValidationRule.NotMapped:
                    attributeType = typeof(NotMappedAttribute);
                    return new CustomAttributeBuilder(
                        attributeType.GetConstructor(Type.EmptyTypes), new object[] { });
            }

            return null;
        }

        public void AddDataContractAttribute()
        {
            Type attributeType = typeof(DataContractAttribute);
            _typeBuilder.SetCustomAttribute(new CustomAttributeBuilder(attributeType.GetConstructor(Type.EmptyTypes), new object[] { }));
        }

        public void AddTableAttribute(string name)
        {
            Type attributeType = typeof(TableAttribute);
            _typeBuilder.SetCustomAttribute(new CustomAttributeBuilder(attributeType.GetConstructor(new[] { typeof(string) }), new object[] { name }));
        }

        // public void AddDataServiceKeyAttribute()
        // {
        //     Type attributeType = typeof(DataServiceKeyAttribute);
        //     _typeBuilder.SetCustomAttribute(new CustomAttributeBuilder(attributeType.GetConstructor(new[] { typeof(string) }), new object[] { "Id" }));
        // }

        #endregion

        #region Interfaces implementation
        private void AddInterfaceProperty(TypeBuilder typeBuilder, PropertyInfo propertyInfo)
        {
            var typeCode = propertyInfo.PropertyType.ToTypeCode();
            DynamicFieldType type = new DynamicFieldType() { Name = propertyInfo.Name, FieldTypeCode = propertyInfo.PropertyType.ToTypeCode() };

            FieldBuilder fieldBuilder = typeBuilder.DefineField(string.Format("<{0}>__BackingField", type.Name.ToLowerInvariant()), type.FieldTypeCode.ToType().AsType(), FieldAttributes.Private);
            PropertyBuilder propertyBuilder = typeBuilder.DefineProperty(type.Name, PropertyAttributes.HasDefault, type.FieldTypeCode.ToType().AsType(), null);

            if(propertyInfo.CustomAttributes.Count() > 0)
            {
                propertyInfo
                    .CustomAttributes                    
                    .ToList()
                    .ForEach(a => 
                    {
                        var t = a.GetType();
                        List<object> values = new List<object>();
                        a.ConstructorArguments.ToList().ForEach((v) => values.Add(v.Value));
                        propertyBuilder.SetCustomAttribute(new CustomAttributeBuilder(a.Constructor, values.ToArray()));
                    });
            }


            MethodAttributes getterAndSetterAttributes = MethodAttributes.Final | MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.SpecialName  | MethodAttributes.NewSlot | MethodAttributes.Virtual;

            propertyBuilder.SetGetMethod(CreateGetMethod(getterAndSetterAttributes, type.Name, type.FieldTypeCode.ToType().AsType(), fieldBuilder));

            propertyBuilder.SetSetMethod(CreateSetMethod(getterAndSetterAttributes, type.Name, type.FieldTypeCode.ToType().AsType(), fieldBuilder));
        }

        private void AddInterfaceMethod(TypeBuilder typeBuilder, MethodInfo methodInfo)
        {
            MethodBuilder methodBuilder = typeBuilder.DefineMethod(methodInfo.Name,
                methodInfo.Attributes | MethodAttributes.SpecialName | MethodAttributes.Virtual,
                methodInfo.ReturnType,
                methodInfo.GetParameters().Select(p => p.GetType()).ToArray());

            ILGenerator methodILGenerator = methodBuilder.GetILGenerator();
        }

        private void AddInterfaceMembers(TypeBuilder typeBuilder, Type interfaceType)
        {
            foreach (var current in interfaceType.GetMembers())
            {
                if (current.MemberType == MemberTypes.Method
                    && !current.Name.StartsWith("get_")
                    && !current.Name.StartsWith("set_"))
                    AddInterfaceMethod(typeBuilder, current as MethodInfo);
                if (current.MemberType == MemberTypes.Property)
                {
                    //if(current.Name)
                    AddInterfaceProperty(typeBuilder, current as PropertyInfo);
                }
            }
            //foreach (var current in interfaceType.GetMethods())
            //{
            //    AddInterfaceMethod(typeBuilder, current);
            //}
            //foreach (var current in interfaceType.GetProperties())
            //{
            //    AddInterfaceProperty(typeBuilder, current);
            //}

            typeBuilder.AddInterfaceImplementation(interfaceType);
        }
        private void AddInterfaceMembers<T>(TypeBuilder typeBuilder)
        {
            var interfaceType = typeof(T);
            AddInterfaceMembers(typeBuilder, interfaceType);            
        }

        #endregion

        #endregion

        #region Getter and Setter creation
        private MethodBuilder CreateGetMethod(MethodAttributes getterAndSetterAttributes, string name, Type type, FieldBuilder fieldBuilder)
        {
            var getMethodBuilder = _typeBuilder.DefineMethod(string.Format("get_{0}", name), getterAndSetterAttributes, type, null);

            var getMethodILGenerator = getMethodBuilder.GetILGenerator();
            getMethodILGenerator.Emit(OpCodes.Ldarg_0);
            getMethodILGenerator.Emit(OpCodes.Ldfld, fieldBuilder);
            getMethodILGenerator.Emit(OpCodes.Ret);

            return getMethodBuilder;
        }

        private MethodBuilder CreateSetMethod(MethodAttributes getterAndSetterAttributes, string name, Type type, FieldBuilder fieldBuilder)
        {
            return CreateSetMethod(getterAndSetterAttributes, name, type, fieldBuilder, null);
        }

        private MethodBuilder CreateSetMethod(MethodAttributes getterAndSetterAttributes, string name, Type type, FieldBuilder fieldBuilder, MethodInfo methodInfo)
        {
            var setMethodBuilder = _typeBuilder.DefineMethod(string.Format("set_{0}", name), getterAndSetterAttributes, null, new Type[] { type });

            var setMethodILGenerator = setMethodBuilder.GetILGenerator();
            setMethodILGenerator.Emit(OpCodes.Ldarg_0);
            setMethodILGenerator.Emit(OpCodes.Ldarg_1);
            setMethodILGenerator.Emit(OpCodes.Stfld, fieldBuilder);

            if (methodInfo != null)
            {
                setMethodILGenerator.Emit(OpCodes.Ldarg_0);
                setMethodILGenerator.Emit(OpCodes.Ldstr, name);
                setMethodILGenerator.EmitCall(OpCodes.Call, methodInfo, null);
            }

            setMethodILGenerator.Emit(OpCodes.Ret);

            return setMethodBuilder;
        }

        #endregion

        public void SaveAssembly()
        {
            //_assemblyBuilder.Save(string.Format("{0}.dll", _assemblyBuilder.GetName().Name));
        }
    }
}

