using System.Collections;

namespace FluentMatrix;

public readonly struct Elements : IReadOnlyCollection<MatrixElement> {
    private readonly Matrix _matrix;
    public Elements(Matrix matrix) { _matrix = matrix; }
    public Matrix.Enumerator GetEnumerator() => new(_matrix);
    IEnumerator<MatrixElement> IEnumerable<MatrixElement>.GetEnumerator() => GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    public int Count => _matrix.Rows * _matrix.Columns;
}
