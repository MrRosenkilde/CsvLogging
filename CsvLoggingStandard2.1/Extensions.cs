using CsvLogging.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace CsvLogging
{
    public static class Extensions
    {
        public static bool IsProperty(this MemberInfo memberInfo) 
            => memberInfo.MemberType == MemberTypes.Property;
        public static bool IsField(this MemberInfo memberInfo) 
            => memberInfo.MemberType == MemberTypes.Field;
        public static bool IsEnum(this MemberInfo memberInfo)
        => memberInfo.TryConvertToPropertyOrFieldType(out Type t) ?
                t.IsEnum
                : false;

        public static bool IsPrimitive(this MemberInfo memberInfo)
        {
            var t = memberInfo.IsProperty() ? 
                (memberInfo as PropertyInfo)?.PropertyType.CustomIsPrimitive() 
                : (memberInfo as FieldInfo)?.FieldType.CustomIsPrimitive();
            return t ?? false;
        }
        public static Type ToFieldType(this MemberInfo memberInfo)
            => (memberInfo as FieldInfo).FieldType;

        public static Type ToPropertyType(this MemberInfo memberInfo)
            => (memberInfo as PropertyInfo).PropertyType;

        public static FieldInfo AsFieldInfo(this MemberInfo memberInfo)
            => memberInfo as FieldInfo;

        public static PropertyInfo AsPropertyInfo(this MemberInfo memberInfo)
            => memberInfo as PropertyInfo;

        public static object GetValue(this MemberInfo memberInfo, object obj) 
        {
            if (memberInfo.IsField())
                return memberInfo.AsFieldInfo().GetValue(obj);
            else if (memberInfo.IsProperty())
                return memberInfo.AsPropertyInfo().GetValue(obj);
            else throw new NotSupportedException("MemberInfo.GetValue(object obj) only works with property or field types");
        }

        public static bool TryConvertToPropertyOrFieldType(this MemberInfo memberInfo, out Type type)
        {
            type = memberInfo.MemberType == MemberTypes.Field
                ? (memberInfo as FieldInfo).FieldType
                : memberInfo.MemberType == MemberTypes.Property
                ? (memberInfo as PropertyInfo).PropertyType
                : null;
            return type != null;
        }
        public static bool HasAttribute(this MemberInfo memberInfo, Type attributeType)
            => Attribute.IsDefined(memberInfo, attributeType);

        public static bool TryGetAttribute<T>(this MemberInfo memberInfo, out T attributeInstance) where T : Attribute
        {
            var hasAttribute = memberInfo.HasAttribute(typeof(T));
            if (hasAttribute)
            {
                attributeInstance = Attribute.GetCustomAttribute(memberInfo, typeof(T)) as T;
                return true;
            }
            else {
                attributeInstance = null;
                return false;
            }
        }
        public static bool IsCollection(this MemberInfo memberInfo) 
        {
            if (memberInfo.TryConvertToPropertyOrFieldType(out Type t))
                return t.IsCollection();
            else return false;
        }
        public static bool IsCollection(this MemberInfo memberInfo, out Type typeofArray)
        {
            if (memberInfo.IsProperty())
            {
                var t = (memberInfo as PropertyInfo).PropertyType;
                bool isArray = t.IsArray;
                typeofArray = isArray ? t.GetElementType() : null;
                return isArray;
            }
            else if (memberInfo.IsField())
            {
                var t = (memberInfo as FieldInfo).FieldType;
                bool isArray = t.IsArray;
                typeofArray = isArray ? t.GetElementType() : null;
                return isArray;
            }
            typeofArray = null;
            return false;
        }

        public static ValueTuple<List<MemberInfo>, List<MemberInfo>>
            PrimitivesAndObjects(
                this Type t,
                Func<MemberInfo, bool> excludePredicate = null,
                Func<MemberInfo, bool> AdditionalIsPrimitiveFunction = null
            ){
            if (excludePredicate is null) excludePredicate = x => true;
            if (AdditionalIsPrimitiveFunction is null) AdditionalIsPrimitiveFunction = x => x.IsPrimitive();
            var primitives = new List<MemberInfo>();
            var objects = new List<MemberInfo>();

            foreach (var m in t.PublicFieldsAndProperties(excludePredicate))
            {
                if (m.IsPrimitive() || AdditionalIsPrimitiveFunction.Invoke(m))
                    primitives.Add(m);
                else objects.Add(m);
            }
            return (primitives, objects);

        }
        public static IEnumerable<MemberInfo> PublicFieldsAndProperties(
            this Type t,
            Func<MemberInfo, bool> excludePredicate = null
        ){
            if (excludePredicate is null)
                excludePredicate = (x) => true;
            return t.GetMembers()
                .Where(x => x.MemberType == MemberTypes.Property || x.MemberType == MemberTypes.Field)
                .Where(x => !excludePredicate.Invoke(x));
        }



        public static bool CustomIsPrimitive(this Type t)
            => t.IsPrimitive || t == typeof(string) || t == typeof(decimal);

        public static bool IsCollection(this Type t)
            => t.IsArray
            || t.GetInterface(nameof(IEnumerable)) != null
            || t.GetInterface(nameof(ICollection)) != null;

        public static Type GetCollectionType(this MemberInfo m) 
        {
            m.TryConvertToPropertyOrFieldType(out Type t);
            return t.GetCollectionType();
        }

        public static Type GetCollectionType(this Type t, bool considerStringCharArray) 
        {
            if(t.IsCollection())
                if (t.HasElementType)
                    return t.GetElementType();
                else if (t.IsGenericType)
                    return t.GenericTypeArguments[0];
                else if (t == typeof(string) && considerStringCharArray)
                    return typeof(char);
            return null;
        }

        public static bool TryGetCollectionType(this MemberInfo m, out Type collectionType, bool considerStringCharArray = false)
        {
            var isFieldOrProperty = m.TryConvertToPropertyOrFieldType(out Type t);
            if (!isFieldOrProperty)
                throw new NotSupportedException("GetCollectionType is only supported with field or property members");
            collectionType = t.GetCollectionType();
            return collectionType != null;

        }
    }
}
