namespace NotificationServer
{
    public abstract class Logic
    {
        public abstract IEvent ProcessEvent(IEvent eventToProcess);

        protected void Publish(IEvent eventToPublish)
        {
            throw new System.NotImplementedException();
        }
    }
}