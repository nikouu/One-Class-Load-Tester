# One Class Load Tester 💪

A really simple few lines of code to get a dirty load tester going. So easy in fact, it's a single class that could **just be copied over**. Meaning if you are unable to clone, or download an .exe, the copying is super simple.


![](images/YouHaveHeardOfMe.jpg)


## Where did this come from?
I needed a really quick and dirty load tester and I wasn't in a position to clone or download projects/libraries/software... So this thing was created 💀

## How does it work?

The core concept is the "test cycle". Each test cycle will depend on a `CycleData` object to outline how many requests are to be sent that cycle and the delay until the next cycle. This allows us to have a more fine grained approach. Note that if we are on a cycle that isn't explicitly outlined by a `CycleData` entry, it will effectively use the previous `CycleData` entry until we're at a cycle that corresponds to a newer `CycleData` value. 

See the example below for a more clear picture.

### CycleData(int Cycle, int Requests, int Delay)

```csharp
readonly record struct CycleData(int Cycle, int Requests, int Delay);
```

Where:
| Parameter  | Usage                                                 |
| ---------- | ----------------------------------------------------- |
| `Cycle`    | The cycle this `CycleData` corresponds to.            |
| `Requests` | How many requests should get sent out for this cycle. |
| `Delay`    | The delay in milliseconds before the next cycle.      |

Note: The final delay actually doesn't matter as the code will end after the delay value. 

### Number of cycles

The number of cycles is determined by the highest value of `Cycle` in the list of `CycleData` objects.


### Example

A much easier to digest example:

```csharp
var requestsPerCycleMapping = new List<CycleData>()
{
    new CycleData(1, 5, 1000),
    new CycleData(3, 10, 500),
    new CycleData(5, 15, 300),
}.OrderBy(x => x.Cycle);
```

Above is the setup and where the `CycleData` objects are defined. Meaning for this we will have five total cycles, which will look like:

| Cycle | Requests | Delay  |
| ----- | -------- | ------ |
| 1     | 5        | 1000ms |
| 2     | 5        | 1000ms |
| 3     | 10       | 500    |
| 4     | 10       | 500    |
| 5     | 15       | 300    |

## Test Server

With this solution is a test server which is a simple minimal API project to help goes along with the default load testing project. Run both at the same time to see the console fill with requests.

## What about tokens, query strings, tokens, mTLS certificates?

With the raw `HttpClient` object sitting there, you should be able to quickly attach anything else you need in the same way you probably have done it in your normal project.