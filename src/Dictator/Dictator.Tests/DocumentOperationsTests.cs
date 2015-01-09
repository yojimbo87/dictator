using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Dictator.Tests
{
    [TestFixture()]
    public class DocumentOperationsTests
    {
        [Test()]
        public void Should_merge_document_fields_with_default_settings()
        {
            var doc1 = Dictator.New()
                .String("foo", "string value")
                .Int("bar", 12345);
            
            var doc2 = Dictator.New()
                .String("foo", "new string value")
                .Int("baz", 54321);
            
            doc1.Merge(doc2);
            
            Assert.AreEqual(doc1.Count, 3);
            Assert.AreEqual(doc1.String("foo"), "new string value");
            Assert.AreEqual(doc1.Int("bar"), 12345);
            Assert.AreEqual(doc1.Int("baz"), 54321);
            
            Assert.AreEqual(doc2.Count, 2);
        }
        
        [Test()]
        public void Should_merge_document_fields_and_keep_existing_fields()
        {
            var doc1 = Dictator.New()
                .String("foo", "string value")
                .Int("bar", 12345);
            
            var doc2 = Dictator.New()
                .String("foo", "new string value")
                .Int("baz", 54321);
            
            doc1.Merge(doc2, MergeBehavior.KeepFields);
            
            Assert.AreEqual(doc1.Count, 3);
            Assert.AreEqual(doc1.String("foo"), "string value");
            Assert.AreEqual(doc1.Int("bar"), 12345);
            Assert.AreEqual(doc1.Int("baz"), 54321);
            
            Assert.AreEqual(doc2.Count, 2);
        }
        
        [Test()]
        public void Should_clone_document()
        {
            var doc1 = Dictator.New()
                .String("foo", "string value")
                .Int("bar", 12345)
                .String("baz.foo", "string value")
                .Int("baz.bar", 123)
                .String("baz.baz.foo", "string value");
            
            Assert.AreEqual(doc1.Count, 3);
            Assert.AreEqual(doc1.String("foo"), "string value");
            Assert.AreEqual(doc1.Int("bar"), 12345);
            Assert.AreEqual(doc1.String("baz.foo"), "string value");
            Assert.AreEqual(doc1.Int("baz.bar"), 123);
            Assert.AreEqual(doc1.String("baz.baz.foo"), "string value");
            
            var clonedDocument = doc1.Clone();
            
            Assert.AreEqual(clonedDocument.Count, 3);
            Assert.AreEqual(clonedDocument.String("foo"), "string value");
            Assert.AreEqual(clonedDocument.Int("bar"), 12345);
            Assert.AreEqual(clonedDocument.String("baz.foo"), "string value");
            Assert.AreEqual(clonedDocument.Int("baz.bar"), 123);
            Assert.AreEqual(clonedDocument.String("baz.baz.foo"), "string value");
            
            clonedDocument
                .String("foo", "new string value")
                .Int("bar", 54321)
                .Drop("baz.foo")
                .Int("baz.bar", 321)
                .String("baz.baz.foo", "new string value");
            
            Assert.AreEqual(doc1.Count, 3);
            Assert.AreEqual(doc1.String("foo"), "string value");
            Assert.AreEqual(doc1.Int("bar"), 12345);
            Assert.AreEqual(doc1.String("baz.foo"), "string value");
            Assert.AreEqual(doc1.Int("baz.bar"), 123);
            Assert.AreEqual(doc1.String("baz.baz.foo"), "string value");
            
            Assert.AreEqual(clonedDocument.Count, 3);
            Assert.AreEqual(clonedDocument.String("foo"), "new string value");
            Assert.AreEqual(clonedDocument.Int("bar"), 54321);
            Assert.AreEqual(clonedDocument.Document("baz").Count, 2);
            Assert.IsFalse(clonedDocument.Has("baz.foo"));
            Assert.AreEqual(clonedDocument.Int("baz.bar"), 321);
            Assert.AreEqual(clonedDocument.String("baz.baz.foo"), "new string value");
        }
        
        [Test()]
        public void Should_cloneExcept_document()
        {
            var doc1 = Dictator.New()
                .String("foo", "string value")
                .Int("bar", 12345)
                .String("baz.foo", "string value")
                .Int("baz.bar", 123)
                .String("baz.baz.foo", "string value");
            
            Assert.AreEqual(doc1.Count, 3);
            Assert.AreEqual(doc1.String("foo"), "string value");
            Assert.AreEqual(doc1.Int("bar"), 12345);
            Assert.AreEqual(doc1.String("baz.foo"), "string value");
            Assert.AreEqual(doc1.Int("baz.bar"), 123);
            Assert.AreEqual(doc1.String("baz.baz.foo"), "string value");
            
            var clonedDocument = doc1.CloneExcept("bar", "baz.bar", "nonExistingField");
            
            Assert.AreEqual(clonedDocument.Count, 2);
            Assert.AreEqual(clonedDocument.String("foo"), "string value");
            Assert.IsFalse(clonedDocument.Has("bar"));
            Assert.AreEqual(clonedDocument.String("baz.foo"), "string value");
            Assert.IsFalse(clonedDocument.Has("baz.bar"));
            Assert.AreEqual(clonedDocument.String("baz.baz.foo"), "string value");
            
            clonedDocument
                .String("foo", "new string value")
                .Drop("baz.foo")
                .String("baz.baz.foo", "new string value")
                .Int("newInt", 111);
            
            Assert.AreEqual(doc1.Count, 3);
            Assert.AreEqual(doc1.String("foo"), "string value");
            Assert.AreEqual(doc1.Int("bar"), 12345);
            Assert.AreEqual(doc1.String("baz.foo"), "string value");
            Assert.AreEqual(doc1.Int("baz.bar"), 123);
            Assert.AreEqual(doc1.String("baz.baz.foo"), "string value");
            
            Assert.AreEqual(clonedDocument.Count, 3);
            Assert.AreEqual(clonedDocument.String("foo"), "new string value");
            Assert.AreEqual(clonedDocument.Document("baz").Count, 1);
            Assert.IsFalse(clonedDocument.Has("baz.foo"));
            Assert.AreEqual(clonedDocument.String("baz.baz.foo"), "new string value");
            Assert.AreEqual(clonedDocument.Int("newInt"), 111);
        }
        
        [Test()]
        public void Should_cloneOnly_document()
        {
            var doc1 = Dictator.New()
                .String("foo", "string value")
                .Int("bar", 12345)
                .String("baz.foo", "string value")
                .Int("baz.bar", 123)
                .String("baz.baz.foo", "string value");
            
            Assert.AreEqual(doc1.Count, 3);
            Assert.AreEqual(doc1.String("foo"), "string value");
            Assert.AreEqual(doc1.Int("bar"), 12345);
            Assert.AreEqual(doc1.String("baz.foo"), "string value");
            Assert.AreEqual(doc1.Int("baz.bar"), 123);
            Assert.AreEqual(doc1.String("baz.baz.foo"), "string value");
            
            var clonedDocument = doc1.CloneOnly("bar", "baz.bar", "baz.baz.foo", "nonExistingField");
            
            Assert.AreEqual(clonedDocument.Count, 2);
            Assert.AreEqual(clonedDocument.Int("bar"), 12345);
            Assert.AreEqual(clonedDocument.Int("baz.bar"), 123);
            Assert.AreEqual(clonedDocument.String("baz.baz.foo"), "string value");
            
            clonedDocument
                .Int("bar", 321)
                .Drop("baz.bar")
                .String("baz.foo", "new string value")
                .String("baz.baz.foo", "new string value")
                .Int("newInt", 111);
            
            Assert.AreEqual(doc1.Count, 3);
            Assert.AreEqual(doc1.String("foo"), "string value");
            Assert.AreEqual(doc1.Int("bar"), 12345);
            Assert.AreEqual(doc1.String("baz.foo"), "string value");
            Assert.AreEqual(doc1.Int("baz.bar"), 123);
            Assert.AreEqual(doc1.String("baz.baz.foo"), "string value");
            
            Assert.AreEqual(clonedDocument.Count, 3);
            Assert.AreEqual(clonedDocument.Int("bar"), 321);
            Assert.AreEqual(clonedDocument.Document("baz").Count, 2);
            Assert.AreEqual(clonedDocument.String("baz.foo"), "new string value");
            Assert.AreEqual(clonedDocument.String("baz.baz.foo"), "new string value");
            Assert.AreEqual(clonedDocument.Int("newInt"), 111);
        }
        
        [Test()]
        public void Should_drop_document_fields()
        {
            var doc1 = Dictator.New()
                .String("foo", "string value")
                .Int("bar", 12345)
                .Int("baq", 12345)
                .Int("baz.foo", 54321)
                .Int("boo.foo", 1)
                .Int("boo.bar", 2);
            
            Assert.AreEqual(doc1.Count, 5);
            Assert.AreEqual(doc1.Document("baz").Count, 1);
            Assert.AreEqual(doc1.Document("boo").Count, 2);
            
            doc1.Drop("bar", "baq", "nonExistingField");
            
            Assert.AreEqual(doc1.Count, 3);
            Assert.IsFalse(doc1.Has("bar"));
            Assert.IsFalse(doc1.Has("baq"));
            Assert.IsTrue(doc1.Has("baz"));
            Assert.IsTrue(doc1.Has("boo"));
            
            doc1.Drop("boo.foo");
            
            Assert.AreEqual(doc1.Document("boo").Count, 1);
            Assert.IsFalse(doc1.Has("boo.foo"));
            Assert.IsTrue(doc1.Has("boo.bar"));
        }
        
        [Test()]
        public void Should_convert_dictionary_to_strongly_typed_object()
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
                .Object("PrimitiveDictionary", primitiveDictionary);
            
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
        }
        
        [Test()]
        public void Should_convert_dictionary_list_to_strongly_typed_object_list()
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
        public void Should_convert_strongly_typed_object_to_document()
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
            
            Assert.AreEqual((int)root.MyEnum, document.Int("MyEnum"));
            Assert.AreEqual(root.MyEnum, document.Enum<MyEnum>("MyEnum"));
        }
        
        [Test()]
        public void Should_convert_strongly_typed_object_list_to_document_list()
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
    }
}
