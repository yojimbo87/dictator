using System;

namespace Dictator
{
	public class NonExistingFieldException : Exception
	{
		public NonExistingFieldException(string message) : base(message)
		{
		}
	}
}
