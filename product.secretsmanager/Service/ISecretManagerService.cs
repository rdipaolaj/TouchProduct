using product.common.Secrets;

namespace product.secretsmanager.Service;
public interface ISecretManagerService
{
    Task<PostgresDbSecrets?> GetPostgresDbSecrets();
    Task<RedisSecrets?> GetRedisSecrets();
}