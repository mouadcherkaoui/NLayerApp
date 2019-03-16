using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;

namespace NLayerAppp.Views.Helpers
{
    public class DynamicEditor : HtmlHelper, IHtmlHelper
    {
        public DynamicEditor(IHtmlGenerator htmlGenerator, ICompositeViewEngine viewEngine, IModelMetadataProvider metadataProvider, IViewBufferScope bufferScope, HtmlEncoder htmlEncoder, UrlEncoder urlEncoder) : base(htmlGenerator, viewEngine, metadataProvider, bufferScope, htmlEncoder, urlEncoder)
        {
        }        
    }

    public static class DynamicHtmlHelperExtensions
    {
        public static IHtmlContent EditorFor(this IHtmlHelper helper, object value, string name)
        {
            var builder = new TagBuilder("input");
            builder.TagRenderMode = TagRenderMode.SelfClosing;

            builder.MergeAttribute("type", "text");
            builder.MergeAttribute("name", name);
            builder.MergeAttribute("value", value.ToString());

            return new HtmlString(
                builder.ToString()
            );
        }

        public static IHtmlContent EditorFor(this IHtmlHelper helper, string propertyName)
        {
            var builder = new TagBuilder("input");
            var model = helper.ViewData.Model;
            object value = null;

            if (model!=null)
            {
                value = model.GetType().GetProperty(propertyName).GetValue(model);
            }

            return EditorFor(helper, value, propertyName);
        }   

        public static IHtmlContent EditorFor<T>(this IHtmlHelper helper, T type, Func<T,object> propertyGetter, string propertyName)
        {
            var builder = new TagBuilder("input");
            builder.TagRenderMode = TagRenderMode.SelfClosing;

            object value = "";
            var model = helper.ViewData.Model;
            if(model!=null)
            {
                value =  propertyGetter(type).ToString();
            }

            return EditorFor(helper, value, propertyName);
        }     

        public static IHtmlContent EditorFor(this IHtmlHelper helper, Type type, object model, string propertyName)
        {
            var typedHelper = helper.GetType().MakeGenericType(new Type[]{type});
            return typedHelper.GetMethod("EditorFor").Invoke(model, new []{propertyName}) as IHtmlContent;
        }

        public static IHtmlContent EditorFor<TModel>(  
                   this HtmlHelper<TModel> htmlHelper, 
                   object instance, 
                   string expression)
        {
            var tType = instance.GetType();
            //TModel = typeof(instance);
            htmlHelper.ViewContext.ViewData.Model = instance;
            var lambda = DynamicExpression.Lambda(Expression.Property(Expression.Parameter(tType, "m"), expression));

            var methods = typeof(HtmlHelper).GetMethods();

            var method = methods.Where(x =>
            {
                var args = x.GetGenericArguments();
                var parms = x.GetParameters();

                return (   args.Count() == 2 
                        && args[0].Name == "TModel"
                        && args[1].Name == "TValue" 
                        && x.Name == "EditorFor"
                        && parms.Count() == 2 
                        && parms[0].ParameterType.Name == "HtmlHelper`1"
                        && parms[1].ParameterType.Name == "Expression`1");
            }).First();

            method = method.MakeGenericMethod(new[] {tType, lambda.Body.Type});

            var htmlHelperType = typeof(HtmlHelper<>)
                                .MakeGenericType(tType);

            var helper = htmlHelper;

            return (IHtmlContent)method.Invoke(
                                                helper, 
                                                new object[]{helper, lambda}
                                                );
        }

        private static IHtmlContent GetPropertyEditor<TModel, TProperty>(this IHtmlHelper htmlHelper, PropertyInfo propertyInfo)
        {
            //Get property lambda expression like "m => m.Property"
            var modelType = typeof(TModel);
            var parameter = Expression.Parameter(modelType, "m");
            var property = Expression.Property(parameter, propertyInfo.Name);
            var propertyExpression = Expression.Lambda<Func<TModel, TProperty>>(property, parameter);

            //Get html string with label, editor and validation message
            var editorContainer = new TagBuilder("div");
            editorContainer.AddCssClass("editor-container");

            editorContainer.InnerHtml.AppendHtml(((IHtmlHelper<TModel>)htmlHelper).LabelFor(propertyExpression));
            editorContainer.InnerHtml.AppendHtml(((IHtmlHelper<TModel>)htmlHelper).EditorFor(propertyExpression));
            editorContainer.InnerHtml.AppendHtml(((IHtmlHelper<TModel>)htmlHelper).ValidationMessageFor(propertyExpression));
            return htmlHelper.Raw(editorContainer.ToString());
        }        
    }
}