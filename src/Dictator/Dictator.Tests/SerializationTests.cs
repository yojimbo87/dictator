using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Dictator.Tests
{
    [TestFixture()]
    public class SerializationTests
    {
        [Test()]
        public void Should_serialize_dictionary_to_strongly_typed_object()
        {
            var children = new List<Dictionary<string, object>>();
            
            for (int i = 0; i < 5; i++)
            {
                var child = new Dictionary<string, object>()
                    .String("Foo", "string " + i)
                    .Int("Bar", i);
                
                children.Add(child);
            }
            
            var parent = new Dictionary<string, object>()
                .String("Foo", "parent string value")
                .Int("Bar", 11111);
            
            var primitiveDictionary = new Dictionary<string, string>();
            primitiveDictionary.Add("aaa", "bbb");
            
            var root = new Dictionary<string, object>()
                .String("Foo", "string value")
                .Int("Bar", 12345)
                .List("FooList", new List<string> { "one", "two", "three" })
                .Document("Parent", parent)
                .List("Children", children)
                .Object("Dictionary", parent)
                .List("Dictionaries", children)
                .Object("PrimitiveDictionary", primitiveDictionary)
                .Array("PrimitiveArray", new [] { "one", "two", "three" });
            
            var stronglyTypedObject = root.ToObject<Dummy>();
            
            Assert.AreEqual(root.String("Foo"), stronglyTypedObject.Foo);
            Assert.AreEqual(root.Int("Bar"), stronglyTypedObject.Bar);
            Assert.AreEqual(root.String("Parent.Foo"), stronglyTypedObject.Parent.Foo);
            Assert.AreEqual(root.Int("Parent.Bar"), stronglyTypedObject.Parent.Bar);
            Assert.AreEqual(new List<string> { "one", "two", "three" }, stronglyTypedObject.FooList);
            
            for (int i = 0; i < 5; i++)
            {
                var obj = stronglyTypedObject.Children[i];
                
                Assert.AreEqual("string " + i, obj.Foo);
                Assert.AreEqual(i, obj.Bar);
            }
            
            Assert.AreEqual(root.String("Dictionary.Foo"), stronglyTypedObject.Dictionary.String("Foo"));
            Assert.AreEqual(root.Int("Dictionary.Bar"), stronglyTypedObject.Dictionary.Int("Bar"));
            
            for (int i = 0; i < 5; i++)
            {
                var dict = stronglyTypedObject.Dictionaries[i];
                
                Assert.AreEqual("string " + i, dict.String("Foo"));
                Assert.AreEqual(i, dict.Int("Bar"));
            }
            
            Assert.AreEqual(1, stronglyTypedObject.PrimitiveDictionary.Count);
            Assert.AreEqual(root.Object<Dictionary<string, string>>("PrimitiveDictionary")["aaa"], stronglyTypedObject.PrimitiveDictionary["aaa"]);
            Assert.AreEqual(root.Array<string>("PrimitiveArray"), stronglyTypedObject.PrimitiveArray);
        }
        
        [Test()]
        public void Should_serialize_dictionary_list_to_strongly_typed_object_list()
        {
            var children = new List<Dictionary<string, object>>();
            
            for (int i = 0; i < 5; i++)
            {
                var child = new Dictionary<string, object>()
                    .String("Foo", "string " + i)
                    .Int("Bar", i);
                
                children.Add(child);
            }
            
            var parent = new Dictionary<string, object>()
                .String("Foo", "parent string value")
                .Int("Bar", 11111);
            
            var dictionaryList = new List<Dictionary<string, object>>();
            
            var primitiveDictionary = new Dictionary<string, string>();
            primitiveDictionary.Add("aaa", "bbb");
            
            for (int i = 0; i < 5; i++)
            {
                var root = new Dictionary<string, object>()
                    .String("Foo", "string value")
                    .Int("Bar", 12345)
                    .List("FooList", new List<string> { "one", "two", "three" })
                    .Document("Parent", parent)
                    .List("Children", children)
                    .Object("Dictionary", parent)
                    .List("Dictionaries", children)
                    .Object("PrimitiveDictionary", primitiveDictionary);
                
                dictionaryList.Add(root);
            }
            
            var stronglyTypedObjectList = Dictator.ToList<Dummy>(dictionaryList);
            
            for (int i = 0; i < stronglyTypedObjectList.Count; i++)
            {
                var stronglyTypedObject = stronglyTypedObjectList[i];
                var root = dictionaryList[i];
                
                Assert.AreEqual(root.String("Foo"), stronglyTypedObject.Foo);
                Assert.AreEqual(root.Int("Bar"), stronglyTypedObject.Bar);
                Assert.AreEqual(root.String("Parent.Foo"), stronglyTypedObject.Parent.Foo);
                Assert.AreEqual(root.Int("Parent.Bar"), stronglyTypedObject.Parent.Bar);
                Assert.AreEqual(new List<string> { "one", "two", "three" }, stronglyTypedObject.FooList);
                
                for (int j = 0; j < 5; j++)
                {
                    var obj = stronglyTypedObject.Children[j];
                    
                    Assert.AreEqual("string " + j, obj.Foo);
                    Assert.AreEqual(j, obj.Bar);
                }
                
                Assert.AreEqual(root.String("Dictionary.Foo"), stronglyTypedObject.Dictionary.String("Foo"));
                Assert.AreEqual(root.Int("Dictionary.Bar"), stronglyTypedObject.Dictionary.Int("Bar"));
                
                for (int j = 0; j < 5; j++)
                {
                    var dict = stronglyTypedObject.Dictionaries[j];
                    
                    Assert.AreEqual("string " + j, dict.String("Foo"));
                    Assert.AreEqual(j, dict.Int("Bar"));
                }
                
                Assert.AreEqual(1, stronglyTypedObject.PrimitiveDictionary.Count);
                Assert.AreEqual(root.Object<Dictionary<string, string>>("PrimitiveDictionary")["aaa"], stronglyTypedObject.PrimitiveDictionary["aaa"]);
            }
        }
        
        [Test()]
        public void Should_serialize_array()
        {
            var doc = new Dictionary<string, object>()
                .Array("PrimitiveStringArray", new [] { "one", "two", "three" })
                .Array("PrimitiveIntArray", new [] { 1, 2, 3 })
                .Array("GenericObjectArray", new [] {
                        new Dummy { Foo = "one", Bar = 1 },
                        new Dummy { Foo = "two", Bar = 2 },
                        new Dummy { Foo = "three", Bar = 3 }
                    });
            
            var entity = doc.ToObject<ArrayEntity>();
            
            // string array
            Assert.AreEqual(doc.Size("PrimitiveStringArray"), entity.PrimitiveStringArray.Length);
            
            for (int i = 0; i < doc.Size("PrimitiveStringArray"); i++)
            {
                Assert.AreEqual(doc.Object<string[]>("PrimitiveStringArray")[i], entity.PrimitiveStringArray[i]);
            }
            
            // int array
            Assert.AreEqual(doc.Size("PrimitiveIntArray"), entity.PrimitiveIntArray.Length);
            
            for (int i = 0; i < doc.Size("PrimitiveIntArray"); i++)
            {
                Assert.AreEqual(doc.Object<int[]>("PrimitiveIntArray")[i], entity.PrimitiveIntArray[i]);
            }
            
            // generic object array
            Assert.AreEqual(doc.Size("GenericObjectArray"), entity.GenericObjectArray.Length);
            
            for (int i = 0; i < doc.Size("GenericObjectArray"); i++)
            {
                var dummy = doc.Object<Dummy[]>("GenericObjectArray")[i];
                
                Assert.AreEqual(dummy.Foo, entity.GenericObjectArray[i].Foo);
                Assert.AreEqual(dummy.Bar, entity.GenericObjectArray[i].Bar);
            }
        }
    }
}
