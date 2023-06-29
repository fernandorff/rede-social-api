namespace RedeSocial.Implementations.PostEntity.Messages
{
    public abstract class PostServiceErrorMessages
    {
        public static string PostNotFound(Guid id) { return $"A postagem de UserId {id} não foi encontrada."; }

        public const string InvalidId = "O UserId do usuário é inválido.";
        public const string InvalidEmailOrPassword = "Usuário com o e-mail ou senha informados não foi encontrado";
        public const string EmailAlreadyExists = "Este e-mail já está em uso. Tento outro e-mail.";

    }
}