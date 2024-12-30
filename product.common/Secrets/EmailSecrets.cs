using product.common.Settings;
using System.Text.Json.Serialization;

namespace product.common.Secrets;
public class EmailSecrets : ISecret
{
    [JsonPropertyName("api-key-email")]
    public string EmailKey { get; set; } = string.Empty;
}
