using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Rock.StaticDependencyInjection
{
    internal static class Extensions
    {
        internal static IEnumerable<TAttribute> AsAttributes<TAttribute>(
            this IEnumerable<CustomAttributeData> attributeDataCollection)
            where TAttribute : Attribute
        {
            foreach (var attributeData in attributeDataCollection)
            {
                TAttribute attribute;

                if (attributeData.TryGetAttribute(out attribute))
                {
                    yield return attribute;
                }
            }
        }

        private static bool TryGetAttribute<TAttribute>(
            this CustomAttributeData attributeData,
            out TAttribute attribute)
            where TAttribute : Attribute
        {
            var attributeType = typeof(TAttribute);

            if (attributeData.Constructor == null
                || attributeData.Constructor.DeclaringType == null
                || attributeData.Constructor.DeclaringType.AssemblyQualifiedName == null
                || attributeData.Constructor.DeclaringType.AssemblyQualifiedName != attributeType.AssemblyQualifiedName)
            {
                attribute = null;
                return false;
            }

            var bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

            var args = attributeData.ConstructorArguments.Select(x => x.Value).ToArray();

            attribute = (TAttribute)Activator.CreateInstance(attributeType, args);

            if (attributeData.NamedArguments != null)
            {
                foreach (var namedArgument in attributeData.NamedArguments)
                {
                    if (namedArgument.MemberInfo is PropertyInfo)
                    {
                        var propertyInfo = attributeType.GetProperty(namedArgument.MemberInfo.Name, bindingFlags);
                        if (propertyInfo != null)
                        {
                            propertyInfo.SetValue(attribute, namedArgument.TypedValue.Value, null);
                        }
                    }
                    else if (namedArgument.MemberInfo is FieldInfo)
                    {
                        var fieldInfo = attributeType.GetField(namedArgument.MemberInfo.Name, bindingFlags);
                        if (fieldInfo != null)
                        {
                            fieldInfo.SetValue(attribute, namedArgument.TypedValue.Value);
                        }
                    }
                }
            }

            return true;
        }
    }
}