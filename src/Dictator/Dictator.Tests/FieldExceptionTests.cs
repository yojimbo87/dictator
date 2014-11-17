using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Dictator.Tests
{
    [TestFixture()]
    public class FieldExceptionTests
    {
        [Test()]
        public void Should_throw_non_existing_field_exception()
        {
            var doc1 = Dictator.New();
            
            Assert.Throws<NonExistingFieldException>(() => {
                var string1 = doc1.String("string1");
            });
        }
        
        [Test()]
        public void Should_throw_invalid_field_type_exception()
        {
            var doc1 = Dictator.New()
                .String("string1", "test1");
            
            Assert.Throws<InvalidFieldTypeException>(() => {
                var int1 = doc1.Int("string1");
            });
        }
        
        [Test()]
        public void Should_throw_invalid_field_exception()
        {
            var doc1 = Dictator.New()
                .String("foo.string1", "test1")
                .String("foo", "test2");
            
            Assert.Throws<InvalidFieldException>(() => {
                var string1 = doc1.String("foo.string1");
            });
        }
    }
}
