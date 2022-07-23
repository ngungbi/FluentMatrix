using System.Collections;

namespace FluentMatrix;

public readonly struct ValueCollection : IEnumerable<double> {
    private readonly Matrix _matrix;

    public ValueCollection(Matrix matrix) { _matrix = matrix; }

    public Enumerator GetEnumerator() => new(_matrix);
    IEnumerator<double> IEnumerable<double>.GetEnumerator() => GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }

    public struct Enumerator : IEnumerator<double> {
        private readonly Matrix _matrix;
        private int _col = -1;

        private int _row = 0;

        public Enumerator(Matrix matrix) { _matrix = matrix; }

        public bool MoveNext() {
            _col++;
            if (_col < _matrix.Columns) return true;
            _col = 0;
            _row++;
            return _row < _matrix.Rows ? true : false;
        }

        public void Reset() {
            _col = -1;
            _row = 0;
        }

        public double Current => _matrix[_row, _col];

        object IEnumerator.Current => Current;

        public void Dispose() { }
    }
}
