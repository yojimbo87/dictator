using System;

namespace Dictator
{
    /// <summary>
    /// Ignores property during when converting object to or from document format.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class IgnoreField : Attribute
    {
        public IgnoreField()
        {
        }
    }
}
