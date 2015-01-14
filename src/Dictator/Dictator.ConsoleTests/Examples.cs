using System;
using System.Collections.Generic;
using fastJSON;
using Dictator;

namespace Dictator.ConsoleTests
{
    public static class Examples
    {
        public static void GeneralUsage()
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
                var foo = document.String("foo");
                var bar = document.Int("bar");
                var embeddedFoo = document.String("embedded.foo");
                
                Console.WriteLine("{0}, {1}, {2}", foo, bar, embeddedFoo);
            }
        }

        #region Set and get operations
        
        public static void BasicSetGetOperations()
        {
            var document = new Dictionary<string, object>()
                .String("foo", "string value")
                .Int("bar", 123)
                .Long("longBar", 12345)
                .Object("null", null);
            
            // standard get operations
            var str = document.String("foo");
            var number = document.Int("bar");
            var longNumber = document.Long("longBar");
            var nullObject = document.Object("null");
            
            // values are automatically converted if possible
            var strNumber = document.String("longBar");
            var intNumber = document.Int("longBar");
            
            // generic get operation
            var bar = document.Object<int>("bar");
            
            Console.WriteLine(bar);
        }
        
        public static void DateTimeSetGetOperations()
        {
            var document = new Dictionary<string, object>()
                // stored as native DateTime object
                .DateTime("dateTime1", DateTime.UtcNow)
                // stored in default yyyy-MM-ddTHH:mm:ss.fffZ string format
                .DateTime("dateTime2", DateTime.UtcNow, DateTimeFormat.String)
                // stored in unix timestamp format as long type
                .DateTime("dateTime3", DateTime.UtcNow, DateTimeFormat.UnixTimeStamp);
            
            // standard get operations
            var dateTime = document.DateTime("dateTime1");
            var dateTimeAsString = document.String("dateTime2");
            var dateTimeAsLong = document.Long("dateTime3");
            
            // automatic conversion
            var stringConvertedToDateTime = document.DateTime("dateTime2");
            var longConvertedToDateTime = document.DateTime("dateTime3");
            
            Console.WriteLine(dateTime.ToString());
            Console.WriteLine(dateTimeAsString);
            Console.WriteLine(dateTimeAsLong);
            Console.WriteLine(stringConvertedToDateTime.ToString());
            Console.WriteLine(longConvertedToDateTime.ToString());
        }
        
        public static void EnumSetGetOperations()
        {
            var document = new Dictionary<string, object>()
                // stored as native enum
                .Enum("enum1", DateTimeFormat.Object)
                // stored as integer
                .Enum("enum2", DateTimeFormat.Object, EnumFormat.Integer)
                // stored as string
                .Enum("enum3", DateTimeFormat.Object, EnumFormat.String);
            
            // standard get operation
            var enum1 = document.Enum<DateTimeFormat>("enum1");
            // retrieve enum from integer value
            var enum2 = document.Enum<DateTimeFormat>("enum2");
            // retrieve enum from string value
            var enum3 = document.Enum<DateTimeFormat>("enum3");
            
            Console.WriteLine(enum1.ToString());
            Console.WriteLine(enum2.ToString());
            Console.WriteLine(enum3.ToString());
        }
        
        public static void ListSetGetOperations()
        {
            var document = new Dictionary<string, object>()
                .List("list1", new List<int>() { 1, 2, 3 });
            
            var list1 = document.List<int>("list1");
            var list1Size = document.Size("list1");
            var item2 = document.Int("list1[1]");
            
            // change value of item at specified index
            document.Int("list1[1]", 222);
            
            // append new value to the list
            document.Int("list1[*]", 4);
            
            document.List<int>("list1").ForEach(x => Console.WriteLine(x));
            
            Console.WriteLine("Size: " + document.Size("list1"));
            Console.WriteLine("Item 2: " + document.Int("list1[1]"));
        }
        
        public static void ArraySetGetOperations()
        {
            var document = new Dictionary<string, object>()
                .Array("array1", new int[] { 1, 2, 3 });
            
            var array1 = document.Array<int>("array1");
            var array1Size = document.Size("array1");
            var item2 = document.Int("array1[1]");
            
            // change value of item at specified index
            document.Int("array1[1]", 222);
            
            for (int i = 0; i < document.Size("array1"); i++)
            {
                Console.WriteLine(document.Array<int>("array1")[i]);
            }
            
            Console.WriteLine("Size: " + document.Size("array1"));
            Console.WriteLine("Item 2: " + document.Int("array1[1]"));
        }
        
        public static void NestedSetGetOperations()
        {
            var document = new Dictionary<string, object>()
                .String("string1", "string value 1")
                .String("foo.string2", "string value 2")
                .String("foo.bar.string3", "string value 3");
            
            // JSON equivalent:
            // {
            //     "string1": "string value 1",
            //     "foo": {
            //         "string2": "string value 2",
            //         "bar": {
            //             "string3": "string value 3"
            //         }
            //     }
            // }
            
            var string1 = document.String("string1");
            var string2 = document.String("foo.string2");
            var string3 = document.String("foo.bar.string3");
            
            Console.WriteLine(string1);
            Console.WriteLine(string2);
            Console.WriteLine(string3);
        }
        
        #endregion
        
        #region Check operations
        
        public static void FieldExistenceCheckOperations()
        {
            var document = new Dictionary<string, object>()
                .String("foo", "foo string value")
                .Object("bar", null);
            
            // true
            var stringFieldExists = document.Has("foo");
            // true
            var nullFieldExists = document.Has("bar");
            // false
            var fieldExists = document.Has("nonExistingField");
            
            Console.WriteLine(stringFieldExists);
            Console.WriteLine(nullFieldExists);
            Console.WriteLine(fieldExists);
        }
        
        public static void ExactTypeCheckOperations()
        {
            var document = new Dictionary<string, object>()
                .String("foo", "foo string value")
                .Int("bar", 12345)
                .Bool("embedded.foo", true);
            
            // true
            var isString = document.IsString("foo");
            // true
            var isInt = document.IsInt("bar");
            // true
            var isBool = document.IsBool("embedded.foo");
            // false
            var isLong = document.IsLong("bar");
            // false
            var isDateTime = document.IsDateTime("nonExistingField");
            
            Console.WriteLine(isString);
            Console.WriteLine(isInt);
            Console.WriteLine(isBool);
            Console.WriteLine(isLong);
            Console.WriteLine(isDateTime);
        }
        
        public static void NullTypeCheckOperations()
        {
            var document = new Dictionary<string, object>()
                .Object("foo", null)
                .Int("bar", 12345);
            
            // true
            var isNull = document.IsNull("foo");
            // true
            var isNotNull = document.IsNotNull("bar");
            // false
            var isNull2 = document.IsNull("nonExistingField");
            // false
            var isNotNull2 = document.IsNotNull("nonExistingField");
            
            Console.WriteLine(isNull);
            Console.WriteLine(isNotNull);
            Console.WriteLine(isNull2);
            Console.WriteLine(isNotNull2);
        }
        
        public static void DateTimeTypeCheckOperations()
        {
            var document = new Dictionary<string, object>()
                .DateTime("dateTime1", DateTime.UtcNow)
                .DateTime("dateTime2", DateTime.UtcNow, DateTimeFormat.String)
                .DateTime("dateTime3", DateTime.UtcNow, DateTimeFormat.UnixTimeStamp);
            
            // true
            var isDateTime1 = document.IsDateTime("dateTime1");
            // true
            var isDateTime2 = document.IsDateTime("dateTime2", DateTimeFormat.String);
            // true
            var isDateTime3 = document.IsDateTime("dateTime3", DateTimeFormat.UnixTimeStamp);
            
            Console.WriteLine(isDateTime1);
            Console.WriteLine(isDateTime2);
            Console.WriteLine(isDateTime3);
        }
        
        public static void EnumTypeCheckOperations()
        {
            var document = new Dictionary<string, object>()
                .Enum("enum1", DateTimeFormat.Object)
                .Int("enum2", 0)
                .String("enum3", "object");
            
            // true
            var isEnum1 = document.IsEnum("enum1");
            // true
            var isEnum2 = document.IsEnum<DateTimeFormat>("enum2");
            // true
            var isEnum3 = document.IsEnum<DateTimeFormat>("enum3");
            // false
            var isEnum4 = document.IsEnum("enum2");
            // false
            var isEnum5 = document.IsEnum("enum3");
            
            Console.WriteLine(isEnum1);
            Console.WriteLine(isEnum2);
            Console.WriteLine(isEnum3);
            Console.WriteLine(isEnum4);
            Console.WriteLine(isEnum5);
        }
        
        public static void GenericTypeCheckOperations()
        {
            var document = new Dictionary<string, object>()
                .String("foo", "foo string value")
                .Int("bar", 12345);
            
            // true
            var isString = document.IsType<string>("foo");
            // true
            var isInt = document.IsType("bar", typeof(int));
            // false
            var isBool = document.IsType<bool>("nonExistingField");
            
            Console.WriteLine(isString);
            Console.WriteLine(isInt);
            Console.WriteLine(isBool);
        }
        
        public static void FieldValueEqualityCheckOperations()
        {
            var document = new Dictionary<string, object>()
                .String("foo", "foo string value")
                .Int("bar", 12345);
            
            // true
            var isEqual1 = document.IsEqual("foo", "foo string value");
            // true
            var isEqual2 = document.IsEqual("bar", 12345);
            // false
            var isEqual3 = document.IsEqual("nonExistingField", "some string value");
            
            Console.WriteLine(isEqual1);
            Console.WriteLine(isEqual2);
            Console.WriteLine(isEqual3);
        }
        
        #endregion
        
        public static void ListAndArrayIteration()
        {
            var document = new Dictionary<string, object>()
                .Array("array", new [] { 1, 2, 3 })
                .List("list", new List<string> { "one", "two", "three" });
            
            document.Each<int>("array", (index, item) => {
                var itemIndex = index;
                var itemValue = item;
            });
            
            document.Each<string>("list", (index, item) => {
                var itemIndex = index;
                var itemValue = item;
            });
            
            document.Each<int>("array", (index, item) => {
                Console.WriteLine("{0}: {1}", index, item);
            });
            
            document.Each<string>("list", (index, item) => {
                Console.WriteLine("{0}: {1}", index, item);
            });
        }
        
        public static void DeletingFields()
        {
            var document = new Dictionary<string, object>()
                .String("foo", "string value")
                .Int("bar", 12345)
                .String("baz.foo", "other string value");
            
            document.Drop("foo", "bar");
            
            // false
            var hasFoo = document.Has("foo");
            // false
            var hasBar = document.Has("bar");
            
            Console.WriteLine(hasFoo);
            Console.WriteLine(hasBar);
        }
        
        public static void CloningDocuments()
        {
            var document = new Dictionary<string, object>()
                .String("foo", "string value")
                .Int("bar", 12345)
                .String("baz.foo", "other string value");
            
            // creates deep clone of the document
            var clone1 = document.Clone();
            
            // creates deep clone with only specified fields
            var clone2 = document.CloneOnly("foo", "bar");
            
            // creates deep clone without specified fields
            var clone3 = document.CloneExcept("baz.foo");
            
            Console.WriteLine(clone2.Has("baz.foo"));
            Console.WriteLine(clone3.Has("baz.foo"));
        }
        
        public static void MergingDocuments()
        {
            var document1 = new Dictionary<string, object>()
                .String("foo", "string value")
                .Int("bar", 12345);
            
            var document2 = Dictator.New()
                .String("foo", "new string value")
                .Int("baz", 54321);
            
            // merges document2 into document1 and overwrites conflicting fields
            document1.Merge(document2);
            
            // merges document2 into document1 and keeps conflicting fields
            document1.Merge(document2, MergeBehavior.KeepFields);
        }
        
        public static void ConvertDocumentToStronglyTypedObject()
        {
            var document = new Dictionary<string, object>()
                .String("Foo", "string value")
                .Int("Bar", 12345);
            
            var dummy = document.ToObject<Dummy>();
            
            var foo = dummy.Foo;
            var bar = dummy.Bar;
            
            Console.WriteLine(foo);
            Console.WriteLine(bar);
        }
        
        #region Dictator static methods
        
        public static void ConvertDocumentListToGenericList()
        {
            var documents = new List<Dictionary<string, object>>
            {
                new Dictionary<string, object>()
                    .String("Foo", "string value one")
                    .Int("Bar", 1),
                new Dictionary<string, object>()
                    .String("Foo", "string value two")
                    .Int("Bar", 2),
                new Dictionary<string, object>()
                    .String("Foo", "string value three")
                    .Int("Bar", 3)
            };
            
            var dummies = Dictator.ToList<Dummy>(documents);
            
            foreach (var dummy in dummies)
            {
                var foo = dummy.Foo;
                var bar = dummy.Bar;
            }
            
            dummies.ForEach(d => Console.WriteLine(d.Foo + " - " + d.Bar));
        }
        
        public static void ConvertStronglyTypedObjectToDocument()
        {
            var dummy = new Dummy();
            dummy.Foo = "string value";
            dummy.Bar = 12345;
            
            var document = Dictator.ToDocument(dummy);
            
            var foo = document.String("Foo");
            var bar = document.Int("Bar");
            
            Console.WriteLine(foo);
            Console.WriteLine(bar);
        }
        
        public static void ConvertGenericListToDocumentList()
        {
            var dummies = new List<Dummy>
            {
                new Dummy { Foo = "string value one", Bar = 1 },
                new Dummy { Foo = "string value two", Bar = 2 },
                new Dummy { Foo = "string value three", Bar = 3 }
            };
            
            var documents = Dictator.ToDocuments(dummies);
            
            foreach (var document in documents)
            {
                var foo = document.String("Foo");
                var bar = document.Int("Bar");
            }
            
            documents.ForEach(d => Console.WriteLine(d.String("Foo") + " - " + d.Int("Bar")));
        }
        
        #endregion
    }
}
