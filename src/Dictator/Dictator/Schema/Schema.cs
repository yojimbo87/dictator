using System;
using System.Collections.Generic;
using System.Linq;

namespace Dictator
{
    public class Schema
    {
        string _lastAddedFieldPath = "";
        Constraint _lastAddedConstraint;
        List<Rule> _rules = new List<Rule>();
        
        #region Field existence constraints
        
        public Schema MustHave(string fieldPath)
        {
            _lastAddedConstraint = Constraint.MustHave;
            _lastAddedFieldPath = fieldPath;
            
            var rule = _rules.FirstOrDefault(r => r.FieldPath == _lastAddedFieldPath && r.Constraint == _lastAddedConstraint);
            
            if (rule == null)
            {
                rule = new Rule();
                rule.FieldPath = _lastAddedFieldPath;
                rule.Constraint = _lastAddedConstraint;
                
                _rules.Add(rule);
            }
            
            return this;
        }
        
        public Schema ShouldHave(string fieldPath)
        {
            _lastAddedConstraint = Constraint.ShouldHave;
            _lastAddedFieldPath = fieldPath;
            
            var rule = _rules.FirstOrDefault(r => r.FieldPath == _lastAddedFieldPath && r.Constraint == _lastAddedConstraint);
            
            if (rule == null)
            {
                rule = new Rule();
                rule.FieldPath = _lastAddedFieldPath;
                rule.Constraint = _lastAddedConstraint;
                
                _rules.Add(rule);
            }
            
            return this;
        }
        
        #endregion
        
        #region Field value constraints
        
        public Schema NotNull()
        {
            _lastAddedConstraint = Constraint.NotNull;
            
            var rule = _rules.FirstOrDefault(r => r.FieldPath == _lastAddedFieldPath && r.Constraint == _lastAddedConstraint);
            
            if (rule == null)
            {
                rule = new Rule();
                rule.FieldPath = _lastAddedFieldPath;
                rule.Constraint = _lastAddedConstraint;
                
                _rules.Add(rule);
            }
            
            return this;
        }
        
        public Schema Type<T>()
        {
            return Type(typeof(T));
        }
        
        public Schema Type(Type type)
        {
            _lastAddedConstraint = Constraint.Type;
            
            var rule = _rules.FirstOrDefault(r => r.FieldPath == _lastAddedFieldPath && r.Constraint == _lastAddedConstraint);
            
            if (rule == null)
            {
                rule = new Rule();
                rule.FieldPath = _lastAddedFieldPath;
                rule.Constraint = _lastAddedConstraint;
                rule.Parameters.Add(type);
                
                _rules.Add(rule);
            }
            else
            {
                rule.Parameters.Clear();
                rule.Parameters.Add(type);
            }
            
            return this;
        }
        
        public Schema Min(int minValue)
        {
            _lastAddedConstraint = Constraint.Min;
            
            var rule = _rules.FirstOrDefault(r => r.FieldPath == _lastAddedFieldPath && r.Constraint == _lastAddedConstraint);
            
            if (rule == null)
            {
                rule = new Rule();
                rule.FieldPath = _lastAddedFieldPath;
                rule.Constraint = _lastAddedConstraint;
                rule.Parameters.Add(minValue);
                
                _rules.Add(rule);
            }
            else
            {
                rule.Parameters.Clear();
                rule.Parameters.Add(minValue);
            }
            
            return this;
        }
        
        public Schema Max(int maxValue)
        {
            _lastAddedConstraint = Constraint.Max;
            
            var rule = _rules.FirstOrDefault(r => r.FieldPath == _lastAddedFieldPath && r.Constraint == _lastAddedConstraint);
            
            if (rule == null)
            {
                rule = new Rule();
                rule.FieldPath = _lastAddedFieldPath;
                rule.Constraint = _lastAddedConstraint;
                rule.Parameters.Add(maxValue);
                
                _rules.Add(rule);
            }
            else
            {
                rule.Parameters.Clear();
                rule.Parameters.Add(maxValue);
            }
            
            return this;
        }
        
        public Schema Range(int minValue, int maxValue)
        {
            _lastAddedConstraint = Constraint.Range;
            
            var rule = _rules.FirstOrDefault(r => r.FieldPath == _lastAddedFieldPath && r.Constraint == _lastAddedConstraint);
            
            if (rule == null)
            {
                rule = new Rule();
                rule.FieldPath = _lastAddedFieldPath;
                rule.Constraint = _lastAddedConstraint;
                rule.Parameters.Add(minValue);
                rule.Parameters.Add(maxValue);
                
                _rules.Add(rule);
            }
            else
            {
                rule.Parameters.Clear();
                rule.Parameters.Add(minValue);
                rule.Parameters.Add(maxValue);
            }
            
            return this;
        }
        
        public Schema Size(int collectionSize)
        {
            _lastAddedConstraint = Constraint.Size;
            
            var rule = _rules.FirstOrDefault(r => r.FieldPath == _lastAddedFieldPath && r.Constraint == _lastAddedConstraint);
            
            if (rule == null)
            {
                rule = new Rule();
                rule.FieldPath = _lastAddedFieldPath;
                rule.Constraint = _lastAddedConstraint;
                rule.Parameters.Add(collectionSize);
                
                _rules.Add(rule);
            }
            else
            {
                rule.Parameters.Clear();
                rule.Parameters.Add(collectionSize);
            }
            
            return this;
        }
        
        public Schema Match(string regex)
        {
            _lastAddedConstraint = Constraint.Match;
            
            var rule = _rules.FirstOrDefault(r => r.FieldPath == _lastAddedFieldPath && r.Constraint == _lastAddedConstraint);
            
            if (rule == null)
            {
                rule = new Rule();
                rule.FieldPath = _lastAddedFieldPath;
                rule.Constraint = _lastAddedConstraint;
                rule.Parameters.Add(regex);
                
                _rules.Add(rule);
            }
            else
            {
                rule.Parameters.Clear();
                rule.Parameters.Add(regex);
            }
            
            return this;
        }
        
        public Schema Message(string errorMessage)
        {
            var rule = _rules.FirstOrDefault(r => r.FieldPath == _lastAddedFieldPath && r.Constraint == _lastAddedConstraint);
            
            if (rule != null)
            {
                rule.Message = errorMessage;
            }
            
            return this;
        }
        
        #endregion
        
        public ValidationResult Analyze(Dictionary<string, object> document)
        {
            var validationResult = new ValidationResult();
            var fieldRules = _rules.Where(rule => rule.Constraint == Constraint.MustHave || rule.Constraint == Constraint.ShouldHave).ToList();
            
            foreach (var fieldRule in fieldRules)
            {
                switch (fieldRule.Constraint)
                {
                    case Constraint.MustHave:
                        if (document.Has(fieldRule.FieldPath))
                        {
                            var fieldValueRules = _rules.Where(rule => rule.FieldPath == fieldRule.FieldPath && rule.Constraint != Constraint.MustHave).ToList();
                            
                            validationResult.AddViolations(ValidateFieldValueRules(fieldValueRules));
                        }
                        else
                        {
                            validationResult.AddViolation(fieldRule);
                        }
                        break;
                    case Constraint.ShouldHave:
                        if (document.Has(fieldRule.FieldPath))
                        {
                            var fieldValueRules = _rules.Where(rule => rule.FieldPath == fieldRule.FieldPath && rule.Constraint != Constraint.ShouldHave).ToList();
                            
                            validationResult.AddViolations(ValidateFieldValueRules(fieldValueRules));
                        }
                        break;
                    default:
                        break;
                }
            }
            
            return validationResult;
        }
        
        List<Rule> ValidateFieldValueRules(List<Rule> fieldValueRules)
        {
            var ruleViolations = new List<Rule>();
            
            foreach (var fieldValueRule in fieldValueRules)
            {
                switch (fieldValueRule.Constraint)
                {
                    case Constraint.NotNull:
                        // TODO:
                        break;
                    case Constraint.Type:
                        
                        break;
                    default:
                        break;
                }
            }
            
            return ruleViolations;
        }
    }
}
