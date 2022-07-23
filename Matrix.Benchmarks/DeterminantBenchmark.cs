using BenchmarkDotNet.Attributes;

namespace Benchmarks;

[MemoryDiagnoser]
public class DeterminantBenchmark {
    [Benchmark]
    public double Determinant3X3() {
        var mat3 = MatrixCollection.Matrix3X3;
        var det = mat3.Determinant();
        return det;
    }
    
    [Benchmark]
    public double Determinant4X4() {
        var mat3 = MatrixCollection.Matrix4X4;
        var det = mat3.Determinant();
        return det;
    }
}
