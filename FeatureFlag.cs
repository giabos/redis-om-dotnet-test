


using Redis.OM.Modeling;

namespace Redis.OM.Skeleton;

[Document(StorageType = StorageType.Json, Prefixes = new []{"FeatureFlag"})]
public class FeatureFlag
{    
    [RedisIdField] public string Id { get; set; }
    
    public bool Active { get; set; }
    public DateTime LastUpdatedOn { get; set; }
    
}
