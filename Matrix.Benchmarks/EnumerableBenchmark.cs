using BenchmarkDotNet.Attributes;
using FluentMatrix;

namespace Benchmarks;

[MemoryDiagnoser]
public class EnumerableBenchmark {
    
    [Benchmark]
    public double ValueEnumerationFromElements() {
        var matrix = MatrixCollection.MatrixA;
        var sum = 0.0;
        foreach (var value in matrix.Elements) {
            sum += value.Value;
        }

        return sum;
    }
    
    [Benchmark]
    public double ValueEnumeration() {
        var matrix = MatrixCollection.MatrixA;
        var sum = 0.0;
        foreach (var value in matrix) {
            sum += value.Value;
        }

        return sum;
    }
    
    [Benchmark]
    public double ValueEnumerationFromValues() {
        var matrix = MatrixCollection.MatrixA;
        var sum = 0.0;
        foreach (var value in matrix.Values) {
            sum += value;
        }

        return sum;
    }
    // [Benchmark]
    // public void DirectEnumeration() {
    //     var matrix = MatrixCollection.MatrixA;
    //     for (int i = 0; i < matrix.Columns; i++) {
    //         for (int j = 0; j < matrix.Rows; j++) {
    //             matrix[i, j] *= 2;
    //         }
    //     }
    // }

    // [Benchmark]
    // public void EnumerateRow() {
    //     var row = MatrixCollection.MatrixA.RowAt(0);
    //     foreach (var elm in row) {
    //         elm.Value *= 2;
    //     }
    // }
    //
    // [Benchmark]
    // public void EnumerateColumn() {
    //     var col = MatrixCollection.MatrixA.ColumnAt(0);
    //     foreach (var elm in col) {
    //         elm.Value *= 2;
    //     }
    // }

    // [Benchmark]
    // public void EnumerateAsRowCollection() {
    //     var matrix = MatrixCollection.MatrixA;
    //     foreach (var row in matrix.AsRowCollection()) {
    //         for (int i = 0, n = row.Count; i < n; i++) {
    //             var item = row[i];
    //             item.Value *= 2;
    //         }
    //     }
    // }
    //
    // [Benchmark]
    // public void EnumerateAsColumnCollectionn() {
    //     var matrix = MatrixCollection.MatrixA;
    //     foreach (var column in matrix.AsColumnCollection()) {
    //         for (int i = 0, n = column.Count; i < n; i++) {
    //             var item = column[i];
    //             item.Value *= 2;
    //         }
    //         // foreach (var elm in column) {
    //         //     elm.Value *= 2;
    //         // }
    //     }
    // }

    // [Benchmark]
    // public void EnumerateElementInMatrix() {
    //     var matrix = MatrixCollection.MatrixA;
    //     foreach (var item in matrix) {
    //         item.Value *= 2;
    //     }
    // }
    //
    // [Benchmark]
    // public void EnumerateElementInColumn() {
    //     var matrix = MatrixCollection.MatrixA.ColumnAt(0);
    //     foreach (var item in matrix) {
    //         item.Value *= 2;
    //     }
    // }
}
