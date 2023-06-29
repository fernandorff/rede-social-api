namespace RedeSocial.Implementations.PostEntity.Messages
{
    public abstract class PostRequestErrorMessages
    {
        public const string BlankTitle = "O campo Título é obrigatório.";
        public const string BlankText = "O campo Texto é obrigatório.";
        public const string BlankVisibility = "O campo Visibilidade é obrigatório.";
        public const string InvalidVisibility = "Visibilidade inválida.";
    }
}