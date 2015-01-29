using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Dictator.Tests
{
    [TestFixture()]
    public class SchemaShouldHaveValidationTests
    {
        #region ShouldHave
        
        [Test()]
        public void Should_pass_existing_ShouldHave_constraint()
        {
            var doc1 = new Dictionary<string, object>()
                .String("string1", "test1");
            
            var validationResult = Dictator.Schema
                .ShouldHave("string1")
                .Validate(doc1);
            
            Assert.IsTrue(validationResult.IsValid);
            Assert.AreEqual(0, validationResult.Violations.Count);
        }
        
        [Test()]
        public void Should_pass_nonexisting_ShouldHave_constraint()
        {
            var doc1 = new Dictionary<string, object>()
                .String("string1", "test1");
            
            var validationResult = Dictator.Schema
                .ShouldHave("nonExistingField")
                .Validate(doc1);
            
            Assert.IsTrue(validationResult.IsValid);
            Assert.AreEqual(0, validationResult.Violations.Count);
        }
        
        #endregion
        
        #region ShouldHave and NotNull
        
        [Test()]
        public void Should_pass_ShouldHave_NotNull_constraints()
        {
            var doc1 = new Dictionary<string, object>()
                .String("string1", "test1");
            
            var validationResult = Dictator.Schema
                .ShouldHave("string1").NotNull()
                .Validate(doc1);
            
            Assert.IsTrue(validationResult.IsValid);
            Assert.AreEqual(0, validationResult.Violations.Count);
        }
        
        [Test()]
        public void Should_fail_ShouldHave_NotNull_constraints()
        {
            var doc1 = new Dictionary<string, object>()
                .Object("string1", null);
            
            var validationResult = Dictator.Schema
                .ShouldHave("string1").NotNull()
                .Validate(doc1);
            
            Assert.IsFalse(validationResult.IsValid);
            Assert.AreEqual(1, validationResult.Violations.Count);
            
            Assert.IsTrue(validationResult.Violations[0].IsViolated);
            Assert.AreEqual("string1", validationResult.Violations[0].FieldPath);
            Assert.AreEqual(Constraint.NotNull, validationResult.Violations[0].Constraint);
        }
        
        #endregion
        
        #region ShouldHave and Type
        
        [Test()]
        public void Should_pass_ShouldHave_Type_constraints()
        {
            var doc1 = new Dictionary<string, object>()
                .String("string1", "test1");
            
            var validationResult = Dictator.Schema
                .ShouldHave("string1").Type<string>()
                .Validate(doc1);
            
            Assert.IsTrue(validationResult.IsValid);
            Assert.AreEqual(0, validationResult.Violations.Count);
        }
        
        [Test()]
        public void Should_fail_ShouldHave_Type_constraints()
        {
            var doc1 = new Dictionary<string, object>()
                .String("string1", "test1");
            
            var validationResult = Dictator.Schema
                .ShouldHave("string1").Type<int>()
                .Validate(doc1);
            
            Assert.IsFalse(validationResult.IsValid);
            Assert.AreEqual(1, validationResult.Violations.Count);
            
            Assert.IsTrue(validationResult.Violations[0].IsViolated);
            Assert.AreEqual("string1", validationResult.Violations[0].FieldPath);
            Assert.AreEqual(Constraint.Type, validationResult.Violations[0].Constraint);
        }
        
        #endregion
        
        #region ShouldHave and Min
        
        [Test()]
        public void Should_pass_ShouldHave_Min_int_constraints()
        {
            var doc1 = new Dictionary<string, object>()
                .Int("int1", 123);
            
            var validationResult = Dictator.Schema
                .ShouldHave("int1").Min(120)
                .Validate(doc1);
            
            Assert.IsTrue(validationResult.IsValid);
            Assert.AreEqual(0, validationResult.Violations.Count);
        }
        
        [Test()]
        public void Should_fail_ShouldHave_Min_int_constraints()
        {
            var doc1 = new Dictionary<string, object>()
                .Int("int1", 123);
            
            var validationResult = Dictator.Schema
                .ShouldHave("int1").Min(125)
                .Validate(doc1);
            
            Assert.IsFalse(validationResult.IsValid);
            Assert.AreEqual(1, validationResult.Violations.Count);
            
            Assert.IsTrue(validationResult.Violations[0].IsViolated);
            Assert.AreEqual("int1", validationResult.Violations[0].FieldPath);
            Assert.AreEqual(Constraint.Min, validationResult.Violations[0].Constraint);
        }
        
        [Test()]
        public void Should_pass_ShouldHave_Min_string_constraints()
        {
            var doc1 = new Dictionary<string, object>()
                .String("string1", "test1");
            
            var validationResult = Dictator.Schema
                .ShouldHave("string1").Min(3)
                .Validate(doc1);
            
            Assert.IsTrue(validationResult.IsValid);
            Assert.AreEqual(0, validationResult.Violations.Count);
        }
        
        [Test()]
        public void Should_fail_ShouldHave_Min_string_constraints()
        {
            var doc1 = new Dictionary<string, object>()
                .String("string1", "test1");
            
            var validationResult = Dictator.Schema
                .ShouldHave("string1").Min(10)
                .Validate(doc1);
            
            Assert.IsFalse(validationResult.IsValid);
            Assert.AreEqual(1, validationResult.Violations.Count);
            
            Assert.IsTrue(validationResult.Violations[0].IsViolated);
            Assert.AreEqual("string1", validationResult.Violations[0].FieldPath);
            Assert.AreEqual(Constraint.Min, validationResult.Violations[0].Constraint);
        }
        
        [Test()]
        public void Should_pass_ShouldHave_Min_list_and_array_constraints()
        {
            var doc1 = new Dictionary<string, object>()
                .List("list1", new List<int> { 1, 2, 3 })
                .Array("array1", new [] { 4, 5, 6 });
            
            var validationResult = Dictator.Schema
                .ShouldHave("list1").Min(2)
                .ShouldHave("array1").Min(2)
                .Validate(doc1);
            
            Assert.IsTrue(validationResult.IsValid);
            Assert.AreEqual(0, validationResult.Violations.Count);
        }
        
        [Test()]
        public void Should_fail_ShouldHave_Min_list_and_array_constraints()
        {
            var doc1 = new Dictionary<string, object>()
                .List("list1", new List<int> { 1, 2, 3 })
                .Array("array1", new [] { 4, 5, 6 });
            
            var validationResult = Dictator.Schema
                .ShouldHave("list1").Min(5)
                .ShouldHave("array1").Min(5)
                .Validate(doc1);
            
            Assert.IsFalse(validationResult.IsValid);
            Assert.AreEqual(2, validationResult.Violations.Count);
            
            Assert.IsTrue(validationResult.Violations[0].IsViolated);
            Assert.AreEqual("list1", validationResult.Violations[0].FieldPath);
            Assert.AreEqual(Constraint.Min, validationResult.Violations[0].Constraint);
            
            Assert.IsTrue(validationResult.Violations[1].IsViolated);
            Assert.AreEqual("array1", validationResult.Violations[1].FieldPath);
            Assert.AreEqual(Constraint.Min, validationResult.Violations[1].Constraint);
        }
        
        #endregion
        
        #region ShouldHave and Max
        
        [Test()]
        public void Should_pass_ShouldHave_Max_int_constraints()
        {
            var doc1 = new Dictionary<string, object>()
                .Int("int1", 123);
            
            var validationResult = Dictator.Schema
                .ShouldHave("int1").Max(150)
                .Validate(doc1);
            
            Assert.IsTrue(validationResult.IsValid);
            Assert.AreEqual(0, validationResult.Violations.Count);
        }
        
        [Test()]
        public void Should_fail_ShouldHave_Max_int_constraints()
        {
            var doc1 = new Dictionary<string, object>()
                .Int("int1", 123);
            
            var validationResult = Dictator.Schema
                .ShouldHave("int1").Max(122)
                .Validate(doc1);
            
            Assert.IsFalse(validationResult.IsValid);
            Assert.AreEqual(1, validationResult.Violations.Count);
            
            Assert.IsTrue(validationResult.Violations[0].IsViolated);
            Assert.AreEqual("int1", validationResult.Violations[0].FieldPath);
            Assert.AreEqual(Constraint.Max, validationResult.Violations[0].Constraint);
        }
        
        [Test()]
        public void Should_pass_ShouldHave_Max_string_constraints()
        {
            var doc1 = new Dictionary<string, object>()
                .String("string1", "test1");
            
            var validationResult = Dictator.Schema
                .ShouldHave("string1").Max(10)
                .Validate(doc1);
            
            Assert.IsTrue(validationResult.IsValid);
            Assert.AreEqual(0, validationResult.Violations.Count);
        }
        
        [Test()]
        public void Should_fail_ShouldHave_Max_string_constraints()
        {
            var doc1 = new Dictionary<string, object>()
                .String("string1", "test1");
            
            var validationResult = Dictator.Schema
                .ShouldHave("string1").Max(3)
                .Validate(doc1);
            
            Assert.IsFalse(validationResult.IsValid);
            Assert.AreEqual(1, validationResult.Violations.Count);
            
            Assert.IsTrue(validationResult.Violations[0].IsViolated);
            Assert.AreEqual("string1", validationResult.Violations[0].FieldPath);
            Assert.AreEqual(Constraint.Max, validationResult.Violations[0].Constraint);
        }
        
        [Test()]
        public void Should_pass_ShouldHave_Max_list_and_array_constraints()
        {
            var doc1 = new Dictionary<string, object>()
                .List("list1", new List<int> { 1, 2, 3 })
                .Array("array1", new [] { 4, 5, 6 });
            
            var validationResult = Dictator.Schema
                .ShouldHave("list1").Max(5)
                .ShouldHave("array1").Max(5)
                .Validate(doc1);
            
            Assert.IsTrue(validationResult.IsValid);
            Assert.AreEqual(0, validationResult.Violations.Count);
        }
        
        [Test()]
        public void Should_fail_ShouldHave_Max_list_and_array_constraints()
        {
            var doc1 = new Dictionary<string, object>()
                .List("list1", new List<int> { 1, 2, 3 })
                .Array("array1", new [] { 4, 5, 6 });
            
            var validationResult = Dictator.Schema
                .ShouldHave("list1").Max(1)
                .ShouldHave("array1").Max(1)
                .Validate(doc1);
            
            Assert.IsFalse(validationResult.IsValid);
            Assert.AreEqual(2, validationResult.Violations.Count);
            
            Assert.IsTrue(validationResult.Violations[0].IsViolated);
            Assert.AreEqual("list1", validationResult.Violations[0].FieldPath);
            Assert.AreEqual(Constraint.Max, validationResult.Violations[0].Constraint);
            
            Assert.IsTrue(validationResult.Violations[1].IsViolated);
            Assert.AreEqual("array1", validationResult.Violations[1].FieldPath);
            Assert.AreEqual(Constraint.Max, validationResult.Violations[1].Constraint);
        }
        
        #endregion
        
        #region MustHave and Range
        
        [Test()]
        public void Should_pass_ShouldHave_Range_int_constraints()
        {
            var doc1 = new Dictionary<string, object>()
                .Int("int1", 123);
            
            var validationResult = Dictator.Schema
                .ShouldHave("int1").Range(100, 150)
                .Validate(doc1);
            
            Assert.IsTrue(validationResult.IsValid);
            Assert.AreEqual(0, validationResult.Violations.Count);
        }
        
        [Test()]
        public void Should_fail_ShouldHave_Range_int_constraints()
        {
            var doc1 = new Dictionary<string, object>()
                .Int("int1", 123);
            
            var validationResult = Dictator.Schema
                .ShouldHave("int1").Range(150, 200)
                .Validate(doc1);
            
            Assert.IsFalse(validationResult.IsValid);
            Assert.AreEqual(1, validationResult.Violations.Count);
            
            Assert.IsTrue(validationResult.Violations[0].IsViolated);
            Assert.AreEqual("int1", validationResult.Violations[0].FieldPath);
            Assert.AreEqual(Constraint.Range, validationResult.Violations[0].Constraint);
        }
        
        [Test()]
        public void Should_pass_ShouldHave_Range_string_constraints()
        {
            var doc1 = new Dictionary<string, object>()
                .String("string1", "test1");
            
            var validationResult = Dictator.Schema
                .ShouldHave("string1").Range(5, 10)
                .Validate(doc1);
            
            Assert.IsTrue(validationResult.IsValid);
            Assert.AreEqual(0, validationResult.Violations.Count);
        }
        
        [Test()]
        public void Should_fail_ShouldHave_Range_string_constraints()
        {
            var doc1 = new Dictionary<string, object>()
                .String("string1", "test1");
            
            var validationResult = Dictator.Schema
                .ShouldHave("string1").Range(10, 15)
                .Validate(doc1);
            
            Assert.IsFalse(validationResult.IsValid);
            Assert.AreEqual(1, validationResult.Violations.Count);
            
            Assert.IsTrue(validationResult.Violations[0].IsViolated);
            Assert.AreEqual("string1", validationResult.Violations[0].FieldPath);
            Assert.AreEqual(Constraint.Range, validationResult.Violations[0].Constraint);
        }
        
        [Test()]
        public void Should_pass_ShouldHave_Range_list_and_array_constraints()
        {
            var doc1 = new Dictionary<string, object>()
                .List("list1", new List<int> { 1, 2, 3 })
                .Array("array1", new [] { 4, 5, 6 });
            
            var validationResult = Dictator.Schema
                .ShouldHave("list1").Range(2, 5)
                .ShouldHave("array1").Range(2, 5)
                .Validate(doc1);
            
            Assert.IsTrue(validationResult.IsValid);
            Assert.AreEqual(0, validationResult.Violations.Count);
        }
        
        [Test()]
        public void Should_fail_ShouldHave_Range_list_and_array_constraints()
        {
            var doc1 = new Dictionary<string, object>()
                .List("list1", new List<int> { 1, 2, 3 })
                .Array("array1", new [] { 4, 5, 6 });
            
            var validationResult = Dictator.Schema
                .ShouldHave("list1").Range(5, 10)
                .ShouldHave("array1").Range(5, 10)
                .Validate(doc1);
            
            Assert.IsFalse(validationResult.IsValid);
            Assert.AreEqual(2, validationResult.Violations.Count);
            
            Assert.IsTrue(validationResult.Violations[0].IsViolated);
            Assert.AreEqual("list1", validationResult.Violations[0].FieldPath);
            Assert.AreEqual(Constraint.Range, validationResult.Violations[0].Constraint);
            
            Assert.IsTrue(validationResult.Violations[1].IsViolated);
            Assert.AreEqual("array1", validationResult.Violations[1].FieldPath);
            Assert.AreEqual(Constraint.Range, validationResult.Violations[1].Constraint);
        }
        
        #endregion
        
        #region ShouldHave and Size
        
        [Test()]
        public void Should_pass_ShouldHave_Size_string_constraints()
        {
            var doc1 = new Dictionary<string, object>()
                .String("string1", "test1");
            
            var validationResult = Dictator.Schema
                .ShouldHave("string1").Size(5)
                .Validate(doc1);
            
            Assert.IsTrue(validationResult.IsValid);
            Assert.AreEqual(0, validationResult.Violations.Count);
        }
        
        [Test()]
        public void Should_fail_ShouldHave_Size_string_constraints()
        {
            var doc1 = new Dictionary<string, object>()
                .String("string1", "test1");
            
            var validationResult = Dictator.Schema
                .ShouldHave("string1").Size(10)
                .Validate(doc1);
            
            Assert.IsFalse(validationResult.IsValid);
            Assert.AreEqual(1, validationResult.Violations.Count);
            
            Assert.IsTrue(validationResult.Violations[0].IsViolated);
            Assert.AreEqual("string1", validationResult.Violations[0].FieldPath);
            Assert.AreEqual(Constraint.Size, validationResult.Violations[0].Constraint);
        }
        
        [Test()]
        public void Should_pass_ShouldHave_Size_list_and_array_constraints()
        {
            var doc1 = new Dictionary<string, object>()
                .List("list1", new List<int> { 1, 2, 3 })
                .Array("array1", new [] { 4, 5, 6 });
            
            var validationResult = Dictator.Schema
                .ShouldHave("list1").Size(3)
                .ShouldHave("array1").Size(3)
                .Validate(doc1);
            
            Assert.IsTrue(validationResult.IsValid);
            Assert.AreEqual(0, validationResult.Violations.Count);
        }
        
        [Test()]
        public void Should_fail_ShouldHave_Size_list_and_array_constraints()
        {
            var doc1 = new Dictionary<string, object>()
                .List("list1", new List<int> { 1, 2, 3 })
                .Array("array1", new [] { 4, 5, 6 });
            
            var validationResult = Dictator.Schema
                .ShouldHave("list1").Size(5)
                .ShouldHave("array1").Size(5)
                .Validate(doc1);
            
            Assert.IsFalse(validationResult.IsValid);
            Assert.AreEqual(2, validationResult.Violations.Count);
            
            Assert.IsTrue(validationResult.Violations[0].IsViolated);
            Assert.AreEqual("list1", validationResult.Violations[0].FieldPath);
            Assert.AreEqual(Constraint.Size, validationResult.Violations[0].Constraint);
            
            Assert.IsTrue(validationResult.Violations[1].IsViolated);
            Assert.AreEqual("array1", validationResult.Violations[1].FieldPath);
            Assert.AreEqual(Constraint.Size, validationResult.Violations[1].Constraint);
        }
        
        #endregion
        
        #region MustHave and Match
        
        [Test()]
        public void Should_pass_ShouldHave_Match_constraints()
        {
            var doc1 = new Dictionary<string, object>()
                .String("email", "my@email.com");
            
            var validationResult = Dictator.Schema
                .ShouldHave("email").Match(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", true)
                .Validate(doc1);
            
            Assert.IsTrue(validationResult.IsValid);
            Assert.AreEqual(0, validationResult.Violations.Count);
        }
        
        [Test()]
        public void Should_fail_ShouldHave_Match_constraints()
        {
            var doc1 = new Dictionary<string, object>()
                .String("email", "my@email.c@m");
            
            var validationResult = Dictator.Schema
                .ShouldHave("email").Match(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", true)
                .Validate(doc1);
            
            Assert.IsFalse(validationResult.IsValid);
            Assert.AreEqual(1, validationResult.Violations.Count);
            
            Assert.IsTrue(validationResult.Violations[0].IsViolated);
            Assert.AreEqual("email", validationResult.Violations[0].FieldPath);
            Assert.AreEqual(Constraint.Match, validationResult.Violations[0].Constraint);
        }
        
        #endregion
    }
}
