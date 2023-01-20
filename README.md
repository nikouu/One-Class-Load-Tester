# One Class Load Tester 💪

A really simple few lines of code to get a dirty load tester going. So easy in fact, it's a single class that could **just be copied over**. Meaning if you are unable to clone, or download an .exe, the copying is super simple.


![](images/YouHaveHeardOfMe.jpg)


## Where did this come from?
I needed a really quick and dirty load tester and I wasn't in a position to clone or download projects/libraries/software... So this thing was created 💀

## How does it work?

The core concept is the "test group" and inside each group is each bunch of requests you're looking to fire at once. Plus a test group has what value of delay to wait before the next bunch of requests. For example, let's say you wanted to do 5 requests every 2 seconds. This is two test groups with five tasks in each, with the delay set to 2000ms. 

See the example below for a more clear picture.

### CycleData(int Cycle, int Requests, int Delay)

```csharp
public record TestGroup(List<Task> TestTasks, int PostTestDelay);
```

Where:
| Parameter       | Usage                                                                          |
| --------------- | ------------------------------------------------------------------------------ |
| `TestTasks`     | All the request to be made per test group. A list of normal C# `Task` objects. |
| `PostTestDelay` | The delay in milliseconds before the next test group.                          |


Note: The final delay actually doesn't matter as the code will end after the delay value. 

## Test Server

With this solution is a test server which is a simple minimal API project to help goes along with the default load testing project. Run both at the same time to see the console fill with requests.

## What about tokens, query strings, tokens, mTLS certificates?

With the raw `HttpClient` object sitting there, you should be able to quickly attach anything else you need in the same way you probably have done it in your normal project.

With limitations of `HttpClient` around things like headers, you may want to create an `HttpClient` object as per what you need and add it to `TestGroup` or create an object to represent TestTasks which has the `Task` and anything else you need to run your test for each run. 