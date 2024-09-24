using System.Diagnostics;
using Prometheus;
using System.Threading;
using System.Diagnostics.Metrics;

Console.WriteLine("Hello, World!");

YourClass.IncrementCounter();
public static class YourClass
{
    private static readonly Counter counter = Metrics.CreateCounter("my_counter", "some help about this",new[]{"foo","bar"});
     
    private static readonly Gauge gauge = Metrics.CreateGauge("my_gauge", "help text", new GaugeConfiguration()
    {
        LabelNames = new[] { "foo", "bar" },
        
        SuppressInitialValue = false // default is false
    });

    private static readonly Summary summary = Metrics.CreateSummary("my_summary", "help text");
    private static readonly Histogram histogram = Metrics.CreateHistogram("my_histogram", "help text");

    static void RaiseException()
    {
        //throw new NotImplementedException();
    }
    public static void IncrementCounter()
    { 
        Metrics.DefaultRegistry.SetStaticLabels(
            new Dictionary<string, string>()
                { 
                    { "Country", "US" },
                });
        var server = new MetricServer(8080);
        server.Start();

        var watch = new Stopwatch();
        watch.Start();
        Thread.Sleep(1000);
        watch.Stop(); 
        summary.Observe(watch.ElapsedMilliseconds);

        //counter.Inc();
        try
        {
            counter.WithLabels(new[] { "1", "2" }).CountExceptions(() => RaiseException());

        }
        catch (Exception ex)
        {
            //counter.Inc();
        }

        while (true)
        {



            gauge.WithLabels(new[]{"1","2"}).Set(100);
           // gauge.Dec(90);

        
            histogram.Observe(100);

            Thread.Sleep(1000);
            Console.WriteLine(DateTime.UtcNow.Ticks);
        }
    }
}