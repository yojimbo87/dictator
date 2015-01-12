
using System;

namespace Dictator.Tests
{
    public class EnumEntity
    {
        public Color MyColor1 { get; set; }
        public Color MyColor2 { get; set; }
        public Color MyColor3 { get; set; }
    }
    
    public enum Color
    {
        Red,
        Green,
        Blue
    }
}
