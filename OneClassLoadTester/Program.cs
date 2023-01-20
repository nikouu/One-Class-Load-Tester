using OneClassLoadTester;

Console.WriteLine("Starting load test.");

var loadTestEngine = new LoadTestEngine();

await loadTestEngine.Run();

Console.WriteLine("Load test complete.");
Console.ReadLine();
