namespace NotificationServer
{
    public interface IRule
    {
        bool IsActivated(IEvent eventToCheck);
    }
}