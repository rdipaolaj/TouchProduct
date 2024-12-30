using product.common.Settings;
using System.Text.Json.Serialization;

namespace product.common.Secrets;
public class JwtSecrets : ISecret
{
    [JsonPropertyName("jwt-signing-key")]
    public string JwtSigningKey { get; set; } = string.Empty;
}