using System;

namespace Dictator
{
	public class InvalidFieldTypeException : Exception
	{
		public InvalidFieldTypeException(string message) : base(message)
		{
		}
	}
}
