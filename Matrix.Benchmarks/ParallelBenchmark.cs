using BenchmarkDotNet.Attributes;
using FluentMatrix;

namespace Benchmarks;

[MemoryDiagnoser]
public class ParallelBenchmark {
    private static readonly Matrix MatrixA = new MatrixBuilder(8, 8) {
        // create example matrix 8x8
        {1, 2, 3, 5, 6, 8, 0, 1},
        {4, 5, 6, 7, 8, 9, 0, 1},
        {7, 8, 9, 1, 2, 3, 0, 1},
        {1, 2, 3, 4, 5, 6, 0, 1},
        {4, 5, 6, 7, 8, 9, 0, 1},
        {7, 8, 9, 1, 2, 3, 0, 1},
        {1, 2, 3, 4, 5, 6, 0, 1},
        {4, 5, 6, 7, 8, 9, 0, 1}
    };

    private static readonly Matrix MatrixB = new MatrixBuilder(8, 8) {
        {1, 2, 3, 5, 6, 8, 0, 1},
        {4, 5, 6, 7, 8, 9, 0, 1},
        {7, 8, 9, 1, 2, 3, 0, 1},
        {1, 2, 3, 4, 5, 6, 0, 1},
        {4, 5, 6, 7, 8, 9, 0, 1},
        {7, 8, 9, 1, 2, 3, 0, 1},
        {1, 2, 3, 4, 5, 6, 0, 1},
        {4, 5, 6, 7, 8, 9, 0, 1}
    };

    private static readonly List<double> List = new List<double>() {1, 2, 3, 4, 5, 6};

    // [Benchmark]
    // public void ForeachMultiply() {
    //     foreach (var item in MatrixA) {
    //         item.Value *= 2;
    //     }
    // }

    // [Benchmark]
    // public void ParallelMultiply() {
    //     Parallel.ForEach(
    //         MatrixA.RowCollection, row => {
    //             foreach (var item in row) {
    //                 item.Value *= 2;
    //             }
    //         }
    //     );
    // }

    // [Benchmark]
    // public void StandardMultiply() {
    //     var enumeroatoer = List.GetEnumerator();
    //     // enumeroatoer.MoveNext();
    //     // enumeroatoer.Dispose();
    //     // var count = 0;
    //     //
    //     // foreach (double item in List) {
    //     //     count++;
    //     // }
    //     // using var enumerator = new Matrix.ElementEnumerator(MatrixA);
    //     // while (enumerator.MoveNext()) {
    //     //     var current = enumerator.Current;
    //     //     current.Value *= 2;
    //     // }
    //     
    //     foreach (var item in MatrixA) {
    //         item.Value *= 2;
    //     }
    //     // MatrixA.ScalarMultiply(2.0);
    //     // return result;
    // }
}
