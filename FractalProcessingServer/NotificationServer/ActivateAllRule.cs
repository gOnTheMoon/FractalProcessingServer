namespace NotificationServer
{
    public class ActivateAllRule : IRule
    {
        public bool IsActivated(IEvent eventToCheck)
        {
            return true;
        }
    }
}