using NBomber.CSharp;
using NBomber.Http.CSharp;

var client = new HttpClient();

var simulation = Simulation.Inject(rate: 50,
    interval: TimeSpan.FromMilliseconds(100),
    during: TimeSpan.FromMinutes(1));

var bufferedScenario = Scenario.Create("read_file_buffered", async _ =>
    {
        var request = Http.CreateRequest("GET", "http://localhost:5167/buffered");

        var response = await Http.Send(client, request);

        return response;
    })
    .WithLoadSimulations(simulation);

var nonBufferedScenario = Scenario.Create("read_file_non_buffered", async _ =>
    {
        var request = Http.CreateRequest("GET", "http://localhost:5167/non-buffered");

        var response = await Http.Send(client, request);

        return response;
    })
    .WithLoadSimulations(Simulation.Pause(TimeSpan.FromSeconds(70)), simulation);

var recyclableScenario = Scenario.Create("read_file_recyclable", async _ =>
    {
        var request = Http.CreateRequest("GET", "http://localhost:5167/recyclable");

        var response = await Http.Send(client, request);

        return response;
    })
    .WithLoadSimulations(Simulation.Pause(TimeSpan.FromSeconds(140)), simulation);

NBomberRunner.RegisterScenarios(bufferedScenario, nonBufferedScenario, recyclableScenario)
    .Run();
