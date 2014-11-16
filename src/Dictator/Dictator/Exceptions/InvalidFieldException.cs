using System;

namespace Dictator
{
	public class InvalidFieldException : Exception
	{
		public InvalidFieldException(string message) : base(message)
		{
		}
	}
}
