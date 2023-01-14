


using Redis.OM.Modeling;

namespace Adesa.FeatureFlags.Models;

[Document(StorageType = StorageType.Hash, Prefixes = new []{"FeatureFlag"})]
public class FeatureFlag
{    
    [RedisIdField] [Indexed] public string Id { get; set; }
    
    public bool Active { get; set; }
    public DateTime LastUpdatedOn { get; set; }
    
}
