using System.Text;
using System.Text.Json;
using NBomber.CSharp;
using NBomber.Http.CSharp;

var client = new HttpClient();

var counter = 1;

var postScenario = Scenario.Create("post_data", async _ =>
    {
        var request = Http.CreateRequest("POST", "http://localhost:5010/hash")
            .WithBody(new StringContent(JsonSerializer.Serialize(new
            {
                Input = Interlocked.Increment(ref counter).ToString()
            }), Encoding.UTF8, "application/json"));

        var response = await Http.Send(client, request);

        return response;
    })
    .WithLoadSimulations(Simulation.Inject(rate: 250,
        interval: TimeSpan.FromMilliseconds(10),
        during: TimeSpan.FromHours(5)));

NBomberRunner.RegisterScenarios(postScenario)
    .Run();
