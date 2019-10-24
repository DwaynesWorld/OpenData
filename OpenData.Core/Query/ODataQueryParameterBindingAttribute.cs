namespace OpenData.Core.Query
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Reflection;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Abstractions;
    using Microsoft.AspNetCore.Mvc.Controllers;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using OpenData.Core.Model;

    /// <summary>
    /// A <see cref="ModelBinderAttribute"/> to bind parameters of type <see cref="ODataQueryOptions"/> to the OData query from the incoming request.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Parameter, Inherited = true, AllowMultiple = false)]
    public sealed class ODataQueryParameterBindingAttribute : ModelBinderAttribute
    {
        /// <summary>
        /// Instantiates a new instance of the <see cref="ODataQueryParameterBindingAttribute"/> class.
        /// </summary>
        public ODataQueryParameterBindingAttribute() : base(typeof(ODataQueryParameterBinding)) { }

        internal class ODataQueryParameterBinding : IModelBinder
        {
            public Task BindModelAsync(ModelBindingContext bindingContext)
            {
                if (bindingContext == null)
                {
                    throw new ArgumentNullException("bindingContext");
                }

                HttpRequest request = bindingContext.HttpContext.Request;
                if (request == null)
                {
                    throw new ArgumentException("actionContext", "SRResources.ActionContextMustHaveRequest");
                }

                ActionDescriptor actionDescriptor = bindingContext.ActionContext.ActionDescriptor;
                if (actionDescriptor == null)
                {
                    throw new ArgumentException("actionContext", "SRResources.ActionContextMustHaveDescriptor");
                }

                // Get the parameter description of the parameter to bind.
                ParameterDescriptor paramDescriptor = bindingContext
                    .ActionContext
                    .ActionDescriptor
                    .Parameters
                    .Where(p => p.Name == bindingContext.FieldName)
                    .FirstOrDefault();

                // Now make sure parameter type is ODataQueryOptions or ODataQueryOptions<T>.
                Type parameterType = paramDescriptor?.ParameterType;
                if (IsODataQueryOptions(parameterType))
                {

                    // Get the entity type from the parameter type if it is ODataQueryOptions<T>.
                    if (paramDescriptor == null)
                    {
                        throw new ArgumentNullException("paramDescriptor");
                    }

                    Type entityClrType = GetEntityClrTypeFromParameterType(parameterType);
                    // var edmType = (EdmComplexType)EdmTypeCache.Map.GetOrAdd(parameterType, t => EdmTypeResolver(t));
                    var edmType = (EdmComplexType)EdmTypeResolver(entityClrType);
                    var entitySet = new EntitySet(nameof(entityClrType), edmType);

                    ODataQueryOptions parameterValue = new ODataQueryOptions(request, entitySet);
                    bindingContext.Result = ModelBindingResult.Success(parameterValue);
                }

                return Task.CompletedTask;
            }

            public static bool IsODataQueryOptions(Type parameterType)
            {
                if (parameterType == null)
                {
                    return false;
                }
                return ((parameterType == typeof(ODataQueryOptions)) ||
                        (parameterType.IsGenericType &&
                         parameterType.GetGenericTypeDefinition() == typeof(ODataQueryOptions<>)));
            }

            private static Type GetEntityClrTypeFromParameterType(Type parameterType)
            {

                if (parameterType.IsGenericType && parameterType.GetGenericTypeDefinition() == typeof(ODataQueryOptions<>))
                {
                    return parameterType.GetGenericArguments().Single();
                }

                return null;
            }

            private static EdmType EdmTypeResolver(Type clrType)
            {
                if (clrType.IsEnum)
                {
                    var members = new List<EdmEnumMember>();

                    foreach (var x in Enum.GetValues(clrType))
                    {
                        members.Add(new EdmEnumMember(x.ToString(), (int)x));
                    }

                    return new EdmEnumType(clrType, members.AsReadOnly());
                }

                if (clrType.IsGenericType)
                {
                    var innerType = clrType.GetGenericArguments()[0];

                    if (typeof(IEnumerable<>).MakeGenericType(innerType).IsAssignableFrom(clrType))
                    {
                        var containedType = EdmTypeCache.Map.GetOrAdd(innerType, EdmTypeResolver(innerType));

                        return EdmTypeCache.Map.GetOrAdd(clrType, t => new EdmCollectionType(t, containedType));
                    }
                }

                EdmType baseType = clrType.BaseType != typeof(object) ? baseType = EdmTypeResolver(clrType.BaseType) : null;

                var clrTypeProperties = clrType
                    .GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                    .OrderBy(p => p.Name);

                var edmProperties = new List<EdmProperty>();
                var edmComplexType = new EdmComplexType(clrType, baseType, edmProperties);

                edmProperties.AddRange(
                    clrTypeProperties.Select(
                        p => new EdmProperty(
                            p.Name,
                            EdmTypeCache.Map.GetOrAdd(p.PropertyType, t => EdmTypeResolver(t)),
                            edmComplexType)));

                return edmComplexType;
            }
        }
    }
}