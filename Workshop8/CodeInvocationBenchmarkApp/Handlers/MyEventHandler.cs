using CodeInvocationBenchmarkApp.Events;

namespace CodeInvocationBenchmarkApp.Handlers;

public class MyEventHandler : IEventHandler<MyEvent>
{
    public void Handle(MyEvent myEvent)
    {
        return;
    }
}
