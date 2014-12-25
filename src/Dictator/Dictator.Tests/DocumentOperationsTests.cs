﻿using System;
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
            
            var root = new Dictionary<string, object>()
                .String("Foo", "string value")
                .Int("Bar", 12345)
                .List("FooList", new List<string> { "one", "two", "three" })
                .Document("Parent", parent)
                .List("Children", children)
                .Object("Dictionary", parent)
                .List("Dictionaries", children);
            
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
        }
    }
}
