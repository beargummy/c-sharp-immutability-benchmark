To run the code simply run following command:
```
dotnet run -c Release -- -f *
```
Benchmark will output results to console and to the `./BenchmarkDotNet.Artifacts` directory.

To run struct benchmarks:
```
dotnet run -c Release -- -f *
```

See other configuration parameters here: [https://benchmarkdotnet.org/articles/guides/console-args.html](https://benchmarkdotnet.org/articles/guides/console-args.html)