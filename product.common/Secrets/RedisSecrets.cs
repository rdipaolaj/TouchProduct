using product.common.Settings;
using System.Text.Json.Serialization;

namespace product.common.Secrets;
public class RedisSecrets : ISecret
{
    [JsonPropertyName("private-key")]
    public string PrivateKey { get; set; } = string.Empty;
}