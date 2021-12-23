using BenchmarkDotNet.Attributes;

public class ClassBenchmark
{
    public class ClassProperty
    {
        public int A { get; set; }
    }

    public class ClassField
    {
        public int A;
    }

    [Params(100,1000,10000)]
    public int size { get; set; }

    [Benchmark]
    public int Class_Field_Immutable()
    {
        var s = new ClassField();
        for (int i = 0; i < size; i++)
        {
            s = new ClassField { A = s.A + s.A };
        }
        return s.A;
    }

    [Benchmark]
    public int Class_Field_Mutable()
    {
        var s = new ClassField();
        for (int i = 0; i < size; i++)
        {
            s.A += s.A;
        }
        return s.A;
    }

    [Benchmark]
    public int Class_Property_Immutable()
    {
        var s = new ClassProperty();
        for (int i = 0; i < size; i++)
        {
            s = new ClassProperty { A = s.A + s.A };
        }
        return s.A;
    }

    [Benchmark]
    public int Class_Property_Mutable()
    {
        var s = new ClassProperty();
        for (int i = 0; i < size; i++)
        {
            s.A += s.A;
        }
        return s.A;
    }
}