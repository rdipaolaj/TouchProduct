using product.common.Responses;

namespace product.internalservices.User;
public interface IUserService
{
    Task<ApiResponse<IEnumerable<string>>> GetAdminEmailsAsync(CancellationToken cancellationToken);
}
