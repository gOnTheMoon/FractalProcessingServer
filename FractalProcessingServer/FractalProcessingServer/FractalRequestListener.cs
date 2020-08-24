using NotificationServer;

namespace FractalProcessingServer
{
    public class RequestListener : Logic, IComponent, IPublisher
    {
        public override IEvent ProcessEvent(IEvent eventToProcess)
        {
            return eventToProcess;
        }
    }
}