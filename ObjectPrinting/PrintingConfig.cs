using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace ObjectPrinting
{
    public class PrintingConfig<TOwner>
    {
        public string PrintToString(TOwner obj)
        {
            return PrintToString(obj, 0);
        }

        private readonly List<Type> exludedTypes = new List<Type>();

        private readonly List<PropertyInfo> excludedProperties = new List<PropertyInfo>();

        internal readonly Dictionary<Type, Delegate> TypeRules = new Dictionary<Type, Delegate>();

        internal readonly Dictionary<Type, Delegate> SpecialCultures = new Dictionary<Type, Delegate>();

        internal readonly Dictionary<PropertyInfo, Delegate> SpecialProperties = new Dictionary<PropertyInfo, Delegate>();

        internal readonly Dictionary<PropertyInfo, int> TrimmedProperties = new Dictionary<PropertyInfo, int>();

        private string GetSerializaryonByProperty(PropertyInfo key, object obj) => 
            (string) SpecialProperties[key].DynamicInvoke(new object[] { key.GetValue(obj) });


        public PrintingConfig<TOwner> ExcludeType<TType>()
        {
            exludedTypes.Add(typeof(TType));
            return this;
        }

        public PrintingConfig<TOwner> ExcludeProperty<T>(Expression<Func<TOwner, T>> memberSelector)
        {
            var propertyInfo = (PropertyInfo)((MemberExpression)memberSelector.Body).Member;
            excludedProperties.Add(propertyInfo);
            return this;
        }

        public SerializePropertyConfig<TOwner, T> Printing<T>(
            Expression<Func<TOwner, T>> memberSelector)
        {
            var propertyInfo = (PropertyInfo)((MemberExpression) memberSelector.Body).Member;
            
            return   new SerializePropertyConfig<TOwner, T>(this, propertyInfo);
        }

        public SerializeConfig<TOwner, TType> ForNumericType<TType>()
        {
            return new SerializeConfig<TOwner, TType>(this);
        }

        public SerializeConfig<TOwner, TType> Printing<TType>()
        {
            return new SerializeConfig<TOwner,TType>(this);
        }
        
        private readonly Type[] finalTypes = new[]
        {
            typeof(int), typeof(double), typeof(float),
            typeof(DateTime), typeof(TimeSpan), typeof(string)
        };

        private string SerializeFinalType(object obj)
        {
            if (SpecialCultures.ContainsKey(obj.GetType()))
                return (string)SpecialCultures[obj.GetType()].DynamicInvoke(new[] { obj });
            return obj + Environment.NewLine;
        }

        private bool IsExcluded(PropertyInfo propertyInfo) =>
            excludedProperties.Contains(propertyInfo) || exludedTypes.Contains(propertyInfo.PropertyType);

        private string SerializeProperty(PropertyInfo propertyInfo, object obj, int nestingLevel)
        {
            var objectFromProperty = propertyInfo.GetValue(obj);
            if (TrimmedProperties.ContainsKey(propertyInfo))
            {
                objectFromProperty = ((string)objectFromProperty).Substring(0, TrimmedProperties[propertyInfo]);
            }

            string serializedProperty;
            if (SpecialProperties.ContainsKey(propertyInfo))
            {
                serializedProperty = GetSerializaryonByProperty(propertyInfo, obj);
            }
            else if (TypeRules.ContainsKey(propertyInfo.GetType()))
            {
                serializedProperty = (string)TypeRules[obj.GetType()].DynamicInvoke(new[] { obj });
            }
            else
            {
                serializedProperty = PrintToString(objectFromProperty, nestingLevel + 1);
            }
            return serializedProperty;
        }

        private string PrintToString(object obj, int nestingLevel)
        {
            if (obj == null)
                return "null" + Environment.NewLine;

            if (SpecialCultures.ContainsKey(obj.GetType()))
            {
                return (string)SpecialCultures[obj.GetType()].DynamicInvoke(new [] { obj });
            }

            if (finalTypes.Contains(obj.GetType()))
            {
                return SerializeFinalType(obj);
            }

            var identation = new string('\t', nestingLevel + 1);
            var sb = new StringBuilder();
            var type = obj.GetType();

            sb.AppendLine(type.Name);
            
            foreach (var propertyInfo in type.GetProperties())
            {
                if (IsExcluded(propertyInfo)) continue;
                
                var serializedProperty = SerializeProperty(propertyInfo, obj, nestingLevel);
                var serialiazed = identation + propertyInfo.Name + " = " + serializedProperty;

                sb.Append(serialiazed);
            }
            
            return sb.ToString();
        }
    }
}