using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Dictator.Tests
{
    [TestFixture()]
    public class DeserializationTests
    {
        [Test()]
        public void Should_deserialize_strongly_typed_object_to_document()
        {
            var children = new List<Dummy>();
            
            for (int i = 0; i < 5; i++)
            {
                var child = new Dummy();
                child.Foo = "string " + i;
                child.Bar = i;
                
                children.Add(child);
            }
            
            var parent = new Dummy();
            parent.Foo = "parent string value";
            parent.Bar = 11111;
            
            var documentChildren = new List<Dictionary<string, object>>();
            
            for (int i = 0; i < 5; i++)
            {
                var child = new Dictionary<string, object>()
                    .String("Foo", "string " + i)
                    .Int("Bar", i);
                
                documentChildren.Add(child);
            }
            
            var documentParent = new Dictionary<string, object>()
                .String("Foo", "parent string value")
                .Int("Bar", 11111);
            
            var primitiveDictionary = new Dictionary<string, string>();
            primitiveDictionary.Add("aaa", "bbb");
            
            var root = new Dummy();
            root.Foo = "string value";
            root.Bar = 12345;
            root.FooList = new List<string> { "one", "two", "three" };
            root.Parent = parent;
            root.Children = children;
            root.Dictionary = documentParent;
            root.Dictionaries = documentChildren;
            root.PrimitiveDictionary = primitiveDictionary;
            root.PrimitiveArray = new [] { "one", "two", "three" };
            root.MyEnum = MyEnum.Option2;
            
            var document = Dictator.ToDocument(root);
            
            Assert.AreEqual(root.Foo, document.String("Foo"));
            Assert.AreEqual(root.Bar, document.Int("Bar"));
            Assert.AreEqual(root.Parent.Foo, document.String("Parent.Foo"));
            Assert.AreEqual(root.Parent.Bar, document.Int("Parent.Bar"));
            Assert.AreEqual(new List<string> { "one", "two", "three" }, document.List<string>("FooList"));
            
            for (int i = 0; i < 5; i++)
            {
                var doc = document.List<Dictionary<string, object>>("Children")[i];
                
                Assert.AreEqual("string " + i, doc.String("Foo"));
                Assert.AreEqual(i, doc.Int("Bar"));
            }
            
            Assert.AreEqual(root.Dictionary.String("Foo"), document.String("Dictionary.Foo"));
            Assert.AreEqual(root.Dictionary.Int("Bar"), document.Int("Dictionary.Bar"));
            
            for (int i = 0; i < 5; i++)
            {
                var dict = document.List<Dictionary<string, object>>("Dictionaries")[i];
                
                Assert.AreEqual("string " + i, dict.String("Foo"));
                Assert.AreEqual(i, dict.Int("Bar"));
            }
            
            Assert.AreEqual(1, document.Object<Dictionary<string, string>>("PrimitiveDictionary").Count);
            Assert.AreEqual(root.PrimitiveDictionary["aaa"], document.Object<Dictionary<string, string>>("PrimitiveDictionary")["aaa"]);
            
            Assert.AreEqual(root.PrimitiveArray, document.Array<string>("PrimitiveArray"));
            
            Assert.AreEqual((int)root.MyEnum, document.Int("MyEnum"));
            Assert.AreEqual(root.MyEnum, document.Enum<MyEnum>("MyEnum"));
        }
        
        [Test()]
        public void Should_deserialize_strongly_typed_object_list_to_document_list()
        {
            var children = new List<Dummy>();
            
            for (int i = 0; i < 5; i++)
            {
                var child = new Dummy();
                child.Foo = "string " + i;
                child.Bar = i;
                
                children.Add(child);
            }
            
            var parent = new Dummy();
            parent.Foo = "parent string value";
            parent.Bar = 11111;
            
            var documentChildren = new List<Dictionary<string, object>>();
            
            for (int i = 0; i < 5; i++)
            {
                var child = new Dictionary<string, object>()
                    .String("Foo", "string " + i)
                    .Int("Bar", i);
                
                documentChildren.Add(child);
            }
            
            var documentParent = new Dictionary<string, object>()
                .String("Foo", "parent string value")
                .Int("Bar", 11111);            
            
            var primitiveDictionary = new Dictionary<string, string>();
            primitiveDictionary.Add("aaa", "bbb");
            
            var objectList = new List<Dummy>();
            
            for (int i = 0; i < 5; i++)
            {
                var root = new Dummy();
                root.Foo = "string value";
                root.Bar = 12345;
                root.FooList = new List<string> { "one", "two", "three" };
                root.Parent = parent;
                root.Children = children;            
                root.Dictionary = documentParent;
                root.Dictionaries = documentChildren;
                root.PrimitiveDictionary = primitiveDictionary;
                
                objectList.Add(root);
            }
            
            var documents = Dictator.ToDocuments(objectList);
            
            for (int i = 0; i < documents.Count; i++)
            {
                var document = documents[i];
                var root = objectList[i];
                
                Assert.AreEqual(root.Foo, document.String("Foo"));
                Assert.AreEqual(root.Bar, document.Int("Bar"));
                Assert.AreEqual(root.Parent.Foo, document.String("Parent.Foo"));
                Assert.AreEqual(root.Parent.Bar, document.Int("Parent.Bar"));
                Assert.AreEqual(new List<string> { "one", "two", "three" }, document.List<string>("FooList"));
                
                for (int j = 0; j < 5; j++)
                {
                    var doc = document.List<Dictionary<string, object>>("Children")[j];
                    
                    Assert.AreEqual("string " + j, doc.String("Foo"));
                    Assert.AreEqual(j, doc.Int("Bar"));
                }
                
                Assert.AreEqual(root.Dictionary.String("Foo"), document.String("Dictionary.Foo"));
                Assert.AreEqual(root.Dictionary.Int("Bar"), document.Int("Dictionary.Bar"));
                
                for (int j = 0; j < 5; j++)
                {
                    var dict = document.List<Dictionary<string, object>>("Dictionaries")[j];
                    
                    Assert.AreEqual("string " + j, dict.String("Foo"));
                    Assert.AreEqual(j, dict.Int("Bar"));
                }
                
                Assert.AreEqual(1, document.Object<Dictionary<string, string>>("PrimitiveDictionary").Count);
                Assert.AreEqual(root.PrimitiveDictionary["aaa"], document.Object<Dictionary<string, string>>("PrimitiveDictionary")["aaa"]);
            }
        }
        
        [Test()]
        public void Should_deserialize_array()
        {
            var entity = new ArrayEntity();
            entity.PrimitiveStringArray = new [] { "one", "two", "three" };
            entity.PrimitiveIntArray = new [] { 1, 2, 3 };
            entity.GenericObjectArray = new [] {
                        new Dummy { Foo = "one", Bar = 1 },
                        new Dummy { Foo = "two", Bar = 2 },
                        new Dummy { Foo = "three", Bar = 3 }
                    };
            
            var doc = Dictator.ToDocument(entity);
            
            // string array
            Assert.AreEqual(entity.PrimitiveStringArray.Length, doc.Size("PrimitiveStringArray"));
            
            for (int i = 0; i < entity.PrimitiveStringArray.Length; i++)
            {
                Assert.AreEqual(entity.PrimitiveStringArray[i], doc.Array<string>("PrimitiveStringArray")[i]);
            }
            
            // int array
            Assert.AreEqual(entity.PrimitiveIntArray.Length, doc.Size("PrimitiveIntArray"));
            
            for (int i = 0; i < entity.PrimitiveIntArray.Length; i++)
            {
                Assert.AreEqual(entity.PrimitiveIntArray[i], doc.Array<int>("PrimitiveIntArray")[i]);
            }
            
            // generic object array
            Assert.AreEqual(entity.GenericObjectArray.Length, doc.Size("GenericObjectArray"));
            
            for (int i = 0; i < entity.GenericObjectArray.Length; i++)
            {
                var dummy = entity.GenericObjectArray[i];
                
                Assert.AreEqual(dummy.Foo, doc.Array<Dummy>("GenericObjectArray")[i].Foo);
                Assert.AreEqual(dummy.Bar, doc.Array<Dummy>("GenericObjectArray")[i].Bar);
            }
        }
    }
}
