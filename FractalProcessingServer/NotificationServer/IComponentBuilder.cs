using System.Collections.Generic;

namespace NotificationServer
{
    public interface IComponentBuilder
    {
        void BuildComponents(IRuntimeContext context);
    }

    public interface IRuntimeContext
    {
        IComponentContainer Components { get; }
    }

    public interface IComponentContainer : ICollection<IComponent>
    {
    }

    public interface IComponent
    {
    }
}