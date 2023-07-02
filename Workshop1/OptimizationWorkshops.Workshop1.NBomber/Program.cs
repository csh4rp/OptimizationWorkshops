using NBomber.CSharp;
using NBomber.Http.CSharp;

var client = new HttpClient();

var scenario = Scenario.Create("get_latest_summary", async cnx =>
    {
        var request = Http.CreateRequest("GET", "http://localhost:5151/data/latest-summary");

        var response = await Http.Send(client, request);

        return response;
    })
    .WithLoadSimulations(Simulation.Inject(rate: 100,
        interval: TimeSpan.FromMilliseconds(100),
        during: TimeSpan.FromMinutes(1)));


NBomberRunner.RegisterScenarios(scenario)
    .Run();
