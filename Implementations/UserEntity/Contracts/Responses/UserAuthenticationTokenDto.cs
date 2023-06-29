using RedeSocial.Notifications;

namespace RedeSocial.Implementations.UserEntity.Contracts.Responses;

public class UserAuthenticationTokenDto : Notifiable
{
    public bool IsAuthenticated { get; set; } = false;
    public Guid UserId { get; set; }
    public string Token { get; set; } = string.Empty;
}