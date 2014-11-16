using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Dictator.Tests
{
    [TestFixture()]
    public class FieldGetSetTests
    {
        [Test()]
        public void Should_set_and_get_bool_fields()
        {
            var doc1 = Dictator.New()
                .Bool("bool1", true)
                .Bool("bool2", false)
                .Bool("foo.bool1", true)
                .Bool("foo.bool2", false);
            
            Assert.IsTrue(doc1.Bool("bool1"));
            Assert.IsFalse(doc1.Bool("bool2"));
            
            Assert.IsTrue(doc1.Bool("foo.bool1"));
            Assert.IsFalse(doc1.Bool("foo.bool2"));
        }
        
        [Test()]
        public void Should_set_and_get_byte_fields()
        {
            var doc1 = Dictator.New()
                .Byte("byte1", 123)
                .Byte("foo.byte1", 132);
            
            Assert.IsTrue(doc1.Byte("byte1") == 123);
            Assert.IsTrue(doc1.Byte("foo.byte1") == 132);
        }
        
        [Test()]
        public void Should_set_and_get_short_fields()
        {
            var doc1 = Dictator.New()
                .Short("short1", 12345)
                .Short("foo.short1", 13245);
            
            Assert.IsTrue(doc1.Short("short1") == 12345);
            Assert.IsTrue(doc1.Short("foo.short1") == 13245);
        }
        
        [Test()]
        public void Should_set_and_get_int_fields()
        {
            var doc1 = Dictator.New()
                .Int("int1", 123456)
                .Int("foo.int1", 132456);
            
            Assert.IsTrue(doc1.Int("int1") == 123456);
            Assert.IsTrue(doc1.Int("foo.int1") == 132456);
        }
        
        [Test()]
        public void Should_set_and_get_long_fields()
        {
            var doc1 = Dictator.New()
                .Long("long1", 123456789)
                .Long("foo.long1", 132456789);
            
            Assert.IsTrue(doc1.Long("long1") == 123456789);
            Assert.IsTrue(doc1.Long("foo.long1") == 132456789);
        }
        
        [Test()]
        public void Should_set_and_get_float_fields()
        {
            var doc1 = Dictator.New()
                .Float("float1", 3.14f)
                .Float("foo.float1", 3.41f);
            
            Assert.IsTrue(doc1.Float("float1") == 3.14f);
            Assert.IsTrue(doc1.Float("foo.float1") == 3.41f);
        }
        
        [Test()]
        public void Should_set_and_get_double_fields()
        {
            var doc1 = Dictator.New()
                .Double("double1", 3.14)
                .Double("foo.double1", 3.41);
            
            Assert.IsTrue(doc1.Double("double1") == 3.14);
            Assert.IsTrue(doc1.Double("foo.double1") == 3.41);
        }
        
        [Test()]
        public void Should_set_and_get_decimal_fields()
        {
            var doc1 = Dictator.New()
                .Decimal("decimal1", new Decimal(3.14))
                .Decimal("foo.decimal1", new Decimal(3.41));
            
            Assert.IsTrue(doc1.Decimal("decimal1") == new Decimal(3.14));
            Assert.IsTrue(doc1.Decimal("foo.decimal1") == new Decimal(3.41));
        }
        
        [Test()]
        public void Should_set_and_get_string_fields()
        {
            var doc1 = Dictator.New()
                .String("string1", "test1")
                .String("foo.string1", "test2");
            
            Assert.IsTrue(doc1.String("string1") == "test1");
            Assert.IsTrue(doc1.String("foo.string1") == "test2");
        }
        
        [Test()]
        public void Should_set_and_get_null_fields()
        {
            var doc1 = Dictator.New()
                .Object("object1", null)
                .Object("foo.object1", null);
            
            Assert.IsTrue(doc1.Object("object1") == null);
            Assert.IsTrue(doc1.Object("foo.object1") == null);
        }
        
        [Test()]
        public void Should_set_and_get_object_fields()
        {
            object obj1 = 123;
            object obj2 = 132;
            
            var doc1 = Dictator.New()
                .Object("object1", obj1)
                .Object("foo.object1", obj2);
            
            Assert.IsTrue(doc1.Object("object1") == obj1);
            Assert.IsTrue(doc1.Object("foo.object1") == obj2);
        }
        
        [Test()]
        public void Should_set_and_get_generic_object_fields()
        {
            var doc1 = Dictator.New()
                .Object<int>("object1", 123)
                .Object<int>("foo.object1", 132);
            
            Assert.IsTrue(doc1.Object<int>("object1") == 123);
            Assert.IsTrue(doc1.Object<int>("foo.object1") == 132);
        }
        
        // TODO: test datetime
        
        // TODO: test enum
        
        // TODO: test list
    }
}
