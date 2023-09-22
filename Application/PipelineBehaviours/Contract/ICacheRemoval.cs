using System;
namespace Application.PipelineBehaviours.Contract
{
	public interface ICacheRemoval
	{
        public List<string> CacheKeys { get; set; }
    }
}

