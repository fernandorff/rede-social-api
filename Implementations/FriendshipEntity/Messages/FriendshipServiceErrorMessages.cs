namespace RedeSocial.Implementations.FriendshipEntity.Messages
{
    public abstract class FriendshipServiceErrorMessages
    {
        public const string SameUserFriendRequest = "Você não pode enviar solicitação de amizade para sí mesmo.";
        public const string AlreadySentRequest = "Você já enviou uma solicitação de amizade para esse usuário.";
        public const string SameUserUnfriend = "Você não pode realizar essa operação.";

        public const string FriendshipNotFound = "Vínculo de amizade não encontrado.";
        public const string NotRequestedToUser = "Usuário não autorizado para realizar essa ação.";
    }
}