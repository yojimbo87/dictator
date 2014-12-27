using System;
using System.Collections.Generic;
using fastJSON;

namespace Dictator.ConsoleTests
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            //DictionaryTest();
            //ListTest();
            PrintTest();
            
            Console.WriteLine("\n\nEND");
            Console.ReadLine();
        }
        
        static void DictionaryTest()
        {
            var doc1 = Dictator.New()
                .Object("foo.bar.barz", null)
                .String("foo.bar.baz.shoe", "barified");
            
            var doc2 = Dictator.New()
                .String("foo.bar.baz.shoe", "barified222");
            
            doc1.String("foo.bar.buzz", "buzzified");
            
            Console.WriteLine(doc1);
            Console.WriteLine(JSON.ToNiceJSON(doc1, new JSONParameters()));
            
            Console.WriteLine(doc2);
            Console.WriteLine(JSON.ToNiceJSON(doc2, new JSONParameters()));
            
            var barz = doc1.Object("foo.bar.barz");
            Console.WriteLine(doc1.Object("foo.bar.barz"));
            Console.WriteLine(doc1.Object("foo.bar.baz"));
        }
        
        static void ListTest()
        {
            var doc1 = Dictator.New()
                .List("foo.bar", new List<int>() { 1, 2, 3 });
            
            var foo = doc1.List<int>("foo.bar");
            
            Console.WriteLine(JSON.ToNiceJSON(doc1, new JSONParameters()));
            Console.WriteLine(foo);
        }
        
        static void PrintTest()
        {
            var document = new Dictionary<string, object>()
                .String("foo", "foo string value")
                .Int("bar", 12345)
                .String("embedded.foo", "embedded foo string value");
            
            // document object would look in JSON representation as follows:
            // {
            //     "foo": "foo string value",
            //     "bar": 12345,
            //     "embedded": {
            //         "foo": "embedded foo string value"
            //      }
            // }
                
            if (document.IsString("foo") && document.IsInt("bar") && document.IsString("embedded.foo"))
            {
                Console.WriteLine("{0}, {1}, {2}", document.String("foo"), document.Int("bar"), document.String("embedded.foo"));
            }
        }
    }
}
