# Dictator

Dictator is a C# library which provides functionality for retrieving, storing and manipulating data inside `Dictionary<string, object>`. Dictionary that consists of string and object key-value pairs serves as the closest .NET low level representation of native JSON documents but for example lacks support for easy access to field data which might be either deeply nested or hard to retrieve without considerable amount of code. Aim of dictator is to help with common tasks that are used to work with JSON-like data structure representation in .NET. Below is a simple example which sets few fields, checks for their types and retrieves their values in specified format.

```
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
```

## Docs

- [Dictionary extension methods](#Dictionary-extension-methods)
  - [Standard field set and get operations](#Standard-field-set-and-get-operations)
  - [DateTime field set and get operations](#DateTime-field-set-and-get-operations)
  - [Enum field set and get operations](#Enum-field-set-and-get-operations)
  - [List field set and get operations](#List-field-set-and-get-operations)
  - [Nested field set and get operations](#Nested-field-set-and-get-operations)

## Dictionary extension methods

Dictator extends `Dictionary<string, object>` with a set of extension methods which are grouped into several operation categories depending on their functionality. These categories are for example include operations for getting, setting or checking fields and other methods which operates over the entire dictionary object.

### Standard field set and get operations

Field set and get operations consists of a list of methods which are used to store and retrieve data out of the dictionary in specified format or type. Types which are supported include `bool`, `byte`, `short`, `int`, `long`, `float`, `double`, `decimal`, `DateTime`, `string`, `object`, `Dictionary<string, object>` (aliased as document), `enum` and `List<T>`.

```
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
```

### DateTime field set and get operations

```
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
```

### Enum field set and get operations

```
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
```

### List field set and get operations

```
var document = new Dictionary<string, object>()
    .List("list1", new List<int>() { 1, 2, 3 });

var list1 = document.List<int>("list1");
var list1Size = document.ListSize("list1");
```

### Nested field set and get operations

```
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
```