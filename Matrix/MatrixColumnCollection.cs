using System.Collections;

namespace FluentMatrix;

public readonly struct MatrixColumnCollection : IReadOnlyCollection<MatrixColumn>, IMatrixElements<MatrixColumn> {
    private readonly Matrix _matrix;

    public int Count => _matrix.Columns;

    public MatrixColumn this[int index] => new(_matrix, index);

    public MatrixColumnCollection(Matrix matrix) { _matrix = matrix; }
    public Enumerator GetEnumerator() => new(this);
    IEnumerator<MatrixColumn> IEnumerable<MatrixColumn>.GetEnumerator() => GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public override bool Equals(object? obj) {
        return obj is MatrixColumnCollection other && Equals(other);
    }

    public override int GetHashCode() {
        return _matrix.GetHashCode();
    }

    public bool Equals(MatrixColumnCollection other) => _matrix.Equals(other._matrix);

    public struct Enumerator : IEnumerator<MatrixColumn> {
        private readonly MatrixColumnCollection _collection;
        private int _index = -1;

        public Enumerator(MatrixColumnCollection collection) { _collection = collection; }


        public bool MoveNext() {
            _index++;
            return _index < _collection.Count;
        }

        public void Reset() { _index = -1; }

        public MatrixColumn Current => _collection[_index];

        object IEnumerator.Current => Current;
        public void Dispose() { }
    }
}
