using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneClassLoadTester
{
    /// <summary>
    /// Works by sending x number of requests then a defined delay, and onto the next group of requests.
    /// </summary>
    public class LoadTestEngine
    {
        public async Task Run()
        {
            var totalTestGroups = 100;
            var totalTestTasks = 50;
            var httpClient = new HttpClient();

            // This represents all the different bunches of requests 
            var testGroups = new List<TestGroup>();

            // 1. Setup test groups
            for (int testGroupCount = 0; testGroupCount < totalTestGroups; testGroupCount++)
            {
                var testTasks = new List<Task>();
                // 2. Setup the tasks in a test group
                for (int testTaskCount = 0; testTaskCount < totalTestTasks; testTaskCount++)
                {
                    // Be extremely careful, this is a COLD task
                    // This is where the work happens, in this lambda 👇
                    var task = new Task(async () =>
                    {
                        //3. Put your request logic here
                        _ = await httpClient.GetAsync("http://localhost:3000/test");
                        Console.WriteLine("Finished task.");
                    });
                    testTasks.Add(task);
                }

                var testGroup = new TestGroup(testTasks, PostTestDelay:Random.Shared.Next(1, 500));
                testGroups.Add(testGroup);
            }

            // 4. Loop through each test group and fire off request
            foreach (var testgroup in testGroups)
            {
                // Fires off all the test tasks in a test group.
                await Task.WhenAll(testgroup.TestTasks.Select(task =>
                {
                    Console.WriteLine($"Starting task.");
                    task.Start();
                    return task;
                }));

                // 5. Wait the delayed amount until firing the next bunch
                await Task.Delay(testgroup.PostTestDelay);
            }
        }
    }

    public record TestGroup(List<Task> TestTasks, int PostTestDelay);
}
