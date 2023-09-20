using System;
namespace Application.Dto.Common
{
	public class CacheSettings
	{
		public int SlidingExpiration { get; set; }
		public string DestinationUrl { get; set; }
		public string ApplicationName { get; set; }
		public bool BypassCache { get; set; }
	}
}

