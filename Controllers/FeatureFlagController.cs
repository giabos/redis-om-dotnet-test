

using Microsoft.AspNetCore.Mvc;
using Redis.OM;
using Redis.OM.Searching;
using Redis.OM.Skeleton;

namespace Redis.OM.Skeleton.Controllers;

[ApiController]
[Route("[controller]")]
public class FeatureFlagController : ControllerBase
{

	private readonly RedisCollection<FeatureFlag> _featureFlags;
	private readonly RedisConnectionProvider _provider;
	public FeatureFlagController(RedisConnectionProvider provider)
	{
		_provider = provider;
		_featureFlags = (RedisCollection<FeatureFlag>)provider.RedisCollection<FeatureFlag>();
	}

	[HttpPost]
	public async Task<FeatureFlag> AddFeatureFlag([FromBody] FeatureFlag ff)
	{
		ff.LastUpdatedOn = DateTime.Now;
		await _featureFlags.InsertAsync(ff);
		return ff;
	}


	[HttpGet()]
	public FeatureFlag FilterByAge(string id)
	{        
		return _featureFlags.Where(x => x.Id == id).FirstOrDefault();
	}


}
