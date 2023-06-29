namespace RedeSocial.Notifications
{
    public class Notifiable
    {
        private readonly List<ErrorMessage> _errorMessages = new();
        private readonly List<SuccessMessage> _successMessages = new();

        public IReadOnlyCollection<ErrorMessage> ErrorMessages => _errorMessages;
        public IReadOnlyCollection<SuccessMessage> SuccessMessages => _successMessages;

        public void AddErrorMessage(string message)
        {
            var errorMessage = new ErrorMessage(message);
            _errorMessages.Add(errorMessage);
        }

        public void AddSuccessMessage(string message)
        {
            var successMessage = new SuccessMessage(message);
            _successMessages.Add(successMessage);
        }

        public bool IsValid() => !_errorMessages.Any();
    }
}