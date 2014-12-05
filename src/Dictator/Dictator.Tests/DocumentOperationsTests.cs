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
    }
}
