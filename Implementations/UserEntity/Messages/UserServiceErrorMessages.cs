namespace RedeSocial.Implementations.UserEntity.Messages
{
    public abstract class UserServiceErrorMessages
    {
        public static string UserNotFound(Guid id) { return $"O usuário de UserId {id} não foi encontrado."; }

        public const string InvalidId = "O UserId do usuário é inválido.";
        public const string InvalidEmailOrPassword = "Usuário com o e-mail ou senha informados não foi encontrado";
        public const string EmailAlreadyExists = "Este e-mail já está em uso. Tento outro e-mail.";

    }
}