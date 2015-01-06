using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Dictator.Tests
{
    [TestFixture()]
    public class CustomAttributeTests
    {
        [Test()]
        public void Should_serialize_generic_object_with_custom_attributes_to_document()
        {
            var dummy = new Dummy();
            dummy.Foo = "foo string value";
            dummy.PropertyToBeAliased = "string value";
            dummy.NullProperty = null;
            dummy.NotNullProperty = "some string";
            
            var doc = Dictator.ToDocument(dummy);
            
            // 9 because Dummy class consists of numerous other properties
            Assert.AreEqual(10, doc.Count);
            Assert.AreEqual("foo string value", doc.String("Foo"));
            Assert.AreEqual("string value", doc.String("myCustomField"));
            Assert.IsFalse(doc.Has("NullProperty"));
            Assert.AreEqual("some string", doc.String("NotNullProperty"));
            Assert.IsFalse(doc.Has("PropertyToBeAliased"));
        }
        
        [Test()]
        public void Should_deserialize_document_to_generic_object_custom_attributes()
        {
            var doc = new Dictionary<string, object>()
                .String("Foo", "foo string value")
                .String("myCustomField", "string value")
                .String("IngnoredPropety", "should not be there")
                .Object("NullProperty", null)
                .String("NotNullProperty", "some string");
            
            var dummy = doc.ToObject<Dummy>();
            
            Assert.AreEqual("foo string value", dummy.Foo);
            Assert.AreEqual("string value", dummy.PropertyToBeAliased);
            Assert.AreEqual("string value", dummy.PropertyToBeAliased);
            Assert.AreEqual(null, dummy.NullProperty);
            Assert.AreEqual("some string", dummy.NotNullProperty);
            Assert.AreEqual(null, dummy.IngnoredPropety);
        }
    }
}
