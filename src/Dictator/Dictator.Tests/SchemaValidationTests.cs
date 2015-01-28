using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Dictator.Tests
{
    [TestFixture()]
    public class SchemaValidationTests
    {
        #region MustHave
        
        [Test()]
        public void Should_pass_MustHave_constraint()
        {
            var doc1 = new Dictionary<string, object>()
                .String("string1", "test1");
            
            var validationResult = Dictator.Schema
                .MustHave("string1")
                .Validate(doc1);
            
            Assert.IsTrue(validationResult.IsValid);
            Assert.AreEqual(0, validationResult.Violations.Count);
        }
        
        [Test()]
        public void Should_fail_MustHave_constraint()
        {
            var doc1 = new Dictionary<string, object>()
                .String("string1", "test1");
            
            var validationResult = Dictator.Schema
                .MustHave("nonExistingField")
                .Validate(doc1);
            
            Assert.IsFalse(validationResult.IsValid);
            Assert.AreEqual(1, validationResult.Violations.Count);
        }
        
        #endregion
        
        #region MustHave and NotNull
        
        [Test()]
        public void Should_pass_MustHave_NotNull_constraints()
        {
            var doc1 = new Dictionary<string, object>()
                .String("string1", "test1");
            
            var validationResult = Dictator.Schema
                .MustHave("string1").NotNull()
                .Validate(doc1);
            
            Assert.IsTrue(validationResult.IsValid);
            Assert.AreEqual(0, validationResult.Violations.Count);
        }
        
        [Test()]
        public void Should_fail_MustHave_NotNull_constraints()
        {
            var doc1 = new Dictionary<string, object>()
                .Object("string1", null);
            
            var validationResult = Dictator.Schema
                .MustHave("string1").NotNull()
                .Validate(doc1);
            
            Assert.IsFalse(validationResult.IsValid);
            Assert.AreEqual(1, validationResult.Violations.Count);
        }
        
        #endregion
    }
}
