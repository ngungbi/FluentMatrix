// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using Benchmarks;

Console.WriteLine("Hello, World!");

// BenchmarkRunner.Run<ParallelBenchmark>();
// BenchmarkRunner.Run<EnumerableBenchmark>();
BenchmarkRunner.Run<DeterminantBenchmark>();
