﻿using System;
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
                .Bool("bool2", false);
            
            Assert.IsTrue(doc1.Bool("bool1"));
            Assert.IsFalse(doc1.Bool("bool2"));
        }
        
        [Test()]
        public void Should_set_and_get_byte_fields()
        {
            var doc1 = Dictator.New()
                .Byte("byte1", 123);
            
            Assert.IsTrue(doc1.Byte("byte1") == 123);
        }
        
        [Test()]
        public void Should_set_and_get_short_fields()
        {
            var doc1 = Dictator.New()
                .Short("short1", 12345);
            
            Assert.IsTrue(doc1.Short("short1") == 12345);
        }
        
        [Test()]
        public void Should_set_and_get_int_fields()
        {
            var doc1 = Dictator.New()
                .Int("int1", 123456);
            
            Assert.IsTrue(doc1.Int("int1") == 123456);
        }
        
        [Test()]
        public void Should_set_and_get_long_fields()
        {
            var doc1 = Dictator.New()
                .Long("long1", 123456789);
            
            Assert.IsTrue(doc1.Long("long1") == 123456789);
        }
        
        [Test()]
        public void Should_set_and_get_float_fields()
        {
            var doc1 = Dictator.New()
                .Float("float1", 3.14f);
            
            Assert.IsTrue(doc1.Float("float1") == 3.14f);
        }
        
        [Test()]
        public void Should_set_and_get_double_fields()
        {
            var doc1 = Dictator.New()
                .Double("double1", 3.14);
            
            Assert.IsTrue(doc1.Double("double1") == 3.14);
        }
        
        [Test()]
        public void Should_set_and_get_decimal_fields()
        {
            var doc1 = Dictator.New()
                .Decimal("decimal1", new Decimal(3.14));
            
            Assert.IsTrue(doc1.Decimal("decimal1") == new Decimal(3.14));
        }
        
        [Test()]
        public void Should_set_and_get_string_fields()
        {
            var doc1 = Dictator.New()
                .String("string1", "test1");
            
            Assert.IsTrue(doc1.String("string1") == "test1");
        }
        
        [Test()]
        public void Should_set_and_get_null_fields()
        {
            var doc1 = Dictator.New()
                .Object("object1", null);
            
            Assert.IsTrue(doc1.Object("object1") == null);
        }
        
        #region Object
        
        [Test()]
        public void Should_set_and_get_object_fields()
        {
            object obj1 = 123;
            object obj2 = 132;
            
            var doc1 = Dictator.New()
                .Object("object1", obj1);
            
            Assert.IsTrue(doc1.Object("object1") == obj1);
        }
        
        [Test()]
        public void Should_set_and_get_generic_object_fields()
        {
            var doc1 = Dictator.New()
                .Object<int>("object1", 123);
            
            Assert.IsTrue(doc1.Object<int>("object1") == 123);
        }
        
        #endregion
        
        #region DateTime
        
        [Test()]
        public void Should_set_and_get_dateTime_object_fields()
        {
            var utcNow = DateTime.UtcNow;
            
            var doc1 = Dictator.New()
                .DateTime("dateTime1", utcNow);
            
            Assert.IsTrue(doc1.DateTime("dateTime1") == utcNow);
        }
        
        [Test()]
        public void Should_set_and_get_dateTime_string_fields()
        {
            var utcNow = DateTime.UtcNow;
            
            var doc1 = Dictator.New()
                .DateTime("dateTime1", utcNow, DateTimeFormat.String);
            
            Assert.IsTrue(doc1.String("dateTime1") == utcNow.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'"));
        }
        
        [Test()]
        public void Should_set_and_get_dateTime_unixTimeStamp_fields()
        {
            var utcNow = DateTime.UtcNow;
            
            TimeSpan span = (utcNow.ToUniversalTime() - Dictator.Settings.UnixEpoch);
        	var nowUnix = (long)span.TotalSeconds;
            
            var doc1 = Dictator.New()
                .DateTime("dateTime1", utcNow, DateTimeFormat.UnixTimeStamp);
            
            Assert.IsTrue(doc1.Long("dateTime1") == nowUnix);
        }
        
        #endregion
        
        #region Enum
        
        [Test()]
        public void Should_set_and_get_enum_object_fields()
        {
            var doc1 = Dictator.New()
                .Enum("enum1", DateTimeFormat.Object);
            
            Assert.IsTrue(doc1.Enum<DateTimeFormat>("enum1") == DateTimeFormat.Object);
        }
        
        [Test()]
        public void Should_set_and_get_enum_int_fields()
        {
            var doc1 = Dictator.New()
                .Int("enum1", 0);
            
            Assert.IsTrue(doc1.Enum<DateTimeFormat>("enum1") == DateTimeFormat.Object);
        }
        
        [Test()]
        public void Should_set_and_get_enum_string_fields()
        {
            var doc1 = Dictator.New()
                .String("enum1", "object");
            
            Assert.IsTrue(doc1.Enum<DateTimeFormat>("enum1") == DateTimeFormat.Object);
        }
        
        #endregion
        
        #region List
            
        [Test()]
        public void Should_set_and_get_list_with_primitive_values()
        {
            var list1 = new List<int>() { 1, 2, 3 };
            
            var doc1 = Dictator.New()
                .List("list1", list1);
            
            Assert.IsTrue(doc1.List<int>("list1") == list1);
        }
        
        [Test()]
        public void Should_set_and_get_list_with_dictionary_values()
        {
            var list1 = new List<Dictionary<string, object>>() 
            {
                Dictator.New()
                    .String("string1", "test1"),
                Dictator.New()
                    .String("string1", "test2"),
                Dictator.New()
                    .String("string1", "test3")
            };
            
            var doc1 = Dictator.New()
                .List("list1", list1);
            
            Assert.IsTrue(doc1.List<Dictionary<string, object>>("list1") == list1);
        }
        
        #endregion
        
        [Test()]
        public void Should_set_and_get_nested_fields()
        {
            var doc1 = Dictator.New()
                .String("string1", "test1")
                .String("foo.string2", "test2")
                .String("foo.bar.string3", "test3");
            
            Assert.IsTrue(doc1.String("string1") == "test1");
            Assert.IsTrue(doc1.String("foo.string2") == "test2");
            Assert.IsTrue(doc1.String("foo.bar.string3") == "test3");
        }
    }
}
