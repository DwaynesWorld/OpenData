namespace OpenData.Core.Query
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Microsoft.AspNetCore.Http;
    using OpenData.Core.Model;

    public interface IODataQueryParser
    {
        ODataQueryOptions ParseOptions(HttpRequest request, Type type);
    }

    public class ODataQueryParser : IODataQueryParser
    {
        public ODataQueryParser() { }

        public ODataQueryOptions ParseOptions(HttpRequest request, Type type)
        {
            var edmType = (EdmComplexType)EdmTypeCache.Map.GetOrAdd(type, t => EdmTypeResolver(t));
            var entitySet = new EntitySet(nameof(type), edmType);
            return new ODataQueryOptions(request, entitySet);
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

            EdmType baseType = clrType.BaseType != typeof(object)
                ? baseType = EdmTypeResolver(clrType.BaseType)
                : null;

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