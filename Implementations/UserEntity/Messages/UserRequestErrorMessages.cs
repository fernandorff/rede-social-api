namespace RedeSocial.Implementations.UserEntity.Messages
{
    public abstract class UserRequestErrorMessages
    {
        // Blank field messages
        public const string BlankEmail = "O campo E-mail é obrigatório.";
        public const string BlankPassword = "O campo Senha é obrigatório.";
        public const string BlankFirstName = "O campo Nome é obrigatório.";
        public const string BlankSurname = "O campo Sobrenome é obrigatório.";
        public const string BlankDateOfBirth = "O campo Data de Nascimento é obrigatório.";
        public const string BlankCep = "O campo CEP é obrigatório.";

        // Max length messages
        public const string MaxLengthFirstName = "O campo Nome Completo não pode exceder {1} caracteres.";
        public const string MaxLengthEmail = "O campo E-mail não pode exceder {1} caracteres.";
        public const string MaxLengthNickname = "O campo Apelido não pode exceder {1} caracteres.";
        public const string MaxLengthCep = "O campo CEP não pode exceder {1} caracteres.";
        public const string MaxLengthPassword = "O campo Senha não pode exceder {1} caracteres.";
        public const string MaxLengthProfilePictureUrl = "O campo Imagem de Perfil não pode exceder {1} caracteres.";

        // Email format message
        public const string InvalidEmailFormat = "Formato de e-mail inválido.";
        public const string InvalidCepFormat = "Formato de CEP inválido.";

    }
}