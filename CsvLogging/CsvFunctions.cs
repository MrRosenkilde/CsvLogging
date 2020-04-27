using CsvLogging.Attributes;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
namespace CsvLogging
{
    public class CsvFunctions
    {
        public static char defaultDelimieter = ';';
        private static readonly IMemoryCache _cache = new MemoryCache(
            new MemoryCacheOptions()
        );
        private static IEnumerable<MemberInfo> MembersToLog(Type type, CollectionStrategy collectionStrategy) 
        {
            var t = type.TryGetAttribute(out LoggingModelAttribute lmAttr)
                ? lmAttr.LoggingModelType
                : type;
            var (primitives, objects) = PrimitivesAndObjects(t);

            foreach (var memberInfo in objects) { 
                if (memberInfo.TryGetAttribute(out LogByValueAttribute lgvAttr)
                    || memberInfo.IsEnum()
                    || collectionStrategy != CollectionStrategy.Newline
                )
                    primitives.Add(memberInfo);
                
                else
                    primitives.AddRange
                    ( 
                        (
                            memberInfo.TryGetCollectionType(out Type collectionType), 
                            memberInfo.IsField(),
                            memberInfo.IsProperty()
                        ) switch
                        {
                            (true, _, _) => MembersToLog(collectionType, collectionStrategy),
                            (_, true, _) => MembersToLog(memberInfo.ToFieldType(), collectionStrategy),
                            (_, _, true) => MembersToLog(memberInfo.ToPropertyType(), collectionStrategy),
                            _ => throw new ArgumentException($"something went wrong with member: {memberInfo}")
                        }
                    );
            }
            return primitives;
        }
        public static string ToHeader(Type type, CsvConfig config) 
            => IterateMembersToLog(type, config.collectionStrategy, m =>
            {
                if (m.TryGetAttribute(out CsvNameAttribute nameAttr))
                    return nameAttr.Name + config.delimiter;
                else return m.Name + config.delimiter;
            });

        private static ValueTuple<List<MemberInfo>, List<MemberInfo>> PrimitivesAndObjects(Type t)
            => t.PrimitivesAndObjects(
                excludePredicate: x => x.HasAttribute(typeof(DontLogAttribute)),
                AdditionalIsPrimitiveFunction: x => x.HasAttribute(typeof(LogByValueAttribute))
            );

        private static IReadOnlyDictionary<object, List<MemberInfo>> ObjectToLoggableMembersMap(
            object baseObj,
            CollectionStrategy collectionStrategy,
            Type baseObjectType = null
        ){
            var result = new Dictionary<object, List<MemberInfo>>();
            

            var t = baseObjectType ?? baseObj.GetType();
            var type =
                t.HasAttribute(typeof(LoggingModelAttribute))
                ? (t.GetCustomAttribute(
                    typeof(LoggingModelAttribute)
                    ) as LoggingModelAttribute).LoggingModelType
                : t;

            var (primitives, objects) = type.PrimitivesAndObjects(
                excludePredicate: x => x.HasAttribute(typeof(DontLogAttribute)),
                AdditionalIsPrimitiveFunction: 
                    x => 
                    x.HasAttribute(typeof(LogByValueAttribute)) 
                    || x.IsCollection()
            );

            result.Add(baseObj, primitives);
            foreach (var memberInfo in objects) 
            {
                var nestedDictionary = 
                    ObjectToLoggableMembersMap(
                        t.GetMember(memberInfo.Name)
                        .FirstOrDefault(x => x.IsField() || x.IsProperty())
                        .GetValue(baseObj),
                        collectionStrategy
                    );

                foreach (var (key, value) in nestedDictionary)
                    result.Add(key, value );
            }
            return result;
        }
        //private static string WriteCollection(MemberInfo member, object obj, CsvConfig config) 
        //{
        //    var collectionType = member.GetCollectionType();
        //    if (config.collectionStrategy == CollectionStrategy.Inline) 
        //    {
        //        var collection = member.GetValue(obj);
        //    }
        //}
        public static string ToLine(object baseObject, in CsvConfig config)
        {
            var type = baseObject.GetType();

            if (type.CustomIsPrimitive())
                return baseObject.ToString();

            if(type.GetInterface(nameof(ICsvLoggable)) != null)
                return (baseObject as ICsvLoggable).ToCsvLine();

            IReadOnlyDictionary<object, List<MemberInfo>> objectToLoggableMemberMap;
            if (_cache.TryGetValue(baseObject, out object cacheValue))
                objectToLoggableMemberMap = cacheValue as IReadOnlyDictionary<object, List<MemberInfo>>;
            else {
                objectToLoggableMemberMap = ObjectToLoggableMembersMap(baseObject, config.collectionStrategy, type);
                _cache.Set(baseObject, objectToLoggableMemberMap);
            }
            var sb = new StringBuilder();

            foreach (var (obj, members) in objectToLoggableMemberMap)
                foreach (var m in members)
                    //if (!m.IsCollection())
                        sb.Append(
                            ToTerm(m.Name, obj, config)
                        );
                    //else {
                    //    sb.Append(
                    //        WriteCollection(m, obj, config)
                    //    );
                    //}

            if (sb.Length > 0)
                sb.Length--;
            
            return sb.ToString();
        }
        private static string ToTerm(string name, object obj, CsvConfig config) 
        {
            var m = obj
                .GetType()
                .GetMember(name)
                .FirstOrDefault(x => 
                    x.MemberType == MemberTypes.Property 
                    || x.MemberType == MemberTypes.Field
                );
            if(m.TryGetAttribute(out CsvFormatAttribute attr))
                return obj
                    .GetType()
                    .GetMethod(attr.nameOfFormatFunction)
                    .Invoke(obj, new object[] { 
                        m.GetValue(obj)
                    }).ToString() + config.delimiter;
            
            return m.GetValue(obj)
                .ToString() 
                + config.delimiter;
        }
        private static string IterateMembersToLog(Type type, CollectionStrategy collectionStrategy, Func<MemberInfo, string> func) 
        {
            var sb = new StringBuilder();
            
            foreach (var m in MembersToLog(type, collectionStrategy))
                sb.Append(func.Invoke(m));
            
            if(sb.Length > 0) 
                sb.Length--;
            
            return sb.ToString();
        }

            
    }
}
