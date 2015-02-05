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
                .Int("boo.bar", 2)
                .List("list", new List<Dictionary<string, object>> 
                      {
                          new Dictionary<string, object>()
                              .String("foo", "string value 1")
                              .Int("bar", 1),
                          new Dictionary<string, object>()
                              .String("foo", "string value 2")
                              .Int("bar", 2)
                      }
                     )
                .Array("array", new []
                       {
                          new Dictionary<string, object>()
                              .String("foo", "string value 1")
                              .Int("bar", 1),
                          new Dictionary<string, object>()
                              .String("foo", "string value 2")
                              .Int("bar", 2)
                       }
                      );
            
            Assert.AreEqual(doc1.Count, 7);
            Assert.AreEqual(doc1.Document("baz").Count, 1);
            Assert.AreEqual(doc1.Document("boo").Count, 2);
            
            // remove root document fields
            doc1.Drop("bar", "baq", "nonExistingField");
            
            Assert.AreEqual(doc1.Count, 5);
            Assert.IsFalse(doc1.Has("bar"));
            Assert.IsFalse(doc1.Has("baq"));
            Assert.IsTrue(doc1.Has("baz"));
            Assert.IsTrue(doc1.Has("boo"));
            
            // remove embedded document field
            doc1.Drop("boo.foo");
            
            Assert.AreEqual(doc1.Document("boo").Count, 1);
            Assert.IsFalse(doc1.Has("boo.foo"));
            Assert.IsTrue(doc1.Has("boo.bar"));
            
            // remove list item field
            Assert.AreEqual(2, doc1.Size("list"));
            Assert.IsTrue(doc1.Document("list[0]").Has("foo"));
            Assert.IsTrue(doc1.Document("list[0]").Has("bar"));
            Assert.IsTrue(doc1.Document("list[1]").Has("foo"));
            Assert.IsTrue(doc1.Document("list[1]").Has("bar"));
            
            doc1.Drop("list[0].foo", "list[1].bar");
            
            Assert.AreEqual(2, doc1.Size("list"));
            Assert.IsFalse(doc1.Document("list[0]").Has("foo"));
            Assert.IsTrue(doc1.Document("list[0]").Has("bar"));
            Assert.IsTrue(doc1.Document("list[1]").Has("foo"));
            Assert.IsFalse(doc1.Document("list[1]").Has("bar"));
            
            // remove array item field
            Assert.AreEqual(2, doc1.Size("array"));
            Assert.IsTrue(doc1.Document("array[0]").Has("foo"));
            Assert.IsTrue(doc1.Document("array[0]").Has("bar"));
            Assert.IsTrue(doc1.Document("array[1]").Has("foo"));
            Assert.IsTrue(doc1.Document("array[1]").Has("bar"));
            
            doc1.Drop("array[0].foo", "array[1].bar");
            
            Assert.AreEqual(2, doc1.Size("array"));
            Assert.IsFalse(doc1.Document("array[0]").Has("foo"));
            Assert.IsTrue(doc1.Document("array[0]").Has("bar"));
            Assert.IsTrue(doc1.Document("array[1]").Has("foo"));
            Assert.IsFalse(doc1.Document("array[1]").Has("bar"));
        }
    }
}
