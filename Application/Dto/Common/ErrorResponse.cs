using System;
namespace Application.Dto.Common
{
	public class ErrorResponse
	{
		public string ApplicationErrorMessage { get; set; }
		public List<string> ErrorMessages { get; set; }
	
	}
}

