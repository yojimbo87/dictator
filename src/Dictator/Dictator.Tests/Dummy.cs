using System.Collections.Generic;

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
    }
}
