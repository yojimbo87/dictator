using System;
using System.Collections.Generic;

namespace Dictator
{
    public static class Dictator
    {
        public static DictatorSettings Settings { get; private set; }
        
        public static Dictionary<string, object> New()
        {
            return new Dictionary<string, object>();
        }
        
        static Dictator()
        {
            Settings = new DictatorSettings();
        }
        
        // TODO: static method for converting List<Dictionary<string, object>> to strongly typed list
        
        // TODO: static method for converting strongly typed object to Dictionary<string, object>
        
        // TODO: static method for converting list of strongly typed objects to List<Dictionary<string, object>>
    }
}

