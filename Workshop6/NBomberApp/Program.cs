using NBomber.CSharp;
using NBomber.Http.CSharp;

var client = new HttpClient();

var asyncScenario = Scenario.Create("get-async", async cnx =>
    {
        var request = Http.CreateRequest("GET", "http://localhost:5281/get-async");

        var response = await Http.Send(client, request);

        return response;
    })
    .WithLoadSimulations(Simulation.Inject(rate: 50,
        interval: TimeSpan.FromMilliseconds(100),
        during: TimeSpan.FromMinutes(1)));

var syncScenario = Scenario.Create("get-sync", async cnx =>
    {
        var request = Http.CreateRequest("GET", "http://localhost:5281/get-sync");

        var response = await Http.Send(client, request);

        return response;
    })
    .WithLoadSimulations(Simulation.Pause(TimeSpan.FromSeconds(70)), Simulation.Inject(rate: 50,
        interval: TimeSpan.FromMilliseconds(100),
        during: TimeSpan.FromMinutes(1)));


NBomberRunner.RegisterScenarios(asyncScenario, syncScenario)
    .Run();
