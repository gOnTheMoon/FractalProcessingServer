using System;

namespace NotificationServer
{
    public class TypeRule : IRule
    {
        private readonly Type mEventType;

        public TypeRule(Type eventType)
        {
            mEventType = eventType;
        }

        public bool IsActivated(IEvent eventToCheck)
        {
            return mEventType.IsInstanceOfType(eventToCheck);
        }
    }
}