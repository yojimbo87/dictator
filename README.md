# Dictator

Dictator is a C# library which provides functionality for retrieving, storing and manipulating data inside `Dictionary<string, object>`. Dictionary that consists of string and object key-value pairs serves as the closest .NET low level representation of native JSON documents but for example lacks support for easy access to field data which might be either deeply nested or hard to retrieve without considerable amount of code. Aim of dictator is to help with common tasks that are used to work with JSON-like data structure representation in .NET. Below is a simple example which sets few fields, checks for their types and retrieves their values in specified format.

```csharp
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
    - [Array of objects](#array-of-objects)
    - [Nested fields](#nested-fields)
  - [Field check operations](#field-check-operations)
    - [Field existence check](#field-existence-check)
    - [Exact type check](#exact-type-check)
    - [Null check](#null-check)
    - [DateTime type check](#datetime-type-check)
    - [Enum type check](#enum-type-check)
    - [List and array type check](#list-and-array-type-check)
    - [Generic type check](#generic-type-check)
    - [Integer value check](#integer-value-check)
    - [Field value equality check](#field-value-equality-check)
  - [List and array iteration](#list-and-array-iteration)
  - [Deleting fields](#deleting-fields)
  - [Cloning documents](#cloning-documents)
  - [Merging documents](#merging-documents)
  - [Convert document to strongly typed object](#convert-document-to-strongly-typed-object)
- [Dictator static methods](#dictator-static-methods)
  - [Convert document list to generic list](#convert-document-list-to-generic-list)
  - [Convert strongly typed object to document](#convert-strongly-typed-object-to-document)
  - [Convert generic list to document list](#convert-generic-list-to-document-list)
- [Property attributes](#property-attributes)
- [Global settings](#global-settings)
- [Schema validation](#schema-validation)

## Dictionary extension methods

Dictator extends `Dictionary<string, object>` with a set of extension methods which are grouped into several operation categories depending on their functionality. These categories are for example include operations for getting, setting or checking fields and other methods which operates over the entire dictionary object.

### Field set and get operations

Field set and get operations consists of a list of methods which are used to store and retrieve data out of the dictionary in specified format or type. Types which are supported include `bool`, `byte`, `short`, `int`, `long`, `float`, `double`, `decimal`, `DateTime`, `Guid`, `string`, `object`, `Dictionary<string, object>` (aliased as document), `enum` and `List<T>`.

#### Basic types

```csharp
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

```csharp
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

```csharp
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

```csharp
var document = new Dictionary<string, object>()
    .List("list1", new List<int>() { 1, 2, 3 });

var list1 = document.List<int>("list1");
var list1Size = document.Size("list1");
var item2 = document.Int("list1[1]");
            
// change value of item at specified index
document.Int("list1[1]", 222);

// append new value to the list
document.Int("list1[*]", 4);
```

#### Array of objects

```csharp
var document = new Dictionary<string, object>()
    .Array("array1", new int[] { 1, 2, 3 });

var array1 = document.Array<int>("array1");
var array1Size = document.Size("array1");
var item2 = document.Int("array1[1]");
            
// change value of item at specified index
document.Int("array1[1]", 222);
```

#### Nested fields

```csharp
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

```csharp
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

```csharp
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

```csharp
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

```csharp
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

```csharp
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

#### List and array type check

```csharp
var document = new Dictionary<string, object>()
    .List("list1", new List<int> { 1, 2, 3})
    .Array("array1", new [] { 4, 5, 6 });

// true
var isList = document.IsList("list1");
// true
var isArray = document.IsArray("array1");
```

#### Generic type check

```csharp
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

#### Integer value check

`IsInteger` method can be used to determined whether specified field path contains integer value among following types: byte, sbyte, short, ushort, int, uint, long and ulong.

```csharp
var document = new Dictionary<string, object>()
    .Byte("byte1", 1)
    .Short("short1", 2)
    .Int("int1", 3)
    .Long("long1", 4)
    .Object("null1", null)
    .String("string1", "string value");

// true
var isInteger1 = document.IsInteger("byte1");
// true
var isInteger2 = document.IsInteger("short1");
// true
var isInteger3 = document.IsInteger("int1");
// true
var isInteger4 = document.IsInteger("long1");
// false
var isInteger5 = document.IsInteger("null1");
// false
var isInteger6 = document.IsInteger("string1");
// false
var isInteger7 = document.IsInteger("nonExistingField");
```

#### Field value equality check

Note: Object's `Equals` method is used for value comparison.

```csharp
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

### List and array iteration

```csharp
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
```

### Deleting fields

```csharp
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

```csharp
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

```csharp
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

```csharp
var document = new Dictionary<string, object>()
    .String("Foo", "string value")
    .Int("Bar", 12345);

var dummy = document.ToObject<Dummy>();

var foo = dummy.Foo;
var bar = dummy.Bar;
```

## Dictator static methods

`Dictator` class consists of several static methods which provide additional functionality for manipulating documents.

### Convert document list to generic list

```csharp
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
```

### Convert strongly typed object to document

```csharp
var dummy = new Dummy();
dummy.Foo = "string value";
dummy.Bar = 12345;

var document = Dictator.ToDocument(dummy);

var foo = document.String("Foo");
var bar = document.Int("Bar");
```

### Convert generic list to document list

```csharp
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
```

## Property attributes

Classes which are meant to have their properties converted to or from document format can use the following attributes:

- `AliasField` - Specified alias will be used as field name to convert property to or from document format.
- `IgnoreField` - Ignores property during when converting object to or from document format.
- `IgnoreNullValue` - Ignores property if it contains null value when converting object to or from document format.

## Global settings

`Dictator.Settings` object contains several properties which determine and affects how certain operations are perfomed. These include:

- `Dictator.Settings.EnumFormat` - Global enum serialization format (default is set to `EnumFormat.Object`).
- `Dictator.Settings.MergeBehavior` - Global documents merge behavior (default is set to `MergeBehavior.OverwriteFields`).
- `Dictator.Settings.DateTimeFormat` - Global DateTime serialization format (default is set to `DateTimeFormat.Object`).
- `Dictator.Settings.DateTimeStringFormat` - Global DateTime string format which will be used when serializing DateTime object in string format (default is set to `yyyy-MM-ddTHH:mm:ss.fffZ`).

## Schema validation

`Dictator.Schema` property returns `Schema` object which contains fluent API for constraint validation of document structure and values with following methods:

- `MustHave(string fieldPath)` - Specifies field path which must exist in the document schema and must conform to further constraints.
- `ShouldHave(string fieldPath)` - Specifies field path which if exists must conform to further constraints.
- `NotNull()` - Previously specified field path cannot have null value.
- `Type<T>()` - Previously specified field path value must be of specified type.
- `Type(Type type)` - Previously specified field path value must be of specified type.
- `Min(int minValue)` - Previously specified field path must have specified minimal value.
- `Max(int maxValue)` - Previously specified field path must have specified maximal value.
- `Range(int minValue, int maxValue)` - Previously specified field path must be in specified range.
- `Size(int collectionSize)` - Previously specified field path must have specified number of items in collection.
- `Match(string regex)` - Previously specified field path must match specified regular expression.
- `Match(string regex, bool ignoreCase)` - Previously specified field path must match specified regular expression.
- `Message(string errorMessage)` - Specifies custom error message if previous schema constraints were violated.
- `Validate(Dictionary<string, object> document)` - Performs validation based on previous constraints on specified document.

Schema validation example:

```csharp
// setup document to be validated
var document = new Dictionary<string, object>()
    .String("string1", "test1")
    .List("list1", new List<int> { 1, 2, 3 });

// setup schema constraints and validate document
var validationResult = Dictator.Schema
    .MustHave("string1").Type<string>().Min(3).Max(4)
    .MustHave("list1").Size(5)
    .Validate(document);

if (!validationResult.IsValid)
{
    foreach (var violation in validationResult.Violations)
    {
        var violationMessage = violation.Message;
    }
}
```

More examples regarding schema validation can be found in following unit tests:
- [SchemaValidationTests.cs](src/Dictator/Dictator.Tests/Schema/SchemaValidationTests.cs).
- [SchemaShouldHaveValidationTests.cs](src/Dictator/Dictator.Tests/Schema/SchemaShouldHaveValidationTests.cs).
- [SchemaMustHaveValidationTests.cs](src/Dictator/Dictator.Tests/Schema/SchemaMustHaveValidationTests.cs).