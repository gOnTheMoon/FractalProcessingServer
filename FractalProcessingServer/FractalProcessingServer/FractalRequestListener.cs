using NotificationServer;

namespace FractalProcessingServer
{
    public class RequestListener : IComponent, IPublisher
    {
        public void Subscribe(IRecipient recipient, IRule rule)
        {
            throw new System.NotImplementedException();
        }
    }
}