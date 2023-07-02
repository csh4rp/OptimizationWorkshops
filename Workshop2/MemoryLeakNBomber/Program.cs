using System.Globalization;
using System.Text;
using System.Text.Json;
using NBomber.CSharp;
using NBomber.Http.CSharp;

var client = new HttpClient();

var postScenario = Scenario.Create("post_data", async cnx =>
    {
        var request = Http.CreateRequest("POST", "http://localhost:5034/data")
            .WithBody(new StringContent(JsonSerializer.Serialize(new
            {
                X = Random.Shared.NextDouble(),
                Y = Random.Shared.NextDouble(),
                Z = Random.Shared.NextDouble(),
            }), Encoding.UTF8, "application/json"));

        var response = await Http.Send(client, request);

        return response;
    })
    .WithLoadSimulations(Simulation.Inject(rate: 5,
        interval: TimeSpan.FromMilliseconds(100),
        during: TimeSpan.FromMinutes(30)));

var getScenario = Scenario.Create("get_summary", async cnx =>
    {
        var request = Http.CreateRequest("GET", "http://localhost:5034/data/summary?tillTime=" + DateTime.UtcNow.ToString("o", CultureInfo.InvariantCulture));

        var response = await Http.Send(client, request);

        return response;
    })
    .WithLoadSimulations(Simulation.Inject(rate: 10,
        interval: TimeSpan.FromMilliseconds(100),
        during: TimeSpan.FromMinutes(30)));


NBomberRunner.RegisterScenarios(postScenario, getScenario)
    .Run();
