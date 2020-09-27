using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace NotificationServer
{
    public abstract class Logic : IStateDrivenEntity, IComponent, IPublisher, IRecipient, IObserver<IEvent>
    {
        private readonly StateDrivenEntityHelper mStateDrivenEntityHelper;
        private readonly ISubject<IEvent> mSubject = new Subject<IEvent>();

        protected Logic()
        {
            mStateDrivenEntityHelper = 
                new StateDrivenEntityHelper(InnerUninitializedToInitialized,
                                            InnerInitializedToStarted,
                                            InnerStartedToInitialized,
                                            InnerInitializedToUninitialized,
                                            InnerAnyToInvalid,
                                            InnerInvalidToUninitialized);
        }

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
            mSubject.Where(x => rule.IsActivated(x))
                .Subscribe(recipient as IObserver<IEvent>);
        }

        public void OnCompleted()
        {
        }

        public void OnError(Exception error)
        {
        }

        public void OnNext(IEvent value)
        {
            IEvent processed = ProcessEvent(value);

            if (!(processed is EventGroup group))
            {
                if (processed != null)
                {
                    mSubject.OnNext(processed);
                }
            }
            else
            {
                foreach (IEvent currentEvent in @group)
                {
                    mSubject.OnNext(currentEvent);
                }
            }
        }

        protected virtual void InnerUninitializedToInitialized()
        {
        }

        protected virtual void InnerInitializedToStarted()
        {
        }

        protected virtual void InnerStartedToInitialized()
        {
        }

        protected virtual void InnerInitializedToUninitialized()
        {
        }

        protected virtual void InnerAnyToInvalid()
        {
        }

        protected virtual void InnerInvalidToUninitialized()
        {
        }

        public State CurrentState => mStateDrivenEntityHelper.CurrentState;

        public void TransformTo(State state)
        {
            mStateDrivenEntityHelper.TransformTo(state);
        }
    }
}