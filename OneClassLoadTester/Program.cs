Console.WriteLine("Starting load test.");

var httpClient = new HttpClient();

var requestsPerCycleMapping = new List<CycleData>()
{
    new CycleData(1, 5, 1000),
    new CycleData(3, 10, 500),
    new CycleData(5, 15, 300),
    new CycleData(8, 20, 100),
    new CycleData(10, 25, 1000),
}.OrderBy(x => x.Cycle);

var cycles = requestsPerCycleMapping.Max(x => x.Cycle);

for (int i = 0; i < cycles; i++)
{
    var cycleData = GetCurrentCycleData(i);
    Console.WriteLine($"Cycle {i + 1}: sending {cycleData.Requests} requests, then delaying by {cycleData.Delay}ms.");

    for (int j = 0; j < cycleData.Requests; j++)
    {
        _ = await httpClient.GetAsync("http://localhost:3000/test");
    }

    await Task.Delay(cycleData.Delay);
}

Console.WriteLine("Load test complete.");
Console.ReadLine();

CycleData GetCurrentCycleData(int currentCycle) => requestsPerCycleMapping.First(x => x.Cycle >= currentCycle);

readonly record struct CycleData(int Cycle, int Requests, int Delay);