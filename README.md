dictator
========

Dictator is a C# library which provides a set of extension methods for handling data stored in `Dictionary<string, object>` object which serves as closest .NET low level representation of native JSON documents.

### Example

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