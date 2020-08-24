namespace NotificationServer
{
    public interface IPublisher
    {
        void Subscribe(IRecipient recipient, IRule rule);
    }

    public interface IRecipient
    {
        void HandleNotification(Notification notification);
    }

    public class Notification
    {
    }
}