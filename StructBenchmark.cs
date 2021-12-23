using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;

[HardwareCounters(
    HardwareCounter.BranchMispredictions,
    HardwareCounter.BranchInstructions)]
[DisassemblyDiagnoser(printSource: true, printInstructionAddresses: true, maxDepth: 5, exportCombinedDisassemblyReport: true)]
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

    [Params(100,1000,10000)]
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
}