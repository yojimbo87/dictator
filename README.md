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

## Docs contents

- [Dictionary extension methods](#dictionary-extension-methods)
  - [Field set and get operations](#field-set-and-get-operations)
    - [Basic types](#basic-types)
    - [DateTime type](#datetime-type)
    - [Enum type](#enum-type)
    - [List of objects](#list-of-objects)
    - [Nested fields](#nested-fields)
  - [Field check operations](#field-check-operations)
    - [Field existence check](#field-existence-check)
    - [Exact type check](#exact-type-check)
    - [Null check](#null-check)
    - [DateTime type check](#datetime-type-check)
    - [Enum type check](#enum-type-check)
    - [Generic type check](#generic-type-check)
    - [Field value equality check](#field-value-equality-check)
  - [Deleting fields](#deleting-fields)
  - [Cloning documents](#cloning-documents)
  - [Merging documents](#merging-documents)
  - [Convert document to strongly typed object](#convert-document-to-strongly-typed-object)

## Dictionary extension methods

Dictator extends `Dictionary<string, object>` with a set of extension methods which are grouped into several operation categories depending on their functionality. These categories are for example include operations for getting, setting or checking fields and other methods which operates over the entire dictionary object.

### Field set and get operations

Field set and get operations consists of a list of methods which are used to store and retrieve data out of the dictionary in specified format or type. Types which are supported include `bool`, `byte`, `short`, `int`, `long`, `float`, `double`, `decimal`, `DateTime`, `string`, `object`, `Dictionary<string, object>` (aliased as document), `enum` and `List<T>`.

#### Basic types

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

#### DateTime type

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

#### Enum type

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

#### List of objects

```
var document = new Dictionary<string, object>()
    .List("list1", new List<int>() { 1, 2, 3 });

var list1 = document.List<int>("list1");
var list1Size = document.ListSize("list1");
```

#### Nested fields

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

### Field check operations

Field check operations consists of a set of methods which checks the presence of a field or it's specific type within the document.

#### Field existence check

```
var document = new Dictionary<string, object>()
    .String("foo", "foo string value")
    .Object("bar", null);

// true
var stringFieldExists = document.Has("foo");
// true
var nullFieldExists = document.Has("bar");
// false
var fieldExists = document.Has("nonExistingField");
```

#### Exact type check

Exact type checking operations returns true only if the specified field exists and contains exact type.

```
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
```

#### Null check

```
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
```

#### DateTime type check

```
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
```

#### Enum type check

```
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
```

#### Generic type check

```
var document = new Dictionary<string, object>()
    .String("foo", "foo string value")
    .Int("bar", 12345);

// true
var isString = document.IsType<string>("foo");
// true
var isInt = document.IsType("bar", typeof(int));
// false
var isBool = document.IsType<bool>("nonExistingField");
```

#### Field value equality check

Note: Object's `Equals` method is used for value comparison.

```
var document = new Dictionary<string, object>()
    .String("foo", "foo string value")
    .Int("bar", 12345);

// true
var isEqual1 = document.IsEqual("foo", "foo string value");
// true
var isEqual2 = document.IsEqual("bar", 12345);
// false
var isEqual3 = document.IsEqual("nonExistingField", "some string value");
```

### Deleting fields

```
var document = new Dictionary<string, object>()
    .String("foo", "string value")
    .Int("bar", 12345)
    .String("baz.foo", "other string value");

document.Drop("foo", "bar");

// false
var hasFoo = document.Has("foo");
// false
var hasBar = document.Has("bar");
```

### Cloning documents

```
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
```

### Merging documents

```
var document1 = new Dictionary<string, object>()
    .String("foo", "string value")
    .Int("bar", 12345);

var document2 = new Dictionary<string, object>()
    .String("foo", "new string value")
    .Int("baz", 54321);

// merges document2 into document1 and overwrites conflicting fields
document1.Merge(document2);

// merges document2 into document1 and keeps conflicting fields
document1.Merge(document2, MergeBehavior.KeepFields);
```

### Convert document to strongly typed object

```
var document = new Dictionary<string, object>()
    .String("Foo", "string value")
    .Int("Bar", 12345);

var dummy = document.ToObject<Dummy>();

var foo = dummy.Foo;
var bar = dummy.Bar;
```