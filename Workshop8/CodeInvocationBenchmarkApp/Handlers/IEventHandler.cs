using CodeInvocationBenchmarkApp.Events;

namespace CodeInvocationBenchmarkApp.Handlers;

public interface IEventHandler<in T> where T : IEvent
{
    void Handle(T @event);
}
