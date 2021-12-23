using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;

public class Program
{
    static void Main(string[] args)
        => BenchmarkSwitcher
            .FromAssembly(typeof(Program).Assembly)
            .Run(args, GetDefaultConfig());

    static IConfig GetDefaultConfig()
        => DefaultConfig.Instance
            .AddJob(Job.Default
                .WithGcServer(true)
                .WithWarmupCount(5)
                .WithMinIterationCount(10)
                .WithMaxIterationCount(20)
                .WithStrategy(RunStrategy.Throughput)
                .AsDefault());
}
