using System;

namespace NotificationServer
{
    public abstract class Dispatcher : IObserver<IEvent>
    {
        public abstract void DoDispatch(EventGroup eventGroup);

        public void OnCompleted()
        {
        }

        public void OnError(Exception error)
        {
        }

        public void OnNext(IEvent value)
        {
            DoDispatch(new EventGroup(){value});
        }
    }
}