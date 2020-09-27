using System;

namespace NotificationServer
{
    public interface IStateDrivenEntity
    {
        State CurrentState { get; }

        void TransformTo(State state);
    }

    public enum State
    {
        Uninitialized,
        Initialized,
        Started,
        Invalid
    }

    public abstract class StateDrivenEntity : IStateDrivenEntity
    {
        protected abstract void InnerUninitializedToInitialized();
        protected abstract void InnerInitializedToStarted();
        protected abstract void InnerStartedToInitialized();
        protected abstract void InnerInitializedToUninitialized();
        protected abstract void InnerAnyToInvalid();
        protected abstract void InnerInvalidToUninitialized();

        public State CurrentState { get; private set; }

        public void TransformTo(State state)
        {
            try
            {
                switch (CurrentState, state)
                {
                    case (not State.Invalid, not State.Invalid):
                        ValidStateTransform(state);
                        break;
                    case (not State.Invalid, State.Invalid):
                        InnerAnyToInvalid();
                        CurrentState = State.Invalid;
                        break;
                    case (State.Invalid, not State.Invalid):
                        InnerInvalidToUninitialized();
                        CurrentState = State.Uninitialized;
                        ValidStateTransform(state);
                        break;
                }

                ValidStateTransform(state);
            }
            catch (Exception e)
            {
                TransformTo(State.Invalid);
            }
        }

        private void ValidStateTransform(State state)
        {
            switch (CurrentState, state)
            {
                case (State.Started, State.Uninitialized):
                    SingleStateTransform(State.Initialized);
                    SingleStateTransform(State.Uninitialized);
                    break;
                case (State.Uninitialized, State.Started):
                    SingleStateTransform(State.Initialized);
                    SingleStateTransform(State.Started);
                    break;
                default:
                    SingleStateTransform(state);
                    break;
            }
        }

        private void SingleStateTransform(State state)
        {
            switch (CurrentState, state)
            {
                case (State.Uninitialized, State.Initialized):
                    InnerUninitializedToInitialized();
                    CurrentState = State.Initialized;
                    break;
                case (State.Initialized, State.Started):
                    InnerInitializedToStarted();
                    CurrentState = State.Started;
                    break;
                case (State.Started, State.Initialized):
                    InnerStartedToInitialized();
                    CurrentState = State.Initialized;
                    break;
                case (State.Initialized, State.Uninitialized):
                    InnerInitializedToUninitialized();
                    CurrentState = State.Uninitialized;
                    break;
            }
        }
    }

    public class StateDrivenEntityHelper : StateDrivenEntity
    {
        private readonly Action mInnerUninitializedToInitialized;
        private readonly Action mInnerInitializedToStarted;
        private readonly Action mInnerStartedToInitialized;
        private readonly Action mInnerInitializedToUninitialized;
        private readonly Action mInnerAnyToInvalid;
        private readonly Action mInnerInvalidToUninitialized;

        public StateDrivenEntityHelper(Action innerUninitializedToInitialized, Action innerInitializedToStarted,
                                       Action innerStartedToInitialized, Action innerInitializedToUninitialized,
                                       Action innerAnyToInvalid, Action innerInvalidToUninitialized)
        {
            mInnerUninitializedToInitialized = innerUninitializedToInitialized ??
                                               throw new ArgumentNullException(nameof(innerUninitializedToInitialized));
            mInnerInitializedToStarted = innerInitializedToStarted ??
                                         throw new ArgumentNullException(nameof(innerInitializedToStarted));
            mInnerStartedToInitialized = innerStartedToInitialized ??
                                         throw new ArgumentNullException(nameof(innerStartedToInitialized));
            mInnerInitializedToUninitialized = innerInitializedToUninitialized ??
                                               throw new ArgumentNullException(nameof(innerInitializedToUninitialized));
            mInnerAnyToInvalid = innerAnyToInvalid ?? throw new ArgumentNullException(nameof(innerAnyToInvalid));
            mInnerInvalidToUninitialized = innerInvalidToUninitialized ??
                                           throw new ArgumentNullException(nameof(innerInvalidToUninitialized));
        }

        protected override void InnerUninitializedToInitialized()
        {
            mInnerUninitializedToInitialized();
        }

        protected override void InnerInitializedToStarted()
        {
            mInnerInitializedToStarted();
        }

        protected override void InnerStartedToInitialized()
        {
            mInnerStartedToInitialized();
        }

        protected override void InnerInitializedToUninitialized()
        {
            mInnerInitializedToUninitialized();
        }

        protected override void InnerAnyToInvalid()
        {
            mInnerAnyToInvalid();
        }

        protected override void InnerInvalidToUninitialized()
        {
            mInnerInvalidToUninitialized();
        }
    }
}