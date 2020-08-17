namespace NotificationServer
{
    public abstract class Dispatcher
    {
        public abstract void DoDispatch(EventGroup eventGroup);
    }
}