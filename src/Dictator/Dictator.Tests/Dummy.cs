using System.Collections.Generic;
using Dictator;

namespace Dictator.Tests
{
    public class Dummy
    {
        public string Foo { get; set; }
        public int Bar { get; set; }
        public List<string> FooList { get; set; }
        public Dummy Parent { get; set; }
        public List<Dummy> Children { get; set; }
        public Dictionary<string, object> Dictionary { get; set; }
        public List<Dictionary<string, object>> Dictionaries { get; set; }
        
        public Dictionary<string, string> PrimitiveDictionary { get; set; }
        
        public MyEnum MyEnum { get; set; }
        
        [AliasField("myCustomField")]
        public string PropertyToBeAliased { get; set; }
        
        [IgnoreNullValue]
        public string NullProperty { get; set; }
        
        [IgnoreNullValue]
        public string NotNullProperty { get; set; }
        
        [IgnoreField]
        public string IngnoredPropety { get; set; }
    }
    
    public enum MyEnum
    {
        Option1,
        Option2,
        Option3
    }
}
