namespace RedeSocial.Notifications
{
    public class NotificationResponse
    {
        public IReadOnlyCollection<ErrorMessage>? ErrorMessages { get; set; }
        public IReadOnlyCollection<SuccessMessage>? SuccessMessages { get; set; }

        public NotificationResponse() { }

        public NotificationResponse(IReadOnlyCollection<ErrorMessage> errorMessages)
        {
            ErrorMessages = errorMessages;
            SuccessMessages = new List<SuccessMessage>();
        }

        public NotificationResponse(IReadOnlyCollection<SuccessMessage> successMessages)
        {
            ErrorMessages = new List<ErrorMessage>();
            SuccessMessages = successMessages;
        }

        public NotificationResponse(ErrorMessage errorMessage)
        {
            ErrorMessages = new List<ErrorMessage> { errorMessage };
            SuccessMessages = new List<SuccessMessage>();
        }

        public NotificationResponse(SuccessMessage successMessage)
        {
            ErrorMessages = new List<ErrorMessage>();
            SuccessMessages = new List<SuccessMessage> { successMessage };
        }
    }
}