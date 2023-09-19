using System;
namespace Application.Exceptions
{
	public class CustomValidationException : Exception
	{
		public List<string> ErrorMessages { get; set; }

		public string ApplicationErrorMessage { get; set; }

		public CustomValidationException(List<string> errorMessages, string singleLineErrorMessage)
			: base(singleLineErrorMessage)
		{
			ErrorMessages = errorMessages;
			ApplicationErrorMessage = singleLineErrorMessage;
		}
	}
}

