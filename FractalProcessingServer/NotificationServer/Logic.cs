namespace NotificationServer
{
    public abstract class Logic : IComponent, IPublisher, IRecipient
    {
        public abstract IEvent ProcessEvent(IEvent eventToProcess);

        protected void Publish(IEvent eventToPublish)
        {
            throw new System.NotImplementedException();
        }

        public void HandleNotification(Notification notification)
        {
            throw new System.NotImplementedException();
        }

        public void Subscribe(IRecipient recipient, IRule rule)
        {
            throw new System.NotImplementedException();
        }
    }
}