using System;
using System.Collections;
using System.Globalization;
using System.Numerics;
using System.Reflection;

namespace DumpObject
{
    public class Dumper
    {
        public Dumper()
        {
            MaxDumpLevel = 10;
        }

        /// <summary>
        /// Set the max recursive level of the dumping.
        /// The default value is 10
        /// </summary>
        public int MaxDumpLevel { get; set; }

        public DumpLevel Dump<T>(T value)
        {
            return InternalDump(value, typeof(T), 0);
        }

        private DumpLevel InternalDump<T>(T value, Type type, int dumpLevel)
        {
            if (!CanContinueDumping(dumpLevel))
                return new StopDumpLevel();

            if ((type.IsClass || type.IsGenericType) && value == null)
                return GetNullDumpLevel(type, dumpLevel); 
            
            if (IsValueType(type))
                return DumpValueType(value, type, dumpLevel);

            return DumpClass((object)value, type, dumpLevel);
        }

        private DumpLevel DumpClass<T>(T value, Type type, int dumpLevel) where T : class
        {
            if (value == null)
                return GetNullDumpLevel(type, dumpLevel);

            DumpLevel dump;

            if (CanDirectDumpClass(type))
                dump = DumpDirectClass(value, type, dumpLevel);
            else if (IsCollectionType(type))
                dump = DumpCollection((object)value, type, dumpLevel);
            else
                dump = DumpObjectInstance(value,type, dumpLevel);

            return dump;
        }

        protected virtual DumpLevel DumpObjectInstance<T>(T value, Type type, int dumpLevel)
        {
            var dump = new DumpLevel
                           {
                               Header = "",
                               Level = dumpLevel,
                               Type = type,
                           };
            foreach (var propertyInfo in type.GetProperties())
            {
                if (propertyInfo.CanRead)
                    dump.AddChildren(DumpProperty(propertyInfo, value, dumpLevel + 1));
            }

            return dump;
        }

        protected DumpLevel DumpProperty(PropertyInfo property, object instance, int dumpLevel)
        {
            var value = property.GetGetMethod().Invoke(instance, null);
            var dumpValue = InternalDump(value, property.PropertyType, dumpLevel);
            dumpValue.Header = property.Name;

            return dumpValue;
        }

        protected virtual DumpLevel DumpCollection<T>(T value, Type type, int dumpLevel) where T:class
        {
            if (value == null)
                return GetNullDumpLevel(type, dumpLevel);

            if (IsIDictionary(value.GetType()))
                return DumpIDictionary(value, type, dumpLevel);

            if (IsICollection(value.GetType()))
                return DumpICollection(value, type, dumpLevel);

            throw new NotImplementedException();
        }

        private static DumpLevel GetNullDumpLevel(Type type, int dumpLevel)
        {
            return new DumpLevel
                       {
                           Level = dumpLevel,
                           Type = type,
                           Value = null
                       };
        }

        private DumpLevel DumpICollection<T>(T value, Type type, int dumpLevel)
        {
            var col = (ICollection)value;
            var dump = new DumpLevel
                           {
                               Header = string.Format("{0} items", col.Count),
                               Level = dumpLevel,
                               Type = type,
                           };
            var enumerator = col.GetEnumerator();
            enumerator.Reset();
            for (var i = 0; i < col.Count; i++)
            {
                enumerator.MoveNext();
                var curValue = enumerator.Current;
                var dumpChild = InternalDump(curValue, curValue.GetType(), dumpLevel + 1);
                dumpChild.Header = i.ToString();
                dump.AddChildren(dumpChild);
            }

            return dump;
        }

        private DumpLevel DumpIDictionary(object value, Type type, int dumpLevel)
        {
            var dict = (IDictionary)value;
            var dump = new DumpLevel
                           {
                               Header = string.Format("{0} items", dict.Count),
                               Level = dumpLevel,
                               Type = type,
                           };

            foreach (var key in dict.Keys)
            {
                var dumpChild = InternalDump(dict[key], dict[key].GetType(), dumpLevel + 1);
                dumpChild.Header = key.ToString();
                dump.AddChildren(dumpChild);
            }
            return dump;
        }

        private static bool IsICollection(Type type)
        {
            return type.GetInterface("ICollection") != null;
        }

        private static bool IsIDictionary(Type type)
        {
            return type.GetInterface("IDictionary") != null;
        }

        protected virtual bool IsCollectionType(Type type)
        {
            return IsICollection(type) || IsIDictionary(type);
        }

        protected virtual DumpLevel DumpDirectClass<T>(T value, Type type, int dumpLevel) where T : class
        {
            if (type == typeof(CultureInfo))
                return new DumpLevel
                {
                    Level = dumpLevel,
                    Type = type,
                    // ReSharper disable PossibleNullReferenceException
                    Value = (value as CultureInfo).Name,
                    // ReSharper restore PossibleNullReferenceException
                };

            throw new NotImplementedException();
        }

        protected virtual bool CanDirectDumpClass(Type type)
        {
            return type == typeof(CultureInfo);
        }

        protected virtual DumpLevel DumpValueType<T>(T value, Type type, int dumpLevel)
        {
            if (CanDirectDumpValueType(type))
                return DumpDirectValueType(value, type, dumpLevel);

            if ((type.IsClass || type.IsGenericType) && value == null)
                return new DumpLevel
                           {
                               Level = dumpLevel,
                               Type = type,
                               Value = null
                           };

            if (type.IsEnum)
                return DumpEnum(value, type, dumpLevel);

            if (!type.IsPrimitive && type.IsLayoutSequential && !type.IsGenericType) // Struct
                return DumpObjectInstance(value, type, dumpLevel);

            return new DumpLevel
                       {
                           Level = dumpLevel,
                           Type = type,
                           Value = value
                       };
        }

        protected virtual bool CanDirectDumpValueType(Type type)
        {
            return type == typeof(BigInteger);
        }

        protected virtual DumpLevel DumpDirectValueType<T>(T value, Type type, int dumpLevel)
        {
            if (type == typeof(BigInteger))
                return new DumpLevel
                {
                    Level = dumpLevel,
                    Type = type,
                    // ReSharper disable PossibleNullReferenceException
                    Value = value.ToString()
                    // ReSharper restore PossibleNullReferenceException
                };

            throw new NotImplementedException();
        }

        private DumpLevel DumpEnum<T>(T value, Type type, int dumpLevel)
        {
            return new DumpLevel
                       {
                           Level = dumpLevel,
                           Type = type,
                           Value = type.Name + "." + Enum.GetName(type, value),
                       };
        }

        protected virtual bool IsValueType(Type type)
        {
            return type == typeof (string) || type.IsValueType;
        }

        protected virtual bool CanContinueDumping(int dumpLevel)
        {
            return dumpLevel < MaxDumpLevel;
        }
    }
}
