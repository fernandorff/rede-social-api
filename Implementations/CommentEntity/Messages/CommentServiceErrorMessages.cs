namespace RedeSocial.Implementations.CommentEntity.Messages
{
    public abstract class CommentServiceErrorMessages
    {
        public static string CommentNotFound(Guid id) { return $"O co de UserId {id} não foi encontrada."; }
        public const string InvalidId = "O UserId do usuário é inválido.";
    }
}