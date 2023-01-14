

using Microsoft.AspNetCore.Mvc;
using Redis.OM;
using Redis.OM.Searching;
using Redis.OM.Skeleton;

namespace Redis.OM.Skeleton.Controllers;

[ApiController]
[Route("[controller]")]
public class FeatureFlagsController : ControllerBase
{

	private readonly RedisCollection<FeatureFlag> _featureFlags;
	private readonly RedisConnectionProvider _provider;
	public FeatureFlagsController(RedisConnectionProvider provider)
	{
		_provider = provider;
		_featureFlags = (RedisCollection<FeatureFlag>)provider.RedisCollection<FeatureFlag>();
	}

	[HttpPost]
	public async Task<FeatureFlag> Add([FromBody] FeatureFlag ff)
	{
		ff.LastUpdatedOn = DateTime.Now;
		await _featureFlags.InsertAsync(ff);
		return ff;
	}

	[HttpPut()]
	public async Task<FeatureFlag> Update(string id, [FromQuery]bool flag)
	{        
		var ff =  await _featureFlags.FindByIdAsync(id);
		ff.Active = flag;
		ff.LastUpdatedOn = DateTime.Now;
		await _featureFlags.UpdateAsync(ff);
		return ff;
	}

	[HttpGet()]
	public async Task<FeatureFlag> GetById(string id)
	{        
		//return _featureFlags.FirstOrDefault(x => x.Id == id);
		return await _featureFlags.FindByIdAsync(id);
	}

}
