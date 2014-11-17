using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Dictator.Tests
{
    [TestFixture()]
    public class FieldCheckTests
    {
        [Test()]
        public void Should_check_field_presence()
        {
            var doc1 = Dictator.New()
                .Object("null1", null)
                .String("string1", "test1")
                .String("foo", "test2");
            
            Assert.IsTrue(doc1.Has("null1"));
            Assert.IsTrue(doc1.Has("string1"));

            Assert.IsFalse(doc1.Has("nonExistingField"));
            Assert.IsFalse(doc1.Has("foo.nonExistingField"));
        }
        
        [Test()]
        public void Should_check_field_null_value()
        {
            var doc1 = Dictator.New()
                .Object("null1", null)
                .String("string1", "test1")
                .String("foo", "test2");
            
            Assert.IsTrue(doc1.IsNull("null1"));
            
            Assert.IsFalse(doc1.IsNull("string1"));
            
            Assert.IsFalse(doc1.IsNull("nonExistingField"));
            Assert.IsFalse(doc1.IsNull("foo.nonExistingField"));
        }
        
        [Test()]
        public void Should_check_field_not_null_value()
        {
            var doc1 = Dictator.New()
                .Object("null1", null)
                .String("string1", "test1")
                .String("foo", "test2");
            
            Assert.IsFalse(doc1.IsNotNull("null1"));
            
            Assert.IsTrue(doc1.IsNotNull("string1"));
            
            Assert.IsFalse(doc1.IsNotNull("nonExistingField"));
            Assert.IsFalse(doc1.IsNotNull("foo.nonExistingField"));
        }
        
        [Test()]
        public void Should_check_field_bool_value()
        {
            var doc1 = Dictator.New()
                .Object("null1", null)
                .Bool("bool1", true)
                .String("foo", "test2");
            
            Assert.IsFalse(doc1.IsBool("null1"));
            Assert.IsTrue(doc1.IsBool("bool1"));

            Assert.IsFalse(doc1.IsBool("nonExistingField"));
            Assert.IsFalse(doc1.IsBool("foo.nonExistingField"));
        }
        
        [Test()]
        public void Should_check_field_byte_value()
        {
            var doc1 = Dictator.New()
                .Object("null1", null)
                .Byte("byte1", 123)
                .String("foo", "test2");
            
            Assert.IsFalse(doc1.IsByte("null1"));
            Assert.IsTrue(doc1.IsByte("byte1"));

            Assert.IsFalse(doc1.IsByte("nonExistingField"));
            Assert.IsFalse(doc1.IsByte("foo.nonExistingField"));
        }
        
        [Test()]
        public void Should_check_field_short_value()
        {
            var doc1 = Dictator.New()
                .Object("null1", null)
                .Short("short1", 12345)
                .String("foo", "test2");
            
            Assert.IsFalse(doc1.IsShort("null1"));
            Assert.IsTrue(doc1.IsShort("short1"));

            Assert.IsFalse(doc1.IsShort("nonExistingField"));
            Assert.IsFalse(doc1.IsShort("foo.nonExistingField"));
        }
        
        [Test()]
        public void Should_check_field_int_value()
        {
            var doc1 = Dictator.New()
                .Object("null1", null)
                .Int("int1", 123456)
                .String("foo", "test2");
            
            Assert.IsFalse(doc1.IsInt("null1"));
            Assert.IsTrue(doc1.IsInt("int1"));

            Assert.IsFalse(doc1.IsInt("nonExistingField"));
            Assert.IsFalse(doc1.IsInt("foo.nonExistingField"));
        }
        
        [Test()]
        public void Should_check_field_long_value()
        {
            var doc1 = Dictator.New()
                .Object("null1", null)
                .Long("long1", 123456789)
                .String("foo", "test2");
            
            Assert.IsFalse(doc1.IsLong("null1"));
            Assert.IsTrue(doc1.IsLong("long1"));

            Assert.IsFalse(doc1.IsLong("nonExistingField"));
            Assert.IsFalse(doc1.IsLong("foo.nonExistingField"));
        }
        
        [Test()]
        public void Should_check_field_float_value()
        {
            var doc1 = Dictator.New()
                .Object("null1", null)
                .Float("float1", 3.14f)
                .String("foo", "test2");
            
            Assert.IsFalse(doc1.IsFloat("null1"));
            Assert.IsTrue(doc1.IsFloat("float1"));

            Assert.IsFalse(doc1.IsFloat("nonExistingField"));
            Assert.IsFalse(doc1.IsFloat("foo.nonExistingField"));
        }
        
        [Test()]
        public void Should_check_field_double_value()
        {
            var doc1 = Dictator.New()
                .Object("null1", null)
                .Double("double1", 3.14)
                .String("foo", "test2");
            
            Assert.IsFalse(doc1.IsDouble("null1"));
            Assert.IsTrue(doc1.IsDouble("double1"));

            Assert.IsFalse(doc1.IsDouble("nonExistingField"));
            Assert.IsFalse(doc1.IsDouble("foo.nonExistingField"));
        }
        
        [Test()]
        public void Should_check_field_decimal_value()
        {
            var doc1 = Dictator.New()
                .Object("null1", null)
                .Decimal("decimal1", new Decimal(3.14))
                .String("foo", "test2");
            
            Assert.IsFalse(doc1.IsDecimal("null1"));
            Assert.IsTrue(doc1.IsDecimal("decimal1"));

            Assert.IsFalse(doc1.IsDecimal("nonExistingField"));
            Assert.IsFalse(doc1.IsDecimal("foo.nonExistingField"));
        }
        
        [Test()]
        public void Should_check_field_dateTime_value()
        {
            var doc1 = Dictator.New()
                .Object("null1", null)
                .DateTime("dateTime1", DateTime.UtcNow)
                .String("foo", "test2");
            
            Assert.IsFalse(doc1.IsDateTime("null1"));
            Assert.IsTrue(doc1.IsDateTime("dateTime1"));

            Assert.IsFalse(doc1.IsDateTime("nonExistingField"));
            Assert.IsFalse(doc1.IsDateTime("foo.nonExistingField"));
        }
        
        [Test()]
        public void Should_check_field_dateTime_value_with_format()
        {
            var doc1 = Dictator.New()
                .Object("null1", null)
                .DateTime("dateTime1", DateTime.UtcNow, DateTimeFormat.String)
                .DateTime("dateTime2", DateTime.UtcNow, DateTimeFormat.UnixTimeStamp)
                .String("foo", "test2");
            
            Assert.IsFalse(doc1.IsDateTime("null1"));
            Assert.IsTrue(doc1.IsDateTime("dateTime1", DateTimeFormat.String));
            Assert.IsTrue(doc1.IsDateTime("dateTime2", DateTimeFormat.UnixTimeStamp));

            Assert.IsFalse(doc1.IsDateTime("nonExistingField"));
            Assert.IsFalse(doc1.IsDateTime("foo.nonExistingField"));
        }
        
        [Test()]
        public void Should_check_field_string_value()
        {
            var doc1 = Dictator.New()
                .Object("null1", null)
                .String("string1", "test1")
                .String("foo", "test2");
            
            Assert.IsFalse(doc1.IsString("null1"));
            Assert.IsTrue(doc1.IsString("string1"));

            Assert.IsFalse(doc1.IsString("nonExistingField"));
            Assert.IsFalse(doc1.IsString("foo.nonExistingField"));
        }
        
        [Test()]
        public void Should_check_field_object_value()
        {
            object obj1 = 123;
            
            var doc1 = Dictator.New()
                .Object("null1", null)
                .Object("object1", obj1)
                .String("foo", "test2");
            
            Assert.IsFalse(doc1.IsObject("null1"));
            Assert.IsTrue(doc1.IsObject("object1"));

            Assert.IsFalse(doc1.IsObject("nonExistingField"));
            Assert.IsFalse(doc1.IsObject("foo.nonExistingField"));
        }
        
        [Test()]
        public void Should_check_field_document_value()
        {
            object obj1 = 123;
            
            var doc1 = Dictator.New()
                .Object("null1", null)
                .Document("document1", Dictator.New())
                .String("foo", "test2")
                .String("bar.string1", "test2");
            
            Assert.IsFalse(doc1.IsDocument("null1"));
            Assert.IsTrue(doc1.IsDocument("document1"));

            Assert.IsFalse(doc1.IsDocument("nonExistingField"));
            Assert.IsFalse(doc1.IsDocument("foo.nonExistingField"));
            Assert.IsTrue(doc1.IsDocument("bar"));
            Assert.IsFalse(doc1.IsDocument("bar.string1"));
        }
        
        [Test()]
        public void Should_check_field_enum_simple_value()
        {
            var doc1 = Dictator.New()
                .Object("null1", null)
                .Enum("enum1", DateTimeFormat.Object)
                .String("foo", "test2");
            
            Assert.IsFalse(doc1.IsEnum("null1"));
            Assert.IsTrue(doc1.IsEnum("enum1"));

            Assert.IsFalse(doc1.IsEnum("nonExistingField"));
            Assert.IsFalse(doc1.IsEnum("foo.nonExistingField"));
        }
        
        [Test()]
        public void Should_check_field_enum_generic_value()
        {
            var doc1 = Dictator.New()
                .Object("null1", null)
                .Int("enum1", 0)
                .String("enum2", "object")
                .String("foo", "test2");
            
            Assert.IsFalse(doc1.IsEnum<DateTimeFormat>("null1"));
            Assert.IsTrue(doc1.IsEnum<DateTimeFormat>("enum1"));
            Assert.IsTrue(doc1.IsEnum<DateTimeFormat>("enum2"));

            Assert.IsFalse(doc1.IsEnum<DateTimeFormat>("nonExistingField"));
            Assert.IsFalse(doc1.IsEnum<DateTimeFormat>("foo.nonExistingField"));
        }
        
        [Test()]
        public void Should_check_field_list_value()
        {
            var doc1 = Dictator.New()
                .Object("null1", null)
                .List("list1", new List<int>() { 1, 2, 3 })
                .String("foo", "test2");
            
            Assert.IsFalse(doc1.IsList("null1"));
            Assert.IsTrue(doc1.IsList("list1"));

            Assert.IsFalse(doc1.IsList("nonExistingField"));
            Assert.IsFalse(doc1.IsList("foo.nonExistingField"));
        }
    }
}
