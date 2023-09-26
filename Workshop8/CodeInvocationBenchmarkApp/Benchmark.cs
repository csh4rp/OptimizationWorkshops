using System.Linq.Expressions;
using System.Reflection.Emit;
using BenchmarkDotNet.Attributes;
using CodeInvocationBenchmarkApp.Events;
using CodeInvocationBenchmarkApp.Handlers;
using CodeInvocationGenerationApp;

namespace CodeInvocationBenchmarkApp;

[MemoryDiagnoser]
public class Benchmark
{
    private static readonly object Handler = new MyEventHandler();
    private static readonly MyEvent Event = new MyEvent();
    private static readonly Action<object, object> Action;
    private static readonly Action<object, object> Lambda;

    static Benchmark()
    {
        var methodInfo = Handler.GetType().GetMethod(nameof(IEventHandler<IEvent>.Handle))!;
        
        var method = new DynamicMethod("Handle", typeof(void), new[] {typeof(object), typeof(object)});
        var ilGenerator = method.GetILGenerator();
        ilGenerator.Emit(OpCodes.Ldarg_0);
        ilGenerator.Emit(OpCodes.Castclass, methodInfo.DeclaringType!);
        ilGenerator.Emit(OpCodes.Ldarg_1);
        ilGenerator.Emit(OpCodes.Castclass, typeof(MyEvent));
        ilGenerator.Emit(OpCodes.Callvirt, methodInfo);
        ilGenerator.Emit(OpCodes.Ret);
        
        Action = method.CreateDelegate<Action<object, object>>();
        
        var handlerParameter = Expression.Parameter(typeof(object), "handler");
        var eventParameter = Expression.Parameter(typeof(object), "event");

        var handlerConvertExpression = Expression.Convert(handlerParameter, methodInfo.DeclaringType!);
        var eventConvertExpression = Expression.Convert(eventParameter, typeof(MyEvent));

        var methodCallExpression = Expression.Call(handlerConvertExpression, methodInfo, eventConvertExpression);

        Lambda = Expression.Lambda<Action<object, object>>(methodCallExpression, handlerParameter, eventParameter).Compile();
    }
    
    [Benchmark]
    public void RunReflection()
    {
        var methodInfo = Handler.GetType().GetMethod(nameof(IEventHandler<IEvent>.Handle))!;

        methodInfo.Invoke(Handler, new object[] { Event });
    }
    
    [Benchmark]
    public void RunDynamic()
    {
        ((dynamic)Handler).Handle(Event);
    }

    [Benchmark]
    public void RunEmittedDelegate()
    {
        Action(Handler, Event);
    }
    
    [Benchmark]
    public void RunEmittedLambdaDelegate()
    {
        Lambda(Handler, Event);
    }
    
    [Benchmark]
    public void RunGeneratedCode()
    {
        Invoker.Invoke(Handler, Event);
    }
}
