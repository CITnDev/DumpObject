using System;
using System.Diagnostics;
using NUnit.Framework;

namespace DumpObjectTests
{
    [TestFixture]
    public class TypesTests
    {
        [Test]
        public void DifferenceType()
        {
            Trace.TraceInformation(string.Format("         IsArray\tIsClass\tIsEnum\tIsGeneric\tIsValueType\tIsLayoutSequential\tIsPrimitive"));
            Trace.TraceInformation(string.Format("MyStruct {0,-7}\t{1,-7}\t{2,-6}\t{3,-9}\t{4,-11}\t{5,-18}\t{6}", typeof(MyStruct).IsArray, typeof(MyStruct).IsClass, typeof(MyStruct).IsEnum, typeof(MyStruct).IsGenericType, typeof(MyStruct).IsValueType, typeof(MyStruct).IsLayoutSequential, typeof(MyStruct).IsPrimitive));
            Trace.TraceInformation(string.Format("DateTime {0,-7}\t{1,-7}\t{2,-6}\t{3,-9}\t{4,-11}\t{5,-18}\t{6}", typeof(DateTime).IsArray, typeof(DateTime).IsClass, typeof(DateTime).IsEnum, typeof(DateTime).IsGenericType, typeof(DateTime).IsValueType, typeof(DateTime).IsLayoutSequential, typeof(DateTime).IsPrimitive));
            Trace.TraceInformation(string.Format("Nullable {0,-7}\t{1,-7}\t{2,-6}\t{3,-9}\t{4,-11}\t{5,-18}\t{6}", typeof(Nullable<long>).IsArray, typeof(Nullable<long>).IsClass, typeof(Nullable<long>).IsEnum, typeof(Nullable<long>).IsGenericType, typeof(Nullable<long>).IsValueType, typeof(Nullable<long>).IsLayoutSequential, typeof(Nullable<long>).IsPrimitive));
            Trace.TraceInformation(string.Format("string   {0,-7}\t{1,-7}\t{2,-6}\t{3,-9}\t{4,-11}\t{5,-18}\t{6}", typeof(string).IsArray, typeof(string).IsClass, typeof(string).IsEnum, typeof(string).IsGenericType, typeof(string).IsValueType, typeof(string).IsLayoutSequential, typeof(string).IsPrimitive));
            Trace.TraceInformation(string.Format("int      {0,-7}\t{1,-7}\t{2,-6}\t{3,-9}\t{4,-11}\t{5,-18}\t{6}", typeof(int).IsArray, typeof(int).IsClass, typeof(int).IsEnum, typeof(int).IsGenericType, typeof(int).IsValueType, typeof(int).IsLayoutSequential, typeof(int).IsPrimitive));
            Trace.TraceInformation(string.Format("MyClass  {0,-7}\t{1,-7}\t{2,-6}\t{3,-9}\t{4,-11}\t{5,-18}\t{6}", typeof(MyClass).IsArray, typeof(MyClass).IsClass, typeof(MyClass).IsEnum, typeof(MyClass).IsGenericType, typeof(MyClass).IsValueType, typeof(MyClass).IsLayoutSequential, typeof(MyClass).IsPrimitive));
            Trace.TraceInformation(string.Format("MyEnum   {0,-7}\t{1,-7}\t{2,-6}\t{3,-9}\t{4,-11}\t{5,-18}\t{6}", typeof(MyEnum).IsArray, typeof(MyEnum).IsClass, typeof(MyEnum).IsEnum, typeof(MyEnum).IsGenericType, typeof(MyEnum).IsValueType, typeof(MyEnum).IsLayoutSequential, typeof(MyEnum).IsPrimitive));
        }

        public struct MyStruct
        {
             
        }

        public enum MyEnum
        {}

        public class MyClass
        {}
    }
}
