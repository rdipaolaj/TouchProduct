using product.common.Secrets;
using System.Runtime;

namespace product.secretsmanager.Service;
public interface ISecretManagerService
{
    Task<PostgresDbSecrets?> GetPostgresDbSecrets();
    Task<RedisSecrets?> GetRedisSecrets();
    Task<EmailSecrets?> GetEmailSecrets();
    Task<JwtSecrets?> GetJwtSecrets();
}