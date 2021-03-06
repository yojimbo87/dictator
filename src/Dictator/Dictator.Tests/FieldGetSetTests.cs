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
        public void Should_set_and_get_guid_fields()
        {
            var guid = Guid.NewGuid();
            
            var doc1 = Dictator.New()
                .Guid("guid1", guid);
            
            Assert.AreEqual(guid.ToString(), doc1.Guid("guid1").ToString());
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
        public void Should_set_and_get_object_fields_with_cast_to_primitive_values()
        {
            object obj1 = 123;
            object obj2 = "test1";
            
            var doc1 = Dictator.New()
                .Object("object1", obj1)
                .Object("object2", obj2);
            
            Assert.IsTrue(doc1.Int("object1") == 123);
            Assert.IsTrue(doc1.String("object2") == "test1");
        }
        
        [Test()]
        public void Should_set_and_get_generic_object_fields()
        {
            var doc1 = Dictator.New()
                .Object<int>("object1", 123);
            
            Assert.IsTrue(doc1.Object<int>("object1") == 123);
        }
        
        #endregion
        
        [Test()]
        public void Should_set_and_get_document_fields()
        {
            var document = Dictator.New();
            
            var doc1 = Dictator.New()
                .Document("document1", document);
            
            Assert.IsTrue(doc1.Document("document1") == document);
        }
        
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
        public void Should_set_and_get_enum_integer_fields()
        {
            var doc1 = Dictator.New()
                .Enum("enum1", DateTimeFormat.Object, EnumFormat.Integer)
                .Byte("byte", 1)
                .Object("sbyte", (sbyte)1)
                .Short("short", 1)
                .Object("ushort", (ushort)1)
                .Int("int", 1)
                .Object("uint", (uint)1)
                .Long("long", 1)
                .Object("ulong", (ulong)1);
            
            Assert.IsTrue(doc1.Enum<DateTimeFormat>("enum1") == DateTimeFormat.Object);
            Assert.IsTrue(doc1.Int("enum1") == 0);
            
            Assert.IsTrue(doc1.Enum<DateTimeFormat>("byte") == DateTimeFormat.String);
            Assert.IsTrue(doc1.Enum<DateTimeFormat>("sbyte") == DateTimeFormat.String);
            Assert.IsTrue(doc1.Enum<DateTimeFormat>("short") == DateTimeFormat.String);
            Assert.IsTrue(doc1.Enum<DateTimeFormat>("ushort") == DateTimeFormat.String);
            Assert.IsTrue(doc1.Enum<DateTimeFormat>("int") == DateTimeFormat.String);
            Assert.IsTrue(doc1.Enum<DateTimeFormat>("uint") == DateTimeFormat.String);
            Assert.IsTrue(doc1.Enum<DateTimeFormat>("long") == DateTimeFormat.String);
            Assert.IsTrue(doc1.Enum<DateTimeFormat>("ulong") == DateTimeFormat.String);
        }
        
        [Test()]
        public void Should_set_and_get_enum_string_fields()
        {
            var doc1 = Dictator.New()
                .Enum("enum1", DateTimeFormat.Object, EnumFormat.String)
                .String("enum2", "string");
            
            Assert.IsTrue(doc1.Enum<DateTimeFormat>("enum1") == DateTimeFormat.Object);
            Assert.IsTrue(doc1.String("enum1") == "Object");
            Assert.IsTrue(doc1.Enum<DateTimeFormat>("enum2") == DateTimeFormat.String);
        }
        
        #endregion
        
        #region List
            
        [Test()]
        public void Should_set_and_get_list_with_primitive_values()
        {
            var list1 = new List<int>() { 1, 2, 3 };
            
            var doc1 = Dictator.New()
                .List("list1", list1);
            
            for (int i = 0; i < list1.Count; i++)
            {
                Assert.IsTrue(doc1.List<int>("list1")[i] == list1[i]);
            }
        }
        
        [Test()]
        public void Should_set_and_get_list_with_object_values()
        {
            var list1 = new List<object>() { 1, 2, 3 };
            
            var doc1 = Dictator.New()
                .List("list1", list1);

            for (int i = 0; i < list1.Count; i++)
            {
                Assert.IsTrue(doc1.List<int>("list1")[i] == (int)list1[i]);
            }
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
            
            for (int i = 0; i < list1.Count; i++)
            {
                Assert.IsTrue(doc1.List<Dictionary<string, object>>("list1")[i].String("string1") == list1[i].String("string1"));
            }
        }
        
        [Test()]
        public void Should_set_and_get_list_size()
        {
            var list1 = new List<int>() { 1, 2, 3 };
            
            var doc1 = Dictator.New()
                .List("list1", list1);
            
            Assert.AreEqual(doc1.Size("list1"), 3);
        }
        
        [Test()]
        public void Should_set_list_and_get_item_at_specified_index()
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
            
            var list2 = new List<int>() { 1, 2, 3 };
            
            var doc1 = Dictator.New()
                .List("list1", list1)
                .List("list2", list2);
            
            Assert.AreEqual(3, doc1.Size("list1"));
            Assert.AreEqual("test1", doc1.String("list1[0].string1"));
            Assert.AreEqual("test2", doc1.String("list1[1].string1"));
            Assert.AreEqual("test3", doc1.String("list1[2].string1"));
            
            Assert.AreEqual(3, doc1.Size("list2"));
            Assert.AreEqual(1, doc1.Int("list2[0]"));
            Assert.AreEqual(2, doc1.Int("list2[1]"));
            Assert.AreEqual(3, doc1.Int("list2[2]"));
        }
        
        [Test()]
        public void Should_set_list_items_at_specified_positions()
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
            
            var list2 = new List<int>() { 1, 2, 3 };
            
            var doc1 = Dictator.New()
                .List("list1", list1)
                .List("list2", list2);
            
            // insert at specified index
            doc1.String("list1[1].string1", "new test2");
            
            Assert.AreEqual(3, doc1.Size("list1"));
            Assert.AreEqual("test1", doc1.String("list1[0].string1"));
            Assert.AreEqual("new test2", doc1.String("list1[1].string1"));
            Assert.AreEqual("test3", doc1.String("list1[2].string1"));
            
            doc1.Int("list2[1]", 222);
            
            Assert.AreEqual(3, doc1.Size("list2"));
            Assert.AreEqual(1, doc1.Int("list2[0]"));
            Assert.AreEqual(222, doc1.Int("list2[1]"));
            Assert.AreEqual(3, doc1.Int("list2[2]"));
            
            // append new value
            doc1.Document("list1[*]", Dictator.New().String("string1", "test4"));
            
            Assert.AreEqual(4, doc1.Size("list1"));
            Assert.AreEqual("test1", doc1.String("list1[0].string1"));
            Assert.AreEqual("new test2", doc1.String("list1[1].string1"));
            Assert.AreEqual("test3", doc1.String("list1[2].string1"));
            Assert.AreEqual("test4", doc1.String("list1[3].string1"));
            
            doc1.Int("list2[*]", 4);
            
            Assert.AreEqual(4, doc1.Size("list2"));
            Assert.AreEqual(1, doc1.Int("list2[0]"));
            Assert.AreEqual(222, doc1.Int("list2[1]"));
            Assert.AreEqual(3, doc1.Int("list2[2]"));
            Assert.AreEqual(4, doc1.Int("list2[3]"));
        }
        
        #endregion
        
        #region Array
        
        [Test()]
        public void Should_set_and_get_array_with_primitive_values()
        {
            var array1 = new [] { 1, 2, 3 };
            
            var doc1 = Dictator.New()
                .Array("array1", array1);
            
            for (int i = 0; i < array1.Length; i++)
            {
                Assert.IsTrue(doc1.Array<int>("array1")[i] == array1[i]);
            }
        }
        
        [Test()]
        public void Should_set_and_get_array_size()
        {
            var array1 = new [] { 1, 2, 3 };
            
            var doc1 = Dictator.New()
                .Array("array1", array1);
            
            Assert.AreEqual(doc1.Size("array1"), 3);
        }
        
        [Test()]
        public void Should_set_array_and_get_item_at_specified_index()
        {
            var array1 = new []
            {
                Dictator.New()
                    .String("string1", "test1"),
                Dictator.New()
                    .String("string1", "test2"),
                Dictator.New()
                    .String("string1", "test3")
            };
            
            var array2 = new [] { 1, 2, 3 };
            
            var doc1 = Dictator.New()
                .Array("array1", array1)
                .Array("array2", array2);
            
            Assert.AreEqual(3, doc1.Size("array1"));
            Assert.AreEqual("test1", doc1.String("array1[0].string1"));
            Assert.AreEqual("test2", doc1.String("array1[1].string1"));
            Assert.AreEqual("test3", doc1.String("array1[2].string1"));
            
            Assert.AreEqual(3, doc1.Size("array2"));
            Assert.AreEqual(1, doc1.Int("array2[0]"));
            Assert.AreEqual(2, doc1.Int("array2[1]"));
            Assert.AreEqual(3, doc1.Int("array2[2]"));
        }
        
        [Test()]
        public void Should_set_array_items_at_specified_positions()
        {
            var array1 = new []
            {
                Dictator.New()
                    .String("string1", "test1"),
                Dictator.New()
                    .String("string1", "test2"),
                Dictator.New()
                    .String("string1", "test3")
            };
            
            var array2 = new [] { 1, 2, 3 };
            
            var doc1 = Dictator.New()
                .Array("array1", array1)
                .Array("array2", array2);
            
            // insert at specified index
            doc1.String("array1[1].string1", "new test2");
            
            Assert.AreEqual(3, doc1.Size("array1"));
            Assert.AreEqual("test1", doc1.String("array1[0].string1"));
            Assert.AreEqual("new test2", doc1.String("array1[1].string1"));
            Assert.AreEqual("test3", doc1.String("array1[2].string1"));
            
            doc1.Int("array2[1]", 222);
            
            Assert.AreEqual(3, doc1.Size("array2"));
            Assert.AreEqual(1, doc1.Int("array2[0]"));
            Assert.AreEqual(222, doc1.Int("array2[1]"));
            Assert.AreEqual(3, doc1.Int("array2[2]"));
        }
        
        #endregion
        
        #region Field items iteration
        
        [Test()]
        public void Should_iterate_over_primitive_list_items()
        {
            var list1 = new List<int>() { 1, 2, 3 };
            
            var doc1 = Dictator.New()
                .List("list1", list1);
            
            var index = 0;
            
            doc1.Each<int>("list1", (i, item) => {
                Assert.AreEqual(index, i);
                Assert.AreEqual(list1[i], item);
               
                index++;
            });
        }
        
        [Test()]
        public void Should_iterate_over_document_list_items()
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
            
            var index = 0;
            
            doc1.Each("list1", (i, item) => {
                Assert.AreEqual(index, i);
                Assert.AreEqual(list1[i].String("string1"), item.String("string1"));
               
                index++;
            });
        }
        
        [Test()]
        public void Should_iterate_over_primitive_array_items()
        {
            var array1 = new [] { 1, 2, 3 };
            
            var doc1 = Dictator.New()
                .Array("array1", array1);
            
            var index = 0;
            
            doc1.Each<int>("array1", (i, item) => {
                Assert.AreEqual(index, i);
                Assert.AreEqual(array1[i], item);
               
                index++;
            });
        }
        
        [Test()]
        public void Should_iterate_over_document_array_items()
        {
            var array1 = new []
            {
                Dictator.New()
                    .String("string1", "test1"),
                Dictator.New()
                    .String("string1", "test2"),
                Dictator.New()
                    .String("string1", "test3")
            };
            
            var doc1 = Dictator.New()
                .Array("array1", array1);
            
            var index = 0;
            
            doc1.Each("array1", (i, item) => {
                Assert.AreEqual(index, i);
                Assert.AreEqual(array1[i].String("string1"), item.String("string1"));
               
                index++;
            });
        }
        
        [Test()]
        public void Should_modify_values_during_iteration()
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
            
            doc1.Each("list1", (i, item) => {
                item.String("string1", "new test " + i);
            });
            
            var index = 0;
            
            doc1.Each("list1", (i, item) => {
                Assert.AreEqual(index, i);
                Assert.AreEqual(list1[i].String("string1"), item.String("string1"));
               
                index++;
            });
        }
        
        #endregion
        
        #region Set long and get int
        
        [Test()]
        public void Should_set_long_and_get_int_fields()
        {
            var doc1 = new Dictionary<string, object>()
                .Long("long1", 123L)
                .List("longList", new List<long> { 1, 2, 3 })
                .Array("longArray", new long[] { 4, 5, 6 })
                .List("stringList", new List<string> { "7", "8", "9" });
            
            Assert.AreEqual(123, doc1.Int("long1"));
            
            Assert.AreEqual(1, doc1.Int("longList[0]"));
            Assert.AreEqual(2, doc1.Int("longList[1]"));
            Assert.AreEqual(3, doc1.Int("longList[2]"));
            
            Assert.AreEqual(1, doc1.List<int>("longList")[0]);
            Assert.AreEqual(2, doc1.List<int>("longList")[1]);
            Assert.AreEqual(3, doc1.List<int>("longList")[2]);
            
            Assert.AreEqual(4, doc1.Int("longArray[0]"));
            Assert.AreEqual(5, doc1.Int("longArray[1]"));
            Assert.AreEqual(6, doc1.Int("longArray[2]"));
            
            Assert.AreEqual(4, doc1.Array<int>("longArray")[0]);
            Assert.AreEqual(5, doc1.Array<int>("longArray")[1]);
            Assert.AreEqual(6, doc1.Array<int>("longArray")[2]);
            
            Assert.AreEqual(7, doc1.Int("stringList[0]"));
            Assert.AreEqual(8, doc1.Int("stringList[1]"));
            Assert.AreEqual(9, doc1.Int("stringList[2]"));
            
            Assert.AreEqual(7, doc1.List<int>("stringList")[0]);
            Assert.AreEqual(8, doc1.List<int>("stringList")[1]);
            Assert.AreEqual(9, doc1.List<int>("stringList")[2]);
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
        
        [Test()]
        public void Should_set_and_get_cast_fields()
        {
            var doc1 = Dictator.New()
                .Object("int1", (long)123)
                .Object("string1", "test2")
                .Object("float1", 3.14); // double for the purpose of later comparison with float
            
            Assert.IsTrue(doc1.Int("int1") == 123);
            Assert.IsTrue(doc1.String("string1") == "test2");
            Assert.IsTrue(doc1.Float("float1") == 3.14f);
        }
    }
}
