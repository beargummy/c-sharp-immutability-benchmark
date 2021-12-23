using System.Diagnostics.Tracing;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Jobs;
using Microsoft.Diagnostics.NETCore.Client;
using Microsoft.Diagnostics.Tracing.Parsers;

[EventPipeProfiler(EventPipeProfile.Jit)]
public class StructBenchmark
{
    public struct StructProperty
    {
        public int A { get; set; }
    }

    public struct StructField
    {
        public int A;
    }

    [Params(1000)]
    public int size { get; set; }

    [Benchmark]
    public int Struct_Field_Immutable()
    {
        var s = new StructField();
        for (int i = 0; i < size; i++)
        {
            s = new StructField { A = s.A + s.A };
        }
        return s.A;
    }

    [Benchmark]
    public int Struct_Field_Mutable()
    {
        var s = new StructField();
        for (int i = 0; i < size; i++)
        {
            s.A += s.A;
        }
        return s.A;
    }

    [Benchmark]
    public int Struct_Property_Immutable()
    {
        var s = new StructProperty();
        for (int i = 0; i < size; i++)
        {
            s = new StructProperty { A = s.A + s.A };
        }
        return s.A;
    }

    [Benchmark]
    public int Struct_Property_Mutable()
    {
        var s = new StructProperty();
        for (int i = 0; i < size; i++)
        {
            s.A += s.A;
        }
        return s.A;
    }

    private class CustomConfig : ManualConfig
    {
        public CustomConfig()
        {
            AddJob(Job.ShortRun.WithRuntime(CoreRuntime.Core60));

            var providers = new[]
            {
                new EventPipeProvider(ClrTraceEventParser.ProviderName, EventLevel.Verbose,
                    (long) (ClrTraceEventParser.Keywords.Exception
                    | ClrTraceEventParser.Keywords.GC
                    | ClrTraceEventParser.Keywords.Jit
                    | ClrTraceEventParser.Keywords.JitTracing // for the inlining events
                    | ClrTraceEventParser.Keywords.Loader
                    | ClrTraceEventParser.Keywords.NGen)),
            };

            AddDiagnoser(new EventPipeProfiler(providers: providers));
        }
    }
}