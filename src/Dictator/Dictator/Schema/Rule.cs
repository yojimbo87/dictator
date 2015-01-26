using System.Collections.Generic;

namespace Dictator
{
    public class Rule
    {
        public string FieldPath { get; set; }
        public Constraint Constraint { get; set; }
        public List<object> Parameters { get; set; }
        public bool IsViolated { get; set; }
        public string Message { get; set; }
        
        public Rule()
        {
            Parameters = new List<object>();
            Message = string.Format("Field '{0}' violated '{1}' constraint rule.", FieldPath, Constraint);
        }
    }
}
