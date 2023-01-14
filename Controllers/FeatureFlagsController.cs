

using Microsoft.AspNetCore.Mvc;
using Redis.OM;
using Redis.OM.Searching;
using Adesa.FeatureFlags.Models;


namespace Adesa.FeatureFlags.Controllers;

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

	[HttpPut("{id:alpha}")]
	public async Task<FeatureFlag> Update(string id, [FromQuery]bool flag)
	{        
		var ff =  await _featureFlags.FindByIdAsync(id);
		ff.Active = flag;
		ff.LastUpdatedOn = DateTime.Now;
		await _featureFlags.UpdateAsync(ff);
		return ff;
	}

	[HttpGet("{id:alpha}")]
	public async Task<IActionResult> GetById(string id)
	{        
		//return _featureFlags.FirstOrDefault(x => x.Id == id);
		var ff = await _featureFlags.FindByIdAsync(id);
		return ff != null ? Ok(ff) : NotFound();
	}

}
