using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Dictator.Tests
{
    [TestFixture()]
    public class SchemaValidationTests
    {
        [Test()]
        public void Should_pass_mixed_constraints()
        {
            var doc1 = new Dictionary<string, object>()
                .String("string1", "test1")
                .Int("int1", 123)
                .String("string2", "test2");
            
            var validationResult = Dictator.Schema
                .MustHave("string1").Type<string>().Size(5)
                .MustHave("int1").Type<int>().Range(100, 150)
                .ShouldHave("string2").Type<string>().Min(3)
                .Validate(doc1);
            
            Assert.IsTrue(validationResult.IsValid);
            Assert.AreEqual(0, validationResult.Violations.Count);
        }
        
        [Test()]
        public void Should_fail_mixed_constraints()
        {
            var doc1 = new Dictionary<string, object>()
                .String("string1", "test1")
                .Int("int1", 123)
                .String("string2", "test2");
            
            var validationResult = Dictator.Schema
                .MustHave("string1").Type<string>().Size(10)
                .MustHave("int1").Type<int>().Range(150, 200)
                .ShouldHave("string2").Type<string>().Min(10)
                .Validate(doc1);
            
            Assert.IsFalse(validationResult.IsValid);
            Assert.AreEqual(3, validationResult.Violations.Count);
            
            Assert.IsTrue(validationResult.Violations[0].IsViolated);
            Assert.AreEqual("string1", validationResult.Violations[0].FieldPath);
            Assert.AreEqual(Constraint.Size, validationResult.Violations[0].Constraint);
            
            Assert.IsTrue(validationResult.Violations[1].IsViolated);
            Assert.AreEqual("int1", validationResult.Violations[1].FieldPath);
            Assert.AreEqual(Constraint.Range, validationResult.Violations[1].Constraint);
            
            Assert.IsTrue(validationResult.Violations[2].IsViolated);
            Assert.AreEqual("string2", validationResult.Violations[2].FieldPath);
            Assert.AreEqual(Constraint.Min, validationResult.Violations[2].Constraint);
        }
        
        [Test()]
        public void Should_fail_with_custom_messages()
        {
            var doc1 = new Dictionary<string, object>()
                .String("string1", "test1")
                .Int("int1", 123);
            
            var validationResult = Dictator.Schema
                .MustHave("string1").Type<string>().Size(10).Message("Wrong size.")
                .ShouldHave("int1").Type<int>().Range(150, 200).Message("Wrong range.")
                .Validate(doc1);
            
            Assert.IsFalse(validationResult.IsValid);
            Assert.AreEqual(2, validationResult.Violations.Count);
            
            Assert.IsTrue(validationResult.Violations[0].IsViolated);
            Assert.AreEqual("string1", validationResult.Violations[0].FieldPath);
            Assert.AreEqual(Constraint.Size, validationResult.Violations[0].Constraint);
            Assert.AreEqual("Wrong size.", validationResult.Violations[0].Message);
            
            Assert.IsTrue(validationResult.Violations[1].IsViolated);
            Assert.AreEqual("int1", validationResult.Violations[1].FieldPath);
            Assert.AreEqual(Constraint.Range, validationResult.Violations[1].Constraint);
            Assert.AreEqual("Wrong range.", validationResult.Violations[1].Message);
        }
    }
}
