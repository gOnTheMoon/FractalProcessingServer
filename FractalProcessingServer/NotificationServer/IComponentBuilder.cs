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

    public class RuntimeContext : IRuntimeContext
    {
        public IComponentContainer Components { get; } =
            new ComponentContainer();
    }

    public class ComponentContainer : List<IComponent>, IComponentContainer
    {
    }
}